using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tempFileStorage.Models;
using System.Threading;

namespace tempFileStorage
{
    /// <summary>
    ///  Класс обеспечивающий временное хранение файлов с контролем по ключам и времени.
    ///  Применяется встроенный, настраиваемый уборщик
    /// </summary>
    public class TempStorageProvider : ITempFileStorageProvider
        {

        // Максимальное время существования файла в хранилище
        const int MAX_TIME_TO_LIVE_MINUTES = 60;

        // Время в минутах между очистками файлового списка
        const int TEMP_FILE_SWEEP_WAIT_IN_MINUTES = 10;

        #region Инициализация

        private static IList<FileRecord> _tempFileStorage;

        private static readonly StaticDestructor Destructor;

        private sealed class StaticDestructor
            {
                private readonly CancellationTokenSource _cts;
                private readonly Task _clearer;

                public StaticDestructor(CancellationTokenSource cts, Task tsk)
                {
                    Contract.Requires(cts != null && tsk != null);
                    _cts = cts;
                    _clearer = tsk;
                }

                ~StaticDestructor()
                {
                    _cts.Cancel();
                    _clearer.Wait();
                    _cts.Dispose();
                }
            }

        static TempStorageProvider()
        {
            _tempFileStorage = new List<FileRecord>();

            CancellationTokenSource cts = new CancellationTokenSource();

            var token = cts.Token;

            Destructor = new StaticDestructor(cts, Task.Run(() =>
            {
                while (true)
                    {
                        if (token.IsCancellationRequested) break;
                        Thread.Sleep(TimeSpan.FromMinutes(TEMP_FILE_SWEEP_WAIT_IN_MINUTES));
                        ClearTempFiles();
                    }
            }, token));
        }

        #endregion

        #region Сервис

        private static void ClearTempFiles()
        {
         lock(_tempFileStorage)
         {
                var currentTime = DateTime.Now;
                var items = _tempFileStorage.Where(m => m.FileCreationTime.AddMinutes(MAX_TIME_TO_LIVE_MINUTES) < currentTime);
                foreach(var item in items)
                  {
                    _tempFileStorage.Remove(item);
                  }
         }
        }

        #endregion

        #region Управление файлами

        public bool DeleteTempFile(Guid id)
            {
              lock(_tempFileStorage)
                  {
                   var item = _tempFileStorage.FirstOrDefault(m => m.FileItem.FileID.Equals(id));
                   if (item == null) return false; else {
                    _tempFileStorage.Remove(item);
                    return true;
                   }
                  }
            }

        public FileData GetTempFile(Guid id)
            {
             lock(_tempFileStorage)
             {
                var item = _tempFileStorage.FirstOrDefault(m => m.FileItem.FileID.Equals(id));
                return item?.FileItem;
              }
            }

        public IList<FileData> GetTempFiles(string key, bool remove = false, string tag = "")
            {
              lock(_tempFileStorage)
              {
                var items = _tempFileStorage.Where(m => m.Key.Equals(key) & m.Tag.Equals(tag)).Select(m => m.FileItem).ToList();
                if (remove) ClearTempFiles(key, tag);
                return items;
              }
            }

        public Guid StoreTempFile(FileData fileItem, string key, string tag = "")
            {
             var item = new FileRecord(fileItem, key) { Tag = tag };
             lock(_tempFileStorage)
             {
                _tempFileStorage.Add(item);
                return item.FileItem.FileID;
             }
            }

        public Guid GetID(FileData fileItem)
            { 
             lock(_tempFileStorage)
              {
                var item = _tempFileStorage.FirstOrDefault(m => m.FileItem.Equals(fileItem));
                return item?.FileItem.FileID ?? default(Guid);
              }

            }

        public void ClearTempFiles(string key, string tag = "")
            {
            lock (_tempFileStorage)
                {
                var items = (string.IsNullOrEmpty(tag)) ? _tempFileStorage.Where(m => m.Key.Equals(key)) :
                   _tempFileStorage.Where(m => m.Key.Equals(key) & m.Tag.Equals(tag));
                _tempFileStorage = _tempFileStorage.Except(items).ToList();

                }

            }

        #endregion

        }
    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tempFileStorage.Models
    {
    /// <summary>
    /// Класс описывающий хранимый файл
    /// </summary>
    public class FileRecord
        {
        #region Инициализация
        public FileRecord(FileData item, string itemKey)
            {
             FileItem = item;
             Key = itemKey;
             FileCreationTime = DateTime.Now;
            }
        #endregion
        public string Key { get; private set; }

        public DateTime FileCreationTime { get; private set; }
        public FileData FileItem { get; set; }
        public string Tag { get; set; }
        }
    }

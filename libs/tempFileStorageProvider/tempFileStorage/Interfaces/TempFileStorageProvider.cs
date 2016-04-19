using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tempFileStorage.Models;

namespace tempFileStorage
    {
    public interface ITempFileStorageProvider
        {
        IList<FileData> GetTempFiles(string key, bool remove = false, string tag = "");
        FileData GetTempFile(Guid id);
        Guid StoreTempFile(FileData fileItem, string key, string tag = "");
        Guid GetID(FileData fileItem);
        bool DeleteTempFile(Guid id);
        void ClearTempFiles(string key, string tag = "");
        }
    }

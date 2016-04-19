using System;

namespace tempFileStorage.Models
    {
    public class FileData
        {
        public FileData()
        {
            FileID = Guid.NewGuid();
        }
        public FileData(string guid)
        {
            Guid id;
            if (!Guid.TryParse(guid, out id))
                id = Guid.NewGuid();
            FileID = id;
            }
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] Data { get; set; }
        public Guid FileID { get; private set; }
        }
    }
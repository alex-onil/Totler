using System;

namespace DataTypes.Models
    {
    public class FileRepo : StoreId
        {
        public FileRepo() { }
        public FileRepo(string author) : base(author) { }
        public virtual string Name { get; set; }
        public virtual string FileMimeType { get; set; }
        public virtual byte[] FileData { get; set; }
        public virtual string GuidFile { get; set; } = Guid.NewGuid().ToString();
        public virtual NewsItem NeewsRefId { get; set; }
        }
    }
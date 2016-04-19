using DataTypes.Models;
using FluentNHibernate.Mapping;

namespace DataTypes.Mappings
    {
    public class FileRepoMap : SubclassMap<FileRepo>
        {
        public FileRepoMap()
        {
            Table("FileRepository");
            Map(v => v.Name, "FILENAME").Not.Nullable().Length(250);
            Map(v => v.GuidFile, "FILEID").Not.Nullable();
            Map(v => v.FileMimeType, "FILEMIME").Not.Nullable();
            Map(v => v.FileData, "FILEDATA").Length(2147483647).LazyLoad();
            References(r => r.NeewsRefId, "newsRef")
                .ForeignKey("ID")
                .Nullable();
        }
        }
    }
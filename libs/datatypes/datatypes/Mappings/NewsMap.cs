using DataTypes.Models;
using FluentNHibernate.Mapping;

namespace DataTypes.Mappings
    {
    public class NewsMap : SubclassMap<NewsItem>
        {
        public NewsMap()
        {
            Map(v => v.PostInfo, "POSTINFO").Length(1000);
            Map(v => v.Author, "NEWSAUTHOR");
            Map(v => v.Date, "NEWSDATE");
            Map(v => v.Header, "NEWSHEADER").Not.Nullable().Length(1000);
            Map(v => v.Text, "NEWSMAINTEXT").Not.Nullable().Length(100000);
            Map(v => v.TextSmall, "NEWSSMALLTEXT").Not.Nullable().Length(1000);
            References(r => r.HeaderImage, "HEADERIMGID").Cascade.All();

            HasMany(m => m.Images)
                .KeyColumn("newsRef")
                .Cascade.All();
        }
        }
    }
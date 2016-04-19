using DataTypes.Models;
using FluentNHibernate.Mapping;

namespace DataTypes.Mappings
    {
    public class RollerImagesMap : SubclassMap<RollerImage>
        {
        public  RollerImagesMap()
        {
            Map(v => v.Name, "ROLLERNAME");
            Map(v => v.FinishDate, "ROLLERENDDATE");
            References(r => r.StoredImage).Cascade.All();
        }
        }
    }
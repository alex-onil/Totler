using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes.Models;
using FluentNHibernate.Mapping;

namespace DataTypes.Mappings
    {
    public class ModelGroupMap : SubclassMap<ModelGroup>
        {
        public ModelGroupMap()
        {
            Table("Models");
            Map(m => m.ModelGroupName, "ModelName").Not.Nullable();
            Map(m => m.ParentRef, "Parent").Nullable().ReadOnly();
            References(r => r.Parent, "Parent").ForeignKey("Parent").Nullable();
            HasMany(hm => hm.Childs).KeyColumn("Parent").KeyNullable().Cascade.AllDeleteOrphan();
            HasMany(hm => hm.Products).KeyColumn("Model").Cascade.AllDeleteOrphan();
        }
        }
    }

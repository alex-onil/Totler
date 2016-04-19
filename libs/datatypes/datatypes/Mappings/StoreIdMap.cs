using FluentNHibernate.Mapping;

namespace DataTypes.Mappings
    {
    public class StoreIdMap : ClassMap<StoreId>
        {
        public StoreIdMap()
            {
            Id(i => i.ItemId, "ID").GeneratedBy.HiLo("1000");
            Map(v => v.AuthorId, "AUTHOR").Not.Nullable();
            Map(v => v.CreationDate, "CREATION_DATE").Not.Nullable();
            Map(v => v.LastChangedUserId, "LASTEDITUSER").Not.Nullable();
            Map(v => v.LastModificationDate, "EDITDATE").Not.Nullable();
            // DiscriminateSubClassesOnColumn("idDiscriminator");
            UseUnionSubclassForInheritanceMapping();
            }
        }
    }
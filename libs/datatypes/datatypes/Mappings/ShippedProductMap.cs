using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes.Models;
using FluentNHibernate.Mapping;

namespace DataTypes.Mappings
    {
    public class ShippedProductMap : SubclassMap<ShippedProduct>
        {
        public ShippedProductMap()
            {
            Table("Products");
            Map(m => m.ModelRef, "Model").ReadOnly().Not.Nullable();
            References(r => r.Model, "Model").Not.Nullable();
            Map(m => m.ProductName, "ProductName").Not.Nullable();
            Map(m => m.SerialNumber, "SerialNumber").Not.Nullable();
            Map(m => m.ProductWarrantyStartDate, "StartWarrantyDate").Not.Nullable();
            Map(m => m.ProductWarrantyInMonth, "WarrantyInMonth").Not.Nullable();
            Map(m => m.Notes, "Notes").Nullable();
            }
        }
    }

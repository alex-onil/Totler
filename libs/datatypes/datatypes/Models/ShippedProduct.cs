using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes.Models
    {
    /// <summary>
    /// Класс описывающий товар
    /// </summary>
    [DataContract]
    public class ShippedProduct : StoreId
        {
        #region Инициализация
            public ShippedProduct() { }
            public ShippedProduct(string author) : base(author) { }
        #endregion
        
        [IgnoreDataMember]
        public virtual ModelGroup Model { get; set; }

        [DataMember]
        public virtual int ModelRef { get; set; }

        [DataMember]
        public virtual string ProductName { get; set; }

        [DataMember]
        public virtual string SerialNumber { get; set; }

        [DataMember]
        public virtual DateTime ProductWarrantyStartDate { get; set; }

        [DataMember]
        public virtual int ProductWarrantyInMonth { get; set; }

        [DataMember]
        public virtual string Notes { get; set; }

        // Потом в модель добавим описание, цену, изображения и т.п.

        }
    }

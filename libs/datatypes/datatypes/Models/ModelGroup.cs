using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes.Models
    {
    /// <summary>
    /// Описывает группу моделей товара
    /// </summary>
    [DataContract]
    public class ModelGroup : StoreId
        {
        #region Инициализация
            public ModelGroup() { }
            public ModelGroup(string author) : base(author) { }
        #endregion

        [DataMember]
        public virtual string ModelGroupName { get; set; }

        [DataMember]
        public virtual IList<ModelGroup> Childs { get; set; }
                               = new List<ModelGroup>();

        [DataMember]
        public virtual int? ParentRef { get; set; }

        [IgnoreDataMember]
        public virtual ModelGroup Parent { get; set; }

        [DataMember]
        public virtual IList<ShippedProduct> Products { get; set; }
                                           = new List<ShippedProduct>();


        }
    }

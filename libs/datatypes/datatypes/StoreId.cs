using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
    {
    /// <summary>
    /// Класс описывающий создателя и последнего изменившего
    /// </summary>
    [DataContract]
    public abstract class StoreId {
        protected StoreId() { }

        protected StoreId(string author)
            {
            Contract.Requires(!string.IsNullOrEmpty(author));
            AuthorId = author;
            LastChangedUserId = author;
            CreationDate = DateTime.Now;
            LastModificationDate = DateTime.Now;
            }

        [DataMember]
        public virtual int ItemId { get; set; }

        [DataMember]
        public virtual string AuthorId { get; set; }

        [DataMember]
        public virtual string LastChangedUserId { get; set; }

        [DataMember]
        public virtual DateTime CreationDate { get; set; }

        [DataMember]
        public virtual DateTime LastModificationDate { get; set; }

        }
    }

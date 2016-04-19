using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;

namespace DataTypes
    {
    /// <summary>
    /// Класс описывающий создателя и последнего изменившего
    /// </summary>
    [DataContract]
    public abstract class StoreId : INotifyPropertyChanged
        {
        protected StoreId() { }

        protected StoreId(string author)
            {
            Contract.Requires(!string.IsNullOrEmpty(author));
            AuthorId = author;
            LastChangedUserId = author;
            CreationDate = DateTime.Now;
            LastModificationDate = DateTime.Now;
            }

        /// <summary>
        /// ID записи БД
        /// </summary>
        [Key]
        [DataMember]
        public virtual int ItemId { get; set; }

        /// <summary>
        /// ID пользователя, создавшего данную группу моделей товара
        /// </summary>
        /// 
        [DataMember]
        public virtual string AuthorId { get; set; }

        /// <summary>
        /// ID полльзователя, производившего последние изменения группы моделей товара
        /// </summary>
        [DataMember]
        public virtual string LastChangedUserId { get; set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        [DataMember]
        public virtual DateTime CreationDate { get; set; }

        /// <summary>
        /// Дата последней модификации записи
        /// </summary>
        [DataMember]
        public virtual DateTime LastModificationDate { get; set; }

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void ChangeProperty(string property)
            {
            PropertyChangedEventHandler handler = PropertyChanged;
            LastModificationDate = DateTime.Now;
            handler?.Invoke(this, new PropertyChangedEventArgs(property));
            }

        protected bool SetField<T>(ref T field, T value, string propertyName)
            {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            ChangeProperty(propertyName);
            return true;
            }
        }

    /// <summary>
    /// Класс описывающий товар
    /// </summary>
    [DataContract]
    public class ShippedProduct : StoreId
        {
        public ShippedProduct() { }
        public ShippedProduct(string author) : base(author) { }

        [ForeignKey("ModelRefId")]
        public virtual ModelGroup Model { get; set; }

        [DataMember]
        public int? ModelRefId { get; set; }

        private string _productName;

        [Required]
        [DataMember]
        public string ProductName
            {
            get { return _productName; }
            set { SetField(ref _productName, value, "ProductName"); }
            }

        private string _serialNumber;

        [Required]
        [DataMember]
        public string SerialNumber
            {
            get { return _serialNumber; }
            set { SetField(ref _serialNumber, value, "SerialNumber"); }
            }

        private DateTime _productWarantyStartDate;

        [Required]
        [DataMember]
        public DateTime ProductWarantyStartDate
            {
            get { return _productWarantyStartDate; }
            set { SetField(ref _productWarantyStartDate, value, "ProductWarantyStartDate"); }
            }

        private int _productWarantyInMonth;

        [Required]
        [DataMember]
        public int ProductWarantyInMonth
            {
            get { return _productWarantyInMonth; }
            set { SetField(ref _productWarantyInMonth, value, "ProductWarantyInMonth"); }
            }

        [DataMember]
        public string Notes { get; set; }
        // Потом в модель добавим описание, цену, изображения и т.п.

        }

    /// <summary>
    /// Описывает группу моделей товара
    /// </summary>
    [DataContract]
    public class ModelGroup : StoreId
        {
        public ModelGroup() { }
        public ModelGroup(string author) : base(author) { }

        [DataMember]
        private string _modelGroupName;

        /// <summary>
        /// Найменования модели группы товара
        /// </summary>
        [Required]
        [DataMember]
        public string ModelGroupName
            {
            get { return _modelGroupName; }
            set { SetField(ref _modelGroupName, value, "ModelGroupName"); }
            }

        /// <summary>
        /// Коллекция подгрупп групп моделей товара
        /// </summary>
        [DataMember]
        [ForeignKey("ParentRefId")]
        public virtual ObservableCollection<ModelGroup> Childs { get; set; }
                               = new ObservableCollection<ModelGroup>();

        [ForeignKey("ParentRefId")]
        public virtual ModelGroup Parent { get; set; }

        private int? _parentRefId;

        /// <summary>
        /// Код родительского элемента или признак его отсутсвия
        /// </summary>
        [DataMember]
        public int? ParentRefId
            {
            get { return _parentRefId; }
            set { SetField(ref _parentRefId, value, "ModelGroupName"); }
            }

        /// <summary>
        /// Коллекция моделей товара
        /// </summary>
        [IgnoreDataMember]
        public virtual ObservableCollection<ShippedProduct> Products { get; set; }
                                           = new ObservableCollection<ShippedProduct>();


        }

    }

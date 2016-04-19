using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using DataTypes.Mapper;
using DataTypes.Models;
using DataTypes.ViewModels.Abstract;

namespace DataTypes.ViewModels
    {
    /// <summary>
    /// Класс описывающий товар
    /// </summary>
    [DataContract]
    public class ShippedProductViewModel : StoreIdViewModel
    {
        public ShippedProductViewModel() { }

        public ShippedProductViewModel(ShippedProduct product)
        {
            var mapper = MapperConfig.GetMapper();
            mapper.Map(product, this);
        }

        [DataMember]
        public int ModelRef { get; set; }

        private string _productName;

        [Required]
        [DataMember]
        public string ProductName
            {
            get { return _productName; }
            set
                {
                _productName = value;
                ChangeProperty();
                }
            }

        private string _serialNumber;

        [Required]
        [DataMember]
        public string SerialNumber
            {
            get { return _serialNumber; }
            set
                {
                _serialNumber = value;
                ChangeProperty();
                }
            }

        private DateTime _productWarrantyStartDate;

        [Required]
        [DataMember]
        public DateTime ProductWarrantyStartDate
            {
            get { return _productWarrantyStartDate; }
            set
                {
                _productWarrantyStartDate = value;
                ChangeProperty();
                }
            }

        private int _productWarrantyInMonth;

        [Required]
        [DataMember]
        public int ProductWarrantyInMonth
            {
            get { return _productWarrantyInMonth; }
            set
                {
                _productWarrantyInMonth = value;
                ChangeProperty();
                }
            }

        [DataMember]
        public string Notes { get; set; }
        // Потом в модель добавим описание, цену, изображения и т.п.

        }
    }

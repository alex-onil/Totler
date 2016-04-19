using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using DataTypes.Mapper;
using DataTypes.Models;
using DataTypes.ViewModels.Abstract;

namespace DataTypes.ViewModels
    {
    /// <summary>
    /// Описывает группу моделей товара
    /// </summary>
    [DataContract]
    public class ModelGroupViewModel : StoreIdViewModel
        {
        public ModelGroupViewModel() { }

        public ModelGroupViewModel(ModelGroup model)
        {
            var mapper = MapperConfig.GetMapper();
            mapper.Map(model, this);
         }

        private string _modelGroupName;

        /// <summary>
        /// Найменования модели группы товара
        /// </summary>
        [Required]
        [DataMember]
        public string ModelGroupName
            {
            get { return _modelGroupName; }
            set
                {
                _modelGroupName = value;
                ChangeProperty();
                }
            }

        /// <summary>
        /// Коллекция подгрупп групп моделей товара
        /// </summary>
        [DataMember]
        public ObservableCollection<ModelGroupViewModel> Childs { get; set; }
                               = new ObservableCollection<ModelGroupViewModel>();

        [DataMember]
        public virtual int? ParentRef { get; set; }

        /// <summary>
        /// Коллекция моделей товара
        /// </summary>
        [IgnoreDataMember]
        public  ObservableCollection<ShippedProductViewModel> Products { get; set; }
                                           = new ObservableCollection<ShippedProductViewModel>();


        }
    }

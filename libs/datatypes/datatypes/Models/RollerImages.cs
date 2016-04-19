using System;
using System.ComponentModel.DataAnnotations;

namespace DataTypes.Models
    {
    public class RollerImage : StoreId
        {
        #region Инициализация 

        public RollerImage() { }
        public RollerImage(string author) : base(author) { }

        #endregion

        [Required]
         [Display(Name="Название:")]
         public virtual string Name { get; set; }

         public virtual FileRepo StoredImage {get; set;}

         [Required]
         [Display(Name = "Дата окончания показа")]
         [DataType(DataType.Date)]
         [UIHint("DateEditor")]
         public virtual DateTime FinishDate { get; set; } = DateTime.Now.Date;
        }
    }
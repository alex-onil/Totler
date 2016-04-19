using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataTypes.Models
    {
    public class NewsItem : StoreId
        {
        public NewsItem(string author) : base(author) { }
        public NewsItem() { }

        [Display(Name = "Пост скриптум")]
        public virtual string PostInfo { get; set; }

        [Required]
        [Display(Name = "Автор новости")]
        public virtual string Author { get; set; }

        [Required]
        [UIHint("DateEditor")]
        [Display(Name = "Дата новости")]
        [DataType(DataType.Date)]
        public virtual DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Заголовок новости")]
        public virtual string Header { get; set; }

        [Required]
        [Display(Name = "Основной текст новости")]
        [MinLength(50, ErrorMessage = "Минимальная длинна новости - 50 символов")]
        public virtual string Text { get; set; }

        [Required]
        //[MaxLength(250, ErrorMessage = "Максимальная длинна новости кратко - 250 символов")]
        [MinLength(10, ErrorMessage = "Минимальная длинна новости кратко - 10 символов")]
        [Display(Name = "Краткий текст новости")]
        public virtual string TextSmall { get; set; }

        [UIHint("FilesUpload")]
        [Display(Name = "Освновное изображение новости")]
        public virtual FileRepo HeaderImage { get; set; }

        [UIHint("FilesUpload")]
        [Display(Name = "Связанные изображения новости")]
        public virtual ICollection<FileRepo> Images { get; set; } = new List<FileRepo>();

        }
    }
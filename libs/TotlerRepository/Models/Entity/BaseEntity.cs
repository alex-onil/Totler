using System;
using TotlerRepository.ModelValidation;

namespace TotlerRepository.Models.Entity
    {
    public abstract class BaseEntity : AbstractValidator
        {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string AuthorId { get; set; }
        public DateTime LastEditDate { get; set; }
        public string LastEditorId { get; set; }

        }
    }

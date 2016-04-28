using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TotlerRepository.ModelValidation
{
    public abstract class AbstractValidator
    {
        public bool TryValidate(out ICollection<ValidationResult> results)
            {
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(
               this, context, results,
                validateAllProperties: true
            );
            }

        [JsonIgnore, NotMapped]
        public bool IsValid => Validator.TryValidateObject(
                this, new ValidationContext(this, null, null),
                new List<ValidationResult>());

        public ICollection<ValidationResult> ValidationMessages()
        {
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(
               this, context, results,
                validateAllProperties: true
            )) return results;

            throw new ValidationException("Model Valid");
        }

    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSample.ViewModels
{
    public class UserViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        public IFormFile Photo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var photo = ((UserViewModel)validationContext.ObjectInstance).Photo;
            var extension = Path.GetExtension(photo.FileName);
            var size = photo.Length;

            if (!extension.ToLower().Equals(".jpg"))
                yield return new ValidationResult("File extension is not valid.");

           if(size > (5 * 1024 * 1024))
                yield return new ValidationResult("File size is bigger than 5MB.");
        }
    }
}

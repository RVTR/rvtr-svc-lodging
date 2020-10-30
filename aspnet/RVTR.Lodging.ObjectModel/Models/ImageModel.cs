using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
    public class ImageModel : IValidatableObject
    {
        /// <summary>
        /// The public key Id for each image url 
        /// </summary>
        [Key]
        public int imageId { get; set; }

        /// <summary>
        /// The id of the lodging the image url belongs to 
        /// </summary>
        public int lodgingId { get; set; }

        /// <summary>
        /// The url for the image itself
        /// </summary>
        public string imageUrl { get; set; }

        /// <summary>
        /// Represents the ImageModel's `Validate` method
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();

    }
}

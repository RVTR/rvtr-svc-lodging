using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.Domain.Attributes
{
    public class LengthAttribute : ValidationAttribute
    {
        private readonly int _minLength;

        private readonly int _maxLength;

        private readonly string _errorMessage;

        public LengthAttribute(int minLength, int maxLength, string errorMessage)
        {
            _maxLength = maxLength;
            _minLength = minLength;
            _errorMessage = errorMessage;
        }

    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        if(value == null || value.ToString().Length < _minLength || value.ToString().Length > _maxLength)
        {
            return new ValidationResult(_errorMessage);
        }

        return ValidationResult.Success;
    }

    }
}

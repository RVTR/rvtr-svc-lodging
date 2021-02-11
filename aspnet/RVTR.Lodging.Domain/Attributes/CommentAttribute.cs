using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.Domain.Attributes
{
  public class CommentAttribute : ValidationAttribute
  {
    public override bool IsValid(object value)
    {
      if(value.ToString().Length > 1 && value.ToString().Length < 1000)
      {
        return true;
      }

      return false;
    }
  }
}

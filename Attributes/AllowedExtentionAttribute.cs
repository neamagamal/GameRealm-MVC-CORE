namespace CrudOperation.Attributes;

public class AllowedExtentionAttribute : ValidationAttribute
{
    private readonly string _AllowedExtentions;
    public AllowedExtentionAttribute(string AllowedExtentions)
    {
        _AllowedExtentions = AllowedExtentions;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var MyFile = value as IFormFile;
        if (MyFile != null)
        {
            var Extention = Path.GetExtension(MyFile.FileName);
            var AllowedExten = _AllowedExtentions.Split(',').Contains(Extention, StringComparer.OrdinalIgnoreCase);
            if (!AllowedExten)
            {
                return new ValidationResult($"Only {_AllowedExtentions} are allowed");
            }
        }
        return ValidationResult.Success;

    }
}

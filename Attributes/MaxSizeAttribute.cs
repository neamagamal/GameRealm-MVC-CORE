namespace CrudOperation.Attributes;

public class MaxSizeAttribute : ValidationAttribute
{
    private readonly int _MaxFileSize;
    public MaxSizeAttribute(int MaxFileSize)
    {
        _MaxFileSize = MaxFileSize;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var MyFile = value as IFormFile;
        if (MyFile != null)
        {
            if (MyFile.Length > _MaxFileSize)
            {
                return new ValidationResult($"Maximun allowed size is{_MaxFileSize}");
            }
        }
        return ValidationResult.Success;

    }
}


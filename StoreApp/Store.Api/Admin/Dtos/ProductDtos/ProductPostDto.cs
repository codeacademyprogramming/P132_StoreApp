using FluentValidation;

namespace Store.Api.Admin.Dtos.ProductDtos
{
    public class ProductPostDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public IFormFile ImageFile { get; set; }
    }

    public class ProductPostDtoValidator : AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.SalePrice).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(x => x.CostPrice).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(x => x.CategoryId).NotNull();
            RuleFor(x => x.DiscountPercent).NotNull().GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);

            //RuleFor(x => x.ImageFile)
            //    .Must(x => x != null).WithMessage("ImageFile is required")
            //    .Must(x => x!=null && (x.ContentType == "image/png" || x.ContentType == "image/jpeg")).WithMessage("File type must be png,jpg or jpeg")
            //    .Must(x => x!=null && (x.Length <= 2097152)).WithMessage("File size must be less or equal than 2MB");

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.ImageFile == null)
                {
                    context.AddFailure("ImageFile", "ImageFile is required");
                }
                else if(x.ImageFile.ContentType != "image/png" && x.ImageFile.ContentType != "image/jpeg")
                {
                    context.AddFailure("ImageFile", "File type must be png,jpg or jpeg");
                }
                else if(x.ImageFile.Length > 2097152)
                {
                    context.AddFailure("ImageFile", "File size must be less or equal than 2MB");
                }
            });
        }
    }
}

using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

public class ProductService
{  
    public Aff<Either<Error, string>> GetProductDetailsAsync(string productId)
    {
        Task<string> productDetailsTask = Task.Run(async () =>
        {
            await Task.Delay(1000);
            // throw new Exception("An error has occurred");
            return $"Product details for {productId}";
        });
        
        var productDetailsEffect = Aff(async () => await productDetailsTask)
            .Map(Right<Error, string>);
        
        return productDetailsEffect;
    }
}

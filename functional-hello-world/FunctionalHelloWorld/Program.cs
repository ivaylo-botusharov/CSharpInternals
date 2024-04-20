using LanguageExt;
using LanguageExt.Common;

ProductService productService = new ProductService();

Fin<Either<Error, string>> productDetails = await productService
    .GetProductDetailsAsync("123")
    .Run();

productDetails.Match(
    Succ: either => either.IfRight(details => Console.WriteLine(details)),
    Fail: error => Console.WriteLine($"Error: {error.Message}")
);

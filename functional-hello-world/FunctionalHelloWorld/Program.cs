using static System.Console;
using LanguageExt;
using LanguageExt.Common;

ProductService productService = new();

Fin<Either<Error, string>> productDetails = await productService
    .GetProductDetailsAsync("123")
    .Run();

productDetails.Match(
    Succ: either => either.IfRight(WriteLine),
    Fail: error => WriteLine($"Error: {error.Message}")
);

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace HelloWorldGenerator;

[Generator]
public class HelloWorldGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context) { }

    public void Execute(GeneratorExecutionContext context)
    {
        var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);

        string? mainMethodNamespace = mainMethod?.ContainingNamespace.ToDisplayString() ?? 
            throw new InvalidOperationException("Main method not found");
        
        var @namespace = SyntaxFactory
            .NamespaceDeclaration(SyntaxFactory.ParseName(mainMethodNamespace))
            .NormalizeWhitespace();

        var @class = SyntaxFactory.ClassDeclaration(mainMethod.ContainingType.Name)
            .AddModifiers(
                SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                SyntaxFactory.Token(SyntaxKind.StaticKeyword),
                SyntaxFactory.Token(SyntaxKind.PartialKeyword)
            )
            .NormalizeWhitespace();

        var consoleWriteLineExpression = SyntaxFactory.MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            SyntaxFactory.IdentifierName("Console"),
            SyntaxFactory.IdentifierName("WriteLine")
        );

        var helloWorldArgument = SyntaxFactory.Argument(
            SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal("Hello, World!"))
        );

        var consoleWriteLineInvocation = SyntaxFactory.InvocationExpression(
            consoleWriteLineExpression,
            SyntaxFactory.ArgumentList(SyntaxFactory.SingletonSeparatedList(helloWorldArgument))
        );

        var consoleWriteLineStatement = SyntaxFactory.ExpressionStatement(consoleWriteLineInvocation);

        var method = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), "SayHello")
            .AddModifiers(
                SyntaxFactory.Token(SyntaxKind.StaticKeyword),
                SyntaxFactory.Token(SyntaxKind.PartialKeyword)
            )
            .WithBody(SyntaxFactory.Block(consoleWriteLineStatement))
            .NormalizeWhitespace();

        @class = @class.AddMembers(method);
        @namespace = @namespace.AddMembers(@class);

        var code = @namespace.ToFullString();
        
        string typeName = mainMethod.ContainingType.Name;

        context.AddSource($"{typeName}.g.cs", SourceText.From(code, Encoding.UTF8));
    }
}
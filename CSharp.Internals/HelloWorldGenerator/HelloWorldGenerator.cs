using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace HelloWorldGenerator;

[Generator]
public class HelloWorldGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context) { }

    public void Execute(GeneratorExecutionContext context)
    {
        IMethodSymbol? mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);

        string? mainMethodNamespaceName = mainMethod?.ContainingNamespace.ToDisplayString() ?? 
            throw new InvalidOperationException("Main method not found");
        
        NamespaceDeclarationSyntax mainMethodNamespace = SyntaxFactory
            .NamespaceDeclaration(SyntaxFactory.ParseName(mainMethodNamespaceName))
            .NormalizeWhitespace();

        SyntaxToken staticKeywordToken = SyntaxFactory.Token(SyntaxKind.StaticKeyword);
        SyntaxToken partialKeywordToken = SyntaxFactory.Token(SyntaxKind.PartialKeyword);

        ClassDeclarationSyntax mainMethodClass = SyntaxFactory
            .ClassDeclaration(mainMethod.ContainingType.Name)
            .AddModifiers(
                SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                staticKeywordToken,
                partialKeywordToken
            )
            .NormalizeWhitespace();

        MemberAccessExpressionSyntax consoleWriteLineExpression = SyntaxFactory.MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            SyntaxFactory.IdentifierName("Console"),
            SyntaxFactory.IdentifierName("WriteLine")
        );

        LiteralExpressionSyntax helloWorldLiteralExpression = SyntaxFactory.LiteralExpression(
            SyntaxKind.StringLiteralExpression,
            SyntaxFactory.Literal("Hello, World!"));

        ArgumentSyntax helloWorldArgument = SyntaxFactory.Argument(helloWorldLiteralExpression);

        ArgumentListSyntax helloWorldArgumentList = SyntaxFactory.ArgumentList(
            SyntaxFactory.SingletonSeparatedList(helloWorldArgument));

        InvocationExpressionSyntax consoleWriteLineInvocation = SyntaxFactory.InvocationExpression(
            consoleWriteLineExpression,
            helloWorldArgumentList);

        ExpressionStatementSyntax consoleWriteLineStatement = SyntaxFactory
            .ExpressionStatement(consoleWriteLineInvocation);

        MethodDeclarationSyntax method = SyntaxFactory
            .MethodDeclaration(SyntaxFactory.ParseTypeName("void"), "SayHello")
            .AddModifiers(
                staticKeywordToken,
                partialKeywordToken
            )
            .WithBody(SyntaxFactory.Block(consoleWriteLineStatement))
            .NormalizeWhitespace();

        mainMethodClass = mainMethodClass.AddMembers(method);
        mainMethodNamespace = mainMethodNamespace.AddMembers(mainMethodClass);

        var mainMethodNamespaceCode = mainMethodNamespace.ToFullString();

        string mainMethodContainingTypeName = mainMethod.ContainingType.Name;

        context.AddSource(
            $"{mainMethodContainingTypeName}.g.cs",
            SourceText.From(mainMethodNamespaceCode, Encoding.UTF8));
    }
}
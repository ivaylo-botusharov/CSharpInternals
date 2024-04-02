// Metaprogramming (using Reflection and IL code generation)
// inspired by: Hello World in .NET written using IL code (Nick Chapsas)(https://www.youtube.com/watch?v=0H66H8PxcB8)

using System.Reflection;
using System.Reflection.Emit;

var assemblyName = "Printer";
var assemblyPath = $"{AppDomain.CurrentDomain.BaseDirectory}{assemblyName}.dll";
var typeName = "ConsolePrinter";
string methodName = "Print";
var message = "Hello, World!";

CreateAndSavePrinterAssembly(assemblyName, assemblyPath, typeName, methodName, message);

Type typeFromAssembly = GetTypeFromAssembly(assemblyPath, typeName);

InvokeMethodFromType(typeFromAssembly, methodName);

void CreateAndSavePrinterAssembly(string assemblyName, string assemblyPath, string typeName, string methodName, string message)
{
    AssemblyName assemblyNameInstance = new(assemblyName);
    AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefinePersistedAssembly(assemblyNameInstance, typeof(object).Assembly);

    ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(name: assemblyName);

    TypeBuilder typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public);

    MethodBuilder methodBuilder = typeBuilder.DefineMethod(
        methodName,
        MethodAttributes.Public | MethodAttributes.Static,
        typeof(void),
        Type.EmptyTypes);

    ILGenerator ilGenerator = methodBuilder.GetILGenerator();

    ilGenerator.Emit(OpCodes.Ldstr, message);

    var parameterTypes = new Type[1];
    parameterTypes[0] = typeof(string);

    MethodInfo writeLineMethod = typeof(Console).GetMethod(nameof(Console.WriteLine), parameterTypes) ?? 
        throw new InvalidOperationException();

    ilGenerator.Emit(OpCodes.Call, writeLineMethod);
    ilGenerator.Emit(OpCodes.Ret);

    _ = typeBuilder.CreateType();

    assemblyBuilder.Save(assemblyPath);
}

Type GetTypeFromAssembly(string assemblyPath, string typeName)
{
    Assembly assembly = Assembly.LoadFrom(assemblyPath);

    Type typeFromAssembly = assembly.GetType(typeName) ?? throw new InvalidOperationException();

    return typeFromAssembly;
}

void InvokeMethodFromType(Type type, string methodName)
{
    MethodInfo methodInfo = type.GetMethod(methodName) ?? throw new InvalidOperationException();

    methodInfo.Invoke(null, []);
}
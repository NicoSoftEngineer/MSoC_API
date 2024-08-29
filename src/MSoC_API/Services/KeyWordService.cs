using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using MSoC_API.Models;
using MSoC_API.Utils;
using Newtonsoft.Json;

//using StaticCodeAnalysis.Loaders;

namespace MSoC_API.Services;

public class KeyWordService(FileSystemOptions fileSystemOptions)
{
    public string? GetKeywords(string path)
    {
        var filePath = Path.Combine(fileSystemOptions.FileSystemPath, path);
        // Load the tree
        var tree = LoadTree(filePath);

        if (tree is null)
        {
            return null;
        }
        //Load keywords
        var keyWords = GetKeyWordsFromSyntaxTree(tree);

        return JsonConvert.SerializeObject(keyWords);
    }


    public string? GetFunctionContentByKeyword(string path, string keyword)
    {
        var filePath = Path.Combine(fileSystemOptions.FileSystemPath, path);

        // Load the tree
        var tree = LoadTree(filePath);

        if (tree is null)
        {
            return null;
        }

        var functions = tree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>()
            .Where(x => x.Identifier.Text == keyword).ToList();

        if (!functions.Any())
        {
            return null;
        }

        var functionsContents = functions.Select(x => x.ToFullString()).ToList();
        return JsonConvert.SerializeObject(functionsContents);
    }

    public SyntaxTree? LoadTree(string path)
    {
        string file = File.ReadAllText(path);

        if (string.IsNullOrWhiteSpace(file))
        {
            Console.WriteLine("The file is empty.");
            return null;
        }

        // Parse the file
        var tree = CSharpSyntaxTree.ParseText(file);

        return tree;
    }

    private static KeyWordModel GetKeyWordsFromSyntaxTree(SyntaxTree tree)
    {
        var keyWords = new KeyWordModel
        {
            Properties = tree.GetRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>()
                .Select(x => x.Identifier.Text).ToHashSet().ToList(),
            Methods = tree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().Select(x => x.Identifier.Text)
                .ToHashSet().ToList(),
            Enums = tree.GetRoot().DescendantNodes().OfType<EnumDeclarationSyntax>().Select(x => x.Identifier.Text)
                .ToHashSet().ToList(),
            Interfaces = tree.GetRoot().DescendantNodes().OfType<InterfaceDeclarationSyntax>()
                .Select(x => x.Identifier.Text).ToHashSet().ToList(),
            Classes = tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Select(x => x.Identifier.Text)
                .ToHashSet().ToList(),
            Fields = tree.GetRoot().DescendantNodes().OfType<FieldDeclarationSyntax>()
                .Select(x => x.Declaration.Variables.First().Identifier.Text).ToHashSet().ToList(),
            Constructors = tree.GetRoot().DescendantNodes().OfType<ConstructorDeclarationSyntax>()
                .Select(x => x.Identifier.Text).ToHashSet().ToList()
        };
        return keyWords;
    }
}
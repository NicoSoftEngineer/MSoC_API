﻿using Microsoft.CodeAnalysis;
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

        var keyWords = new KeyWordModel
        {
            Properties = tree.GetRoot().DescendantNodes().OfType<PropertyDeclarationSyntax>().Select(x => x.Identifier.Text).ToList(),
            Methods = tree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().Select(x => x.Identifier.Text).ToList(),
            Enums = tree.GetRoot().DescendantNodes().OfType<EnumDeclarationSyntax>().Select(x => x.Identifier.Text).ToList(),
            Interfaces = tree.GetRoot().DescendantNodes().OfType<InterfaceDeclarationSyntax>().Select(x => x.Identifier.Text).ToList(),
            Classes = tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Select(x => x.Identifier.Text).ToList(),
            Fields = tree.GetRoot().DescendantNodes().OfType<FieldDeclarationSyntax>().Select(x => x.Declaration.Variables.First().Identifier.Text).ToList(),
            Constructors = tree.GetRoot().DescendantNodes().OfType<ConstructorDeclarationSyntax>().Select(x => x.Identifier.Text).ToList()
        };

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

        var function = tree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().FirstOrDefault(x => x.Identifier.Text == keyword);

        if (function is null)
        {
            return null;
        }

        return function.ToFullString();
    }

    public SyntaxTree? LoadTree(string path)
    {
        string file = File.ReadAllText(path);

        if(string.IsNullOrWhiteSpace(file))
        {
            Console.WriteLine("The file is empty.");
            return null;
        }

        // Parse the file
        var tree = CSharpSyntaxTree.ParseText(file);

        return tree;
    }
}
namespace MSoC_API.Models;

public class KeyWordModel
{
    public List<string> Properties { get; set; } = new List<string>();//PropertyDeclarationSyntax
    public List<string> Methods { get; set; } = new List<string>();//MethodDeclarationSyntax
    public List<string> Enums { get; set; } = new List<string>();//EnumDeclarationSyntax
    public List<string> Interfaces { get; set; } = new List<string>();//InterfaceDeclarationSyntax
    public List<string> Classes { get; set; } = new List<string>();//ClassDeclarationSyntax
    public List<string> Fields { get; set; } = new List<string>(); //FieldDeclarationSyntax
    public List<string> Constructors { get; set; } = new List<string>();//ConstructorDeclarationSyntax

}
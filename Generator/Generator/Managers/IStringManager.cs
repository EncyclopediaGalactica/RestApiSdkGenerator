namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

/// <summary>
///     String manager interface
///     <remarks>
///         It provides methods for dealing with all strings related operations during code generation
///     </remarks>
/// </summary>
public interface IStringManager
{
    /// <summary>
    ///     Concatenates two string fragments
    /// </summary>
    /// <param name="s1">string 1</param>
    /// <param name="s2">string 2</param>
    /// <returns>the new string</returns>
    string Concat(string s1, string? s2);

    /// <summary>
    ///     Concatenates three string fragments
    /// </summary>
    /// <param name="s1">string 1</param>
    /// <param name="s2">string 2</param>
    /// <param name="s3">string 3</param>
    /// <returns>the new string</returns>
    string Concat(string s1, string s2, string s3);

    /// <summary>
    ///     Makes the first character of the provided string uppercase
    ///     <remarks>
    ///         If the input is null, empty or whitespace the method returns these
    ///     </remarks>
    /// </summary>
    /// <param name="s">string</param>
    /// <returns>the new string</returns>
    string MakeFirstCharUpperCase(string s);

    /// <summary>
    ///     Concatenates two string as they were C# namespaces.
    ///     <remarks>
    ///         The two string will be separated by a dot (.).
    ///         If both of the strings are null, empty or whitespace the method returns string.empty
    ///     </remarks>
    /// </summary>
    /// <param name="s1">namespace 1</param>
    /// <param name="s2">namespace 2</param>
    /// <returns>Concatenated namespaces</returns>
    string ConcatCsharpNamespaceTokens(string s1, string? s2);

    /// <summary>
    ///     Makes all capital letter in the provided string uppercase
    ///     <remarks>
    ///         C# namespaces are in this format
    ///         If the input string is null, empty or whitespace the method returns string.empty
    ///     </remarks>
    /// </summary>
    /// <param name="s">string</param>
    /// <returns>modified string</returns>
    string? MakeUppercaseTheCharAfterTheDot(string? s);

    /// <summary>
    ///     Makes the input string lowercase
    ///     <remarks>
    ///         If the input string is null, empty or whitespace string empty sill be returned
    ///     </remarks>
    /// </summary>
    /// <param name="s">string</param>
    /// <returns>modified string</returns>
    string ToLowerCase(string? s);

    /// <summary>
    ///     Checks if the first character of the provided string is "/" and throws if it is.
    /// </summary>
    /// <param name="s">string</param>
    void CheckIfFirstCharIsSlashAndThrow(string? s);

    /// <summary>
    ///     Checks if the provided string's last character is a "/", if so removes it
    ///     <remarks>
    ///         If the input string null, empty or whitespace string.empty will return
    ///     </remarks>
    /// </summary>
    /// <param name="s">input string</param>
    /// <returns>modified string</returns>
    string CheckIfLastCharSlashAndRemoveIt(string? s);

    /// <summary>
    ///     Checks if the first char of the provided string is a "/"
    /// </summary>
    /// <param name="s">input string</param>
    /// <returns>bool</returns>
    bool IsFirstCharIsASlash(string? s);

    /// <summary>
    ///     Transforms a snake_case string to PascalCase
    /// </summary>
    /// <param name="s">input string</param>
    /// <returns>modified string</returns>
    string? MakeSnakeCaseToPascalCase(string? s);

    /// <summary>
    ///     Checks if the first character of the provided string is a dot ".", if not adds it
    /// </summary>
    /// <param name="s">input string</param>
    /// <returns>modified string</returns>
    string? CheckIfFirstCharIsDotOrAddIt(string? s);
}
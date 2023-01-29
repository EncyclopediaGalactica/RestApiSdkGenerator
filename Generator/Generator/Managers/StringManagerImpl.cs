namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

using System.Text;
using Microsoft.Extensions.Logging;

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

/// <inheritdoc />
public class StringManagerImpl : IStringManager
{
    private readonly Logger<StringManagerImpl> _logger = new(LoggerFactory.Create(c => c.AddConsole()));

    /// <inheritdoc />
    public string Concat(string s1, string? s2)
    {
        StringBuilder builder = new StringBuilder();
        if (!string.IsNullOrEmpty(s1) && !string.IsNullOrWhiteSpace(s1))
        {
            builder.Append(s1.Trim());
        }

        if (!string.IsNullOrEmpty(s2) && !string.IsNullOrWhiteSpace(s2))
        {
            builder.Append(s2.Trim());
        }

        return builder.ToString();
    }

    /// <inheritdoc />
    public string Concat(string s1, string s2, string s3)
    {
        StringBuilder builder = new StringBuilder();
        if (!string.IsNullOrEmpty(s1) && !string.IsNullOrWhiteSpace(s1))
        {
            builder.Append(s1.Trim());
        }

        if (!string.IsNullOrEmpty(s2) && !string.IsNullOrWhiteSpace(s2))
        {
            builder.Append(s2.Trim());
        }

        if (!string.IsNullOrEmpty(s3) && !string.IsNullOrWhiteSpace(s3))
        {
            builder.Append(s3.Trim());
        }

        return builder.ToString();
    }

    /// <inheritdoc />
    public string MakeFirstCharUpperCase(string s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            _logger.LogInformation("Input string is null, empty or whitespace");
            return s;
        }

        return $"{s[0].ToString().ToUpper()}{s.Substring(1, s.Length - 1)}";
    }

    /// <inheritdoc />
    public string ConcatCsharpNamespaceTokens(string? s1, string? s2)
    {
        StringBuilder builder = new StringBuilder();
        if (!string.IsNullOrEmpty(s1) && !string.IsNullOrWhiteSpace(s1))
        {
            if (s1[0].ToString() == ".")
            {
                s1 = s1[1..];
            }

            if (s1[^1].ToString() == ".")
            {
                builder.Append(s1.Substring(0, s1.Length - 1));
            }
            else
            {
                builder.Append(s1);
            }
        }

        if (!string.IsNullOrEmpty(s2) && !string.IsNullOrWhiteSpace(s2))
        {
            if (s2[0].ToString() == ".")
            {
                s2 = s2[1..];
            }

            if (s2[^1].ToString() == ".")
            {
                if (builder.Length > 0)
                {
                    builder.Append(".").Append(s2[..^1]);
                }
                else
                {
                    builder.Append(s2[..^1]);
                }
            }
            else
            {
                if (builder.Length > 0)
                {
                    builder.Append(".").Append(s2);
                }
                else
                {
                    builder.Append(s2);
                }
            }
        }

        return builder.ToString();
    }

    /// <inheritdoc />
    public string MakeUppercaseTheCharAfterTheDot(string? s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return string.Empty;
        }

        StringBuilder builder = new StringBuilder();
        bool shouldBeCapital = false;
        for (int i = 0; i < s.Length; i++)
        {
            if (i == 0)
            {
                builder.Append(s[i].ToString().ToUpper());
                continue;
            }

            if (shouldBeCapital)
            {
                builder.Append(s[i].ToString().ToUpper());
                shouldBeCapital = false;
                continue;
            }

            if (s[i].ToString() == ".")
            {
                shouldBeCapital = true;
                builder.Append(s[i]);
            }
            else
            {
                builder.Append(s[i]);
            }
        }

        return builder.ToString();
    }

    /// <inheritdoc />
    public string ToLowerCase(string? s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return String.Empty;
        }

        return s.ToLower();
    }

    /// <inheritdoc />
    public void CheckIfFirstCharIsSlashAndThrow(string? s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return;
        }

        if (s[0].ToString() == "/")
        {
            throw new ArgumentException("First character of string cannot be /");
        }
    }

    public string CheckIfLastCharSlashAndRemoveIt(string? s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return String.Empty;
        }

        if (s[^1].ToString() == "/")
        {
            return s[..^1];
        }

        return s;
    }

    /// <inheritdoc />
    public bool IsFirstCharIsASlash(string? s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return false;
        }

        if (s[0].ToString() == "/")
        {
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public string? MakeSnakeCaseToPascalCase(string? s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return s;
        }

        bool makeNextCharCapital = false;
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            if (i == 0 && s[i].ToString() != "_")
            {
                builder.Append(s[0].ToString().ToUpper());
                continue;
            }

            if (s[i].ToString() == "_")
            {
                makeNextCharCapital = true;
                continue;
            }

            if (makeNextCharCapital)
            {
                builder.Append(s[i].ToString().ToUpper());
                makeNextCharCapital = false;
                continue;
            }

            builder.Append(s[i]);
        }

        return builder.ToString();
    }

    /// <inheritdoc />
    public string? CheckIfFirstCharIsDotOrAddIt(string? s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return s;
        }

        if (s[0].ToString() != ".")
        {
            return $".{s}";
        }

        return s;
    }
}
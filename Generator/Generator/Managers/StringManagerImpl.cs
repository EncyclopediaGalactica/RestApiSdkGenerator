namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

using System.Text;
using Microsoft.Extensions.Logging;

public interface IStringManager
{
    string Concat(string? s1, string? s2);
    string Concat(string? s1, string? s2, string? s3);
    string? MakeFirstCharUpperCase(string s);
    string ConcatCsharpNamespaceTokens(string s1, string s2);
    string MakeCapitalLetterTheOneAfterTheDot(string s);
    string ToLowerCase(string s);
    void CheckIfFirstCharIsSlashAndThrow(string s);
    string? CheckIfLastCharSlashAndRemoveIt(string s);
    bool IsLastCharASlash(string s);
    bool IsFirstCharIsASlash(string s);
    string MakeSnakeCaseToPascalCase(string s);
}

public class StringManagerImpl : IStringManager
{
    private readonly Logger<StringManagerImpl> _logger = new(LoggerFactory.Create(c => c.AddConsole()));

    public string Concat(string? s1, string? s2)
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

    public string Concat(string? s1, string? s2, string? s3)
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

    public string? MakeFirstCharUpperCase(string s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            _logger.LogInformation("Input string is null, empty or whitespace");
            return s;
        }

        return $"{s[0].ToString().ToUpper()}{s.Substring(1, s.Length - 1)}";
    }

    public string ConcatCsharpNamespaceTokens(string s1, string s2)
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

    public string MakeCapitalLetterTheOneAfterTheDot(string s)
    {
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

    public string ToLowerCase(string s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return s;
        }

        return s.ToLower();
    }

    public void CheckIfFirstCharIsSlashAndThrow(string s)
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

    public string? CheckIfLastCharSlashAndRemoveIt(string s)
    {
        if (string.IsNullOrEmpty(s) && string.IsNullOrWhiteSpace(s))
        {
            return s;
        }

        if (s[^1].ToString() == "/")
        {
            return s[..^1];
        }

        return s;
    }

    public bool IsLastCharASlash(string s)
    {
        if (string.IsNullOrEmpty(s) && string.IsNullOrWhiteSpace(s))
        {
            return false;
        }

        if (s[^1].ToString() == "/")
        {
            return true;
        }

        return false;
    }

    public bool IsFirstCharIsASlash(string s)
    {
        if (string.IsNullOrEmpty(s) && string.IsNullOrWhiteSpace(s))
        {
            return false;
        }

        if (s[0].ToString() == "/")
        {
            return true;
        }

        return false;
    }

    public string MakeSnakeCaseToPascalCase(string s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return s;
        }

        bool makeNextcharCapital = false;
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
                makeNextcharCapital = true;
                continue;
            }

            if (makeNextcharCapital)
            {
                builder.Append(s[i].ToString().ToUpper());
                makeNextcharCapital = false;
                continue;
            }

            builder.Append(s[i]);
        }

        return builder.ToString();
    }
}
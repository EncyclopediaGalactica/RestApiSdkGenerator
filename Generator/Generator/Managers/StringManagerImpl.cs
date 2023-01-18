namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers;

using System.Text;
using Microsoft.Extensions.Logging;

public interface IStringManager
{
    string Concat(string s1, string s2);
    string Concat(string s1, string s2, string s3);
    string MakeFirstCharUpperCase(string s);
    string ConcatCsharpNamespaceTokens(string s1, string s2);
    string ValidateCsharpNamespace(string s);
    string ToLowerCase(string s);
    void CheckIfFirstCharIsSlashAndThrow(string s);
    string CheckIfLastCharSlashAndRemoveIt(string s);
    bool IsLastCharASlash(string s);
    bool IsFirstCharIsASlash(string s);
}

public class StringManagerImpl : IStringManager
{
    private readonly Logger<StringManagerImpl> _logger = new(LoggerFactory.Create(c => c.AddConsole()));

    public string Concat(string s1, string s2)
    {
        if (string.IsNullOrEmpty(s1)
            || string.IsNullOrWhiteSpace(s1)
            || string.IsNullOrEmpty(s2)
            || string.IsNullOrWhiteSpace(s2))
        {
            _logger.LogInformation("First or second parameter is empty, or null or whitespace");
        }

        return $"{s1}{s2}";
    }

    public string Concat(string s1, string s2, string s3)
    {
        StringBuilder builder = new StringBuilder();
        if (!string.IsNullOrEmpty(s1) && !string.IsNullOrWhiteSpace(s1))
        {
            builder.Append(s1);
        }

        if (!string.IsNullOrEmpty(s2) && !string.IsNullOrWhiteSpace(s2))
        {
            builder.Append(s2);
        }

        if (!string.IsNullOrEmpty(s3) && !string.IsNullOrWhiteSpace(s3))
        {
            builder.Append(s3);
        }

        return builder.ToString();
    }

    public string MakeFirstCharUpperCase(string s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            _logger.LogInformation("Input string is null, empty or whitespace");
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
                builder.Append(s1[^2]);
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
                builder.Append(".").Append(s2[^2]);
            }
            else
            {
                builder.Append(s2);
            }
        }

        return builder.ToString();
    }

    public string ValidateCsharpNamespace(string s)
    {
        StringBuilder builder = new StringBuilder();
        bool shouldBeCapital = false;
        for (int i = 0; i < s.Length; i++)
        {
            if (i == 0)
            {
                builder.Append(s[i].ToString().ToUpper());
            }

            if (shouldBeCapital)
            {
                builder.Append(s[i].ToString().ToUpper());
                shouldBeCapital = false;
            }

            if (s[i].ToString() == ".")
            {
                shouldBeCapital = true;
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

    public string CheckIfLastCharSlashAndRemoveIt(string s)
    {
        if (string.IsNullOrEmpty(s) && string.IsNullOrWhiteSpace(s))
        {
            return s;
        }

        if (s[^1].ToString() == "/")
        {
            return s.Substring(0, s.Length - 2);
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
}
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{

    static List<string> CheckInput(string input)
    {
        List<string> issues = new List<string>();

        if (string.IsNullOrWhiteSpace(input))
        {
            issues.Add("EMPTY");
            return issues;
        }

        if (input.Length > 100)
        {
            issues.Add("LONG_INPUT");
        }

        string lower = input.ToLower();

        if (lower.Contains("' or") || lower.Contains("or 1=1") || lower.Contains("union select") || lower.Contains("drop table"))
        {
            issues.Add("SQL_INJECTION");
        }

        if (lower.Contains("<script") || lower.Contains("onerror=") || lower.Contains("javascript:"))
        {
            issues.Add("XSS");
        }

        if (lower.Contains("../") || lower.Contains("..\\"))
        {
            issues.Add("PATH_TRAVERSAL");
        }

        if (lower.Contains(";") && (lower.Contains("select") || lower.Contains("delete")))
        {
            if (!issues.Contains("SQL_INJECTION"))
            {
                issues.Add("SQL_INJECTION");
            }
        }

        return issues;
    }


    static string SanitizeInput(string input)
    {
        if (input == null)
        {
            return string.Empty;
        }

        string sanitized = input;

        sanitized = sanitized
            .Replace("<", string.Empty)
            .Replace(">", string.Empty)
            .Replace("'", string.Empty)
            .Replace("\"", string.Empty)
            .Replace(";", string.Empty)
            .Replace("--", string.Empty);

        sanitized = sanitized.Trim();

        return sanitized;
    }


    static string GetRiskLevel(List<string> issues)
    {
        if (issues == null)
        {
            return "LOW";
        }

        if (issues.Contains("SQL_INJECTION") || issues.Contains("XSS"))
        {
            return "HIGH";
        }

        if (issues.Contains("PATH_TRAVERSAL"))
        {
            return "HIGH";
        }

        if (issues.Contains("LONG_INPUT"))
        {
            return "MEDIUM";
        }

        if (issues.Contains("EMPTY"))
        {
            return "MEDIUM";
        }

        return "LOW";
    }


    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: dotnet run -- <input_string>");
            return;
        }

        string input = args[0];

        List<string> issues = CheckInput(input);
        string sanitized = SanitizeInput(input);
        string risk = GetRiskLevel(issues);

        if (issues.Count == 0)
        {
            Console.WriteLine("Status: CLEAN");
        }
        else
        {
            Console.WriteLine("Issues found:");
            foreach (string issue in issues)
            {
                Console.WriteLine("- " + issue);
            }
        }

        Console.WriteLine($"Original input: {input}");
        Console.WriteLine($"Sanitized input: {sanitized}");
        Console.WriteLine($"Risk Level: {risk}");

        return;
    }
}
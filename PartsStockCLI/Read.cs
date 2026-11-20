using System.Globalization;
using System;
namespace PartsStockCLI;

public static class Read
{
    public static string Text(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine()
               ?? throw new EndOfStreamException("Console input stream was closed.");
    }

    public static string RequiredText(string prompt)
    {
        while (true)
        {
            string value = Text(prompt).Trim();
            if (!string.IsNullOrWhiteSpace(value)) return value;
            Console.WriteLine("  A value is required.");
        }
    }

    public static int Int(string prompt, int min = 0)
    {
        while (true)
        {
            string raw = Text(prompt).Trim();
            if (int.TryParse(raw, out int value) && value >= min) return value;
            Console.WriteLine(min > 0
                ? $"  Enter a whole number of at least {min}."
                : "  Enter a whole number.");
        }
    }
    public static decimal Decimal(string prompt, decimal min = 0m)
    {
        while (true)
        {
            string raw = Text(prompt).Trim();

            if (decimal.TryParse(raw, out decimal value))
            {
                decimal rounded = decimal.Round(
                    value, 2, MidpointRounding.AwayFromZero
                );

                if (rounded >= min)
                    return rounded;
            }

            Console.WriteLine("  Enter a valid number, e.g. 12.50");
        }
    }

    public static string PastOrTodayDate(string prompt)
    {
        while (true)
        {
            string raw = Text(prompt).Trim();
    
            if (DateTime.TryParseExact(
                    raw,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime date))
            {
                if (date.Date <= DateTime.Today)
                {
                    return date.ToString("yyyy-MM-dd");
                }

                Console.WriteLine("  Future dates are not allowed.");
                continue;
            }

            Console.WriteLine("  Enter a valid date in YYYY-MM-DD format.");
        }
    }
    public static bool Yes(string prompt)
    {
        string raw = Text(prompt).Trim().ToLowerInvariant();
        return raw == "y" || raw == "yes";
    }
}

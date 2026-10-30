using System.Text.RegularExpressions;

namespace F1Predict.Domain.ValueObjects;

/// <summary>
/// Преобразование времени круга между миллисекундами и форматом Формулы 1: "1:24.758".
/// Само время везде хранится как int (миллисекунды).
/// </summary>
public static partial class LapTimeFormat
{
    // Минуты (1-2 цифры) : секунды (ровно 2 цифры, 00-59) . миллисекунды (ровно 3 цифры)
    [GeneratedRegex(@"^(\d{1,2}):([0-5]\d)\.(\d{3})$")]
    private static partial Regex LapTimeRegex();
    
    public static string ToText(int ms)
    {
        if (ms < 0) throw new ArgumentOutOfRangeException(nameof(ms), "Время круга не может быть отрицательным.");

        var minutes = ms / 60_000;
        var seconds = ms % 60_000 / 1_000;
        var millis = ms % 1_000;

        return $"{minutes}:{seconds:D2}.{millis:D3}";
    }

    /// <summary>
    /// Возвращает false, если строка не соответствует формату.
    /// </summary>
    public static bool TryParse(string? text, out int ms)
    {
        ms = 0;

        if (string.IsNullOrWhiteSpace(text))
            return false;

        var match = LapTimeRegex().Match(text.Trim());
        if (!match.Success)
            return false;

        var minutes = int.Parse(match.Groups[1].Value);
        var seconds = int.Parse(match.Groups[2].Value);
        var millis = int.Parse(match.Groups[3].Value);

        ms = minutes * 60_000 + seconds * 1_000 + millis;
        return true;
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class ReflexTester
{
    private static List<TimeSpan> todayResults = new List<TimeSpan>();
    private static string dataFile = "reflex_data.txt";

    static void Main(string[] args)
    {
        LoadTodayResults();

        Console.WriteLine("=== ТЕСТЕР РЕФЛЕКСОВ ===");
        Console.WriteLine("Нажмите любую клавишу, когда увидите 'GO!'");
        Console.WriteLine("Для выхода нажмите 'a'");
        Console.WriteLine();

        while (true)
        {
            Console.WriteLine("Готовы? Нажмите любую клавишу для начала раунда...");
            Console.ReadKey(true);

            if (RunReflexTest())
                break;

            Console.WriteLine();
        }

        SaveTodayResults();
    }

    static bool RunReflexTest()
    {
        
        Console.WriteLine("Приготовьтесь...");

        // Случайная задержка от 1 до 5 секунд
        Random rand = new Random();
        int delay = rand.Next(1000, 5000);
        Thread.Sleep(delay);

        Console.Clear();
        Console.WriteLine("GO!");

        DateTime startTime = DateTime.Now;
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        if (keyInfo.KeyChar == 'a' || keyInfo.KeyChar == 'A')
            return true;

        DateTime endTime = DateTime.Now;
        TimeSpan reactionTime = endTime - startTime;

        todayResults.Add(reactionTime);

        Console.WriteLine($"\nВаше время реакции: {reactionTime.TotalMilliseconds:F0} мс");

        TimeSpan bestTime = GetBestTime();
        Console.WriteLine($"Лучшее время за сегодня: {bestTime.TotalMilliseconds:F0} мс");

        Console.WriteLine($"\nПопыток сегодня: {todayResults.Count}");
        Console.WriteLine($"Среднее время: {GetAverageTime().TotalMilliseconds:F0} мс");

        return false;
    }

    static TimeSpan GetBestTime()
    {
        if (todayResults.Count == 0)
            return TimeSpan.Zero;

        TimeSpan best = todayResults[0];
        foreach (var time in todayResults)
        {
            if (time < best)
                best = time;
        }
        return best;
    }

    static TimeSpan GetAverageTime()
    {
        if (todayResults.Count == 0)
            return TimeSpan.Zero;

        double totalMs = 0;
        foreach (var time in todayResults)
        {
            totalMs += time.TotalMilliseconds;
        }
        return TimeSpan.FromMilliseconds(totalMs / todayResults.Count);
    }

    static void LoadTodayResults()
    {
        try
        {
            if (File.Exists(dataFile))
            {
                string[] lines = File.ReadAllLines(dataFile);
                string today = DateTime.Now.ToString("yyyy-MM-dd");

                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 2 && parts[0] == today)
                    {
                        if (double.TryParse(parts[1], out double ms))
                        {
                            todayResults.Add(TimeSpan.FromMilliseconds(ms));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки данных: {ex.Message}");
        }
    }

    static void SaveTodayResults()
    {
        try
        {
            List<string> allData = new List<string>();
            string today = DateTime.Now.ToString("yyyy-MM-dd");

            // Читаем существующие данные 
            if (File.Exists(dataFile))
            {
                string[] lines = File.ReadAllLines(dataFile);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 2 && parts[0] != today)
                    {
                        allData.Add(line);
                    }
                }
            }

            // сегодняшние результаты
            foreach (var time in todayResults)
            {
                allData.Add($"{today}|{time.TotalMilliseconds}");
            }

            File.WriteAllLines(dataFile, allData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сохранения данных: {ex.Message}");
        }
    }
}
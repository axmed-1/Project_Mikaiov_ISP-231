using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Тестер рефлексов (простая версия)");
        Console.WriteLine("Нажми Enter когда будешь готов...");
        Console.ReadLine(); 

        Console.WriteLine("Когда увидишь слово СТАРТ — нажми Enter как можно быстрее!");
        Console.WriteLine("Нажми Enter и смотри внимательно...");
        Console.ReadLine();

        Console.WriteLine("СТАРТ! ЖМИ!");

        DateTime startTime = DateTime.Now; 
        Console.ReadLine(); 
        DateTime endTime = DateTime.Now; 

        TimeSpan reaction = endTime - startTime; 

        Console.WriteLine("Твоя реакция: " + reaction.TotalMilliseconds + " миллисекунд");
        Console.WriteLine("Нажми Enter для выхода.");
        Console.ReadLine();
    }
}

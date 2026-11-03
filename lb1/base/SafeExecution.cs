using System;
using System.IO;
using System.Text;

namespace pr1.Base;

public static class SafeExecutor
{
    private const string LogFilePath = "log.txt";

    //void
    public static void Run(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
    }

    //func T
    public static T? Execute<T>(Func<T> func)
    {
        try
        {
            return func();
        }
        catch (Exception ex)
        {
            LogError(ex);
            return default;
        }
    }

    // 3. Централізований запис у журнал (log.txt)
    private static void LogError(Exception ex)
    {
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{ex.GetType().Name}]\n" +
                          $"Повідомлення: {ex.Message}\n" +
                          $"Стек викликів: {ex.StackTrace?.Trim()}\n" +
                          $"{new string('-', 50)}\n";

        try
        {
            File.AppendAllText(LogFilePath, logEntry, Encoding.UTF8);
            Console.WriteLine($"[SafeExecutor] Виникла помилка ({ex.GetType().Name}) - '{LogFilePath}'.");
        }
        catch (Exception logEx)
        {
            Console.WriteLine($"[SafeExecutor] Не вдалося записати лог: {logEx.Message}");
        }
    }
}
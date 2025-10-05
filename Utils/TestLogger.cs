using System;

public static class TestLogger {
    public static void Log(string message) {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    }
}
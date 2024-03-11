using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
  static Dictionary<string, string> errorWords = new Dictionary<string, string>();

  static void Main()
  {
    Console.Write("Введите путь к директории: ");
    string directoryPath = Console.ReadLine();
    bool IsTrue = false;

    while (IsTrue != true)
    {
      Console.WriteLine("Введите правильное слово: ");
      string userInput = Console.ReadLine().ToLower();
      FillTheDirectory(userInput);
      ProcessFilesInDirectory(directoryPath);
      Console.WriteLine("Для завершения нажмите Escape, для продолжения любую кнопку");
      ConsoleKey keyInfo = Console.ReadKey(true).Key;
      if (keyInfo == ConsoleKey.Escape)
      {
        IsTrue = true;
      }
      else
      {
        Console.Clear();
      }
    }
  }

  static void FillTheDirectory(string word)
  {
    for (int currentSymbol = 0; currentSymbol < word.Length; ++currentSymbol)
    {
    char[] splitedWord = word.ToCharArray();
      for(int secondCurrentSymbol = 0; secondCurrentSymbol < word.Length; ++secondCurrentSymbol)
      {
        char temp = splitedWord[currentSymbol];
        splitedWord[currentSymbol] = splitedWord[secondCurrentSymbol];
        splitedWord[secondCurrentSymbol] = temp;
        string finallyWord = new string(splitedWord);
        Console.WriteLine(finallyWord);
        errorWords[finallyWord] = word;

      }
    }
  }
      

  static void ProcessFilesInDirectory(string directoryPath)
  {
    string[] files = Directory.GetFiles(directoryPath, "*.txt", SearchOption.AllDirectories);

    foreach (string file in files)
    {
      string text = File.ReadAllText(file);

      foreach (var error in errorWords)
      {
        text = text.Replace(error.Key, error.Value);
      }

      string pattern = @"\(\d{3}\) \d{3}-\d{2}-\d{2}";
      string replacement = @"+380 12 345";
      text = Regex.Replace(text, pattern, replacement);

      File.WriteAllText(file, text);
    }

    Console.WriteLine("Процесс обработки файлов завершен.");
  }
}

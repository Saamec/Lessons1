using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Text;

string textFromFile = "";

using (FileStream fileStream = new FileStream("C:\\Users\\Admin\\Desktop\\cdev_Text.txt", FileMode.OpenOrCreate))
{
    byte[] buffer = new byte[fileStream.Length];

    await fileStream.ReadAsync(buffer, 0, buffer.Length);

    textFromFile = Encoding.Default.GetString(buffer);
}
//Console.WriteLine(textFromFile);
char[] delimiters = new char[] { ' ', '\r', '\n'};
textFromFile = new string(textFromFile.Where(c => !char.IsPunctuation(c)).ToArray());

//Console.WriteLine(textFromFile);

string[] text = textFromFile.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
//Console.WriteLine(text.Length);

LinkedList<string> lines = new LinkedList<string>();
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();

foreach (string line in text)
{
    if (!line.Equals("\r\n")) lines.AddLast(line);
}
stopWatch.Stop();
TimeSpan ts = stopWatch.Elapsed;
Console.WriteLine("Время на вставку в ЛинкедЛист " + ts.Milliseconds);

List<string> lines2 = new List<string>();
Stopwatch stopWatch2 = new Stopwatch();
stopWatch2.Start();

foreach (string line in text)
{
    if (!line.Equals("\r\n")) lines2.Add(line);
}
stopWatch2.Stop();
TimeSpan ts2 = stopWatch2.Elapsed;
Console.WriteLine("Время на вставку в Лист " + ts2.Milliseconds);

Array.Sort(text);

//foreach (string line in text)
//{
//    Console.WriteLine(line);
//}
Dictionary<String, int> map = new Dictionary<String, int>();
int count =0;
for(int i = 0; i < text.Length-1; i++)
{
    if (text[i].Equals(text[i + 1]))
    {
        count++;
    }
    else
    {
        map.Add(text[i], count);
        count= 0;
    }
}
//foreach (var pair in map)
//{
//    Console.WriteLine("{0} {1}",
//    pair.Key,
//    pair.Value);
//}
var sortedDict = map.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
int kol = 1;
Console.WriteLine("*** список самых частых слов в тексте и их повторений ***");
foreach (var pair in sortedDict)
{
    Console.WriteLine("Слово \"{0}\" найдено в тексте  {1} раз",
    pair.Key,
    pair.Value);
    kol++;
    if (kol > 10) break;
}


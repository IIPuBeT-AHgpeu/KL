string textFromFile;

using(FileStream fs = new FileStream(@"C:\Users\Kirpa\source\repos\KL\KL\7.txt", FileMode.Open))
{
    byte[] bytes = new byte[fs.Length];

    fs.Read(bytes, 0, bytes.Length);

    textFromFile = System.Text.Encoding.Default.GetString(bytes);
}

string[] links = textFromFile.Split('\n');

KL.Parser parser = new KL.Parser(links[0]);
parser.GetHtmlBody();

Console.WriteLine(parser.HtmlBody);

Console.ReadLine();
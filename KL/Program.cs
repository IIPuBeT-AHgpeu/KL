string textFromFile;

using(FileStream fs = new FileStream(@"C:\Users\Kirpa\source\repos\KL\KL\7.txt", FileMode.Open))
{
    byte[] bytes = new byte[fs.Length];

    fs.Read(bytes, 0, bytes.Length);

    textFromFile = System.Text.Encoding.Default.GetString(bytes);
}

string[] links = textFromFile.Split('\n');

KL.Parser parser;
string write;

for (int i = 0; i < links.Length; i++)
{
    parser = new KL.Parser(links[i]);
    parser.GetHtmlBody();
    parser.ParseHtmlBody();

    using (FileStream fs = new FileStream(@$"C:\Users\Kirpa\source\repos\result\{i+1}.txt", FileMode.Create))
    {
        write = "Abstract:\n" + parser.Patent.Abstract + "\nDescription:\n" + parser.Patent.Description + "\nClaims:\n" + parser.Patent.Claims;

        byte[] bytes = System.Text.Encoding.Default.GetBytes(write);
        fs.Write(bytes, 0, bytes.Length);
    }
}
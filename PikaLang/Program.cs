using Pika.PikaLang;

var parser = new PikaParser();
TextReader stream = new StreamReader("../../../../GameData/Games/test.pika");
var game = parser.Parse(stream);

Console.WriteLine("Done...");

using Pika.PikaLang;

var parser = new PikaParser();
TextReader stream = new StreamReader("../../../../GameData/Games/hades/hades.pika");
var game = parser.Parse(stream);

Console.WriteLine("Done...");

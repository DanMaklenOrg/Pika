using Pika.PikaLang;

var parser = new PikaParser();
TextReader stream = new StreamReader("../../../../DomainData/Domains/hades/hades.pika");
var domain = parser.Parse(stream);

Console.WriteLine("Done...");

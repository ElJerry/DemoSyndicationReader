using System;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System.Xml;
using System.Threading.Tasks;

namespace DemoWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            SyndicationPerson person = new SyndicationPerson();
            person.Email = "champion@asdasd.com";

            XmlWriter xmlWriter = XmlWriter.Create(Console.Out); 
            Rss20FeedWriter writer = new Rss20FeedWriter(xmlWriter);

            Task t = writer.Write(person);
            Task.WaitAll(t);
            xmlWriter.Flush();

            Console.Read();
        }
    }
}
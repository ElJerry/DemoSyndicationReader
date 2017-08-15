using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SyndicationFeed.Rss;
using Microsoft.SyndicationFeed;
using System.Xml;
using System.Threading;

namespace DemoTest1
{
    class ReaderConsoleExample
    {
        public static async Task ReadFeed()
        {
            using (XmlReader xmlReader = XmlReader.Create(@"testfeeds\rss20.xml", new XmlReaderSettings() { Async = true }))
            using (XmlWriter xmlWriter = XmlWriter.Create(Console.Out, new XmlWriterSettings() { Indent = true, Async = true }))
            {
                Rss20FeedReader reader = new Rss20FeedReader(xmlReader);
                Rss20FeedWriter writer = new Rss20FeedWriter(xmlWriter);

                while (await reader.Read())
                {
                    switch (reader.ElementType)
                    {
                        case SyndicationElementType.Category:
                            ISyndicationCategory category = await reader.ReadCategory();
                            await writer.Write(category);
                            break;

                        case SyndicationElementType.Image:
                            ISyndicationImage image = await reader.ReadImage();
                            await writer.Write(image);
                            break;

                        case SyndicationElementType.Item:
                            ISyndicationItem item = await reader.ReadItem();
                            await writer.Write(item);
                            break;

                        case SyndicationElementType.Link:
                            ISyndicationLink link = await reader.ReadLink();
                            await writer.Write(link);
                            break;

                        case SyndicationElementType.Person:
                            ISyndicationPerson person = await reader.ReadPerson();
                            await writer.Write(person);
                            break;

                        default:
                            ISyndicationContent content = await reader.ReadContent();
                            await writer.Write(content);
                            break;
                    }

                    xmlWriter.Flush();
                    Thread.Sleep(200);
                }
            }
        }
    }
}




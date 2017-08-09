// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*
 * In this example we create an Rss20FeedReader using an XmlReader.
 * 
 * While we can read the feed, which means we still have things to read,
 * we will check what ElementType the field we read is, and with information
 * we decide how we are going to parse the content.
 * 
 */

using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace Examples
{
    class RssReadFeedExample
    {
        // Read an RssFeed
        public static async Task CreateRssFeedReaderExample()
        {
            // Create an XmlReader
            using (var xmlReader = XmlReader.Create(@"Feeds\rss20-2items.xml"))
            {
                // Instantiate an Rss20FeedReader using the XmlReader.
                // This will assign as default an Rss20FeedParser as parser.
                var feedReader = new Rss20FeedReader(xmlReader);

                // Loop While the reader reads and use the ElementType
                while(await feedReader.Read())
                {
                    Console.WriteLine("Read: " + feedReader.ElementType);

                    switch (feedReader.ElementType)
                    {
                        // Read as category
                        case SyndicationElementType.Category:
                            ISyndicationCategory category = await feedReader.ReadCategory();
                            break;

                        // Read as Image
                        case SyndicationElementType.Image:
                            ISyndicationImage image = await feedReader.ReadImage();
                            break;

                        // Read as Item
                        case SyndicationElementType.Item:
                            ISyndicationItem item = await feedReader.ReadItem();
                            break;

                        // Read as link
                        case SyndicationElementType.Link:
                            ISyndicationLink link = await feedReader.ReadLink();
                            break;

                        // Read as Person
                        case SyndicationElementType.Person:
                            ISyndicationPerson person = await feedReader.ReadPerson();
                            break;

                        // Read as content
                        default:
                            ISyndicationContent content = await feedReader.ReadContent();
                            break;
                    }
                }
            }
        }
    }
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*
 * In this example we know that the item contains a field that the current parser 
 * doesn't recognize.
 * 
 * A way to work arround this problem is to read the item as content, search in 
 * the fields for the unrecognized field.
 * 
 * Optionaly the content can be transformed to an Item using the Rss parser.
 * 
 */ 
 
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Examples
{
    class RssReadItemAsContentExample
    {
        // Read unrecognized tag from an item.
        public static async Task ReadItemAsContent()
        {
            using (var xmlReader = XmlReader.Create(@"Feeds\rss20-2items.xml"))
            {
                // Instantiate an Rss20FeedReader using the XmlReader.

                var parser = new Rss20FeedParser();
                var feedReader = new Rss20FeedReader(xmlReader, parser);

                // Loop until you find an item.
                while (await feedReader.Read())
                {
                    if (feedReader.ElementType == SyndicationElementType.Item)
                    {
                        // Read the item as content.
                        ISyndicationContent content = await feedReader.ReadContent();

                        // Parse the item without the unrecognized tag.
                        ISyndicationItem item = parser.CreateItem(content);

                        // Search for a custom tag
                        ISyndicationContent field = content.Fields.FirstOrDefault(f => f.Name == "customElement");

                        if (field != null)
                        {
                            Console.WriteLine("Parsed a custom field " + field.Name + ": " + field.Value);
                        }

                        break;
                    }
                }
            }
        }
    }
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*
 * 
 * In this example we will create a simple RSS Feed, giving the user the
 * idea of how to use the writer.
 * 
 */

using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Examples
{
    class RssFeedWriterExample
    {
        public static async Task WriteFeed()
        {
            var sw = new StringWriter();
            using (XmlWriter xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { Encoding = Encoding.UTF8 }))
            {
                var writer = new Rss20FeedWriter(xmlWriter);

                //
                // Title
                var title = new SyndicationContent(Rss20ElementNames.Title, "Example of Rss20FeedWriter");
                await writer.Write(title);

                //
                // Description
                var description = new SyndicationContent(Rss20ElementNames.Description, "This feed is for educational purposes only");
                await writer.Write(description);

                //
                // Link
                var link = new SyndicationLink(new Uri("https://github.com/dotnet/wcf"), Rss20LinkTypes.Alternate);
                await writer.Write(link);

                //
                // ManagingEditor
                var managingEditor = new SyndicationPerson()
                {
                    Email = "managingeditor@contoso.com",
                    RelationshipType = Rss20ContributorTypes.ManagingEditor
                };
                await writer.Write(managingEditor);

                //
                // PubDate
                var pubDate = new SyndicationContent(Rss20ElementNames.PubDate, "Thu, 06 Jul 2017 20:25:00 GMT");
                await writer.Write(pubDate);

                //
                // CustomElement
                var CustomElement = new SyndicationContent("ExtraInformation");

                // Add any field or attributes
                CustomElement.AddAttribute(new SyndicationAttribute("customElement","true"));
                CustomElement.AddField(new SyndicationContent("Company", "Contoso"));
                CustomElement.AddField(new SyndicationContent("Year", "2017"));
                await writer.Write(CustomElement);

                //
                // Items
                var item = new SyndicationItem();
                item.Title = "Rss Writer Avaliable";
                item.Description = "The new Rss Writer is now avaliable to download as NuGet Package!";
                item.AddLink(new SyndicationLink(new Uri("https://github.com/dotnet/wcf"), Rss20LinkTypes.Alternate));
                item.AddCategory(new SyndicationCategory("Technology"));
                item.Id = "https://github.com/dotnet/wcf/tree/lab/lab/src/Microsoft.SyndicationFeed/src";
                item.AddContributor(new SyndicationPerson() { Email = "test@mail.com", RelationshipType = Rss20ContributorTypes.Author });

                await writer.Write(item);

                xmlWriter.Flush();
            }

            string result = sw.ToString();
            Console.WriteLine(result);
        }
    }
}

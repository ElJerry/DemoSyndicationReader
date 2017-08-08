// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Examples
{
    class RssWriterWriteCustomItem
    {
        public static async Task WriteCustomItem()
        {
            var sw = new StringWriter();
            using (XmlWriter xmlWriter = XmlWriter.Create(sw))
            {
                var formatter = new Rss20Formatter(xmlWriter.Settings);
                var writer = new Rss20FeedWriter(xmlWriter);


                //
                // Create an item
                var item = new SyndicationItem();
                item.Title = "Rss Writer Avaliable";
                item.Description = "The new Rss Writer is now avaliable to download as NuGet Package!";
                item.AddLink(new SyndicationLink(new Uri("https://github.com/dotnet/wcf"), Rss20LinkTypes.Alternate));
                item.AddCategory(new SyndicationCategory("Technology"));
                item.Id = "https://github.com/dotnet/wcf/tree/lab/lab/src/Microsoft.SyndicationFeed/src";
                item.AddContributor(new SyndicationPerson() { Email = "test@mail.com", RelationshipType = Rss20ContributorTypes.Author });

                //
                // Format it to a content
                SyndicationContent itemContent = formatter.CreateContent(item) as SyndicationContent;

                itemContent.AddField(new SyndicationContent("ExtraField", "Extra value"));

                await writer.Write(itemContent);

                xmlWriter.Flush();
            }

            string res = sw.ToString();
            Console.WriteLine(res);
        }
    }
}

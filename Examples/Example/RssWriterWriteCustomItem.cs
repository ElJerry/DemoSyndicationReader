// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*
 * 
 * In this example we show how to create an item using SyndicationContent.
 * Everything in SyndicationFeed can be represented as SyndicationContent.
 * 
 * If the user needs to create a custom object for parsing he can do the 
 * following.
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
                // Create an item as content
                var item = new SyndicationContent(Rss20ElementNames.Item);

                // Add Title
                item.AddField(new SyndicationContent(Rss20ElementNames.Title, "Rss Writer Avaliable"));

                // Add Description
                item.AddField(new SyndicationContent(Rss20ElementNames.Description, "The new Rss Writer is now avaliable to download as NuGet Package!"));

                // Add Link
                item.AddField(new SyndicationContent(Rss20ElementNames.Link, "https://github.com/dotnet/wcf"));

                // Add Category
                item.AddField(new SyndicationContent(Rss20ElementNames.Category, "Technology"));

                // Add Contributor
                item.AddField(new SyndicationContent(Rss20ElementNames.Author, "test@mail.com"));

                // Add Custom fields
                item.AddField(new SyndicationContent("CustomField", "Custom Value"));

                await writer.Write(item);

                xmlWriter.Flush();
            }

            string res = sw.ToString();
            Console.WriteLine(res);
        }
    }
}

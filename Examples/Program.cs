// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

//using Microsoft.SyndicationFeed;
//using Microsoft.SyndicationFeed.Rss;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Syndication Feed Examples");

            Task t;
            //t = RssWriteItemWithCustomElement.WriteCustomItem();
            //t = AtomFeedReaderExample.ReadAtomFeed(@"C:\git\wcf\lab\src\Microsoft.SyndicationFeed\tests\TestFeeds\simpleAtomFeed.xml");
            //t = ReadRssItemWithCustomFields.ReadFeed(@"C:\git\wcf\lab\src\Microsoft.SyndicationFeed\tests\TestFeeds\rss20-2items.xml");
            //t = RssWriteItemWithCustomElement.WriteCustomItem();
            //t = ReadRssItemWithCustomFields.ReadFeed(@"C:\git\wcf\lab\src\Microsoft.SyndicationFeed\tests\TestFeeds\rss20-2items.xml");
            Task.WaitAll(t);

            //PerformanceTest.RunTest().Wait();

        }
    }
}
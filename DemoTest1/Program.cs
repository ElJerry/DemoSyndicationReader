// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace DemoTest1
{
    using System.Threading.Tasks;
    class Program
    {
        static void Main(string[] args)
        {

            //Demo read and write a feed
            //Task demoReadWrite = ReaderConsoleExample.ReadFeed();
            //Task.WaitAll(demoReadWrite);


            //Demo feed 3gb
            //Task Demo1 = new FeedReaderConsumer().ConsumeFeed(@"testfeeds\rss20.xml");
            Task Demo1 = new FeedReaderConsumer().ConsumeFeed(@"\\funbox\Share\Personal\t-luhurt\feed3gb.xml");
            Task.WaitAll(Demo1);
        }
    }
}
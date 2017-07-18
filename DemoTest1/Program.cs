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
            FeedReaderConsumer feedReaderConsumer = new FeedReaderConsumer();

            //Task read = feedReaderConsumer.ConsumeFeed(@"TestFeeds\rss20.xml");
            Task read = feedReaderConsumer.ConsumeFeed(@"\\funbox\Share\Personal\t-luhurt\feed3gb.xml");
            Task.WaitAll(read);
        }
    }
}
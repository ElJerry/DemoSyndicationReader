using System;
using Microsoft.SyndicationFeed;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Threading;

namespace DemoTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            FeedReaderConsumer feedReaderConsumer = new FeedReaderConsumer();

            Task read = feedReaderConsumer.ConsumeFeed("bigrss.xml");
            //Task read = feedReaderConsumer.ConsumeFeed(@"\\funbox\Share\Personal\t-luhurt\feed3gb.xml");
            Task.WaitAll(read);
        }
    }
}
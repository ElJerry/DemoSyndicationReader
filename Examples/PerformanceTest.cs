//using Microsoft.SyndicationFeed;
//using Microsoft.SyndicationFeed.Rss;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml;

//namespace Examples
//{
//    class PerformanceTest
//    {
//        public static async Task RunTest()
//        {
//            Stopwatch sw1 = new Stopwatch();

//            sw1.Start();
//            int repetitions = 100000; //5

//            var attrs = new List<SyndicationAttribute>()
//            {
//                new SyndicationAttribute("xmlns:content", "http://contoso.com/"),
//            };

//            var content = new SyndicationContent("content:hello", "http://contoso.com/", "world");
//            var c1 = new SyndicationContent("hello", "world");

//            for (int i = 0; i < repetitions; ++i)
//            {
//                StringWriter sw = new StringWriter();

//                //using (XmlWriter xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { }))
//                using (XmlWriter xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { NamespaceHandling = NamespaceHandling.OmitDuplicates }))
//                {
//                    //var writer = new Rss20FeedWriter(xmlWriter, attrs, new Rss20Formatter(attrs, xmlWriter.Settings));
//                    var writer = new Rss20FeedWriter(xmlWriter, null, new OldFormatter());

//                    for (int j = 0; j < 10; ++j) //500000
//                    {
//                        await writer.Write(content);
//                        await writer.Write(c1);
//                    }

//                    xmlWriter.Flush();
//                }

//                //string res = sw.ToString();
//                sw.Dispose();
//            }

//            sw1.Stop();

//            Console.WriteLine(sw1.ElapsedMilliseconds);
//        }
//    }
//}

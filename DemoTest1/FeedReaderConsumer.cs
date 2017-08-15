// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace DemoTest1
{
    using Microsoft.SyndicationFeed;
    using Microsoft.SyndicationFeed.Rss;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    class FeedReaderConsumer
    {
        //Approx. size of elements
        private int _sizeOfItem = 0;

        public async Task ConsumeFeed(string filePath)
        {
            bool verbose = false;

            // Information to display.
            double size = new FileInfo(filePath).Length;
            double currentSize = 0;
            int itemsRead = 0;

            // Transform the size of the file to Kb - Mb - Gb.
            Tuple<double, string> sizeInfo = utils.ConvertBytesToSize(size);

            // Display the Size of the feed and ask for verbose.
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Size of the Feed: {0:N2} {1}",sizeInfo.Item1,sizeInfo.Item2); 
            Console.Write("Verbose Items (Y/N): ");

            Console.ForegroundColor = ConsoleColor.White;
            string input = Console.ReadLine();

            verbose = utils.ValidateVerbose(input);
            Console.CursorVisible = false;
            Stopwatch stopWatch = null;

            using (var xmlReader = XmlReader.Create(filePath, new XmlReaderSettings() { Async = true }))
            {
                var reader = new Rss20FeedReader(xmlReader);
                stopWatch = new Stopwatch();
                stopWatch.Start();
                ElementDisplayer displayer = new ElementDisplayer();

                while (await reader.Read())
                {
                    if (verbose)
                    {
                        utils.ClearInformation();
                    }

                    switch (reader.ElementType)
                    {
                        case SyndicationElementType.Content:
                            ISyndicationContent content = await reader.ReadContent();

                            if (verbose)
                            {
                                displayer.DisplayContent(content);
                            }
                            break;

                        case SyndicationElementType.Item:
                            ISyndicationItem item = await ReadItem(reader);

                            itemsRead++;
                            if (verbose)
                            {
                                displayer.DisplayItem(item);
                            }
                            currentSize += _sizeOfItem;
                            break;

                        case SyndicationElementType.Person:
                            ISyndicationPerson person = await ReadPerson(reader);
                            if (verbose)
                            {
                                displayer.DisplayPerson(person);
                            }
                            break;

                        case SyndicationElementType.Image:
                            ISyndicationImage image = await ReadImage(reader);
                            if (verbose)
                            {
                                displayer.DisplayImage(image);
                            }
                            break;

                        case SyndicationElementType.Link:
                            ISyndicationLink link = await ReadLink(reader);
                            if (verbose)
                            {
                                displayer.DisplayLink(link);
                            }
                            break;

                        case SyndicationElementType.Category:
                            ISyndicationCategory category = await ReadCategory(reader);
                            if (verbose)
                            {
                                displayer.DisplayCategory(category);
                            }
                            break;
                    }

                    double percentage = ((currentSize * 100) / size);

                    if(itemsRead % 200 == 0)
                    {
                        utils.WriteInformation(percentage, itemsRead, stopWatch.Elapsed);
                    }
                }
            }
            utils.ClearInformation();

            //Print end of reading
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Finished Reading, press enter to close.\n\n");
            utils.WriteInformation(100, itemsRead, stopWatch.Elapsed);
            Console.ReadLine();
        }      

        private async Task<ISyndicationItem> ReadItem(Rss20FeedReader reader)
        {
            ISyndicationItem item = null;
            if (_sizeOfItem == 0)
            {
                //The size of the item has not been read.
                ISyndicationContent content = await reader.ReadContent();
                //_sizeOfItem = Encoding.UTF8.GetByteCount(content.RawContent);
                _sizeOfItem = 291; // content.RawContent.Length;

                Rss20FeedParser parser = reader.Parser as Rss20FeedParser;
                item = parser.CreateItem(content);
            }
            else
            {
                item = await reader.ReadItem();
            }

            return item;
        }

        private async Task<ISyndicationPerson> ReadPerson(Rss20FeedReader reader)
        {
            ISyndicationPerson person = null;
            person = await reader.ReadPerson();

            return person;
        }

        private async Task<ISyndicationImage> ReadImage(Rss20FeedReader reader)
        {
            ISyndicationImage image = null;
            image = await reader.ReadImage();

            return image;
        }

        private async Task<ISyndicationLink> ReadLink(Rss20FeedReader reader)
        {
            ISyndicationLink link = null;
            link = await reader.ReadLink();

            return link;
        }

        private async Task<ISyndicationCategory> ReadCategory(Rss20FeedReader reader)
        {
            ISyndicationCategory category = null;
            category = await reader.ReadCategory();

            return category;
        }
    }
}

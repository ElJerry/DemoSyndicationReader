using Microsoft.SyndicationFeed;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace DemoTest1
{
    class FeedReaderConsumer
    {
        //Average size of item
        private int _sizeOfItem = 0;
        private int _sizeOfPerson = 0;
        private int _sizeOfImage = 0;
        private int _sizeOfLink = 0;
        private int _sizeOfCategory = 0;

        public async Task ConsumeFeed(string filePath)
        {
            bool verbose = false;

            //Information to display.
            double size = new System.IO.FileInfo(filePath).Length;
            double currentSize = 0;
            int itemsRead = 0;

            Tuple<double, string> sizeInfo = ConvertBytesToSize(size);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Size of the Feed: {0:N2} {1}",sizeInfo.Item1,sizeInfo.Item2); 
            Console.Write("Verbose Items (Y/N): ");
            Console.ForegroundColor = ConsoleColor.White;
            string input = Console.ReadLine();
            verbose = ValidateVerbose(input);

            Console.CursorVisible = false;
            Stopwatch stopWatch = null;

            using (var xmlReader = XmlReader.Create(filePath, new XmlReaderSettings() { Async = true }))
            {
                var reader = new Rss20FeedReader(xmlReader);
                stopWatch = new Stopwatch();
                stopWatch.Start();

                while (await reader.Read())
                {
                    if (verbose)
                    {
                        ClearInformation();
                    }

                    switch (reader.ElementType)
                    {
                        case SyndicationElementType.Content:
                            ISyndicationContent content = await reader.ReadContent();
                            if (verbose)
                            {
                                DisplayContent(content);
                            }
                            currentSize += Encoding.Unicode.GetByteCount(content.RawContent);
                            break;

                        case SyndicationElementType.Item:
                            ISyndicationItem item = await ReadItem(reader);

                            itemsRead++;
                            if (verbose)
                            {
                                DisplayItem(item);
                            }
                            currentSize += _sizeOfItem;
                            break;

                        case SyndicationElementType.Person:
                            ISyndicationPerson person = await ReadPerson(reader);
                            if (verbose)
                            {
                                DisplayPerson(person);
                            }
                            currentSize += _sizeOfPerson;
                            break;

                        case SyndicationElementType.Image:
                            ISyndicationImage image = await ReadImage(reader);
                            if (verbose)
                            {
                                DisplayImage(image);
                            }
                            currentSize += _sizeOfImage;
                            break;

                        case SyndicationElementType.Link:
                            ISyndicationLink link = await ReadLink(reader);
                            if (verbose)
                            {
                                DisplayLink(link);
                            }
                            currentSize += _sizeOfLink;
                            break;

                        case SyndicationElementType.Category:
                            ISyndicationCategory category = await ReadCategory(reader);
                            if (verbose)
                            {
                                DisplayCategory(category);
                            }
                            currentSize += _sizeOfCategory;
                            break;
                    }

                    double percentage = Math.Min(((currentSize * 100) / size), 90);

                    WriteInformation(percentage, itemsRead, stopWatch.Elapsed);
                }
            }
            ClearInformation();
            Console.WriteLine("Finished Reading, press enter to close.\n\n");
            WriteInformation(100, itemsRead, stopWatch.Elapsed);
            Console.ReadLine();
        }

        private void DisplayContent(ISyndicationContent content)
        {
            Console.WriteLine("--- Content read ---");
            Console.WriteLine(content.Name + ": " + content.RawContent);
            Console.WriteLine();
        }

        private void DisplayItem(ISyndicationItem item)
        {
            Console.WriteLine("--- Item Read ---");
            Console.WriteLine("Title: " + item.Title);
            Console.WriteLine("Description: " + item.Description);
            Console.WriteLine("PubDate: " + item.Published);
            Console.WriteLine();
        }

        private void DisplayPerson(ISyndicationPerson person)
        {
            Console.WriteLine("--- Person Read ---");
            Console.WriteLine("Email: " + person.Email);
            Console.WriteLine();
        }

        private void DisplayImage(ISyndicationImage image)
        {
            Console.WriteLine("--- Image Read ---");
            Console.WriteLine("Image Link: " + image.Link.Uri.AbsoluteUri);
            Console.WriteLine();
        }

        private void DisplayLink(ISyndicationLink link)
        {
            Console.WriteLine("--- Link Read ---");
            Console.WriteLine("Link: " + link.Uri.AbsoluteUri);
            Console.WriteLine();
        }

        private static void DisplayCategory(ISyndicationCategory category)
        {
            Console.WriteLine("--- Category Read ---");
            Console.WriteLine("Category: " + category.Name);
            Console.WriteLine();
        }

        private void WriteInformation(double percent, int items, TimeSpan time)
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            Console.CursorTop = Console.WindowTop + Console.WindowHeight - 2;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Percentage Read: {0:N2}%  \nItems: {1} Time: {2}:{3} Seconds",percent,items, time.Seconds, time.Milliseconds);

            // Restore previous position
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.SetCursorPosition(x, y);
        }

        private bool ValidateVerbose(string input)
        {
            input = input.Trim();
            input = input.ToUpper();
            char answer = input[0];

            if(answer == 'Y')
            {
                return true;
            }
            return false;
        }

        private void ClearInformation()
        {
            int currentY = Console.CursorTop;
            Console.SetCursorPosition(0,Console.WindowHeight + Console.WindowTop-2);
            //Console.BackgroundColor = ConsoleColor.Red;
            string emptyLine = new string(' ', Console.WindowWidth - 1);
            Console.Write(emptyLine);
            Console.Write(emptyLine);
            //Console.BackgroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(0, currentY);
        }

        private Tuple<double,string> ConvertBytesToSize(double bytes)
        {
            int timesDivided = 0;

            while(bytes > 1024)
            {
                timesDivided++;
                bytes /= 1024;
            }

            string name = null;

            switch (timesDivided)
            {
                case 1:
                    name = "Kb";
                    break;
                case 2:
                    name = "Mb";
                    break;
                case 3:
                    name = "Gb";
                    break;
                case 4:
                    name = "Tb";
                    break;
                default:
                    name = "Bytes";
                    break;
            }

            Tuple<double, string> result = new Tuple<double, string>(bytes,name);
            return result;
        }

        private async Task<ISyndicationItem> ReadItem(Rss20FeedReader reader)
        {
            ISyndicationItem item = null;
            if (_sizeOfItem == 0)
            {
                //The size of the item has not been read.
                ISyndicationContent content = await reader.ReadContent();
                _sizeOfItem = Encoding.Unicode.GetByteCount(content.RawContent);
                item = new Rss20FeedFormatter().ParseItem(content.RawContent);
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
            if (_sizeOfPerson == 0)
            {
                //The size of the item has not been read.
                ISyndicationContent content = await reader.ReadContent();
                _sizeOfPerson = Encoding.Unicode.GetByteCount(content.RawContent);
                person = new Rss20FeedFormatter().ParsePerson(content.RawContent);
            }
            else
            {
                person = await reader.ReadPerson();
            }

            return person;
        }

        private async Task<ISyndicationImage> ReadImage(Rss20FeedReader reader)
        {
            ISyndicationImage image = null;
            if (_sizeOfImage == 0)
            {
                //The size of the item has not been read.
                ISyndicationContent content = await reader.ReadContent();
                _sizeOfImage = Encoding.Unicode.GetByteCount(content.RawContent);
                image = new Rss20FeedFormatter().ParseImage(content.RawContent);
            }
            else
            {
                image = await reader.ReadImage();
            }

            return image;
        }

        private async Task<ISyndicationLink> ReadLink(Rss20FeedReader reader)
        {
            ISyndicationLink link = null;
            if (_sizeOfLink == 0)
            {
                //The size of the item has not been read.
                ISyndicationContent content = await reader.ReadContent();
                _sizeOfImage = Encoding.Unicode.GetByteCount(content.RawContent);
                link = new Rss20FeedFormatter().ParseLink(content.RawContent);
            }
            else
            {
                link = await reader.ReadLink();
            }

            return link;
        }

        private async Task<ISyndicationCategory> ReadCategory(Rss20FeedReader reader)
        {
            ISyndicationCategory category = null;
            if (_sizeOfCategory == 0)
            {
                //The size of the item has not been read.
                ISyndicationContent content = await reader.ReadContent();
                _sizeOfCategory = Encoding.Unicode.GetByteCount(content.RawContent);
                category = new Rss20FeedFormatter().ParseCategory(content.RawContent);
            }
            else
            {
                category = await reader.ReadCategory();
            }

            return category;
        }
    }
}

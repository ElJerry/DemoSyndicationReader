// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using System.Threading.Tasks;
using System.Xml;

namespace Examples
{
    /// <summary>
    /// A simple AtomFeedReader that consumes an entire Atom Feed.
    /// </summary>
    class AtomFeedReaderExample
    {
        public static async Task ReadAtomFeed()
        {
            // Create an XmlReader from file
            using (XmlReader xmlReader = XmlReader.Create(@"Feeds\simpleAtomFeed.xml"))
            {
                // Create an AtomFeedReader using the XmlReader
                var reader = new AtomFeedReader(xmlReader);

                // While there are fields to read...
                while (await reader.Read())
                {

                    // Check the ElementType of the field read.
                    switch (reader.ElementType)
                    {
                        // Read category
                        case SyndicationElementType.Category:
                            ISyndicationCategory category = await reader.ReadCategory();
                            break;

                        // Read image
                        case SyndicationElementType.Image:
                            ISyndicationImage image = await reader.ReadImage();
                            break;

                        // Read entry 
                        case SyndicationElementType.Item:
                            IAtomEntry entry = await reader.ReadEntry();                            
                            break;

                        // Read as link
                        case SyndicationElementType.Link:
                            ISyndicationLink link = await reader.ReadLink();
                            break;

                        // Read as person
                        case SyndicationElementType.Person:
                            ISyndicationPerson person = await reader.ReadPerson();
                            break;

                        // Read as content
                        default:
                            ISyndicationContent content = await reader.ReadContent();
                            break;
                    }
                }
            }
        }
    }
}

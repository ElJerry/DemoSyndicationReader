// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace DemoTest1
{
    using Microsoft.SyndicationFeed;
    using System;

    class ElementDisplayer
    {
        internal void DisplayContent(ISyndicationContent content)
        {
            Console.WriteLine("--- Content read ---");
            Console.WriteLine(content.Name + ": " + content.RawContent);
            Console.WriteLine();
        }

        internal void DisplayItem(ISyndicationItem item)
        {
            Console.WriteLine("--- Item Read ---");
            Console.WriteLine("Title: " + item.Title);
            Console.WriteLine("Description: " + item.Description);
            Console.WriteLine("PubDate: " + item.Published);
            Console.WriteLine();
        }

        internal void DisplayPerson(ISyndicationPerson person)
        {
            Console.WriteLine("--- Person Read ---");
            Console.WriteLine("Email: " + person.Email);
            Console.WriteLine();
        }

        internal void DisplayImage(ISyndicationImage image)
        {
            Console.WriteLine("--- Image Read ---");
            Console.WriteLine("Image Link: " + image.Link.Uri.AbsoluteUri);
            Console.WriteLine();
        }

        internal void DisplayLink(ISyndicationLink link)
        {
            Console.WriteLine("--- Link Read ---");
            Console.WriteLine("Link: " + link.Uri.AbsoluteUri);
            Console.WriteLine();
        }

        internal void DisplayCategory(ISyndicationCategory category)
        {
            Console.WriteLine("--- Category Read ---");
            Console.WriteLine("Category: " + category.Name);
            Console.WriteLine();
        }
    }
}

using System;
using System.Threading.Tasks;

namespace DemoReadWrite
{
    class Program
    {
        static void Main(string[] args)
        {
          ReaderConsoleExample.ReadFeed().GetAwaiter().GetResult();
        }
    }
}
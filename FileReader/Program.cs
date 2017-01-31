using FileReader.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    class Program
    {
        static void Main(string[] args)
        {
            IReader oReaader = new PlainTextReader();
            Console.WriteLine(oReaader.Read("mi_archivo.txt"));

            Console.ReadLine();
        }
    }
}

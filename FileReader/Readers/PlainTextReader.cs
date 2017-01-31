using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Readers
{
    class PlainTextReader : IReader
    {
        public string Name()
        {
            return "txt";
        }

        public string Read(string path)
        {
            return System.IO.File.ReadAllText(path);
        }
    }
}

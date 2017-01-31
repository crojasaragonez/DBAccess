using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Readers
{
    class Base64 : IReader
    {
        public string Name()
        {
           return "Base64";
        }

        public string Read(string path) {
            String text = System.IO.File.ReadAllText(path);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}


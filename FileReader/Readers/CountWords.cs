using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Readers
{
    class CountWords : IReader
    {
        public string Name()
        {
            return "txt";
        }

        public string Read(string path)
        {
            string texto = System.IO.File.ReadAllText(path);
            int count = 0;

            for (int i = 0; i < texto.Length; i++)
            {

                if (texto.ElementAt(i).Equals(' ') || i == 0 || texto.ElementAt(i).Equals('\r'))
                {
                        count++; 
                }
            }

            return Convert.ToString(count);
        }

    }
}

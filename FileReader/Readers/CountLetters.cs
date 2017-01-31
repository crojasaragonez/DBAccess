using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace FileReader.Readers
{
    class CountLetters : IReader
    {
        public string Name()
        {
            return "Count Letters";
        }

        public string Read(string path)
        {
            int cont = 0;

            String cadena = File.ReadAllText(path);
            foreach (char item in cadena)
            {
                if (char.IsLetter(item))
                {
                    cont++;
                }

            }

            return cont.ToString();

        }
    }
}
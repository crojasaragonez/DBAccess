using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Readers
{
    class Inverts : IReader
    {
        public string Name()
        {
            return "txt";
        }

        public string Read(string path)
        {

            String cadena = "";
            List<string> lista = new List<string>();
            String[] lines = System.IO.File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {

                lista.Add(lines[i]);

            }
            lista.Reverse();
            foreach (string ili in lista)
            {

                cadena += ili + "\n";
            }


            return cadena;
        }
    }
}

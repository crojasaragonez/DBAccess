using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Readers
{
    class First10Lines : IReader
    {
        public string Name()
        {
            return "First 10 Lines";
        }

      

        public string Read(string path)
        {
            int counter = 0;
            string line;
            string texto = "";

            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while (counter != 10 && (line = file.ReadLine()) != null)
            {
                texto += "\n" + (counter + 1) + "-" + line;
                counter++;
            }

            file.Close();
            return texto;
        }
    }
}

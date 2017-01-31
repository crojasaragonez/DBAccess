using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Readers
{
    class ProcesoLineas : IReader
    {

        public string Name()
        {
            return "ContarLineas";
        }

        public string Read(string path)
        {
            StreamReader sr = new StreamReader(path);
            String linea = "";
            int i = 0;

            while (sr.Peek() != -1)

            {

                sr.ReadLine();

                i++;

            }
            linea = "El archivo contiene " + i + " lineas.";

            return linea;
        }
    }
}

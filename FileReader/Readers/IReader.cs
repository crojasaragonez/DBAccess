using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Readers
{
    interface IReader
    {
        //describes the file reader
        string Name();
        //returns the file content
        string Read(string path);

        void contadorLineas(string path);

        String devolverTexto();



    }


}

using Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Readers
{
    class TenLines : IReader
    {
        List<String> lista = new List<String>();
        List<String> list = new List<String>();
        public string Name()
        {
            return "10 Lines";
        }

        public string Read(string path)
        {
            this.contadorLineas(path);
            return this.devolverTexto();
        }

        private void contadorLineas(string path)
        {



            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {

                lista.Add(line);

            }


            file.Close();



        }

        private String devolverTexto()
        {
            String x = "";
            int c = 0;
            int cont = lista.Count();
            lista.Reverse();

            foreach (String item in lista)
            {

                c++;
                if (c < 11)
                {
                    list.Add(item);

                }
            }
            list.Reverse();
            string a ="";
            foreach (String dato in list)
            {
               
                a += dato +"\n";
            }

            return a;
        }
    }
}

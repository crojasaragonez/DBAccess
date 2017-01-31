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
            if (args.Length < 2) {
                Program.Usage();
                return;
            }
            string reader = args[0];
            string path = args[1];
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IReader).IsAssignableFrom(p))
                .Where(a => !a.FullName.Equals("FileReader.Readers.IReader"));

            foreach (var imp in types)
            {
                IReader implementation = (IReader)Activator.CreateInstance(imp);
                if (implementation.Name().Equals(reader))
                {
                    Console.WriteLine("Reader: {0}", implementation.Name());
                    Console.WriteLine(implementation.Read(path));
                    return;
                }
            }
            Console.Error.WriteLine("Reader {0} was not found", reader);
        }

        public static void Usage() {
            Console.WriteLine("Usage:\n FileReader.exe reader_name file_path");
        }
    }
}

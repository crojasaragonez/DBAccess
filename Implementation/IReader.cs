using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public interface IReader
    {
        //describes the file reader
        string Name();
        //returns the file content
        string Read(string path);
    }
}

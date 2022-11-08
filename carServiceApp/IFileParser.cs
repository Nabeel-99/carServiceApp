using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carServiceApp
{
    internal interface IFileParser
    {
        List<T> ParseFiles<T>(string filePath);
    }
}

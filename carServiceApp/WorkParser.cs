using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace carServiceApp
{
    internal class WorkParser : IFileParser
    {
        public List<T> ParseFiles <T> (string filePath)
        {
            List<Work> works = new List<Work>();
            string LoadFile = string.Format(filePath);
            using (StreamReader reader = new StreamReader(LoadFile))
            {
              string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    Work work = new Work
                    {
                        Services = parts[0],
                        MaterialCost = Convert.ToDouble(parts[2]),
                        Mins = Convert.ToInt32(parts[1])
                    };
                    works.Add(work);
                }
            }
            return new List<T>(works as IEnumerable<T> ?? throw new InvalidOperationException());
        }

    }
}

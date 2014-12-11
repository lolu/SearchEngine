using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchEngine;

namespace SearchEngineClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Document a = new Document("C:\\Users\\LOLU\\Documents\\csc322\\doc1", "txt");
            Document b = new Document("C:\\Users\\LOLU\\Documents\\csc322\\doc2", "txt");
            InvertedIndex.add(a);
            InvertedIndex.add(b);

            Dictionary<string, Dictionary<string, List<int>>> dict = InvertedIndex.Index();

        }
    }
}

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
            Document a = new Document("C:\\Users\\LOLU\\Documents\\csc322\\doc1.txt");
            Console.WriteLine(a.FilePath);
            Console.WriteLine(a.Type);
            
            Document b = new Document("C:\\Users\\LOLU\\Documents\\csc322\\doc2", "txt");
            InvertedIndex.add(a);
            InvertedIndex.add(b);

            
            Query query = new Query("web information");
            Console.WriteLine(query.QueryType());
            Console.WriteLine(query.tokens().Length);
            foreach(var item in query.RankedResults()){
                Console.WriteLine(item);
            }
            

        }
    }
}

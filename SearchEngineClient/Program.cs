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
            Document c = new Document("C:\\Users\\LOLU\\Books~Tutorials\\OSS2014.pdf");
            Document d = new Document("C:\\Users\\LOLU\\Books~Tutorials\\codility lessons\\1-TimeComplexity.pdf");
            Document e = new Document("C:\\Users\\LOLU\\Books~Tutorials\\codility lessons\\2-CountingElements.pdf");
            Document f = new Document("C:\\Users\\LOLU\\Books~Tutorials\\codility lessons\\3-PrefixSums.pdf");
            Document g = new Document("C:\\Users\\LOLU\\Documents\\csc322\\test", "html");

            InvertedIndex.add(a);
            InvertedIndex.add(b);
            InvertedIndex.add(c);

            /*
            Query query = new Query("open source information");
            Console.WriteLine(query.QueryType());
            Console.WriteLine(query.tokens().Count);
            foreach(var item in query.RankedResults()){
                Console.WriteLine(item);
            }*/
        }
    }
}

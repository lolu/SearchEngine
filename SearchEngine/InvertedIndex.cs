using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public static class InvertedIndex
    {
        private static Dictionary<string, Dictionary<string, List<int>>> index;
        //private constructor, use it to create inverted index from file
        static InvertedIndex()
        {
            index = new Dictionary<string, Dictionary<string, List<int>>>();
            //deserialize();
        }

        public static void add(Document document)
        {
            String[] terms = document.tokens();
            for (int i = 0; i < terms.Length; i++)
            {
                //term is already in index
                if (index.ContainsKey(terms[i]))
                {
                    Dictionary<string, List<int>> temp = index[terms[i]];
                    //term already exists in document
                    if (temp.ContainsKey(document.ToString()))
                    {
                        //append to list
                        temp[document.ToString()].Add(i);
                        index[terms[i]] = temp;
                    }
                    //first occurence in a document of term that already exists
                    else
                    {
                        List<int> positionsList = new List<int>();
                        positionsList.Add(i);
                        temp[document.ToString()] = positionsList;
                    }
                }
                //new term entry
                else
                {
                    Dictionary<string, List<int>> temp = new Dictionary<string, List<int>>();
                    List<int> positionsList = new List<int>();
                    positionsList.Add(i);
                    temp[document.ToString()] = positionsList;
                    index[terms[i]] = temp;
                }
            }
            //serialize();
        }

        /*
        //stores the state of the inverted index to a file
        private static void serialize()
        {
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(InvertedIndex));
            System.IO.StreamWriter file = new System.IO.StreamWriter(
                @"c:\temp\InvertedIndex.xml");
            writer.Serialize(file, index);
            file.Close();
        }

        //recreates the inverted index from a persisted state
        private static void deserialize()
        {
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(InvertedIndex));
            System.IO.StreamReader file = new System.IO.StreamReader(
                @"c:\temp\InvertedIndex.xml");
            index = (Dictionary<string, List<List<string>>>)reader.Deserialize(file);
        }
        */
        public static Dictionary<string, Dictionary<string, List<int>>> Index()
        {       
            return index;
        }

    }
}

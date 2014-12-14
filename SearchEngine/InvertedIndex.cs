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
        private static Dictionary<string, Dictionary<string, int>> tfIndex;//term frequency index
        private static Dictionary<string, int> dfIndex;//document frequency index
        private static int numOfDocuments;
        //private constructor, use it to create inverted index from file
        static InvertedIndex()
        {
            index = new Dictionary<string, Dictionary<string, List<int>>>();
            tfIndex = new Dictionary<string, Dictionary<string, int>>();
            dfIndex = new Dictionary<string, int>();
            //deserialize();
        }

        public static void add(Document document)
        {
            numOfDocuments += 1;
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
                        tfIndex[terms[i]][document.ToString()] += 1;//update tfIndex
                        temp[document.ToString()].Add(i);
                        index[terms[i]] = temp;
                    }
                    //first occurence in a document of term that already exists
                    else
                    {
                        dfIndex[terms[i]] += 1;
                        tfIndex[terms[i]][document.ToString()] = 1;
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
                    
                    Dictionary<string, int> tf = new Dictionary<string, int>();
                    tf[document.ToString()] = 1;
                    tfIndex[terms[i]] = tf;
                    dfIndex[terms[i]] = 1;
                }
            }
            //serialize();
        }

        
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
            index = (Dictionary<string, Dictionary<string, List<int>>>)reader.Deserialize(file);
        }
        
        public static Dictionary<string, Dictionary<string, List<int>>> Index()
        {       
            return index;
        }

        public static Dictionary<string, int> DFIndex()
        {
            return dfIndex;
        }

        public static Dictionary<string, Dictionary<string, int>> TFIndex()
        {
            return tfIndex;
        }

        public static int size()
        {
            return numOfDocuments;
        }

        public static double getTF(string documentID, String term)
        {
            return Math.Sqrt(tfIndex[term][documentID]);
        }

        public static double getIDF(string term)
        {
            return 1 + Math.Log(numOfDocuments / (dfIndex[term] + 1));
        }
    }
}

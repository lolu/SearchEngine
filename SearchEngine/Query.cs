using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class Query
    {
        private string queryString;
        private static Dictionary<string, Dictionary<string, List<int>>> index;

        public Query(string queryString)
        {
            this.queryString = queryString.Trim().ToLower();
            index = InvertedIndex.Index();
        }

        public string QueryType()
        {
            if (queryString.StartsWith("\"") && queryString.EndsWith("\""))
            {
                return "PQ"; //Phrase Query
            }
            else if (queryString.Split(' ').Length > 1)
            {
                return "MWQ"; //Multi Word Query
            }
            else
            {
                return "OWQ"; //One Word Query
            }
        }

        private HashSet<string> handleOWQ(string query)
        {
            Dictionary<string, List<int>> value = new Dictionary<string,List<int>>();
            if (index.TryGetValue(query, out value))
            {
                return new HashSet<string>(value.Keys.ToList());
            }
            else
            {
                return new HashSet<string>();
            }

        }

        private HashSet<string> handleMWQ()
        {
            Dictionary<string, List<int>> value = new Dictionary<string, List<int>>();
            HashSet<string> results = new HashSet<string>();
            List<string> terms = this.tokens();
            foreach (var term in terms)
            {
                if (index.TryGetValue(term, out value))
                {
                    foreach (var item in value.Keys)
                    {
                        results.Add(item);
                    }
                }
            }
            return results;
        }

        
        private HashSet<string> handlePQ()
        {
            List<HashSet<string>> results = new List<HashSet<string>>();
            List<string> terms = this.tokens();
            foreach (var term in terms)
            {
                results.Add(new HashSet<string>(handleOWQ(term)));
            }

            HashSet<string> temp = results[0];
            for (int i = 1; i < results.Count; i++)
            {
                temp.IntersectWith(results[i]);
            }

            return temp;
        }
        

        public List<string> tokens()
        {
            List<string> result = new List<string>();
            List<string> stop_words = System.IO.File.ReadAllLines(
                @"C:\Users\LOLU\Documents\csc322\stop-words.txt").ToList();
            //strip stop words, lemmatize and tokenize.
            if (QueryType() == "PQ"){
                char[] separator = {'\"'};
                return tokenize(queryString.TrimStart(separator).TrimEnd(separator));
            }
            return tokenize(queryString);
        }

        private List<string> tokenize(string text)
        {
            List<string> result = new List<string>();
            List<string> stop_words = System.IO.File.ReadAllLines(
                @"C:\Users\LOLU\Documents\csc322\stop-words.txt").ToList();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] tokens = queryString.Split(delimiterChars);
            foreach (string token in tokens)
            {
                if (!stop_words.Contains(token))
                {
                    result.Add(token);
                }
            }
            return result;
        }

        // returns list of documents matching a query.
        public List<string> Results()
        {
            if (QueryType() == "OWQ")
            {
                return new List<string>(handleOWQ(queryString));
            }
            else if (QueryType() == "MWQ")
            {
                return new List<string>(handleMWQ());
            }
            else
            {
                return new List<string>(handlePQ());
            }
        }

        public List<KeyValuePair<string, double>> RankedResults()
        {
            
            if (QueryType() == "OWQ")
            {
                Ranker ranker = new Ranker(queryString, new List<string>(handleOWQ(queryString)));
                return ranker.RankedResults();
            }
            else if (QueryType() == "MWQ")
            {
                Ranker ranker = new Ranker(queryString, new List<string>(handleMWQ()));
                return ranker.RankedResults();
            }
            else
            {
                Ranker ranker = new Ranker(queryString, new List<string>(handlePQ()));
                return ranker.RankedResults();
            }
        }

        public List<double> GetVector(HashSet<string> corpus)
        {
            List<double> result = new List<double>();
            foreach (string term in corpus)
            {
                if (this.tokens().Contains(term))
                {
                    result.Add(1);
                }
                else
                {
                    result.Add(0);
                }
            }
            return result;
        }
    }
}

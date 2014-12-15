using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class Ranker
    {
        private string queryString;
        private List<string> results;

        public Ranker(string queryString, List<string> results)
        {
            this.queryString = queryString;
            this.results = results;
        }

        public List<KeyValuePair<string, double>> RankedResults()
        {
            List<Document> documents = new List<Document>();
            HashSet<string> dataSet = new HashSet<string>();
            foreach (var result in results)
            {
                Document d = new Document(result);
                documents.Add(d);
                foreach (var term in d.tokens()){
                    dataSet.Add(term);
                }          
            }
            //Build Document Vectors
            Dictionary<string, Vector> documentVectors = new Dictionary<string, Vector>();
            foreach (var document in documents)
            {
                documentVectors.Add(document.ToString(), new Vector(dataSet, document));
            }
            //Build Query Vector
            Query query = new Query(queryString);
            Vector queryVector = new Vector(dataSet, query);

            Dictionary<string, double> relevance = new Dictionary<string, double>();
            foreach (var documentVector in documentVectors)
            {
                relevance.Add(documentVector.Key, Vector.GetSimilarityScore(queryVector, documentVector.Value));
            }
            //Sort result by most relevant     
            List<KeyValuePair<string, double>> myList = relevance.ToList();
            return myList;        
        }  
    }
}


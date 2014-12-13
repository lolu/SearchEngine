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
        private HashSet<string> dataSet;

        public Ranker(string queryString, List<string> results)
        {
            this.queryString = queryString;
            this.results = results;
            dataSet = new HashSet<string>();
        }

        public List<KeyValuePair<string, double>> RankedResults()
        {
            List<Document> documents = new List<Document>();
            foreach (var result in results)
            {
                Document d = new Document(result);
                documents.Add(d);
                foreach (var term in d.tokens()){
                    dataSet.Add(term);
                }          
            }
            Dictionary<string, List<double>> documentVectors = new Dictionary<string, List<double>>();
            foreach (var document in documents)
            {
                documentVectors.Add(document.ToString(), document.GetVector(dataSet));
            }

            Query query = new Query(queryString);
            List<double> queryVector = query.GetVector(dataSet);

            Dictionary<string, double> relevance = new Dictionary<string, double>();

            foreach (var documentVector in documentVectors)
            {
                relevance.Add(documentVector.Key, GetSimilarityScore(queryVector, documentVector.Value));
            }
            
            List<KeyValuePair<string, double>> myList = relevance.ToList();
            myList.ToList().Sort((x, y) => x.Value.CompareTo(y.Value));

            return myList;
             
        }

        //compares two vectors and returns their similarity using cosine similarity model.
        private double GetSimilarityScore(List<double> vectorA, List<double> vectorB)
        {
            double dotProduct = GetDotProduct(vectorA, vectorB);
            return Math.Acos(dotProduct / ((GetVectorNorm(vectorA)) * GetVectorNorm(vectorB)));
        }

        private double GetVectorNorm(List<double> vector)
        {
            double result = 0;
            foreach (var weight in vector){
                result += weight * weight;
            }
            return result;
        }

        private double GetDotProduct(List<double> vectorA, List<double> vectorB)
        {
            double result = 0;
            for (int i = 0; i < vectorA.Count; i++)
            {
                result += vectorA[i] * vectorB[i];
            }
            return result;
        }

        private double square(double weight)
        {
            return weight * weight;
        }
    }
}


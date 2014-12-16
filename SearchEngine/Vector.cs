using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{

    public class Vector
    {
        private HashSet<string> dataSet;
        private List<double> vectorRep;
        public Vector(HashSet<string> dataSet, Document document)
        {
            this.dataSet = dataSet;
            vectorRep = document.GetVector(dataSet);
        }

        public Vector(HashSet<string> dataSet, Query query)
        {
            this.dataSet = dataSet;
            vectorRep = query.GetVector(dataSet);
        }

        public Vector(double[] vectorRep)
        {
            this.vectorRep = new List<double>(vectorRep);
        }

        //compares two vectors and returns their similarity using cosine similarity model.
        public static double GetSimilarityScore(Vector vectorA, Vector vectorB)
        { 
            double dotProduct = GetDotProduct(vectorA, vectorB);
            if (dotProduct == 0)
            {
                return 0;
            }
            else
            {
                return Math.Cos(dotProduct / ((GetVectorNorm(vectorA)) * GetVectorNorm(vectorB)));
            }
        }

        public static double GetVectorNorm(Vector vector)
        {
            double result = 0.0;
            foreach (var weight in vector.vectorRep)
            {
                result += weight * weight;
            }
            return result;
        }

        public static double GetDotProduct(Vector vectorA, Vector vectorB)
        {
            double result = 0.0;
            for (int i = 0; i < vectorA.vectorRep.Count; i++)
            {
                result += vectorA.vectorRep[i] * vectorB.vectorRep[i];
            }
            return result;
        }

        private double square(double weight)
        {
            return weight * weight;
        }
    }
}

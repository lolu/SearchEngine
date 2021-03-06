﻿using HtmlAgilityPack;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class Document
    {
        private string filePath;
        private string type;

        //filePath e.g C:\\Users\\LOLU\\Documents\\CSC322\\doc1
        //type e.g txt
        public Document(string filePath, string type)
        {
            this.filePath = filePath;
            this.type = type;
        }

        public Document(string fullPath)
        {
            int point = fullPath.LastIndexOf(".");
            this.filePath = fullPath.Substring(0, point);
            this.type = fullPath.Substring(point + 1);
        }

        public string FilePath
        {
           get {return filePath; }
           set { filePath = value; }
        }

        public string Type{
            get {return type; }
            set { type = value; }
        }

        //requires: this must not be null
        //effects: returns an array of all the tokens found in a document.
        // A token is any sequence of characters seperated by a space.
        //throws NullPointerException if this is null.
        public List<string> tokens()
        {
            List<string> result = new List<string>();
            List<string> stop_words = System.IO.File.ReadAllLines(
                @"C:\Users\LOLU\Documents\csc322\stop-words.txt").ToList();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] tokens = this.documentText().ToLower().Split(delimiterChars);
            foreach (string token in tokens){
                if (!stop_words.Contains(token)){
                    result.Add(token);
                }
            }
            return result;
        }

        //requires: this must not be null
        //effects: returns a string containing all the text contained in a document
        //
        private string documentText()
        {
            if (type == "pdf")
            {
                return ExtractTextFromPdf(this.ToString());
            }
            else if (type == "html")
            {
                return ExtractTextFromHtml(this.ToString());
            }
            else if (type == "txt")
            {
                string path = filePath + "." + type;
                return System.IO.File.ReadAllText(@path);
            }
            else
            {
                return "";
            }
            
        }

        private static string ExtractTextFromHtml(string path)
        {
            HtmlDocument doc = new HtmlDocument();
            var sb = new StringBuilder();
            doc.LoadHtml(@path);
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//text()"))
            {
                sb.Append(node.InnerText.Trim());
            }
            return sb.ToString();
        }

        private static string ExtractTextFromPdf(string path)
        {
          PDDocument doc = null;
          try {
              doc = PDDocument.load(path);
              PDFTextStripper stripper = new PDFTextStripper();
              return stripper.getText(doc);
          }
          finally {
            if (doc != null) {
              doc.close();
            }
          }
        }  

        public string ToString()
        {
            return this.filePath + "." + this.type;
        }

        public List<double> GetVector(HashSet<string> corpus)
        {
            List<double> result = new List<double>();
            foreach (string term in corpus)
            {
                if (InvertedIndex.TFIndex().ContainsKey(term))
                {
                    if (InvertedIndex.TFIndex()[term].ContainsKey(this.ToString()))
                    {
                        double tf = InvertedIndex.getTF(this.ToString(), term);
                        double idf = InvertedIndex.getIDF(term);
                        result.Add(tf * idf);
                    }
                    else
                    {
                        result.Add(0);
                    }
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

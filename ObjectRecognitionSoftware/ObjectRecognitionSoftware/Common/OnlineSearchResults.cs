// The code below has been taken and refactored from the following website http://blog.zaletskyy.com/how-to-parse-google-search-results
using System.Text;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using NScrape;
using System.Text.RegularExpressions;
using ObjectRecognitionSoftware.Entities;
using System.Collections.Generic;
using ObjectRecognitionSoftware.Views.Controls.ButtonIcons;

namespace ObjectRecognitionSoftware.Common
{
    public static class OnlineSearchResults
    {
        public static List<SearchResultEntity> GetSearchResults (string textSearch)
        {
            var searchResults = new List<SearchResultEntity>();
            var bufferForHtml = new StringBuilder();
            var encodedBytes = new byte[8192];
            var urlForSearch = "http://google.com/search?q=" + textSearch;
            var request = (HttpWebRequest)System.Net.WebRequest.Create(urlForSearch);
            var response = (HttpWebResponse)request.GetResponse();

            using (Stream responseFromGoogle = response.GetResponseStream())
            {
                var enc = response.GetEncoding();

                int count = 0;
                do
                {
                    count = responseFromGoogle.Read(encodedBytes, 0, encodedBytes.Length);
                    if (count != 0)
                    {
                        var tempString = enc.GetString(encodedBytes, 0, count);
                        bufferForHtml.Append(tempString);
                    }
                }

                while (count > 0);
            }
            string sbb = bufferForHtml.ToString();

            var processedHtml = new HtmlDocument
            {
                OptionOutputAsXml = true
            };
            processedHtml.LoadHtml(sbb);
            var doc = processedHtml.DocumentNode;

            foreach (var link in doc.SelectNodes("//a[@href]"))
            {
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                if (!hrefValue.ToUpper().Contains("GOOGLE")
                    && hrefValue.Contains("/url?q=")
                    && hrefValue.ToUpper().Contains("HTTP"))

                {
                    int index = hrefValue.IndexOf("&");
                    if (index > 0)
                    {
                        hrefValue = hrefValue.Substring(0, index);
                        var hyperlink = hrefValue.Replace("/url?q=", string.Empty);
                        var title = Regex.Replace(link.InnerText, "&quot;\\.?", string.Empty);

                        if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(title))
                        {
                            searchResults.Add(new SearchResultEntity
                            {
                                Heading = title,
                                Description = "Google Search Result",
                                Icon = new WebIcon(),
                                Url = hyperlink
                            });
                        }                        
                    }
                }
            }
            return searchResults;
        }
    }
}

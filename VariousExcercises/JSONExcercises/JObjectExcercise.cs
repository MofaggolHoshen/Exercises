using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JSONExcercises
{
    [TestClass]
    public class JObjectExcercise
    {
        [TestMethod]
        public void StringToJObject()
        {
            string json = @"{
                              'channel': {
                                'title': 'James Newton-King',
                                'link': 'http://james.newtonking.com',
                                'description': 'James Newton-King\'s blog.',
                                'item': [
                                  {
                                    'title': 'Json.NET 1.3 + New license + Now on CodePlex',
                                    'description': 'Announcing the release of Json.NET 1.3, the MIT license and the source on CodePlex',
                                    'link': 'http://james.newtonking.com/projects/json-net.aspx',
                                    'categories': [
                                      'Json.NET',
                                      'CodePlex'
                                    ]
                                  },
                                  {
                                    'title': 'LINQ to JSON beta',
                                    'description': 'Announcing LINQ to JSON',
                                    'link': 'http://james.newtonking.com/projects/json-net.aspx',
                                    'categories': [
                                      'Json.NET',
                                      'LINQ'
                                    ]
                                  }
                                ]
                              }
                            }";

            JObject rss = JObject.Parse(json);

            rss.Add("obj", "I am obj");

            string obj = (string)rss["obj"];


            string rssTitle = (string)rss["channel"]["title"];
            // James Newton-King

            string itemTitle = (string)rss["channel"]["item"][0]["title"];
            // Json.NET 1.3 + New license + Now on CodePlex

            JArray categories = (JArray)rss["channel"]["item"][0]["categories"];
            // ["Json.NET", "CodePlex"]

            IList<string> categoriesText = categories.Select(c => (string)c).ToList();
            // Json.NET
            // CodePlex
        }

        [TestMethod]
        public void JObjectToString()
        {

            //var temap = new
            //{
            //    riskCategoryTemplate = new
            //    {
            //        Name = "Template for Micro # Weight = 1",
            //        ErrorMessage = "Here is the error",
            //        riskSubCategoryTemplates = new[]
            //        {
            //       new {
            //           Name = "kk1",
            //           ErrorMessage = "Here is the Error",
            //           indicator = new[]
            //           {
            //               new {Name = "indicator1", ErrorMessage = "Herer error message"}
            //           }
            //       },
            //       new {
            //           Name = "kk1",
            //           ErrorMessage = "Here is the Error",
            //           indicator = new[]
            //           {
            //               new {Name = "indicator1", ErrorMessage = "Herer error message"}
            //           }
            //        }    

            //       }
            //    }
            //};

            var temap = new
            {
                riskCategoryTemplate = new
                {
                    Name = "Template for Micro # Weight = 1",
                    ErrorMessage = "Here is the error",
                    riskSubCategoryTemplates = new List<object>()
                }
            };

            temap.riskCategoryTemplate.riskSubCategoryTemplates.Add(new
            {
                Name = "kk1",
                ErrorMessage = "Here is the Error"
            });


            var str = JsonConvert.SerializeObject(temap);

            var jobj = JsonConvert.DeserializeObject<JObject>(str);

            var v = (string)jobj["riskCategoryTemplate"]["Name"];
        }

        [TestMethod]
        public void CreateJObject()
        {
           
            JObject jObject = new JObject();

            jObject.Add(new JProperty("riskCategory", new JObject(
                new JProperty("Name", "Risk Category"),
                new JProperty("ErrorMessage", "I am error message")
                )));

            var k = jObject["riskCategory"]["Name"] as JObject;

            k.AddAfterSelf(new JProperty("ristSubIndicators",
                new JArray(new JObject(
                new JProperty("Name", "Risk Category"),
                new JProperty("ErrorMessage", "I am error message")
                ))));
            //jObject.Add("json2", "name");
            //jObject.Add("json3", "name");
            //jObject.Add("json4", "name");


            var str =(string)jObject["riskCategory"]["Name"];

            var st = jObject.ToString();
        }
    }
}

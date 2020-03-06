using System;
using RestSharp.Deserializers;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System.Net;

namespace RestSharp
{
    [TestFixture]
    public class UnitTest1
    {     
        [Test]
        public void CreatePet()
        {
            var client = new RestClient("https://petstore.swagger.io/");

            var request = new RestRequest("v2/pet", Method.POST);
            request.AddJsonBody(new { name = "Fafik", id = "100" });

            var response = client.Execute(request);

            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            var status = output["name"];

            Assert.That(status, Is.EqualTo("Fafik"), "Nieprawidłowy status");
        }

        [Test]
        public void GetPet()
        {
            var client = new RestClient("https://petstore.swagger.io/");

            var request = new RestRequest("v2/pet/{petid}", Method.GET);
            request.AddUrlSegment("petid", 100);

            var response = client.Execute(request);
           
            JObject obs = JObject.Parse(response.Content);

            Assert.That(obs["name"].ToString(), Is.EqualTo("Fafik"), "Nieprawidłowa nazwa");
        }

        [Test]
        public void UpdatePet()
        {
            var client = new RestClient("https://petstore.swagger.io/");

            var request = new RestRequest("v2/pet", Method.PUT);
            request.AddJsonBody(new { name = "Fafor", id = "100" });

            var response = client.Execute(request);

            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            var status = output["name"];

            Assert.That(status, Is.EqualTo("Fafor"), "Nieprawidłowy status");
        }

        [Test]
        public void DeletePet()
        {
            var client = new RestClient("https://petstore.swagger.io/");

            var request = new RestRequest("v2/pet/{petid}", Method.DELETE);
            request.AddUrlSegment("petid", 100);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

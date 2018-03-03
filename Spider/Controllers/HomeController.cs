using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spider.Models;
using HtmlAgilityPack;

namespace Spider.Controllers
{
    public class HomeController : Controller
    {
        public List <Scrap> Links = new List<Scrap>();
        public int ID;
        public int Rank;


        public IActionResult Index()
        {
            GetHtml(@"https://hirespace.com/",1);
            int rows = Links.Count;
            Debug.WriteLine("************************"+ rows +"******************************");
            for (var i = 2; i < rows; i++)
            {
                var id = Links.FirstOrDefault(c => c.Id == i);
                if(id != null) {
                    GetHtml(id.Url,2);
                    Debug.WriteLine("**********" + id.Url + id.Id +"**************************");
                }
              
            }

            var model = Links;

            return View(model);
        }


        public void GetHtml(string html,int rank)
        {
            //html = @"https://hirespace.com/";

            var web = new HtmlWeb();
            var htmlDoc = web.Load(html);       

            var nodes = htmlDoc.DocumentNode.Descendants("a");
   
            Scrap obj;
          
            foreach (var n in nodes)
            {    
                var url = n.GetAttributeValue("href", "");
                if (url.Length != 0 && url.StartsWith("/") || url.StartsWith("https://hirespace.com"))
                { 
                    if (url.StartsWith("/"))
                    {
                        url = "https://hirespace.com" + url;
                    }
                
                    if (!Links.Any(cus => cus.Url == url))
                    {
                        ID += 1;
                        obj = new Scrap();
                        obj.Url = url;
                        obj.Rank = rank;
                        obj.Count += 1;
                        obj.Id =ID;
                        Links.Add(obj);
                    }
                }           
            }
        }                   
    }     
}

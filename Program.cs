using HttpRequestService.Models;
using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace HttpRequestService;

    class Program
    {

    static void Main(string[] args)
    {
    
        //Test the HttpRequestHelper class functions

        string baseUri = "https://jsonplaceholder.typicode.com";

        HttpRequestHelper helper = new HttpRequestHelper(baseUri);

        List<jsonPlaceolderModel> test = helper.GET<jsonPlaceolderModel>("/posts", null,new Dictionary<string, string> { {"id","1" } });

        Console.WriteLine(test.Count);

        Dictionary<string, string> DefaultHeader = new Dictionary<string, string>() { {"accept","*/*" } };

        test = helper.GET<jsonPlaceolderModel>("/posts", null,DefaultHeader);

        Console.WriteLine(test.Count);

        jsonPlaceolderModel x = new jsonPlaceolderModel();

        x.title = "Test";
        x.id = 101;
        x.userId = 1;
        x.body = "Random text for testing";

        example example = new example();

        example.id = "1";
        example.title = "Test";
        example.views = 44;

        List<jsonPlaceolderModel> result = helper.PUT<jsonPlaceolderModel, jsonPlaceolderModel>("/posts/1",x, null,null);

        foreach(jsonPlaceolderModel t in result)
        {
            Console.WriteLine(t.id+" : "+t.body);
        }

        bool fin = helper.DELETE("/comments/2", DefaultHeader);

        Console.WriteLine(fin);

        List<jsonPlaceolderModel> Postresult = helper.POST<jsonPlaceolderModel, jsonPlaceolderModel>("/posts",x, null,null);

        foreach (jsonPlaceolderModel t in Postresult)
        {
            Console.WriteLine(t.id + " : " + t.body);
        }

        List<jsonPlaceolderModel> Patchresult = helper.PATCH<jsonPlaceolderModel, jsonPlaceolderModel>("/posts/1", x, null,null);

        foreach (jsonPlaceolderModel t in Patchresult)
        {
            Console.WriteLine(t.id + " : " + t.body);
        }



    }
}


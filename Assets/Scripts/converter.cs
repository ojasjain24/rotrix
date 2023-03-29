// using System;
// using Newtonsoft.Json;
// using System.Collections.Generic;

// public class Landmark
// {
//     public double x { get; set; }
//     public double y { get; set; }
//     public double z { get; set; }
// }

// public class RootObject
// {
//     public List<Landmark> landmark { get; set; }
// }

// public class jsonUtils
// {
//     public static void getJson(string jsonString)
//     {
//         jsonString = jsonString.Replace("landmark", "\"landmark\"");
//         jsonString = jsonString.Replace("x:", "\"x\":");
//         jsonString = jsonString.Replace("y:", "\"y\":");
//         jsonString = jsonString.Replace("z:", "\"z\":");

//         List<Landmark> root = JsonConvert.DeserializeObject<List<Landmark>>(jsonString);
//         //  rootObjects = root.landmark;

//         Console.WriteLine(root);
//     }
// }

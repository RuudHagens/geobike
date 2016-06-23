using UnityEngine;
using System.Collections;

public static class StaticObjects
{
    public static Dijkstra dijkstraInstance { get; set;}
    public static string startPoint { get; set; }
    public static string endPoint { get; set; }
    public static bool enableCityNames { get; set; }
}

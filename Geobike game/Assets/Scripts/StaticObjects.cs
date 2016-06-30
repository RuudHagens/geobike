using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

public static class StaticObjects
{
    public static Dijkstra dijkstraInstance { get; set;}
    public static string startPoint { get; set; }
    public static string endPoint { get; set; }
    public static string startProvince { get; set; }
    public static bool enableCityNames { get; set; }
    public static int winningPlayer { get; set; }
    public static List<string> visitedLocationsPlayer1 = new List<string>();
    public static List<string> visitedLocationsPlayer2 = new List<string>();

    public static float secondsleftPlayer1 { get; set; }
    public static float minutesleftPlayer1 { get; set; }
    public static float secondsleftPlayer2 { get; set; }
    public static float minutesleftPlayer2 { get; set; }
    public static bool clickSound { get; set; }
}

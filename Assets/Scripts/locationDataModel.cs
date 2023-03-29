using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Location
{
    public string x;
    public int y;

    public static Location CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Location>(jsonString);
    }
}

public class Landmark
{
    public List<Location> landmark;

    public static Landmark CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Landmark>(jsonString);
    }

}
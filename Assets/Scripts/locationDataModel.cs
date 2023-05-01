using UnityEngine;
using System.Collections.Generic;

[System.Serializable]

// For future Purpose : Model for xyz coordinates of hand. 

public class Location
{
    public string x;
    public string y;
    public string z;

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
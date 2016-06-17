using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Model
{
    public string modelName;
    public List<JsonObject> modelObjects;

    public Model()
    {
        modelName = "Model";
        modelObjects = new List<JsonObject>();
    }
}

[Serializable]
public class JsonObject
{
    public Int32 ID;
    public string objectType;
    public string name;
    public string description;
    public Coordinates coordinates;
    public List<Proprty> properties;

    public JsonObject(int id)
    {
        ID = id;
        objectType = this.GetType().ToString();
        name = "Base empty";
        description = "Base object for all other";
        coordinates = new Coordinates();
        properties = new List<Proprty>();
    }

}

[Serializable]
public class Pipe : JsonObject
{
    public Pipe(int id) : base(id)
    {
        properties = new List<Proprty>();
        Proprty diametr = new Proprty { name = "diametr", value = "1" };
        Proprty marka = new Proprty { name = "marka", value = "180" };
        properties.Add(diametr);
        properties.Add(marka);

        objectType = GetType().ToString();
        name = "Pipe 1";
        description = "Pipe description";
    }
}

[Serializable]
public class Manometr : JsonObject
{
    public Manometr(int id) : base(id)
    {
        properties = new List<Proprty>();
        Proprty maxPressure = new Proprty { name = "maxPressure", value = "100" };
        properties.Add(maxPressure);

        objectType = GetType().ToString();
        name = "Manometr 1";
        description = "Manometr description";
    }

}

[Serializable]
public class Proprty
{
    public string name;
    public string value;
}

[Serializable]
public class Coordinates
{
    public float X;
    public float Y;
    public float Z;

    public float endX;
    public float endY;
    public float endZ;

    public Coordinates()
    {
        X = 0;
        Y = 0;
        Z = 0;

        endX = 0;
        endY = 0;
        endZ = 0;
    }

    public Vector3 StartPos()
    {
        return new Vector3(X, Y, Z);
    }

    public Vector3 EndPos()
    {
        return new Vector3(endX, endY, endZ);
    }

}


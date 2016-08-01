using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class VoidLayer{

    public string name;
    public Material material;
    public bool isVisible = true;
    public List<VoidTopology> objects = new List<VoidTopology>();

    public void Register(ref VoidTopology obj) {
        if (!objects.Contains(obj)) {
            objects.Add(obj);
        }
    }

    public void Remove(ref VoidTopology obj) {
        if (objects.Contains(obj)) {
            objects.Remove(obj);
        }
    }

}

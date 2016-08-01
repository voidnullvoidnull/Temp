using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class VoidLayer{

    public string name;
    public bool visible = true;
    public Material material;
    public List<GraphicRaw> layerObjects = new List<GraphicRaw>();
    
    public void Register(GraphicRaw subject) {
        layerObjects.Add(subject);
    }

    public void UnRegister(GraphicRaw subject) {
        layerObjects.Remove(subject);
    }
}

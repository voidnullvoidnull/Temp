using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Геометрическое отображение сварного шва
/// </summary>
[System.Serializable]
public class GraphicSeam : GraphicRaw {

    public float radius;
    public float width;
    public int subdiv = 8;

    public override void Recalculate() {
        var mesh = new GraphicBand(radius, radius, new Vector3(0, 0, -width), new Vector3(0, 0, width), Vector3.zero, Vector3.zero, subdiv);
        points = mesh.points;
        quads = mesh.quads;
        }
    }

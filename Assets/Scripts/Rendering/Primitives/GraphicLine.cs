using UnityEngine;
using System.Collections;

/// <summary>
/// Геометрическое отображение прямолинейного объекта
/// </summary>
[System.Serializable]
public class GraphicLine : GraphicRaw {
    public float radius;
    public float lenght;
    public int subdiv = 8;

    public override void Recalculate() {
        var mesh = new GraphicBand(radius, radius, Vector3.zero, new Vector3(0, 0, lenght), Vector3.zero, Vector3.zero, subdiv);
        points = mesh.points;
        quads = mesh.quads;
        }
    }

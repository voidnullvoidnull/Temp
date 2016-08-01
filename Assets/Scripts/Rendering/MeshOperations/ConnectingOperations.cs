using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Статический класс-библиотека методов, реализующих операции с геометрическими объектами
/// </summary>

public static partial class MeshOperations{

    public static int[] BridgeLine(int lineLenght) {

        List<int> quads = new List<int>();
        for (int i = 0; i < lineLenght - 1; i++) {
            quads.Add(i);
            quads.Add(i + lineLenght);
            quads.Add(i + lineLenght + 1);
            quads.Add(i + 1);
            }

        return quads.ToArray();
        }

    public static int[] BridgeCircle(int circleSegments) {

        List<int> quads = new List<int>();
        for (int i = 0; i < circleSegments - 1; i++) {
            quads.Add(i);
            quads.Add(i + circleSegments);
            quads.Add(i + circleSegments + 1);
            quads.Add(i + 1);
            }

        quads.Add(circleSegments - 1);
        quads.Add(circleSegments * 2 - 1);
        quads.Add(circleSegments);
        quads.Add(0);

        return quads.ToArray();
        }

    public static GraphicRaw GetBand(Vector3[] circle1, Vector3[] circle2) {
        List<Vector3> verts = new List<Vector3>(circle1);
        verts.AddRange(circle2);
        List<int> quads = new List<int>(MeshOperations.BridgeCircle(circle1.Length));
        return new GraphicRaw(verts.ToArray(), quads.ToArray());
        }

    }

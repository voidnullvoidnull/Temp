using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public struct VoidTopology {

    public Vector3[] points;
    public int[] quads;
    public Matrix4x4 matrix;

    public static VoidTopology operator + (VoidTopology first, VoidTopology second) {

        int indexShift = first.points.Length;
        List<Vector3> resultPoints = new List<Vector3>(first.points);
        resultPoints.AddRange(second.points);

        List<int> secondShiftedQuads = new List<int>();
        foreach(int p in second.quads) {
            secondShiftedQuads.Add(p+indexShift);
        }
        List<int> resultQuads = new List<int>(first.quads);
        resultQuads.AddRange(secondShiftedQuads);

        return new VoidTopology { points = resultPoints.ToArray(), quads = resultQuads.ToArray()};
    }
}

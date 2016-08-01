using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Реализует представление "сырых" данных модели в виде массива координат точек и массива индексов вершин, образующих фейсы
/// </summary>
[System.Serializable]
public class GraphicRaw {

    public Vector3[] points;
    public int[] quads;


    public virtual void Recalculate() { }


    public GraphicRaw() {
        points = new Vector3[] { Vector3.zero };
        quads = new int[] { 0 };
        }

    public GraphicRaw(Vector3[] verts, int[] faces) {
        points = verts;
        quads = faces;
        }

    public static GraphicRaw operator +(GraphicRaw first, GraphicRaw second) {

        int indexShift = first.points.Length;
        List<Vector3> resultPoints = new List<Vector3>(first.points);
        resultPoints.AddRange(second.points);

        List<int> secondShiftedQuads = new List<int>();
        foreach (int p in second.quads) {
            secondShiftedQuads.Add(p + indexShift);
            }
        List<int> resultQuads = new List<int>(first.quads);
        resultQuads.AddRange(secondShiftedQuads);

        return new GraphicRaw { points = resultPoints.ToArray(), quads = resultQuads.ToArray() };
        }

    public void SetTransform(Matrix4x4 matrix) {
        Vector3[] newPoints = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++) {
            newPoints[i] = matrix.MultiplyPoint(points[i]);
            }
        points = newPoints;
        }

    public void SetTransform(Vector3 position, Vector3 rotation, Vector3 scale) {
        Matrix4x4 matrix = Matrix4x4.TRS(position, Quaternion.Euler(rotation), scale);
        SetTransform(matrix);
        }
    }

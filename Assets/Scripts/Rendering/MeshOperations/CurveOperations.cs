using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static partial class MeshOperations {

    public static Vector3[] GetBezierPoints(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 shift, Vector3 rotation, int subdiv) {

        Matrix4x4 matrix = Matrix4x4.TRS(shift, Quaternion.Euler(rotation), Vector3.one);
        float step = 1f / subdiv;
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < subdiv+1; i++) {
            float t = step * i;
            Vector3 h1 = Vector3.Lerp(p0, p1, t);
            Vector3 h2 = Vector3.Lerp(p1, p2, t);
            Vector3 h3 = Vector3.Lerp(p2, p3, t);
            Vector3 h4 = Vector3.Lerp(h1, h2, t);
            Vector3 h5 = Vector3.Lerp(h2, h3, t);
            points.Add(matrix.MultiplyPoint(Vector3.Lerp(h4, h5, t)));
            }

        return points.ToArray();
        }

    public static Vector3[] GetDirectionsOnLine(Vector3[] points) {
        List<Vector3> directions = new List<Vector3>();
        for (int i = 0; i < points.Length - 1; i++) {
            Vector3 from = points[i + 1] - points[i];
            directions.Add(from.normalized);
            }
        directions.Add(directions[directions.Count - 1]);
        return directions.ToArray();
        }
    
    public static GraphicRaw CircleAlongLine(Vector3[] line, int divisions, float radius) {
        return CircleAlongLine(line, divisions, radius, radius, Vector3.up);
        }

    public static GraphicRaw CircleAlongLine(Vector3[] line, int divisions, float startRadius, float endRadius, Vector3 up) {

        List<Vector3[]> circles = new List<Vector3[]>();
        Vector3[] directions = MeshOperations.GetDirectionsOnLine(line);

        for (int i = 0; i < line.Length; i++) {
            Quaternion rot = Quaternion.LookRotation(directions[i], up);
            float t = 1f / line.Length;
            float radius = Mathf.Lerp(startRadius, endRadius, t * i);
            circles.Add(MeshOperations.GetCirclePoints(radius, divisions, line[i], rot.eulerAngles));
            }

        GraphicRaw mesh = GetBand(circles[0], circles[1]);
        for (int i = 1; i < line.Length - 1; i++) {
            GraphicRaw band = GetBand(circles[i], circles[i + 1]);
            mesh = mesh + band;
            }

        return mesh;
        }
    }

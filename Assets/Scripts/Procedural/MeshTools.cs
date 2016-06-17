using UnityEngine;
using System.Collections.Generic;


public static class MeshTools{

    public static List<Vector3> PointsOnCircle(int segments, float radius, Matrix4x4 matrix)
    {
        List<Vector3> points = new List<Vector3>();
        float segmentAngle = Mathf.PI * 2 / segments;
        float angle = 0f;
        for (int i = 0; i < segments; i++)
        {
            Vector3 point = new Vector3(radius * Mathf.Sin(angle), radius * Mathf.Cos(angle));
            point = matrix.MultiplyPoint3x4(point);

            points.Add(point);
            angle += segmentAngle;
        }

     return points;
    }

    public static List<Vector3> PointsOnCircle(int segments, float radius, Vector3 shift)
    {
        List<Vector3> points = new List<Vector3>();
        float segmentAngle = Mathf.PI * 2 / segments;
        float angle = 0f;
        for (int i = 0; i < segments; i++)
        {
            Vector3 point = new Vector3(radius * Mathf.Sin(angle), radius * Mathf.Cos(angle))+shift;
            points.Add(point);
            angle += segmentAngle;
        }
        return points;
    }

    public static List<Vector3> Arc (float raduis, float fill, int segments)
    {
        Quaternion rot = Quaternion.Euler(new Vector3(-90, 0, 0));
        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rot, Vector3.one);
        List<Vector3> arc = new List<Vector3>();
        float segmentAngle = (Mathf.PI * 2 * fill) / segments;

        float angle = 0;
        for (int i =0; i < segments; i++)
        {
            Vector3 point = new Vector3( raduis*Mathf.Sin(angle), raduis*Mathf.Cos(angle));
            point = matrix.MultiplyPoint3x4(point);
            arc.Add(point);
            angle += segmentAngle;
        }

        return arc;
    }

    public static Mesh SpinedPipe(float majorRadius, float minorRadius, float fill, int majorSegments, int minorSegments)
    {
        Mesh spined = new Mesh();
        List<CombineInstance> instances = new List<CombineInstance>();
        List<Vector3> major = Arc(majorRadius, fill, majorSegments);
        float angle = 90f;
        float segmentAngle = 360*fill/majorSegments;
        Vector3 shift = new Vector3(0, 0, majorRadius);

        Matrix4x4 stM = new Matrix4x4();
        Matrix4x4 enM = new Matrix4x4();

        for (int i = 0; i < majorSegments-1; i++)
        {
            Quaternion sRot = Quaternion.Euler(0, angle,0);
            Quaternion eRot = Quaternion.Euler(0, angle-segmentAngle,0);

            stM = Matrix4x4.TRS(major[i]+shift, sRot, Vector3.one);
            enM = Matrix4x4.TRS(major[i+1]+shift, eRot, Vector3.one);

            var start = PointsOnCircle(minorSegments, minorRadius, stM);
            var end = PointsOnCircle(minorSegments, minorRadius, enM);


            CombineInstance segment = new CombineInstance(); 
            segment.mesh = Band(start, end);
            instances.Add(segment);
            angle -= segmentAngle;
        }
        spined.CombineMeshes(instances.ToArray(), true,false);
        spined.RecalculateBounds();
        return spined;
    }

    /*
    public static Mesh BezierTube(Vector3 end, float hardness, int majorSegments, int minorSegments, float radius)
    {
        Mesh tube = new Mesh();
        Vector3[] path = Handles.MakeBezierPoints(Vector3.zero, end, Vector3.forward*hardness, end+Vector3.back*hardness, majorSegments);
        List<CombineInstance> instances = new List<CombineInstance>();

        for (int i = 0; i<path.Length-1; i++)
        {
            CombineInstance instance = new CombineInstance();
            Matrix4x4 stM = Matrix4x4.TRS(path[i], Quaternion.identity, Vector3.one);
            Matrix4x4 enM = Matrix4x4.TRS(path[i+1], Quaternion.identity, Vector3.one);

            var startCircle = PointsOnCircle(minorSegments, radius, stM);
            var endCircle = PointsOnCircle(minorSegments, radius, enM);

            instance.mesh = Band(startCircle, endCircle);
            instances.Add(instance);
        }
        tube.CombineMeshes(instances.ToArray(), true, false);
        tube.RecalculateBounds();
        return tube;
    }
    */

    public static Mesh Pipe(Vector3 end, float radius, int segments)
    {
        Quaternion circleRot = Quaternion.LookRotation(end);
        Matrix4x4 startMatrix = Matrix4x4.TRS(Vector3.zero, circleRot, Vector3.one);
        Matrix4x4 endMatrix = Matrix4x4.TRS(end, circleRot, Vector3.one);

        List<Vector3> startCircle = PointsOnCircle(segments, radius, startMatrix);
        List<Vector3> endCircle = PointsOnCircle(segments, radius, endMatrix);

        Mesh pipe = Band(startCircle, endCircle);
        pipe.RecalculateBounds();
        return pipe;
    }

    public static Mesh PipeWithoutRotation(float lenght, float radius, int segments)
    {
        List<Vector3> startCircle = PointsOnCircle(segments, radius, Vector3.zero);
        List<Vector3> endCircle = PointsOnCircle(segments, radius, new Vector3(0,0,lenght));

        Mesh pipe = Band(startCircle, endCircle);
        pipe.RecalculateBounds();
        return pipe;
    }

    public static Mesh Band(List<Vector3> lowerRing, List<Vector3> upperRing)
    {
        Mesh mesh = new Mesh();
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();

        List<Vector3> lowerNormals = new List<Vector3>();
        List<Vector3> upperNormals = new List<Vector3>();
        List<Vector2> lowerUv = new List<Vector2>();
        List<Vector2> upperUv = new List<Vector2>();

        verts.AddRange(lowerRing);
        verts.AddRange(upperRing);
        mesh.vertices = verts.ToArray();

        int i0, i1, i2, i3;
        Vector3 v0, v1, v2, v3;
        for (int i = 0; i < lowerRing.Count - 1; i++)
        {
            i0 = i;
            i1 = i + lowerRing.Count;
            i2 = i + 1;
            i3 = i + 1 + lowerRing.Count;
            v0 = mesh.vertices[i0];
            v1 = mesh.vertices[i1];
            v2 = mesh.vertices[i2];
            v3 = mesh.vertices[i3];
            tris.AddRange(new[] { i0, i1, i2 });
            tris.AddRange(new[] { i2, i1, i3 });

            lowerNormals.Add(Vector3.Cross(v1 - v0, v2 - v0).normalized);
            upperNormals.Add(Vector3.Cross(v3 - v1, v0 - v1).normalized);

            var u = (float)i / (lowerRing.Count - 1);
            lowerUv.Add(new Vector2(u, 0));
            upperUv.Add(new Vector2(u, 1));
        }

        i0 = lowerRing.Count - 1;
        i1 = lowerRing.Count * 2 - 1;
        i2 = 0;
        i3 = lowerRing.Count;
        v0 = mesh.vertices[i0];
        v1 = mesh.vertices[i1];
        v2 = mesh.vertices[i2];
        v3 = mesh.vertices[i3];
        tris.AddRange(new[] { i0, i1, i2 });
        tris.AddRange(new[] { i2, i1, i3 });

        lowerNormals.Add(Vector3.Cross(v1 - v0, v2 - v0).normalized);
        upperNormals.Add(Vector3.Cross(v3 - v1, v0 - v1).normalized);
        normals.AddRange(lowerNormals);
        normals.AddRange(upperNormals);

        lowerUv.Add(new Vector2(1, 0));
        upperUv.Add(new Vector2(1, 1));
        uvs.AddRange(lowerUv);
        uvs.AddRange(upperUv);

        mesh.triangles = tris.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uvs.ToArray();
        return mesh;
    }
}

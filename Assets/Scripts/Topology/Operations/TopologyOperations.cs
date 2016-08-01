using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TopologyOperations {


    public static Vector3[] GetCirclePoints(float radius, int subdiv, Vector3 pos, Vector3 rot) {

        Matrix4x4 m = Matrix4x4.TRS(pos, Quaternion.Euler(rot), Vector3.one);

        float angle = (Mathf.PI*2)/subdiv;
        Vector3[] points = new Vector3[subdiv];
        for (int i=0; i< subdiv; i++) {
            float x = Mathf.Sin(angle*i)*radius;
            float y = Mathf.Cos(angle*i)*radius;
            points[i]=m.MultiplyPoint(new Vector3(x,y));
        }
        return points;
    }

    public static int[] BridgeLine(int lineLenght) {

        List<int> quads = new List<int>();
        for (int i=0; i< lineLenght-1; i++) {
            quads.Add(i);
            quads.Add(i+lineLenght);
            quads.Add(i+lineLenght+1);
            quads.Add(i+1);
        }

        return quads.ToArray();
    }

    public static int[] BridgeCircle(int circleSegments) {

        List<int> quads = new List<int>();
        for (int i = 0; i<circleSegments-1; i++) {
            quads.Add(i);
            quads.Add(i+circleSegments);
            quads.Add(i+circleSegments+1);
            quads.Add(i+1);
        }

        quads.Add(circleSegments-1);
        quads.Add(circleSegments*2-1);
        quads.Add(circleSegments);
        quads.Add(0);

        return quads.ToArray();
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BandTopology :VoidTopology {

    public int subdiv { get; private set; }


    public BandTopology(float radius1, float radius2, Vector3 pos1, Vector3 pos2, Vector3 rot1, Vector3 rot2, int segments) {
        subdiv = segments;
        Create(radius1, radius2, pos1, pos2,rot1, rot2);
    }


    public void Create(float radius1, float radius2, Vector3 pos1, Vector3 pos2, Vector3 rot1, Vector3 rot2) {
        Vector3[] circle1 = TopologyOperations.GetCirclePoints(radius1, subdiv, pos1, rot1);
        Vector3[] circle2 = TopologyOperations.GetCirclePoints(radius2, subdiv, pos2, rot2);

        List<Vector3> pnts = new List<Vector3>(circle1);
        pnts.AddRange(circle2);
        int[] qds = TopologyOperations.BridgeCircle(subdiv);

        points=pnts.ToArray();
        quads=qds;     
    }

    public void AddCircle(float radius, Vector3 pos, Vector3 rot) {
        Vector3[] circle = TopologyOperations.GetCirclePoints(radius, subdiv, pos,rot);
        int quadsShift = points.Length;
        int[] addQuads = TopologyOperations.BridgeCircle(subdiv);

        List<int> newQuads = new List<int>(quads);
        foreach (int p in addQuads) {
            newQuads.Add(p+points.Length-subdiv);
        }
        List<Vector3> newPoints = new List<Vector3>(points);
        newPoints.AddRange(circle);

        points=newPoints.ToArray();
        quads=newQuads.ToArray();


    }

}

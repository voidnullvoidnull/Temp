using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphicBand : GraphicRaw {

    public int subdiv { get; private set; }

    public GraphicBand(float radius1, float radius2, Vector3 pos1, Vector3 pos2, Vector3 rot1, Vector3 rot2, int segments) {
        subdiv = segments;
        Create(radius1, radius2, pos1, pos2, rot1, rot2);
        }


    public void Create(float radius1, float radius2, Vector3 pos1, Vector3 pos2, Vector3 rot1, Vector3 rot2) {
        Vector3[] circle1 =  MeshOperations.GetCirclePoints(radius1, subdiv, pos1, rot1);
        Vector3[] circle2 = MeshOperations.GetCirclePoints(radius2, subdiv, pos2, rot2);

        List<Vector3> pnts = new List<Vector3>(circle1);
        pnts.AddRange(circle2);
        GraphicRaw topology = MeshOperations.GetBand(circle1, circle2);
        points = topology.points;
        quads = topology.quads;
        }

    public void AddCircle(float radius, Vector3 pos, Vector3 rot) {
        Vector3[] circle = MeshOperations.GetCirclePoints(radius, subdiv, pos, rot);
        int quadsShift = points.Length;
        int[] addQuads = MeshOperations.BridgeCircle(subdiv);

        List<int> newQuads = new List<int>(quads);
        foreach (int p in addQuads) {
            newQuads.Add(p + points.Length - subdiv);
            }
        List<Vector3> newPoints = new List<Vector3>(points);
        newPoints.AddRange(circle);

        points = newPoints.ToArray();
        quads = newQuads.ToArray();
        }

    }

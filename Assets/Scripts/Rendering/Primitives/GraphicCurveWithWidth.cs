using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Геометрическое отображение кубическое еривой безье линия которой образуем ось трубы с заданным радиусом
/// </summary>
[System.Serializable]
public class GraphicCurveWithWidth : GraphicRaw {

    public Vector3 p0;
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;

    public float startRadius = 0.1f;
    public float endRadius = 0.1f;
    public int majorSegments = 32;
    public int minorSegments = 8;

    public override void Recalculate() {
        Vector3 n1 = p1 - p2;
        Vector3 n2 = p3 - p2;
        Vector3 up = Vector3.Cross(n2, n1);
        var line = MeshOperations.GetBezierPoints(p0, p1, p2, p3, Vector3.zero, Vector3.zero, majorSegments);
        var mesh = MeshOperations.CircleAlongLine(line, minorSegments, startRadius, endRadius, up);
        points = mesh.points;
        quads = mesh.quads;
        }
    }

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Статический класс-библиотека методов, реализующих операции с геометрическими объектами
/// </summary>
public static partial class MeshOperations {


    public static Vector3[] GetCirclePoints(float radius, int subdiv) {
        return GetCirclePoints(radius, subdiv, Vector3.zero, Vector3.zero);
        }

    public static Vector3[] GetCirclePoints(float radius, int subdiv, Vector3 shift) {
        return GetCirclePoints(radius, subdiv, shift, Vector3.zero);
        }

    public static Vector3[] GetCirclePoints(float radius, int subdiv, Vector3 pos, Vector3 rot) {

        Matrix4x4 m = Matrix4x4.TRS(pos, Quaternion.Euler(rot), Vector3.one);

        float angle = (Mathf.PI * 2) / subdiv;
        Vector3[] points = new Vector3[subdiv];
        for (int i = 0; i < subdiv; i++) {
            float x = Mathf.Sin(angle * i) * radius;
            float y = Mathf.Cos(angle * i) * radius;
            points[i] = m.MultiplyPoint(new Vector3(x, y));
            }
        return points;
        }

    }

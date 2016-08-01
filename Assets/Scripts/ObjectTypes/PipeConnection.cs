using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Реализует отоброжение соединения двух труб криволиненым участком трубы.
/// </summary>
public class PipeConnection : ModelObjectBase, IConnectable {

    int id;
    string name;
    string firstPipeId;
    string secondPipeId;
    public Pipe first;
    public Pipe second;
    public GraphicRaw topology;

    public static void CreateObject(ModelObject obj) {
        GameObject go = new GameObject(obj.id.ToString());
        PipeConnection connection = go.AddComponent<PipeConnection>();
        connection.id = obj.id;
        connection.name = obj.name;
        connection.firstPipeId = obj.values["firstPipe"].ToString();
        connection.secondPipeId = obj.values["secondPipe"].ToString();
        go.transform.SetParent(parent.transform);
        }

    void Start() {
        Recalculate();
        VoidRender.sharedInstance.Register("default", topology);
        }

    public void Recalculate() {

        first = GameObject.Find(firstPipeId).GetComponent<Pipe>();
        second = GameObject.Find(secondPipeId).GetComponent<Pipe>();

        Vector3 firstEnd = first.transform.TransformPoint(0, 0, first.lenght);
        transform.position = firstEnd;
        Vector3 p3 = second.transform.position - transform.position;
        float hardness = (second.transform.position - firstEnd).magnitude * 0.75f;
        Vector3 p1 = first.transform.TransformPoint(0, 0, first.lenght + hardness) - transform.position;
        Vector3 p2 = second.transform.TransformPoint(0, 0, -hardness) - transform.position;

        GraphicCurveWithWidth curve = new GraphicCurveWithWidth();
        curve.startRadius = first.radius;
        curve.endRadius = second.radius;
        curve.majorSegments = 32;
        curve.minorSegments = 8;
        curve.p0 = Vector3.zero;
        curve.p1 = p1;
        curve.p2 = p2;
        curve.p3 = p3;
        curve.Recalculate();
        topology = curve;
        topology.SetTransform(transform.localToWorldMatrix);
        }

    public Vector3 GetStartPoint() {
        return transform.position;
        }

    public Vector3 GetEndPoint() {
        return second.transform.position;
        }

    public Quaternion GetStartRotation() {
        return first.transform.rotation;
        }
    public Quaternion GetEndRotation() {
        return second.transform.rotation;
        }

    public float GetStartRadius() {
        return first.radius;
        }

    public float GetEndRadius() {
        return second.radius;
        }
    }

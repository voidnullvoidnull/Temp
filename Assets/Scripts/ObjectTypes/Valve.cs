using UnityEngine;
using System.Collections;
using System;

public class Valve : ModelObjectBase, IConnectable {

    int id;
    string name;
    string firstPipeId;
    string secondPipeId;
    public Pipe first;
    public Pipe second;
    public GraphicRaw topology;


    public static void CreateObject(ModelObject obj) {
        GameObject go = new GameObject(obj.id.ToString());
        Valve valve = go.AddComponent<Valve>();
        valve.id = obj.id;
        valve.name = obj.name;
        valve.firstPipeId = obj.values["firstPipe"].ToString();
        valve.secondPipeId = obj.values["secondPipe"].ToString();
        go.transform.SetParent(parent.transform);
        }

    void Start() {
        Recalculate();
        VoidRender.sharedInstance.Register("seams", topology);
        }

    public void Recalculate() {

        first = GameObject.Find(firstPipeId).GetComponent<Pipe>();
        second = GameObject.Find(secondPipeId).GetComponent<Pipe>();

        Vector3 center = (second.GetStartPoint() + first.GetEndPoint()) / 2;
        topology = DataManager.LoadMesh("valve");
        topology.SetTransform(center, Vector3.zero, Vector3.one);
        }

    public Vector3 GetEndPoint() {
        throw new NotImplementedException();
        }

    public float GetEndRadius() {
        throw new NotImplementedException();
        }

    public Quaternion GetEndRotation() {
        throw new NotImplementedException();
        }

    public Vector3 GetStartPoint() {
        throw new NotImplementedException();
        }

    public float GetStartRadius() {
        throw new NotImplementedException();
        }

    public Quaternion GetStartRotation() {
        throw new NotImplementedException();
        }
    }

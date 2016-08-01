using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Реализует геометричское отображения сварного шва между элементами
/// </summary>
public class Seam : ModelObjectBase {

    public int id;
    public string name;

    string fromId;
    string toId;

    public ModelObjectBase from;
    public ModelObjectBase to;
    public GraphicRaw topology;

    public static void CreateObject(ModelObject obj) {
        GameObject go = new GameObject(obj.id.ToString());
        Seam seam = go.AddComponent<Seam>();
        seam.id = obj.id;
        seam.name = obj.name;
        seam.fromId = obj.values["from"].ToString();
        seam.toId = obj.values["to"].ToString();
        go.transform.SetParent(parent.transform);
        }

    void Start() {
        Recalculate();
        VoidRender.sharedInstance.Register("seams", topology);
        }

    public void Recalculate() {
        from = GameObject.Find(fromId).GetComponent<ModelObjectBase>();
        to = GameObject.Find(toId).GetComponent<ModelObjectBase>();
        float radius;
        Vector3 pos;
        Quaternion rot;

        if (from is IConnectable) {
            pos = ((IConnectable)from).GetEndPoint();
            rot = ((IConnectable)from).GetEndRotation();
            radius = ((IConnectable)from).GetEndRadius() * 1.5f;
            }
        if (to is IConnectable) {
            pos = ((IConnectable)to).GetStartPoint();
            rot = ((IConnectable)to).GetStartRotation();
            radius = ((IConnectable)to).GetStartRadius() * 1.5f;
            }
        else {
            pos = from.transform.position;
            rot = from.transform.rotation;
            radius = 1;
            }

        transform.position = pos;
        transform.rotation = rot;

        GraphicSeam seam = new GraphicSeam();
        seam.radius = radius;
        seam.width = 0.1f;
        seam.Recalculate();
        topology = seam;
        topology.SetTransform(transform.localToWorldMatrix);
        }

    }

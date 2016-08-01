using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using System;

/// <summary>
/// Реализует геометрическое отображения данных о трубе
/// </summary>
public class Pipe : ModelObjectBase, IConnectable {
    public int id;
    public string name;
    public float radius;
    public float lenght;
    GraphicRaw topology;

    public static void CreateObject(ModelObject obj) {
        //Создаем объект на сцене, цепляем к нему соответствующий компонент и делаем его потомком менеджера сцены для дальнейшего управления
        GameObject go = new GameObject(obj.id.ToString());
        Pipe pipe = go.AddComponent<Pipe>();

        //Устанавливаем свойства, которые можно установить сразу, также парсим и определяем типы данных
        pipe.id = obj.id;
        pipe.name = obj.name;
        Vector3 start = Point3D.FromJson(obj.values["start"]).GetVector();
        Vector3 end = Point3D.FromJson(obj.values["end"]).GetVector();
        pipe.radius = JsonConvert.DeserializeObject<float>(obj.values["radius"].ToString());
        go.transform.SetParent(parent.transform);
        go.transform.position = start;
        go.transform.LookAt(end);
        pipe.lenght = (end - start).magnitude;

        //Cоздадим коллайдер по направлению и длинной ровно по трубе
        CapsuleCollider collider = go.AddComponent<CapsuleCollider>();
        collider.radius = pipe.radius;
        collider.direction = 2;
        collider.height = pipe.lenght;
        collider.center = new Vector3(0, 0, pipe.lenght / 2);
        }

    void Start() {
        Recalculate();
        VoidRender.sharedInstance.Register("default", topology);
        }

    public void Recalculate() {
        GraphicLine line = new GraphicLine();
        line.lenght = lenght;
        line.radius = radius;
        line.Recalculate();
        topology = line;
        topology.SetTransform(transform.localToWorldMatrix);
        }

    public Vector3 GetStartPoint() {
        return transform.position;
        }

    public Vector3 GetEndPoint() {
        return transform.TransformPoint(new Vector3(0, 0, lenght));
        }

    public Quaternion GetStartRotation() {
        return transform.rotation;
        }
    public Quaternion GetEndRotation() {
        return transform.rotation;
        }

    public float GetStartRadius() {
        return radius;
        }

    public float GetEndRadius() {
        return radius;
        }
    }

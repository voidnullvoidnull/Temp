using UnityEngine;
using System.Collections;

/// <summary>
/// Задает список методов, необходимых для соединения объектов "сварочным швом"
/// </summary>
public interface IConnectable {

    Vector3 GetStartPoint();
    Vector3 GetEndPoint();
    Quaternion GetStartRotation();
    Quaternion GetEndRotation();
    float GetStartRadius();
    float GetEndRadius();
    }

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

/// <summary>
/// Представляет отражение точки в трехмерном пространстве. Нужен, в основном, для преобразования Vector3 для последующего хранения и конвертирования обратно
/// </summary>
[Serializable]
public struct Point3D {
    public float x;
    public float y;
    public float z;

    /// <summary>
    /// Возвращает UnityEngine.Vecto3 из текущей точки
    /// </summary>
    public Vector3 GetVector() {
        return new Vector3(x, y, z);
        }

    /// <summary>
    /// Принимает UnityEngine.Vector3, и записывает в точку
    /// </summary>
    public void SetVector(Vector3 vector) {
        x = vector.x;
        y = vector.y;
        z = vector.z;
        }

    /// <summary>
    /// Распознает точку из JSON строки
    /// </summary>
    public static Point3D FromJson(object json) {
        return JsonConvert.DeserializeObject<Point3D>(json.ToString());
        }

    }

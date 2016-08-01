using UnityEngine;
using System.Collections;

/// <summary>
/// Базовый класс для объекта, отражающего данные модели. Исопльзуется для распознавания модулем рефлексии при определении целевого типа
/// </summary>
public class ModelObjectBase : MonoBehaviour {
    /// <summary>
    /// Родительский объект для каждого наследующего класса
    /// </summary>
    public static GameObject parent;
    /// <summary>
    /// Менеджер текущей сцены
    /// </summary>
    public static SceneManager manager;
    }

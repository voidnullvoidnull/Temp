/* Void Null
 * voidnull.ru */

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отражающий и хранящий в себе "сырые" данные загруженой модели.
/// </summary>
[Serializable]
public class Model {

    /// <summary>
    /// Имя модели
    /// </summary>
    public string name;
    /// <summary>
    /// Массив объектов "сырых" даных модели
    /// </summary>
    public List<ModelObject> objects = new List<ModelObject>();

    /* При генерировании объектов, чтобы не усложнять код и без проблем добавлять новые типы реализован такой подход:
     * Сначала получаем все типы проекта, затем, оттуда выделяем все типы, унаследованные от ModelObjectBase,
     * Таким образом отсекаем возможные совпадения в названиях типов. Во всех типах, унаследованных от ModelObjectBase,
     * должен быть объявлен статический метод "void СreateObject (ModelObject obj)", который уже занимается распознаванием отдельных свойств
     * данного конкретного типа. Таким образом мы выносим код распознавания и обработки типов в отдальные автономные модули.
     * Для этого нужно соблюдать только лишь базовые правила: каждый тип, который может быть встречен в модели, должен быть отражен 
     * в виде класса, унаследованного от ModelObjectBase и реализовывать метод CreateObject.
     */

    /// <summary>
    /// Осуществяет распознавание типов объектов и передачу управления по обработке данных классу, ответственному за каждый конкретный тип объектов
    /// </summary>
    public void GenerateObjects() {
        foreach (ModelObject obj in objects) {
            switch (obj.type) {
                case "Pipe":
                Pipe.CreateObject(obj);
                break;

                case "PipeConnection":
                PipeConnection.CreateObject(obj);
                break;

                case "Seam":
                Seam.CreateObject(obj);
                break;

                case "Valve":
                Valve.CreateObject(obj);
                break;

                case "Topology":
                Topology.CreateObject(obj);
                break;

                default: break;
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GravityManager : MonoBehaviour
{
    public int layer = 8;
    const float gravityConstant = 0.1f;
    public float discretteTime = 0.2f;
    Rigidbody[] grObjects;


    public float timer = 0;
    Vector3 centerOfMass;
    float averageMass = 0;


    void Start()
    {
        grObjects = new Rigidbody[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            grObjects[i] = transform.GetChild(i).GetComponent<Rigidbody>();
        }
        Debug.Log(grObjects.Length + " objects for attraction");
    }

    void Update()
    {
        centerOfMass = new Vector3();
        averageMass = 0;

        for (int i = 0; i < grObjects.Length; i++)
        {
            centerOfMass += grObjects[i].position * grObjects[i].mass;
            averageMass += grObjects[i].mass;
        }

        centerOfMass = centerOfMass / averageMass;
    }

    public void FixedUpdate()
    {
        foreach (Rigidbody go in grObjects)
        {
            Vector3 normal = centerOfMass - go.transform.position;
            float magnitude = normal.magnitude;
            Vector3 dir = (normal / Mathf.Sqrt(magnitude)) * averageMass;
            go.AddForce(dir*gravityConstant*Time.fixedDeltaTime);
        }
    }


    public static Vector3 GetPotential(Vector3 first, float firstMass, Vector3 second, float secondMass)
    {
        Vector3 center = ((first * firstMass) + (second * secondMass)) / (firstMass + secondMass);
        Vector3 potential = (center-first).normalized/Mathf.Sqrt((center-first).magnitude);
        return potential;
    }
}

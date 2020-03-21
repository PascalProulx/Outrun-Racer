using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script that create a dummy so that the platforms can spawn, use only in the menu
/// </summary>
public class DummyCar : MonoBehaviour
{
    public static GameObject dummy;
    public static GameObject currentPlatform;
    Vector3 startPosition;

    void OnCollisionEnter(Collision other)
    {
        currentPlatform = other.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        dummy = this.gameObject;
        startPosition = dummy.transform.position;
        GenerateRoad.RunDummy();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateRoad.RunDummy();
    }

    void OnTriggerEnter(Collider other)
    {
        GenerateRoad.RunDummy();
    }
}

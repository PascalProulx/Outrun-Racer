using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script that generate the game world
/// </summary>
public class GenerateWorld : MonoBehaviour
{
    static public GameObject dummyTraveller;    // Static gameObject that represent the dummy
    static public GameObject lastPlatform;      // Static gameObject that represent the last platform that the player was

    void Awake()
    {
        dummyTraveller = new GameObject("dummy");
    }

    /// <summary>
    /// Function that generate the world 
    /// </summary>
    public static void RunDummy()
    {
        GameObject p = Pool.singleton.GetRandom();
        if (p == null) return;
        // Spawn the world in front of the player if he on a T-style platform
        if (lastPlatform != null)
        {
            if(lastPlatform.tag == "platformTSection")
                dummyTraveller.transform.position = lastPlatform.transform.position +
                    Car.player.transform.forward * 20;
            else
                dummyTraveller.transform.position = lastPlatform.transform.position +
                    Car.player.transform.forward * 24.2f;

            if (lastPlatform.tag == "roadUp")
                dummyTraveller.transform.Translate(0, 1.932f, 0);
        }

        lastPlatform = p;
        p.SetActive(true);
        p.transform.position = dummyTraveller.transform.position;
        p.transform.rotation = dummyTraveller.transform.rotation;

        if (p.tag == "roadDown")
        {
            dummyTraveller.transform.Translate(0, -1.932f, 0);
            p.transform.Rotate(0, 180, 0);
            p.transform.position = dummyTraveller.transform.position;
        }

        if (p.tag == "RightCurve")
        {
            p.transform.Rotate(0, 180, 0);
            p.transform.position = dummyTraveller.transform.position;
        }
    }

}

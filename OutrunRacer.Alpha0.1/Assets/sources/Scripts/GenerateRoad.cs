using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script that generate a single road (Mostly for the main menu, but still can be used during testing)
/// </summary>
public class GenerateRoad : MonoBehaviour
{
    /// <summary>
    /// Static objects 
    /// </summary>
    static public GameObject dummyTraveller;
    static public GameObject lastPlatform;
    void Awake()
    {
        dummyTraveller = new GameObject("dummy");
    }

    // Function that generate the platforms
    public static void RunDummy()
    {
        GameObject p = Pool.singleton.GetRandom();
        if (p == null) return;
        if (lastPlatform != null)
        {
            if (lastPlatform.tag == "platformTSection")
                dummyTraveller.transform.position = lastPlatform.transform.position +
                    DummyCar.dummy.transform.forward;
            else
                dummyTraveller.transform.position = lastPlatform.transform.position +
                    DummyCar.dummy.transform.forward * 24.2f;
        }
        lastPlatform = p;
        p.SetActive(true);
        p.transform.position = dummyTraveller.transform.position;
        p.transform.rotation = dummyTraveller.transform.rotation;
    }
}

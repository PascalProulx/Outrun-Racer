using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script that make a object scroll
/// </summary>
public class Scroll : MonoBehaviour
{
    void FixedUpdate()
    {
        this.transform.position += DummyCar.dummy.transform.forward * -10f * Time.deltaTime;

        if (DummyCar.currentPlatform == null) return;

    }
}

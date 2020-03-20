using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollDummy : MonoBehaviour
{
    void FixedUpdate()
    {
        this.transform.position += DummyCar.dummy.transform.forward * -10f * Time.deltaTime;

        if (DummyCar.currentPlatform == null) return;

    }
}

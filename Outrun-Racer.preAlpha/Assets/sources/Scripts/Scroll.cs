using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    void FixedUpdate()
    {
        this.transform.position += CarController.player.transform.forward * -0.1f;

        if (CarController.currentPlatform == null) return;
        if (CarController.currentPlatform.tag == "stairsUp")
            this.transform.Translate(0, -0.06f, 0);
        if (CarController.currentPlatform.tag == "stairsDown")
            this.transform.Translate(0, 0.06f, 0);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script that scroll the platforms
/// </summary>
public class Scroll : MonoBehaviour
{
    void FixedUpdate()
    {
        this.transform.position += CarController.player.transform.forward * -CarController.Instance.CarSpeed * Time.deltaTime;

        if (DummyCar.currentPlatform == null) return;

    }
}

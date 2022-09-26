using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
   public float Delay = 3f;

    void Start ()
    {
        Destroy (gameObject, Delay);
    }
}

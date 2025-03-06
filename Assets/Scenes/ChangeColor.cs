using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorLoop : MonoBehaviour
{
    public float changeInterval = 1f; // เปลี่ยนสีทุกๆ 1 วินาที

    void Start()
    {
        InvokeRepeating("ChangeCubeColor", 0f, changeInterval);
    }

    void ChangeCubeColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}

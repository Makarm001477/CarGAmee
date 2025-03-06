using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float rotationSpeed = 30f; // ความเร็วในการหมุน (องศาต่อวินาที)

    void Update()
    {
        // การหมุนไอเท็ม
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); // หมุนรอบแกน Y
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] cars; // Prefabs ของรถทั้งหมด
    public Transform spawnPoint; // จุดเกิดรถ

    void Start()
    {
        int selectedCarIndex = PlayerPrefs.GetInt("SelectedCar", 0); // โหลดค่ารถที่เลือก
        Instantiate(cars[selectedCarIndex], spawnPoint.position, spawnPoint.rotation);
    }
}


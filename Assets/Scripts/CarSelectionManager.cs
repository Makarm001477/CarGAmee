using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // เพิ่มการใช้ TextMeshPro

public class CarSelectionManager : MonoBehaviour
{
    public GameObject[] cars; // Prefab ของรถทั้งหมด
    public Transform spawnPoint; // จุดเกิดของรถ
    public TMP_Text carNameText; // ใช้ TMP_Text แทน Text ปกติ
    public Button nextButton, prevButton, selectButton; // ปุ่ม UI

    private int currentCarIndex = 0;
    private GameObject currentCarInstance;

    void Start()
    {
        ShowCar();
        nextButton.onClick.AddListener(NextCar);
        prevButton.onClick.AddListener(PreviousCar);
        selectButton.onClick.AddListener(SelectCar);
    }

    void ShowCar()
    {
        // ลบรถก่อนหน้า
        if (currentCarInstance != null)
        {
            Destroy(currentCarInstance);
        }

        // สร้างรถใหม่
        currentCarInstance = Instantiate(cars[currentCarIndex], spawnPoint.position, spawnPoint.rotation);
        carNameText.text = cars[currentCarIndex].name; // อัปเดตชื่อรถ
    }

    void NextCar()
    {
        currentCarIndex = (currentCarIndex + 1) % cars.Length; // เลื่อนไปคันถัดไป
        ShowCar();
    }

    void PreviousCar()
    {
        currentCarIndex = (currentCarIndex - 1 + cars.Length) % cars.Length; // เลื่อนย้อนกลับ
        ShowCar();
    }

    void SelectCar()
    {
        PlayerPrefs.SetInt("SelectedCar", currentCarIndex); // บันทึกค่ารถที่เลือก
        Debug.Log("Car Selected: " + cars[currentCarIndex].name);
    }
}

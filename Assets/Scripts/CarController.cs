using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For Slider UI
using TMPro; // For TextMeshPro

public class CarController : MonoBehaviour
{
    public float moveSpeed = 10f; // ความเร็วปกติ
    public float turnSpeed = 100f; // ความเร็วในการหมุน
    public float boostMultiplier = 1.5f; // ค่าการเร่งความเร็ว
    public float decelerationRate = 5f; // ค่าการหน่วงให้รถหยุดค่อยๆ ลดความเร็ว
    public float turnSmoothness = 5f; // ค่าความสมูทของการเลี้ยว
    public float boostDecreaseRate = 10f; // อัตราการลดหลอดบูสต์เมื่อใช้

    public Slider boostSlider; // ตัวแปร Slider สำหรับ UI
    public float maxBoost = 100f; // ค่าหลอดบูสต์สูงสุด
    public float boostAmount = 20f; // ค่าที่เพิ่มเมื่อเก็บไอเท็มบูสต์
    private float currentBoost = 0f; // ค่าหลอดบูสต์ปัจจุบัน

    public TextMeshProUGUI boostText; // ตัวแปร TextMeshProUGUI สำหรับแสดงค่าบูสต์

    private float currentSpeed = 0f; // ความเร็วปัจจุบัน
    private float currentTurnSpeed = 0f; // ค่าการหมุนปัจจุบัน

    // ตัวแปรใหม่เพื่อเก็บสถานะของไอเท็ม
    private GameObject boostItem;

    void Start()
    {
        // ตั้งค่าหลอดบูสต์ให้เท่ากับค่าหลอดบูสต์สูงสุด
        boostSlider.maxValue = maxBoost;
        boostSlider.value = currentBoost;
        UpdateBoostText(); // อัปเดตข้อความแสดงค่าบูสต์
    }

    void Update()
    {
        float targetSpeed = 0f;
        float targetTurnSpeed = 0f;

        // เร่งความเร็วเฉพาะเมื่อกด Shift และมีบูสต์เหลืออยู่
        float speedMultiplier = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && currentBoost > 0 ? boostMultiplier : 1f;

        // ลดค่าหลอดบูสต์เมื่อใช้บูสต์
        if (speedMultiplier > 1f)
        {
            currentBoost -= boostDecreaseRate * Time.deltaTime;
            currentBoost = Mathf.Clamp(currentBoost, 0f, maxBoost);
            boostSlider.value = currentBoost;
            UpdateBoostText(); // อัปเดตข้อความแสดงค่าบูสต์
        }

        // ควบคุมการเร่งความเร็ว
        if (Input.GetKey(KeyCode.W)) // กด W เพื่อไปข้างหน้า
        {
            targetSpeed = moveSpeed * speedMultiplier;
        }
        else if (Input.GetKey(KeyCode.S)) // กด S เพื่อถอยหลัง
        {
            targetSpeed = -moveSpeed * speedMultiplier;
        }

        // ใช้ Lerp เพื่อทำให้การเปลี่ยนแปลงความเร็วลื่นไหล
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * decelerationRate);

        // การหมุนแบบสมูท
        if (Input.GetKey(KeyCode.A)) // กด A เพื่อหมุนซ้าย
        {
            targetTurnSpeed = -turnSpeed;
        }
        else if (Input.GetKey(KeyCode.D)) // กด D เพื่อหมุนขวา
        {
            targetTurnSpeed = turnSpeed;
        }

        // ใช้ Lerp เพื่อทำให้การหมุนลื่นไหล
        currentTurnSpeed = Mathf.Lerp(currentTurnSpeed, targetTurnSpeed, Time.deltaTime * turnSmoothness);

        // เคลื่อนที่ไปข้างหน้าและถอยหลังแบบ Smooth
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        // การหมุนรถแบบ Smooth
        transform.Rotate(Vector3.up * currentTurnSpeed * Time.deltaTime);
    }

    // ฟังก์ชันที่เรียกเมื่อเก็บไอเท็มบูสต์
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BoostItem"))
        {
            // เพิ่มค่าหลอดบูสต์
            currentBoost += boostAmount;
            currentBoost = Mathf.Clamp(currentBoost, 0f, maxBoost);
            boostSlider.value = currentBoost;
            UpdateBoostText(); // อัปเดตข้อความแสดงค่าบูสต์

            // หยุดแสดงไอเท็มบูสต์และรอ 10 วินาที
            boostItem = other.gameObject;
            boostItem.SetActive(false); // ซ่อนไอเท็ม

            // เรียกฟังก์ชันใหม่ที่ใช้เวลารอ 10 วินาที
            StartCoroutine(ReactivateBoostItem());
        }
    }

    // ฟังก์ชันเพื่อเปิดไอเท็มบูสต์หลังจาก 10 วินาที
    private IEnumerator ReactivateBoostItem()
    {
        yield return new WaitForSeconds(6f); // รอ 10 วินาที
        boostItem.SetActive(true); // เปิดไอเท็มใหม่
    }

    // อัปเดตข้อความแสดงค่าบูสต์
    private void UpdateBoostText()
    {
        boostText.text = "Power: " + currentBoost.ToString("F0") + "%"; // แสดงค่าบูสต์ใน TextMeshPro
    }
}

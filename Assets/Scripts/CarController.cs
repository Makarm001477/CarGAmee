using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 10f; // ความเร็วปกติ
    public float turnSpeed = 100f; // ความเร็วในการหมุน
    public float boostMultiplier = 1.5f; // ค่าการเร่งความเร็ว
    public float decelerationRate = 5f; // ค่าการหน่วงให้รถหยุดค่อยๆ ลดความเร็ว
    public float turnSmoothness = 5f; // ค่าความสมูทของการเลี้ยว

    private float currentSpeed = 0f; // ความเร็วปัจจุบัน
    private float currentTurnSpeed = 0f; // ค่าการหมุนปัจจุบัน

    void Update()
    {
        float targetSpeed = 0f;
        float targetTurnSpeed = 0f;

        // เช็คว่ากด Shift หรือไม่เพื่อเร่งความเร็ว
        float speedMultiplier = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? boostMultiplier : 1f;

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
}

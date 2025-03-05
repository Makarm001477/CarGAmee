using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWaypointController : MonoBehaviour
{
    public Transform[] waypoints;  // Array ของ Waypoints
    public float speed = 5f;       // ความเร็วในการเคลื่อนที่
    public float rotationSpeed = 5f; // ความเร็วในการหมุน

   

    private int currentWaypointIndex = 0; // ตัวบ่งชี้ waypoint ที่ AI กำลังจะไป

    void Update()
    {

       
        // ตรวจสอบว่า AI ถึง waypoint แล้วหรือยัง
        if (waypoints.Length == 0)
            return;

        // หาตำแหน่งที่ต้องไป (Waypoint ปัจจุบัน)
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // คำนวณทิศทางจากตำแหน่ง AI ไปยัง waypoint
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        // หมุน AI ไปยัง waypoint
        Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        // เคลื่อนที่ไปข้างหน้า
        transform.position += transform.forward * speed * Time.deltaTime;

        // เช็คว่า AI ถึง waypoint แล้วหรือยัง
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 1f)
        {
            // เปลี่ยนไปยัง waypoint ถัดไป
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;  // การวนลูปกลับไปที่ waypoint แรกเมื่อจบ
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public float torqueAmount = 1f; // กำหนดขนาดแรงบิด

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ตรวจจับตำแหน่งที่ Player ยืนบนคาน
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = transform.position;

            // ถ้า Player อยู่ฝั่งซ้าย
            if (contactPoint.x < center.x)
            {
                // หมุนคานทวนเข็มนาฬิกา
                GetComponent<Rigidbody2D>().AddTorque(torqueAmount);
            }
            // ถ้า Player อยู่ฝั่งขวา
            else
            {
                // หมุนคานตามเข็มนาฬิกา
                GetComponent<Rigidbody2D>().AddTorque(-torqueAmount);
            }
        }
    }
}

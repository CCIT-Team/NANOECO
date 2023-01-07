using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCam : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal * moveSpeed * Time.deltaTime,0, vertical * moveSpeed * Time.deltaTime);

        transform.position -= move;

        if(Input.GetKey(KeyCode.U))
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.I))
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
    }
}

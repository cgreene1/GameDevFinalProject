using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 05f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horzIn = Input.GetAxis("Horizontal");
        float verIn = Input.GetAxis("Vertical");
        transform.position = transform.position + new Vector3(horzIn * speed * Time.deltaTime, verIn * speed * Time.deltaTime, 0);
    }
}

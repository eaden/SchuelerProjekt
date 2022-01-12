using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1CameraMovement : MonoBehaviour
{
    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(0f, 0.5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1CameraMovement : MonoBehaviour
{
    Rigidbody2D rigid;
    public static float camMovementSpeed = 3f;
	[SerializeField]
	public bool Stop {get; set;}

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
		Stop = false;
    }

    void FixedUpdate()
    {
		if (Stop) {
			rigid.velocity = Vector2.zero;
		} else {
			rigid.velocity = new Vector2(0f, camMovementSpeed);
		}
    }
}

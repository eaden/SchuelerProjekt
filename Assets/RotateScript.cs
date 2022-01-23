using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    Vector3 rotateVector;
    // Start is called before the first frame update
    void Start()
    {
        rotateVector = new Vector3(0, 0, -15);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateVector * Time.deltaTime);
    }
}

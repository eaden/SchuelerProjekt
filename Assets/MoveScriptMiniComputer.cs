using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScriptMiniComputer : MonoBehaviour
{
    Vector3 firstposition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        firstposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime*5);
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x > 1f)
        {
            transform.position = firstposition;
        }
    }
}

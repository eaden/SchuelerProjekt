using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSAbflugscript : MonoBehaviour
{
    float timer = 0; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.right * Time.deltaTime * 10);
        /*
        if (gameObject.transform.localScale.x < 10f)
        {
            timer = (Time.deltaTime);
            gameObject.transform.localScale += new Vector3(timer, timer, timer);
            //gameObject.transform.Translate(Vector3.right * Time.deltaTime/2);
            if (gameObject.transform.localScale.x > 5f)
            {
                gameObject.transform.Translate(Vector3.right * Time.deltaTime*2);
            }
        }
        else
        {
            
        }
        */
    }
}

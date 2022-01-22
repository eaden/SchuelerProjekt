using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if(gameObject.tag == "SearchedItem")
        {
            // richtiges Item angeklickt
            ClickSceneManager.Instance.NextClickObject();
            //Destroy(this.gameObject);
        }
    }
}

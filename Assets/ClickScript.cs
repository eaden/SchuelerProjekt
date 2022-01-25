using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour
{
    ClickSceneManager csm;
    // Start is called before the first frame update
    void Start()
    {
        csm = ClickSceneManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if(gameObject.tag == "SearchedItem")
        {
            Debug.Log("Objekt getroffen");
            csm.NextClickObject();
            // richtiges Item angeklickt
            //ClickSceneManager.Instance.NextClickObject();
            //Destroy(this.gameObject);
        }
        else
        {
            AudioManager.Instance.Play2("ouiii"); 
            Debug.Log("Objekt nicht getroffen");
        }
    }
}

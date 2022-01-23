using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScriptSteinSchere : MonoBehaviour
{
    SteinSchereManager ssm;
    // Start is called before the first frame update
    void Start()
    {
        ssm = SteinSchereManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (gameObject.tag == "SearchedItem" && !ssm.objektWurdeGewaehlt)
        {
            ssm.ObjektWahl(gameObject.name);
            // richtiges Item angeklickt
            //ClickSceneManager.Instance.NextClickObject();
            //Destroy(this.gameObject);
        }
    }
}

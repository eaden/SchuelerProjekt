using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObObjectScript : MonoBehaviour
{
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            /*
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up,1200);
            if (hit.collider != null && hit.transform.gameObject.CompareTag("SearchedItem"))
                Debug.Log("Yeah");
            else
                Debug.Log("möp");*/

            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 1200))
            {
                if(hit.transform == null)
                {

                    // Buzzersound
                    Debug.Log("möp");
                }
                else if(hit.transform.gameObject.CompareTag("SearchedItem"))
                {
                    Debug.Log("Yeah");
                }
            }
            
            /*
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
            }
            */
                /*
                if (hit.collider != null && hit.transform.gameObject.CompareTag("SearchedItem"))
                    Debug.Log("Yeah");
                else
                    Debug.Log("möp");
                */

            }
    }
}

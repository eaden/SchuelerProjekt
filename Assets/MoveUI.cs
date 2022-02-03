using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUI : MonoBehaviour
{
    private RectTransform rectTransform;
    bool endeErreicht = false;
    float scrollSpeed = 80;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = rectTransform.anchoredPosition;
        //Debug.Log(gameObject.name + " " + position.y);
        position.y += Time.deltaTime * scrollSpeed;
        if(position.y > 0)
        {
            if(!endeErreicht)
            {
                TeilnehmendeManager.Instance.szeneVerlassen = true;
                FadeInOut.Instance.FadeBlackIn(4f);
                endeErreicht = true;
            }
        }

        rectTransform.anchoredPosition = position;
    }
}

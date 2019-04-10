using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private GameObject canvas;
    private GameObject jelly;
    private GameObject presskey;
    private float fadespeed;
    private float movespeed;
    private bool isfade = true;
    private float jellyfishup;
    private float jellyfishdown;
    private bool isup = true;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        jelly = canvas.transform.Find("jellyfish").gameObject;
        jellyfishup = jelly.transform.position.y + 100;
        jellyfishdown = jelly.transform.position.y - 60;
        presskey = canvas.transform.Find("Press Any Key").gameObject;
        fadespeed = 2.0f;
        movespeed = 30.0f;
    }

    // Update is called once per frame
    void Update()
    {
        presskeymove();
        jellyfishmove();
    }

    void jellyfishmove()
    {
        Vector3 pos = jelly.transform.position;
        if (isup)
        {
            pos.y += movespeed * Time.deltaTime;
            if (pos.y >= jellyfishup)
                isup = false;
        }
        else
        {
            pos.y -= movespeed * Time.deltaTime;
            if (pos.y <= jellyfishdown)
                isup = true;
        }
        jelly.transform.position = pos;
    }

    void presskeymove()
    {
        CanvasGroup canvasGroup = presskey.GetComponent<CanvasGroup>();
        if(isfade)
        {
            canvasGroup.alpha -= fadespeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0.1)
                isfade = false;
        }
        else
        {
            canvasGroup.alpha += fadespeed * Time.deltaTime;
            if (canvasGroup.alpha >= 0.9)
                isfade = true;
        }
    }
}

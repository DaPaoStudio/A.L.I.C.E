using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    private GameObject canvas;
    private GameObject jelly;
    private GameObject jelly1;
    private GameObject jelly2;
    private GameObject presskey;
    private float fadespeed;
    private float movespeed;
    private bool isfade = true;
    private float jellyfishup;
    private float jellyfishdown;
    private float jellyfishup1;
    private float jellyfishdown1;
    private float jellyfishup2;
    private float jellyfishdown2;
    private bool isup = true;
    private GameObject[] bubbles;
    private GameObject[] fishes;
    private GameObject[] jellyfishes;
    private float bubblemovespeed;
    private AudioSource AudioSource;
    private GameObject black;
    private GameObject dialog;
    private bool isdo = false;
    private List<string> dialogs = new List<string>(new string[] {"孤独是永恒的","他像冰冷的深海海水","不论天空中是风和日丽还是狂风暴雨","如果你发出的声音谁也不能理解",
       "--那么","你，会觉得孤独吗?","这是她的故事，也是他们的故事。\n 是渺小的人，与巨大的鲸的故事。"});
    // Start is called before the first frame update
    void Start()
    {
        dialog =transform.Find("dialog").gameObject;
        canvas = GameObject.Find("Canvas");
        jelly = canvas.transform.Find("jellyfish").gameObject;
        jelly1 = canvas.transform.Find("jellyfish1").gameObject;
        jelly2 = canvas.transform.Find("jellyfish2").gameObject;
        jellyfishup = jelly.transform.position.y + 40;
        jellyfishdown = jelly.transform.position.y - 20;
        presskey = canvas.transform.Find("Press Any Key").gameObject;
        fadespeed = 1.2f;
        movespeed = 5.0f;
        bubbles = GameObject.FindGameObjectsWithTag("bubble");
        fishes = GameObject.FindGameObjectsWithTag("fish");
        bubblemovespeed = 40f;
        AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = Resources.Load(@"Audios/SFX/enterwater") as AudioClip;
        black = canvas.transform.Find("black").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        presskeymove();
        jellyfishmove();
        bubblesmove();
        fishesmove();
        if (Input.anyKeyDown && isdo == false)
        {
            StartCoroutine("startgame");
            isdo = true;
        }
    }

    void jellyfishmove()
    {
        Vector3 pos = jelly.transform.position;
        Vector3 pos1 = jelly1.transform.position;
        Vector3 pos2 = jelly2.transform.position;
        if (isup)
        {
            pos.y += movespeed * Time.deltaTime;
            pos1.y += movespeed * Time.deltaTime;
            pos2.y += movespeed * Time.deltaTime;
            if (pos.y >= jellyfishup)
                isup = false;
        }
        else
        {
            pos.y -= movespeed * Time.deltaTime;
            pos1.y -= movespeed * Time.deltaTime;
            pos2.y -= movespeed * Time.deltaTime;
            if (pos.y <= jellyfishdown)
                isup = true;
        }
        jelly.transform.position = pos;
        jelly1.transform.position = pos1;
        jelly2.transform.position = pos2;
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
    void bubblesmove()
    {
        foreach (var bubble in bubbles)
        {
            Vector3 pos = bubble.GetComponent<RectTransform>().anchoredPosition3D;
            pos.y += bubblemovespeed * Time.deltaTime;
            if (pos.y >= 842)
                pos.y = -600;
            bubble.GetComponent<RectTransform>().anchoredPosition3D = pos;
        }
    }
    void fishesmove()
    {
        foreach (var fish in fishes)
        {
            Vector3 pos = fish.GetComponent<RectTransform>().anchoredPosition3D;
            pos.x -= movespeed * Time.deltaTime;
            if (pos.x <= -1426)
                pos.x = 1409;
            fish.GetComponent<RectTransform>().anchoredPosition3D = pos;
        }
    }
    IEnumerator startgame()
    {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().enabled = false;
        AudioSource.Play();
        AsyncOperation op = null;
        op = GameManager.gameManager.loadscene("1.California");
        black.GetComponent<Image>().DOFade(1, 4.5f);
        yield return new WaitForSeconds(5);
        StartCoroutine("showdialog", op);
    }
    IEnumerator showdialog(AsyncOperation op)
    {
        foreach(string p in dialogs)
        {
            dialog.GetComponent<Text>().text = p;
            Tweener tw = dialog.GetComponent<Text>().DOFade(1, 2f);
            tw.OnComplete(delegate { dialog.GetComponent<Text>().DOFade(0, 2f); });
            yield return new WaitForSeconds(4.5f);
        }
        op.allowSceneActivation = true;
    }
}

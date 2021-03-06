﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MapManager : MonoBehaviour
{
    public GameObject text;
    private GameObject canvas;
    private GameObject whale;
    private AudioSource Audio;
    private GameObject blackpanel;
    AsyncOperation op = null;
    private bool isdo;
    // Start is called before the first frame update
    void Start()
    {
        text.GetComponent<Text>().text = "当前位置：" + GameManager.gameManager.currentplace;
        Cursor.visible = true;
        isdo = false;
        canvas = GameObject.Find("Canvas");
        whale = GameObject.Find("Alice");
        Audio = GetComponent<AudioSource>();
        blackpanel = canvas.transform.Find("Black").gameObject;
        whale.transform.position = canvas.transform.Find(GameManager.gameManager.switchword(GameManager.gameManager.currentplace)).position;
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("button"))
            p.GetComponent<Button>().interactable = false;
        foreach (string p in GameManager.gameManager.currentplacelist)
            canvas.transform.Find(p).GetComponent<Button>().interactable = true;
        foreach (string p in GameManager.gameManager.isgone)
            canvas.transform.Find(p).GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void xiaweiyi()
    {
        if (isdo == false)
        {
            isdo = true;
            GameManager.gameManager.currentplacelist = GameManager.gameManager.xiaweiyi;
            GameManager.gameManager.isgone.Add("xiaweiyi");
            GameManager.gameManager.currentplace = "夏威夷海";
            op = GameManager.gameManager.loadscene("1.California");
            Tweener tw = whale.transform.DOMove(canvas.transform.Find("xiaweiyi").position, 2);
            tw.OnComplete(twcomplete);
        }
    }
    public void beitaipingyang()
    {
        if (isdo == false)
        {
            isdo = true;
            GameManager.gameManager.currentplacelist = GameManager.gameManager.beitaipingyang;
            GameManager.gameManager.isgone.Add("beitaipingyang");
            GameManager.gameManager.currentplace = "北太平洋";
            op = GameManager.gameManager.loadscene("4.Inside Sea");
            Tweener tw = whale.transform.DOMove(canvas.transform.Find("beitaipingyang").position, 2);
            tw.OnComplete(twcomplete);
        }
    }
    public void bailinghai()
    {
        if (isdo == false)
        {
            isdo = true;
            GameManager.gameManager.currentplacelist = GameManager.gameManager.bailinghai;
            GameManager.gameManager.isgone.Add("bailinghai");
            GameManager.gameManager.currentplace = "白令海";
            op = GameManager.gameManager.loadscene("4.Inside Sea");
            Tweener tw = whale.transform.DOMove(canvas.transform.Find("bailinghai").position, 2);
            tw.OnComplete(twcomplete);
        }
    }
    public void jiazhouwaihai()
    {
        if (isdo == false)
        {
            isdo = true;
            GameManager.gameManager.currentplacelist = GameManager.gameManager.jiazhouwaihai;
            GameManager.gameManager.isgone.Add("jiazhouwaihai");
            GameManager.gameManager.currentplace = "加州外海";
            op = GameManager.gameManager.loadscene("4.Inside Sea");
            Tweener tw = whale.transform.DOMove(canvas.transform.Find("jiazhouwaihai").position, 2);
            tw.OnComplete(twcomplete);
        }
    }
    public void alasijia()
    {
        if (isdo == false)
        {
            isdo = true;
            GameManager.gameManager.currentplacelist = GameManager.gameManager.alasijia;
            GameManager.gameManager.isgone.Add("alasijia");
            GameManager.gameManager.currentplace = "阿拉斯加湾";
            op = GameManager.gameManager.loadscene("1.California");
            Tweener tw = whale.transform.DOMove(canvas.transform.Find("alasijia").position, 2);
            tw.OnComplete(twcomplete);
        }
    }
    public void twcomplete()
    {
        int index = (int)Random.Range(1, 5f);
        Audio.clip = GameManager.gameManager.getclip(@"SFX/" + "Whale" + index.ToString());
        Audio.Play();
        Tweener twr=blackpanel.GetComponent<Image>().DOFade(1, 8f);
        twr.OnComplete(delegate () { op.allowSceneActivation = true; });
    }
}

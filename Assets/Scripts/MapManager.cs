using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MapManager : MonoBehaviour
{
    private GameObject canvas;
    private GameObject whale;
    private AudioSource Audio;
    private GameObject blackpanel;
    AsyncOperation op = null;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        whale = GameObject.Find("Alice");
        Audio = GetComponent<AudioSource>();
        blackpanel = canvas.transform.Find("Black").gameObject;
        whale.transform.position = canvas.transform.Find(GameManager.gameManager.currentplace).position;
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
        Debug.Log("a");
        GameManager.gameManager.currentplacelist = GameManager.gameManager.xiaweiyi;
        GameManager.gameManager.isgone.Add("xiaweiyi");
        GameManager.gameManager.currentplace = "xiaweiyi";
        op=GameManager.gameManager.loadscene("3.Coast");
        Tweener tw= whale.transform.DOMove(canvas.transform.Find("xiaweiyi").position, 2);
        tw.OnComplete(twcomplete);
    }
    public void beitaipingyang()
    {
        GameManager.gameManager.currentplacelist = GameManager.gameManager.beitaipingyang;
        GameManager.gameManager.isgone.Add("beitaipingyang");
        GameManager.gameManager.currentplace = "beitaipingyang";
        op = GameManager.gameManager.loadscene("3.Coast");
        Tweener tw = whale.transform.DOMove(canvas.transform.Find("beitaipingyang").position, 2);
        tw.OnComplete(twcomplete);
    }
    public void bailinghai()
    {
        GameManager.gameManager.currentplacelist = GameManager.gameManager.bailinghai;
        GameManager.gameManager.isgone.Add("bailinghai");
        GameManager.gameManager.currentplace = "bailinghai";
        op = GameManager.gameManager.loadscene("3.Coast");
        Tweener tw = whale.transform.DOMove(canvas.transform.Find("bailinghai").position, 2);
        tw.OnComplete(twcomplete);
    }
        public void jiazhouwaihai()
    {
        GameManager.gameManager.currentplacelist = GameManager.gameManager.jiazhouwaihai;
        GameManager.gameManager.isgone.Add("jiazhouwaihai");
        GameManager.gameManager.currentplace = "jiazhouwaihai";
        op = GameManager.gameManager.loadscene("3.Coast");
        Tweener tw = whale.transform.DOMove(canvas.transform.Find("jiazhouwaihai").position, 2);
        tw.OnComplete(twcomplete);
    }
    public void alasijia()
    {
        GameManager.gameManager.currentplacelist = GameManager.gameManager.alasijia;
        GameManager.gameManager.isgone.Add("alasijia");
        GameManager.gameManager.currentplace = "alasijia";
        op = GameManager.gameManager.loadscene("3.Coast");
        Tweener tw = whale.transform.DOMove(canvas.transform.Find("alasijia").position, 2);
        tw.OnComplete(twcomplete);
    }
    public void twcomplete()
    {
        int index = (int)Random.Range(0, 5f);
        Audio.clip = GameManager.gameManager.getclip(@"SFX/" + "Whale" + index.ToString());
        Audio.Play();
        Tweener twr=blackpanel.GetComponent<Image>().DOFade(1, 8f);
        twr.OnComplete(delegate () { op.allowSceneActivation = true; });
    }
}

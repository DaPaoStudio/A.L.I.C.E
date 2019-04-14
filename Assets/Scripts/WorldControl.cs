using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class WorldControl : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject canvas;
    private GameObject HP;
    private GameObject EN;
    private GameObject showplace;
    private GameObject black;
    private GameObject hudtip;
    private GameObject dialog;
  
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        dialog = transform.Find("dialog").gameObject;
        HP = canvas.transform.Find("HP").gameObject;
        EN= canvas.transform.Find("EN").gameObject;
        showplace = GameObject.Find("showplace").gameObject;
        black = GameObject.Find("black").gameObject;
        hudtip = transform.Find("Text").gameObject;
        createfish();
        createtrash();
        StartCoroutine("show");
        hudtip.GetComponent<Text>().text = GameManager.gameManager.currentplace + " " + GameManager.gameManager.year.ToString();
        showplace.GetComponent<Text>().text = GameManager.gameManager.currentplace + " " + GameManager.gameManager.year.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        showHPandEN();
        if (GameManager.gameManager.HP <= 0)
            die();
    }

    void die()
    {
        if (GameManager.gameManager.currentplace == "阿拉斯加湾" || GameManager.gameManager.currentplace == "夏威夷海" || GameManager.gameManager.currentplace == "加州湾")
            GameManager.gameManager.loadscene("5.Stranded End").allowSceneActivation=true;
        else
            GameManager.gameManager.loadscene("6.Whale Fall End").allowSceneActivation=true;
    }
    void showHPandEN()
    {
        HP.GetComponent<Slider>().value =(float) GameManager.gameManager.HP /(float) GameManager.gameManager.maxHP * 100;
        EN.GetComponent<Slider>().value = (float)GameManager.gameManager.MP / (float)GameManager.gameManager.maxMP * 100;
    }
    IEnumerator show()
    {
        showplace.GetComponent<Text>().DOFade(1, 2f);
        yield return new WaitForSeconds(3);
        showplace.GetComponent<Text>().DOFade(0, 2f);
        yield return new WaitForSeconds(3);
        black.GetComponent<Image>().DOFade(0, 2f);
    }
    void createfish()
    {
        for(int i=1;i<=GameManager.gameManager.fishamount;i++)
        {
            int index = Random.Range(1, 25);
            GameObject fish = Poolcontrol.MyPool.getobj("fish" + index.ToString());
            if (SceneManager.GetActiveScene().name== "1.California")
            {
                float positionx = Random.Range(-3f, 97f);
                float positiony = Random.Range(18f, 108f);
                float positionz = Random.Range(-37f, 63f);
                fish.transform.position = new Vector3(positionx, positiony, positionz);
            }
           if(SceneManager.GetActiveScene().name == "4.Inside Sea")
            {
                float positionx = Random.Range(-132f, 128f);
                float positiony = Random.Range(-180f, 220f);
                float positionz = Random.Range(-125f, 135f);
                fish.transform.position = new Vector3(positionx, positiony, positionz);
            }
        }
    }
    void createtrash()
    {
        for (int i = 1; i <= GameManager.gameManager.trashamount; i++)
        {
            int index = Random.Range(1, 30);
            GameObject fish = Poolcontrol.MyPool.getobj("trash" + index.ToString());
            if (SceneManager.GetActiveScene().name == "1.California")
            {
                float positionx = Random.Range(-3f, 97f);
                float positiony = Random.Range(15f, 78f);
                float positionz = Random.Range(-37f, 63f);
                fish.transform.position = new Vector3(positionx, positiony, positionz);
            }
            if (SceneManager.GetActiveScene().name == "4.Inside Sea")
            {
                float positionx = Random.Range(-132f, 128f);
                float positiony = Random.Range(-180f, 220f);
                float positionz = Random.Range(-125f, 135f);
                fish.transform.position = new Vector3(positionx, positiony, positionz);
            }
        }
    }
    public void tip(string p)
    {
        showplace.GetComponent<Text>().text = p;
        Tweener tw= showplace.GetComponent<Text>().DOFade(1, 2f);
        tw.OnComplete(delegate { showplace.GetComponent<Text>().DOFade(0, 2f); });
    }
    public void becomeblack(AsyncOperation op)
    {
        Tweener tw= black.GetComponent<Image>().DOFade(1, 3f);
        tw.OnComplete(delegate { op.allowSceneActivation = true; });
    }
    public void showdialogone(AsyncOperation op)
    {
        Tweener tw = black.GetComponent<Image>().DOFade(1, 3f);
        tw.OnComplete(delegate { StartCoroutine("showdialog", op); });
    }
    IEnumerator showdialog(AsyncOperation op)
    {
        foreach (string p in GameManager.gameManager.dialogone)
        {
            dialog.GetComponent<Text>().text = p;
            dialog.GetComponent<Text>().DOFade(1, 2f);
            yield return new WaitForSeconds(3f);
            dialog.GetComponent<Text>().DOFade(0, 2f);
            yield return new WaitForSeconds(3f);
        }
        op.allowSceneActivation = true;
    }
}

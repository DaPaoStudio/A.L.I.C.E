using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class WorldControl : MonoBehaviour
{
    private AsyncOperation canleaoutop = null;
    private bool canleaveout=false;
    private AudioSource audioSource;
    private GameObject canvas;
    private GameObject HP;
    private GameObject EN;
    private GameObject showplace;
    private GameObject black;
    private GameObject hudtip;
    private GameObject dialog;
    private GameObject hpt;
    private GameObject mpt;
    public bool cando;
    private bool low;
    public bool stoplow;
    private GameObject intro;
    private bool deathplay = false;
    private float timer ;
    public float timer2;
    private GameObject leaveouttip;
  
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        canleaveout = false;
        timer2 = 0;
        timer = 0;
        stoplow = false;
        low = false;
        cando = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load(@"Audios/SFX/WhaleDeath") as AudioClip;
        canvas = GameObject.Find("Canvas");
        dialog = transform.Find("dialog").gameObject;
        HP = canvas.transform.Find("HP").gameObject;
        EN= canvas.transform.Find("EN").gameObject;
        hpt= transform.Find("hpt").gameObject;
        mpt= transform.Find("mpt").gameObject;
        leaveouttip = transform.Find("leaveouttip").gameObject;
        intro = transform.Find("intro").gameObject;
        showplace = GameObject.Find("showplace").gameObject;
        black = GameObject.Find("black").gameObject;
        hudtip = transform.Find("Text").gameObject;
        createfish();
        createtrash();
        StartCoroutine("show");
        StartCoroutine("showintro");
        hudtip.GetComponent<Text>().text = GameManager.gameManager.currentplace + " " + GameManager.gameManager.year.ToString();
        showplace.GetComponent<Text>().text = GameManager.gameManager.currentplace + " " + GameManager.gameManager.year.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        if(GameManager.gameManager.MP<=1&&timer>=3)
        {
            GameManager.gameManager.changehp(-1);
            hptip(-1);
            timer = 0;
        }
        if(timer2>=8&& GameManager.gameManager.MP>0)
        {
            GameManager.gameManager.changemp(-1);
            mptip(-1);
            timer2 = 0;
        }
        if (timer >= 9)
            timer = 9;
        if (timer2 >= 14)
            timer2 =14;
        showHPandEN();
        if (GameManager.gameManager.HP <= 0 && deathplay == false)
        {
            die();
            deathplay = true;
        }
        if (GameManager.gameManager.HP <= 10&&low==false)
        {
            StartCoroutine("hplow");
            low = true;
        }
        leaveout();
    }

    void die()
    {
        cando = false;
        audioSource.Play();
        AsyncOperation op = null;
        if (GameManager.gameManager.currentplace == "阿拉斯加湾" || GameManager.gameManager.currentplace == "夏威夷海" || GameManager.gameManager.currentplace == "加州湾")
            op = GameManager.gameManager.loadscene("5.Stranded End");
        else
            op = GameManager.gameManager.loadscene("6.Whale Fall End");
        becomeblack(op);
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
        cando = true;
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
        stoplow = true;
        Tweener tw= black.GetComponent<Image>().DOFade(1, 3f);
        tw.OnComplete(delegate { op.allowSceneActivation = true; });
    }
    public void showdialogone(AsyncOperation op)
    {
        canleaoutop = op;
        stoplow = true;
        Tweener tw = black.GetComponent<Image>().DOFade(1, 3f);
        canleaveout = true;
        tw.OnComplete(delegate { StartCoroutine("showdialog", op); });
    }
    IEnumerator showdialog(AsyncOperation op)
    {
        leaveouttip.GetComponent<Text>().DOFade(1, 2f);
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
    public void hptip(int p)
    {
        hpt.GetComponent<Text>().text = p.ToString();
        Tweener tw = hpt.GetComponent<Text>().DOFade(1, 1f);
        tw.OnComplete(delegate { hpt.GetComponent<Text>().DOFade(0, 1f); });
    }
    public void mptip(int p)
    {
        if(p>=0)
            mpt.GetComponent<Text>().text ="+"+ p.ToString();
        else
            mpt.GetComponent<Text>().text =  p.ToString();
        Tweener tw = mpt.GetComponent<Text>().DOFade(1, 1f);
        tw.OnComplete(delegate { mpt.GetComponent<Text>().DOFade(0, 1f); });
    }
    IEnumerator hplow()
    {
        while(!stoplow)
        {
            Tweener tw= black.GetComponent<Image>().DOFade(0.6f, 2f);
            tw.OnComplete(delegate { tw = black.GetComponent<Image>().DOFade(0, 1f); });
            yield return new WaitForSeconds(3);
        }
    }
    public void tiptwo(bool p)
    {
        showplace.GetComponent<Text>().text = "Alice已为远行做好了准备";
        if(p)
            showplace.GetComponent<Text>().DOFade(1, 2f);
        else
            showplace.GetComponent<Text>().DOFade(0, 2f);
    }
    IEnumerator showintro()
    {
        yield return new WaitForSeconds(6);
        intro.GetComponent<Image>().DOFade(1, 2f);
        yield return new WaitForSeconds(6);
        intro.GetComponent<Image>().DOFade(0, 2f);
    }
    private void leaveout()
    {
        if (canleaveout)
        {
            if (Input.GetMouseButtonDown(0) && canleaoutop != null)
            {
                StopAllCoroutines();
                leaveouttip.GetComponent<Text>().DOFade(0, 2f);
                Tweener tw = dialog.GetComponent<Text>().DOFade(0, 2f);
                tw.OnComplete(delegate { canleaoutop.allowSceneActivation = true; });
            }
        }
    }
}

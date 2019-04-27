using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody rigid;
    private AudioSource[] audioSources;
    private float rotatespeed;
    private float maxspeed=10;
    private float movespeed = 1;
    private bool slowdown = true;
    private bool isplay = false;
    private GameObject partical;
    private bool istip = false;
    private bool canplayclip;
    private bool beat;
    private bool particalplay = false;
    private bool hasgone;
    private float waittime = 0;
    // Start is called before the first frame update
    void Start()
    {
        rotatespeed =3*GameManager.gameManager.mouse;
        hasgone = false;
        if(GameManager.gameManager.currentplace!="加州湾")
            GameManager.gameManager.MP -= 70;
        beat = false;
        rigid = GetComponent<Rigidbody>();
        audioSources = GetComponents<AudioSource>();
        audioSources[1].clip = GameManager.gameManager.getclip(@"SFX/" + "beats");
        rigid.velocity = transform.forward * movespeed;
        partical = transform.Find("Particle System").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        waittime += Time.deltaTime;
        //Debug.Log(rigid.velocity);
        if (GameObject.Find("Canvas").GetComponent<WorldControl>().cando)
        {
            movecontrol();
            audioplay();
            /* if (slowdown && slowtimer >= 0.2f)
             {
                 rigid.velocity = Vector3.Lerp(rigid.velocity, transform.forward * movespeed, 0.8f);
                 slowtimer = 0;
             }*/
            if(slowdown)
                rigid.velocity = Vector3.Lerp(rigid.velocity, transform.forward * movespeed, 0.01f);
            lineplay();
            playcliplist();
            if (rigid.velocity.magnitude >= 7 && particalplay == false)
            {
                partical.GetComponent<ParticleSystem>().Play();
                particalplay = true;
            }
            if (rigid.velocity.magnitude <= 7)
            {
                partical.GetComponent<ParticleSystem>().Stop();
                particalplay = false;
            }
            if (GameManager.gameManager.MP >= 80 && istip == false)
            {
                GameObject.Find("Canvas").SendMessage("tiptwo", true);
                istip = true;
            }
            if (GameManager.gameManager.MP < 80 && istip == true)
            {
                GameObject.Find("Canvas").SendMessage("tiptwo", false);
                istip = false;
            }
            playwait();
        }
    }

    void movecontrol()
    {
   /*     if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(Vector3.right, -rotatespeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
            transform.Rotate(Vector3.right, rotatespeed * Time.deltaTime);
         if (Input.GetKey(KeyCode.A))
             transform.Rotate(Vector3.up, -rotatespeed * Time.deltaTime);
         if (Input.GetKey(KeyCode.D))
             transform.Rotate(Vector3.up, rotatespeed * Time.deltaTime);*/
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotatespeed * Time.deltaTime);
        if(GameManager.gameManager.isposition)
            transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * rotatespeed * Time.deltaTime);
        else
            transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * -rotatespeed * Time.deltaTime);
        if (Input.GetMouseButton(0))
        {
            rigid.velocity += transform.forward * 10 * Time.deltaTime;
            slowdown = false;
        }
        else
        {
            slowdown = true;
        }
        Vector3 angle = transform.eulerAngles;
        transform.eulerAngles = limitrotate(angle);
        float value = Mathf.Clamp(rigid.velocity.magnitude, 0, maxspeed);
        if(rigid.velocity.magnitude>0)
            rigid.velocity = rigid.velocity * value / rigid.velocity.magnitude;
    }

    void audioplay()
    {
        if (GameManager.gameManager.HP <= 10 && beat == false)
        {
            audioSources[1].loop = true;
            audioSources[1].Play();
            beat = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (audioSources[0].isPlaying==false)
            {
                int index = (int)Random.Range(1, 5f);
                audioSources[0].clip = GameManager.gameManager.getclip(@"SFX/" + "Whale" + index.ToString());
                audioSources[0].Play();
            }
            if (GameManager.gameManager.currentplace == "加州湾" && GameManager.gameManager.firstsonginjiazhou == false)
            {
                StartCoroutine("jinggeline", "进入加州湾第一次唱响鲸歌");
            }
            if (GameManager.gameManager.currentplace != "加州湾" && GameManager.gameManager.firstsonginjiazhou == true && GameManager.gameManager.secondsong == false)
            {
                StartCoroutine("jinggeline", "在除加州湾以外的地区触发鲸歌并且在加州湾触发过");
            }
        }
    }

    private Vector3 limitrotate(Vector3 angle)
    {
        angle.x -= 180;
        if (angle.x > 0)
            angle.x -= 180;
        else
            angle.x += 180;
        angle.x= Mathf.Clamp(angle.x, -70, 70);
        angle.z = 0;
        return angle;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "fish")
        {
            if (GameObject.Find("Canvas").GetComponent<WorldControl>().cando)
            {
                int change= (int)Random.Range(1, 3);
                GameManager.gameManager.changemp(change);
                collision.gameObject.SetActive(false);
                audioSources[3].clip = GameManager.gameManager.getclip(@"SFX/Chew");
                audioSources[3].Play();
                GameObject.Find("Canvas").SendMessage("mptip", 1);
                GameObject.Find("Canvas").GetComponent<WorldControl>().timer2 = 0;
            }
        }
        else if (collision.transform.tag == "trash")
        {
            if (GameObject.Find("Canvas").GetComponent<WorldControl>().cando)
            {
                int change = (int)Random.Range(-9, -2);
                GameManager.gameManager.changehp(change);
                GameObject.Find("Canvas").SendMessage("hptip", change);
                collision.gameObject.SetActive(false);
                if (GameManager.gameManager.currentplace != "加州湾" && GameManager.gameManager.firsteattrash == false)
                {
                    AudioClip clip = GameManager.gameManager.getline("在加州湾以外的地方第一次吃到垃圾");
                    if (audioSources[2].isPlaying)
                        GameManager.gameManager.toplay.Add(clip);
                    else
                    {
                        audioSources[2].clip = clip;
                        audioSources[2].Play();                     
                    }
                    waittime = 0;
                    GameManager.gameManager.firsteattrash = true;
                }
                audioSources[3].clip = GameManager.gameManager.getclip(@"SFX/Crash");
                audioSources[3].Play();
            }
        }
        else if (collision.transform.tag == "walll")
        {
            AsyncOperation op = null;
            if (GameManager.gameManager.MP >= 80&&hasgone==false)
            {
                GameObject.Find("Canvas").GetComponent<WorldControl>().cando = false;
                op = GameManager.gameManager.loadscene("2.Map");
                GameObject.Find("Canvas").SendMessage("tiptwo", false);
                GameManager.gameManager.fishamount = (int)(0.8 * GameManager.gameManager.fishamount);
                GameManager.gameManager.trashamount = (int)(1.5 * GameManager.gameManager.trashamount);
                GameManager.gameManager.year += 5;
                hasgone = true;
                if(GameManager.gameManager.currentplace=="加州湾"&& GameManager.gameManager.dialogoneplayed==false)
                {
                    audioSources[1].Stop();
                    GameObject.Find("Canvas").SendMessage("showdialogone", op);
                    GameManager.gameManager.dialogoneplayed = true;
                    AudioClip clip = GameManager.gameManager.getline("离开加州湾");
                    audioSources[2].clip = clip;
                    audioSources[2].Play();
                }
                else
                    GameObject.Find("Canvas").SendMessage("becomeblack",op);
            }
             else if(GameManager.gameManager.MP <= 80)
            {
                GameObject.Find("Canvas").SendMessage("tip","Alice还没有为远行做好准备");
            }
        }
    }

    void lineplay()
    {
        if(GameManager.gameManager.currentplace!="加州湾"&&rigid.velocity.magnitude==maxspeed&&GameManager.gameManager.firstacc==false)
        {
            AudioClip clip = GameManager.gameManager.getline("在加州湾以外的地方第一次加速到上限");
            if (audioSources[2].isPlaying)
                GameManager.gameManager.toplay.Add(clip);
            else
            {
                audioSources[2].clip = clip;
                audioSources[2].Play();
            }
            waittime = 0;
            GameManager.gameManager.firstacc = true;
        }
        if (GameManager.gameManager.currentplace != "加州湾" && GameManager.gameManager.HP<=50 && GameManager.gameManager.firstreducehp == false)
        {
            AudioClip clip = GameManager.gameManager.getline("在加州湾以外的地方第一次受到生命值伤害");
            if (audioSources[2].isPlaying)
                GameManager.gameManager.toplay.Add(clip);
            else
            {
                audioSources[2].clip = clip;
                audioSources[2].Play();               
            }
            GameManager.gameManager.firstreducehp = true;
            waittime = 0;
        }
        if (GameManager.gameManager.currentplace != "加州湾" && GameManager.gameManager.HP <= 50 && GameManager.gameManager.firstreducehp == false)
        {
            AudioClip clip = GameManager.gameManager.getline("在加州湾以外的地方第一次受到生命值伤害");
            if (audioSources[2].isPlaying)
                GameManager.gameManager.toplay.Add(clip);
            else
            {
                audioSources[2].clip = clip;
                audioSources[2].Play();
            }
            GameManager.gameManager.firstreducehp = true;
            waittime = 0;
        }
        if (GameManager.gameManager.MP >= 30 && GameManager.gameManager.over30 == false)
        {
            AudioClip clip = GameManager.gameManager.getline("能量超过30");
            if (audioSources[2].isPlaying)
                GameManager.gameManager.toplay.Add(clip);
            else
            {
                audioSources[2].clip = clip;
                audioSources[2].Play();
            }
            GameManager.gameManager.over30 = true;
            waittime = 0;
        }
        if (waittime >= 10 && GameManager.gameManager.firstsonginjiazhou == false)
        {
            if (audioSources[0].isPlaying == false)
            {
                int index = (int)Random.Range(1, 5f);
                audioSources[0].clip = GameManager.gameManager.getclip(@"SFX/" + "Whale" + index.ToString());
                audioSources[0].Play();
            }
            StartCoroutine("jinggeline", "进入加州湾第一次唱响鲸歌");
        }
        if (GameManager.gameManager.MP >= 50 && GameManager.gameManager.over50 == false)
        {
            AudioClip clip = GameManager.gameManager.getline("能量超过50");
            if (audioSources[2].isPlaying)
                GameManager.gameManager.toplay.Add(clip);
            else
            {
                audioSources[2].clip = clip;
                audioSources[2].Play();                
            }
            GameManager.gameManager.over50 = true;
            waittime = 0;
        }               
    }
    IEnumerator jinggeline(string linename)
    {
        if (linename == "在除加州湾以外的地区触发鲸歌并且在加州湾触发过")
            GameManager.gameManager.secondsong = true;
        if (linename == "进入加州湾第一次唱响鲸歌")
            GameManager.gameManager.firstsonginjiazhou = true;
        AudioClip clip = GameManager.gameManager.getline(linename);
        yield return new WaitForSeconds(10);
        if (audioSources[2].isPlaying)
            GameManager.gameManager.toplay.Add(clip);
        else
        {
            audioSources[2].clip = clip;
            audioSources[2].Play();
        }
        waittime = 0;       
    }
    private void playcliplist()
    {
        if(audioSources[2].isPlaying==false&&GameManager.gameManager.toplay.Count>0)
        {
            audioSources[2].clip = GameManager.gameManager.toplay[0];
            audioSources[2].Play();
            waittime = 0;
            GameManager.gameManager.toplay.RemoveAt(0);
        }
    }
    private void playwait()
    {
        if(waittime>=120&&GameManager.gameManager.waitfirst==false)
        {
            AudioClip clip = GameManager.gameManager.getline("超过120秒没有语音则播放-1");
            if (audioSources[2].isPlaying)
                GameManager.gameManager.toplay.Add(clip);
            else
            {
                audioSources[2].clip = clip;
                audioSources[2].Play();               
            }
            waittime = 0;
            GameManager.gameManager.waitfirst = true;
        }
        if (waittime >= 120 && GameManager.gameManager.waitfirst == true&& GameManager.gameManager.waitsecond == false)
        {
            AudioClip clip = GameManager.gameManager.getline("超过120秒没有语音则播放-2");
            if (audioSources[2].isPlaying)
                GameManager.gameManager.toplay.Add(clip);
            else
            {
                audioSources[2].clip = clip;
                audioSources[2].Play();                
            }
            waittime = 0;
            GameManager.gameManager.waitsecond = true;
        }
        if (waittime >= 120 && GameManager.gameManager.waitfirst == true && GameManager.gameManager.waitsecond == true&& GameManager.gameManager.waitthird == false)
        {
            AudioClip clip = GameManager.gameManager.getline("超过120秒没有语音则播放-3");
            if (audioSources[2].isPlaying)
                GameManager.gameManager.toplay.Add(clip);
            else
            {
                audioSources[2].clip = clip;
                audioSources[2].Play();
            }
            waittime = 0;
            GameManager.gameManager.waitthird = true;
        }
        if (GameManager.gameManager.waitthird == true)
            waittime = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    private static GameManager gamemanager;
    public static GameManager gameManager
    {
        get
        {
            if (gamemanager == null)
                gamemanager = new GameManager();
            return gamemanager;
        }
    }
    public int HP=100;
    public int MP=20;
    public int fishamount;
    public int trashamount;
    public int year;
    public List<string> jiazhouwan = new List<string>(new string[]  { "alasijia", "jiazhouwaihai", "xiaweiyi"  });
    public List<string> alasijia = new List<string>(new string[] { "jiazhouwaihai", "bailinghai" });
    public List<string> jiazhouwaihai = new List<string>(new string[] { "alasijia", "xiaweiyi","beitaipingyang" });
    public List<string> xiaweiyi = new List<string>(new string[] { "beitaipingyang", "jiazhouwaihai" });
    public List<string> beitaipingyang = new List<string>(new string[] { "jiazhouwaihai", "bailinghai","xiaweiyi" });
    public List<string> bailinghai = new List<string>(new string[] { "alasijia", "beitaipingyang" });
    public List<string> isgone = new List<string>(new string[] { "jiazhouwan" });
    public List<string> currentplacelist = new List<string>(new string[] { "alasijia", "jiazhouwaihai", "xiaweiyi" });
    public string currentplace = "jiazhouwan";


    public void changehp(int hpchange)
    {
        HP += hpchange;
    }
    public void changemp(int mpchange)
    {
        MP += mpchange;
    }

    public AudioClip getclip(string name)
    {
        AudioClip clip = Resources.Load(@"Audios/" + name) as AudioClip;
        return clip;
    }
    public AsyncOperation loadscene(string name)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(name);
        op.allowSceneActivation = false;
        return op;
    }
}

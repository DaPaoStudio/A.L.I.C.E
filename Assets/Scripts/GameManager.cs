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
    public int HP = 100;
    public int maxHP = 100;
    public int MP = 20;
    public int maxMP = 100;
    public int fishamount = 200;
    public int trashamount = 200;
    public int year = 1992;
    public List<string> jiazhouwan = new List<string>(new string[] { "alasijia", "jiazhouwaihai", "xiaweiyi" });
    public List<string> alasijia = new List<string>(new string[] { "jiazhouwaihai", "bailinghai" });
    public List<string> jiazhouwaihai = new List<string>(new string[] { "alasijia", "xiaweiyi", "beitaipingyang" });
    public List<string> xiaweiyi = new List<string>(new string[] { "beitaipingyang", "jiazhouwaihai" });
    public List<string> beitaipingyang = new List<string>(new string[] { "jiazhouwaihai", "bailinghai", "xiaweiyi" });
    public List<string> bailinghai = new List<string>(new string[] { "alasijia", "beitaipingyang" });
    public List<string> isgone = new List<string>(new string[] { "jiazhouwan" });
    public List<string> currentplacelist = new List<string>(new string[] { "alasijia", "jiazhouwaihai", "xiaweiyi" });
    public string currentplace = "加州湾";
    public List<string> dialogone = new List<string>(new string[] { "哦……她会不会很孤独？没有人应和她的歌声", "如果向全世界发言却没有人回应算不上孤独的话",
                  "我很难想象孤独这个词是形容什么的,，死亡吗？","那或许孤独比死亡更加可怕吧"});
    public bool dialogoneplayed = false;
    public bool firstacc = false;
    public bool firsteattrash = false;
    public bool firstsonginjiazhou = false;
    public bool secondsong = false;
    public bool firstreducehp = false;
    public bool over30 = false;
    public bool over50 = false;
    public List<string> we = new List<string>(new string[] { "也许，等待另一只鲸的出现，需要的不只是时间而已", "游戏设计 | Game Design：Dapao、BoringFish、Thirteen\n" +
        "音乐音效 | Music&SFX: DaPao、Thirteen\n配音 | CV: Meredith、OliverK、Thirteen\n程序 | Program: BoringFish\n场景 | Art&Model: DaPao\n文案 | PlayWright: Thirteen",
         "指导老师 | Instructor：朱伟 WilsonZhu","A.L.I.C.E关爱委员会 荣誉出品"});


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
    public AudioClip getline(string name)
    {
        AudioClip clip = Resources.Load(@"Audios/Lines/" + name) as AudioClip;
        return clip;
    }
    public AsyncOperation loadscene(string name)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(name);
        op.allowSceneActivation = false;
        return op;
    }
    public string switchword(string a)
    {
        switch(a)
        {
            case "加州湾": { return "jiazhouwan"; }
            case "阿拉斯加湾": { return "alasijia"; }
            case "夏威夷海": { return "xiaweiyi"; }
            case "白令海": { return "bailinghai"; }
            case "加州外海": { return "jiazhouwaihai"; }
            case "北太平洋": { return "beitaipingyang"; }
        }
        return "";
    }
}

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

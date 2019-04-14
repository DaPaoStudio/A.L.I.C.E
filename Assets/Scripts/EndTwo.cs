using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndTwo : MonoBehaviour
{
    private GameObject black;
    private GameObject image;
    private GameObject image1;
    private GameObject text;
    private GameObject geqian;
    private AudioSource Audio;
    private string word1 = "也许长久的孤独，只是为了等待另一个同样的声音出现。\n就算发出的声音谁都不理解，也依然活着，唱着\n这份情感并非虚假。";
    private string word2 = "本游戏所叙述的故事根据真实事件改变：\n在现实当中，Alice的结局同样压抑——我们已经失去了Alice的踪迹，" +
        "\n不论是出于什么原因，是Alice死了，或是人类放弃了观察。\n不知何时能相见是比无法再相见更加残酷的刑罚，" +
        "\n这个孤独的歌唱家再也没有听众，\n但我们都知道，她依然会唱着。";
    private string word3 = "于是我们希望Alice不是孤独的，我们希望大海的仙境只是美好与浪漫。\n我们已经做了太多出格的事情。" +
        "\n我们对大海倾泻着不该倾泻的物件，于是大海回报我们不那么美好的故事。\n鲸歌也许难以解析，但我们都明白一点：" +
        "\n如果不能够停止对海洋的肆意迫害，我们终将无法再听到这神秘空灵的歌声。";
    private string word4 = "Thank You For Playing";
    // Start is called before the first frame update
    void Start()
    {
        black = transform.Find("black").gameObject;
        image = transform.Find("Image").gameObject;
        image1 = transform.Find("Image1").gameObject;
        text = transform.Find("Text").gameObject;
        geqian = transform.Find("geqian").gameObject;
        Audio = GetComponent<AudioSource>();
        StartCoroutine("End");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   /* IEnumerator end()
    {
        geqian.GetComponent<Text>().DOFade(1, 2f);
        yield return new WaitForSeconds(3);
        geqian.GetComponent<Text>().DOFade(0, 2f);
        yield return new WaitForSeconds(3);
        black.GetComponent<Image>().DOFade(0, 2f);
        yield return new WaitForSeconds(5);
        black.GetComponent<Image>().DOFade(1, 2f);
        yield return new WaitForSeconds(3);
        text.GetComponent<Text>().text = GameManager.gameManager.we[0];
        text.GetComponent<Text>().DOFade(1, 3f);
        yield return new WaitForSeconds(4);
        text.GetComponent<Text>().DOFade(0, 3f);
        yield return new WaitForSeconds(5);
        text.GetComponent<Text>().text = GameManager.gameManager.we[1];
        text.GetComponent<Text>().DOFade(1, 3f);
        yield return new WaitForSeconds(4);
        text.GetComponent<Text>().DOFade(0, 3f);
        yield return new WaitForSeconds(5);
        black.GetComponent<Image>().DOFade(0, 2f);
    }*/
    IEnumerator End()
    {
       // geqian.GetComponent<Text>().DOFade(1, 2f);
       // yield return new WaitForSeconds(4);
       // geqian.GetComponent<Text>().DOFade(0, 2f);
       // yield return new WaitForSeconds(5);
        Audio.Play();
        DOTween.To(() => GameObject.Find("Main Camera").GetComponent<AudioSource>().volume, x => GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = x, 0.25f, 3);
        foreach (string p in GameManager.gameManager.we)
        {
            text.GetComponent<Text>().text = p;
            text.GetComponent<Text>().DOFade(1, 3f);
            yield return new WaitForSeconds(5.5f);
            text.GetComponent<Text>().DOFade(0, 3f);
            yield return new WaitForSeconds(6.5f);
        }
        black.GetComponent<Image>().DOFade(0, 2f);
        yield return new WaitForSeconds(5);
        black.GetComponent<Image>().DOFade(1, 2f);
        yield return new WaitForSeconds(3);
        image1.SetActive(false);
        black.GetComponent<Image>().DOFade(0, 2f);
        yield return new WaitForSeconds(9);
        black.GetComponent<Image>().DOFade(1, 2f);
        yield return new WaitForSeconds(3);
        DOTween.To(() => GameObject.Find("Main Camera").GetComponent<AudioSource>().volume, x => GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = x, 1f, 3);
        //word
        text.GetComponent<Text>().text = word1;
        text.GetComponent<Text>().fontSize = 20;
        text.GetComponent<Text>().DOFade(1, 3f);
        yield return new WaitForSeconds(5);
        text.GetComponent<Text>().DOFade(0, 3f);
        yield return new WaitForSeconds(4);
        text.GetComponent<Text>().text = word2;
        text.GetComponent<Text>().fontSize = 20;
        text.GetComponent<Text>().DOFade(1, 3f);
        yield return new WaitForSeconds(8);
        text.GetComponent<Text>().DOFade(0, 3f);
        yield return new WaitForSeconds(4);
        text.GetComponent<Text>().text = word3;
        text.GetComponent<Text>().fontSize = 20;
        text.GetComponent<Text>().DOFade(1, 3f);
        yield return new WaitForSeconds(8);
        text.GetComponent<Text>().DOFade(0, 3f);
        yield return new WaitForSeconds(4);
        text.GetComponent<Text>().text = word4;
        text.GetComponent<Text>().fontSize = 30;
        text.GetComponent<Text>().DOFade(1, 3f);
        yield return new WaitForSeconds(10);
        Application.Quit();
    }
}

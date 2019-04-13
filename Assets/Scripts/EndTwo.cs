using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndTwo : MonoBehaviour
{
    private GameObject black;
    private GameObject image;
    private GameObject text;
    private GameObject geqian;
    // Start is called before the first frame update
    void Start()
    {
        black = transform.Find("black").gameObject;
        image = transform.Find("Image").gameObject;
        text = transform.Find("Text").gameObject;
        geqian = transform.Find("geqian").gameObject;
        StartCoroutine("end");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator end()
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
    }
}

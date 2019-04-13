using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndOne : MonoBehaviour
{
    private GameObject black;
    private GameObject image1;
    private GameObject image2;
    private GameObject image3;
    private GameObject text;
    private GameObject jingluo;
    // Start is called before the first frame update
    void Start()
    {
        black = transform.Find("black").gameObject;
        image1 = transform.Find("Image1").gameObject;
        image2 = transform.Find("Image2").gameObject;
        image3 = transform.Find("Image3").gameObject;
        text = transform.Find("Text").gameObject;
        jingluo = transform.Find("jingluo").gameObject;
        StartCoroutine("end");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator end()
    {
        jingluo.GetComponent<Text>().DOFade(1, 2f);
        yield return new WaitForSeconds(3);
        jingluo.GetComponent<Text>().DOFade(0, 2f);
        yield return new WaitForSeconds(3);
        black.GetComponent<Image>().DOFade(0, 2f);
        yield return new WaitForSeconds(5);
        black.GetComponent<Image>().DOFade(1, 2f);
        yield return new WaitForSeconds(3);
        text.GetComponent<Text>().text = GameManager.gameManager.we[0];
        text.GetComponent<Text>().DOFade(1, 3f);
        yield return new WaitForSeconds(4);
        text.GetComponent<Text>().DOFade(0, 3f);
        image1.SetActive(false);
        yield return new WaitForSeconds(4);
        black.GetComponent<Image>().DOFade(0, 2f);
        yield return new WaitForSeconds(5);
        black.GetComponent<Image>().DOFade(1, 2f);
        yield return new WaitForSeconds(3);
        text.GetComponent<Text>().text = GameManager.gameManager.we[1];
        text.GetComponent<Text>().DOFade(1, 3f);
        yield return new WaitForSeconds(4);
        text.GetComponent<Text>().DOFade(0, 3f);
        image2.SetActive(false);
        yield return new WaitForSeconds(4);
        black.GetComponent<Image>().DOFade(0, 2f);
    }
}

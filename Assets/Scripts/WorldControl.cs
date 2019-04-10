using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldControl : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.gameManager.fishamount =(int) 0.8* GameManager.gameManager.fishamount;
        GameManager.gameManager.trashamount = (int)1.5 * GameManager.gameManager.trashamount;
        GameManager.gameManager.year += 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

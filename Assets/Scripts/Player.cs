using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigid;
    private AudioSource[] audioSources;
    private float rotatespeed;
    private float maxspeed;
    private float movespeed;
    private bool slowdown = true;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        audioSources = GetComponents<AudioSource>();
        audioSources[1].clip = GameManager.gameManager.getclip(@"SFX/" + "beats");
    }

    // Update is called once per frame
    void Update()
    {
        movecontrol();
        audioplay();
        if (slowdown)
            rigid.velocity = Vector3.Lerp(rigid.velocity, transform.forward * movespeed, 0.8f);
    }

    void movecontrol()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Rotate(Vector3.right, rotatespeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Rotate(Vector3.right, -rotatespeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up, rotatespeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.W))
            transform.Rotate(Vector3.up, -rotatespeed * Time.deltaTime);
        if (Input.GetMouseButton(0))
        {
            rigid.velocity += transform.forward * 10 * Time.deltaTime;
            slowdown = false;
        }
        else
            slowdown = true;
        Vector3 angle = transform.eulerAngles;
        angle.x = Mathf.Clamp(angle.x,-70, 70);
        transform.eulerAngles = angle;
        float value = Mathf.Clamp(rigid.velocity.magnitude, 0, maxspeed);
        rigid.velocity = rigid.velocity * value / rigid.velocity.magnitude;
    }

    void audioplay()
    {
        if (GameManager.gameManager.HP <= 10)
            audioSources[1].Play();
        else
            audioSources[1].Stop();
        if (Input.GetMouseButtonDown(1))
        {
            int index = (int)Random.Range(0, 5f);
            audioSources[0].clip = GameManager.gameManager.getclip(@"SFX/" + "Whale" + index.ToString());
            audioSources[0].Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "fish")
            GameManager.gameManager.changemp(1);
        else if (collision.transform.tag == "trash")
            GameManager.gameManager.changehp((int)Random.Range(-5, -1));
        else if(collision.transform.tag == "wall")
        {
            AsyncOperation op = null;
            if (GameManager.gameManager.MP >= 80)
            {
                op = GameManager.gameManager.loadscene("Map");
                op.allowSceneActivation = true;
            }
        }
    }
}

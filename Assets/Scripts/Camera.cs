using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private GameObject player;
    private float rotationspeed;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        rotationspeed = 2.0f;
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rotate();
    }
    void rotate()
    {
        transform.RotateAround(player.transform.position, player.transform.up, Input.GetAxis("Mouse X") * rotationspeed);
        transform.LookAt(player.transform);
        offset = transform.position - player.transform.position;
    }
}

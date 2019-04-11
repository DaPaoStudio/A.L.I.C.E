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
        rotationspeed = 100.0f;
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rotate();
        transform.position = player.transform.position + offset;
    }
    void rotate()
    {
        //transform.RotateAround(player.transform.position, player.transform.up, Input.GetAxis("Mouse X") * rotationspeed);
        //transform.RotateAround(player.transform.position, transform.right, rotationspeed * Time.deltaTime);
        //transform.LookAt(player.transform);
        //offset = (transform.position - player.transform.position) * offset.magnitude / (transform.position - player.transform.position).magnitude;
        Quaternion quaternionx = Quaternion.AngleAxis( Input.GetAxis("Mouse X") * rotationspeed*Time.deltaTime, player.transform.up);
        Quaternion quaterniony = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotationspeed*Time.deltaTime, transform.right);
        offset = quaternionx * quaterniony * offset;
        transform.LookAt(player.transform);
    }
}

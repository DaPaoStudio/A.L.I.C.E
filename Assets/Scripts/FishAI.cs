using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    private Rigidbody Rigidbody;
    private float rotatetime;
    private float timer = 0;
    private float movespeed = 10f;
    private Vector3 nowforword;
    private float rotatespeed = 5;
    private float raydistance = 4;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        rotatetime = Random.Range(6f, 10f);
        nowforword = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=rotatetime)
        {
            rotate();
            timer = 0;
            rotatetime = Random.Range(6f, 10f);
        }
        transform.forward = Vector3.Lerp(transform.forward, nowforword, rotatespeed*Time.deltaTime);
        transform.eulerAngles = limitrotate(transform.eulerAngles);
        escape();
        if ((nowforword - transform.forward).magnitude <= 0.1f)
            transform.forward = nowforword;
        Rigidbody.velocity = transform.forward * movespeed * Time.deltaTime;    
    }

    void rotate()
    {
        Quaternion qx = Quaternion.AngleAxis(Random.Range(-70f, 70f), transform.up);
        Quaternion qy= Quaternion.AngleAxis(Random.Range(-60f, 60f), transform.right);
        nowforword = qx * qy * transform.forward;
        //Debug.Log("a");
    }

    void escape()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward,out hit,raydistance))
        {
            if (hit.transform.tag == "walld")
            {
                float anglevalue = Vector3.Angle(hit.transform.up, transform.forward);
                nowforword = Quaternion.AngleAxis(180 - 2 * anglevalue, transform.right) * transform.forward;
            }
            if (hit.transform.tag == "walll")
            {
                float anglevalue = Vector3.Angle(hit.transform.right, transform.forward);
                if(anglevalue>=90)
                    nowforword = Quaternion.AngleAxis(2 * anglevalue-360, transform.up) * transform.forward;
                else
                    nowforword = Quaternion.AngleAxis( 2 * anglevalue, transform.up) * transform.forward;
            }
        }
    }
    private Vector3 limitrotate(Vector3 angle)
    {
        angle.x -= 180;
        if (angle.x > 0)
            angle.x -= 180;
        else
            angle.x += 180;
        angle.x = Mathf.Clamp(angle.x, -70, 70);
        return angle;
    }
}

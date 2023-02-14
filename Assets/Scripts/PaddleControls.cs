using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControls : MonoBehaviour
{
    public static float paddleSpeed1 = 5.0f;
    public static float paddleSpeed2 = 5.0f;
    private float up1;
    private float down1;
    private float up2;
    private float down2;
    private GameObject _p1;
    private GameObject _p2;
    private Rigidbody rb1;
    private Rigidbody rb2;
    private string disable = "";
    // Start is called before the first frame update
    void Start()
    {
        _p1 = GameObject.Find("Paddle1");
        _p2 = GameObject.Find("Paddle2");
        up1 = paddleSpeed1;
        down1 = -paddleSpeed1;
        up2 = paddleSpeed2;
        down2 = -paddleSpeed2;
        rb1 = _p1.GetComponent<Rigidbody>();
        rb2 = _p2.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        up1 = paddleSpeed1;
        down1 = -paddleSpeed1;
        up2 = paddleSpeed2;
        down2 = -paddleSpeed2;
        
        disable = PowerUps.disable;
        
        if (Input.GetKey(KeyCode.W) && disable != "left") 
        {
            rb1.velocity = new Vector3(0.0f, 0.0f, up1);
            //Debug.Log("up1: " + up1);
        }
        if (Input.GetKey(KeyCode.S) && disable != "left")
        {
            rb1.velocity = new Vector3(0.0f, 0.0f, down1);
            //Debug.Log("down1: " + down1);
        }
        if (Input.GetKey(KeyCode.UpArrow) && disable != "right")
        {
            rb2.velocity = new Vector3(0.0f, 0.0f, up2);
            //Debug.Log("up2: " + up2);
        }
        if (Input.GetKey(KeyCode.DownArrow) && disable != "right")
        {
            rb2.velocity = new Vector3(0.0f, 0.0f, down2);
            //Debug.Log("down2: " + down2);
        }
    }
}

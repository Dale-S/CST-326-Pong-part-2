using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUps : MonoBehaviour
{
    //Public variables
    public static string disable = "";

    //Game objects
    public GameObject mid;
    public GameObject bonus;
    public GameObject pwrUp;
    private GameObject fBall;
    private GameObject fP1;
    private GameObject fP2;
    private GameObject fW1;
    private GameObject fW2;
    private GameObject ball;
    private GameObject P1;
    private GameObject P2;
    private GameObject W1;
    private GameObject W2;
    private GameObject s1;
    private GameObject s2;



    //Initiate renderer values
    private MeshRenderer mWall1;
    private MeshRenderer mWall2;
    private MeshRenderer mBall;
    private MeshRenderer mP1;
    private MeshRenderer mP2;
    private MeshRenderer mFwall1;
    private MeshRenderer mFwall2;
    private MeshRenderer mFball;
    private MeshRenderer mFp1;
    private MeshRenderer mFp2;
    private MeshRenderer mPwrUp;
    private BoxCollider cPwrUp;
    private MeshRenderer mS1;
    private BoxCollider cS1;
    private MeshRenderer mS2;
    private BoxCollider cS2;
    private TrailRenderer ballTrail;

    //Initiate sound values;
    public AudioSource fBack;
    public AudioClip theWorld;
    public AudioClip endWorld;
    public AudioClip accel;
    private AudioSource shieldS1;
    private AudioSource shieldS2;

    //Powerup variables
    private bool wOver;
    private bool s1Over;
    private bool s2Over;
    private bool aOver;
    private bool pwrOver;
    private Vector3 position;
    private int item = 1;
    private int timer = 0;
    private float powerY = 0;
    private int shield1 = 0;
    private int shield2 = 0;
    private int s1Time = 0;
    private int s2Time = 0;
    private string lastHit;
    public TextMeshProUGUI p1Shield;
    public TextMeshProUGUI p2Shield;
    private float movementZ = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        wOver = false;
        aOver = false;
        s1Over = true;
        s2Over = true;
        pwrOver = true;
        mWall1 = GameObject.Find("Top").GetComponent<MeshRenderer>();
        mWall2 = GameObject.Find("Bottom").GetComponent<MeshRenderer>();
        mBall = GameObject.Find("Ball").GetComponent<MeshRenderer>();
        ballTrail = GameObject.Find("Trail").GetComponent<TrailRenderer>();
        mP1 = GameObject.Find("Paddle1").GetComponent<MeshRenderer>();
        mP2 = GameObject.Find("Paddle2").GetComponent<MeshRenderer>();
        mFwall1 = GameObject.Find("fakeTop").GetComponent<MeshRenderer>();
        mFwall2 = GameObject.Find("fakeBottom").GetComponent<MeshRenderer>();
        mFball = GameObject.Find("fakeBall").GetComponent<MeshRenderer>();
        mFp1 = GameObject.Find("fakePaddle1").GetComponent<MeshRenderer>();
        mFp2 = GameObject.Find("fakePaddle2").GetComponent<MeshRenderer>();
        fBack = GameObject.Find("fakeBackground").GetComponent<AudioSource>();
        mid = GameObject.Find("Midline");
        fBall = GameObject.Find("fakeBall");
        fP1 = GameObject.Find("fakePaddle1");
        fP2 = GameObject.Find("fakePaddle2");
        fW1 = GameObject.Find("fakeTop");
        fW2 = GameObject.Find("fakeBottom");
        ball = GameObject.Find("Ball");
        P1 = GameObject.Find("Paddle1");
        P2 = GameObject.Find("Paddle2");
        W1 = GameObject.Find("Top");
        W2 = GameObject.Find("Bottom");
        s1 = GameObject.Find("Shield1");
        s2 = GameObject.Find("Shield2");
        cPwrUp = pwrUp.GetComponent<BoxCollider>();
        mPwrUp = pwrUp.GetComponent<MeshRenderer>();
        cS1 = s1.GetComponent<BoxCollider>();
        mS1 = s1.GetComponent<MeshRenderer>();
        cS2 = s2.GetComponent<BoxCollider>();
        mS2 = s2.GetComponent<MeshRenderer>();
        shieldS1 = s1.GetComponent<AudioSource>();
        shieldS2 = s2.GetComponent<AudioSource>();
    }

    IEnumerator wait(int x)
    {
        yield return new WaitForSeconds(x);
        Debug.Log(x + " seconds have passed");
        if (item == 1)
        {
            wOver = true;
        }
        if (aOver == false)
        {
            aOver = true;
            pwrOver = true;
            timer = 0;
        }
    }

    void TheWorld()
    {
        fBall.transform.position = ball.transform.position;
        fP1.transform.position = P1.transform.position;
        fP2.transform.position = P2.transform.position;

        if (Ball.lastHit == "right")
        {
            disable = "left";
        }

        if (Ball.lastHit == "left")
        {
            disable = "right";
        }
        mid.SetActive(false);
        mWall1.enabled = false;
        mWall2.enabled = false;
        mBall.enabled = false;
        mP1.enabled = false;
        mP2.enabled = false;
        mFwall1.enabled = true;
        mFwall2.enabled = true;
        mFball.enabled = true;
        mFp1.enabled = true;
        mFp2.enabled = true;
        bonus.SetActive(false);
        cPwrUp.enabled = false;
        mPwrUp.enabled = false;
        ballTrail.enabled = false;
        fBack.clip = theWorld;
        fBack.Play();
        StartCoroutine(wait(3));
    }

    void Shield()
    {
        if (lastHit == "left")
        {
            if (shield1 != 1)
            {
                p1Shield.enabled = true;
                Debug.Log("P1 shield given");
                shield1 = 1;
            }
        }
        if (lastHit == "right")
        {
            if (shield2 != 1)
            {
                p2Shield.enabled = true;
                Debug.Log("P2 shield given");
                shield2 = 1;
            }
        }
    }

    void acceleration()
    {
        aOver = false;
        pwrOver = false;
        if (lastHit == "left")
        { 
            PaddleControls.paddleSpeed1 = 12.0f;
        }

        if (lastHit == "right")
        {
            PaddleControls.paddleSpeed2 = 12.0f;
        }

        StartCoroutine(wait(6));
    }

    void OnCollisionEnter(Collision collide)
    {
        item = Random.Range(1, 4);
        if (collide.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Item: " + item);
            cPwrUp.enabled = false;
            mPwrUp.enabled = false;
            if (item == 1)
            {
                TheWorld();
            }
            if (item == 2)
            {
                Shield();
                pwrOver = true;
            }
            if (item == 3)
            {
                acceleration();
                fBack.clip = accel;
                fBack.Play();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pwrUp.transform.position.z >= 9.0f)
        {
            movementZ = movementZ * -1.0f;
        }
        if (pwrUp.transform.position.z <= -9.0f)
        {
            movementZ = movementZ * -1.0f;
        }

        float tempZ = pwrUp.transform.position.z;
        pwrUp.transform.position = new Vector3(pwrUp.transform.position.x, pwrUp.transform.position.y, tempZ += movementZ);
        lastHit = Ball.lastHit;
        powerY += 1.0f;
        
        pwrUp.transform.rotation = Quaternion.Euler(0.0f, powerY, 0.0f);
        fW1.transform.localScale = W1.transform.localScale;
        fW2.transform.localScale = W2.transform.localScale;
        if (timer == 820) //1010 
        {
            Debug.Log("pwrOver: " + pwrOver);
            if (pwrOver == true)
            {
                int newX = Random.Range(-18, 19);
                int newZ = Random.Range(-8, 9);
                cPwrUp.enabled = true;
                mPwrUp.enabled = true;
                position = new Vector3(newX, 0.0f, newZ);
                pwrUp.transform.position = position;
                pwrOver = false;
            }
            timer = 0;
        }

        timer += 1;

        if (wOver)
        {
            bonus.SetActive(true);
            fBack.clip = endWorld;
            fBack.Play();
            mWall1.enabled = true;
            mid.SetActive(true);
            mWall2.enabled = true;
            mBall.enabled = true;
            mP1.enabled = true;
            mP2.enabled = true;
            ballTrail.enabled = true;
            mFwall1.enabled = false;
            mFwall2.enabled = false;
            mFball.enabled = false;
            mFp1.enabled = false;
            mFp2.enabled = false;
            disable = "";
            wOver = false;
            timer = 0;
            pwrOver = true;
        }

        if (Input.GetKey(KeyCode.F))
        {
            if (shield1 == 1)
            {
                shieldS1.Play();
                p1Shield.enabled = false;
                shield1 = 0;
                s1Over = false;
            }
        }

        if (!s1Over)
        {
            mS1.enabled = true;
            cS1.enabled = true;
            s1Time += 1;
            if (s1Time > 90)
            {
                s1Over = true;
                s1Time = 0;
            }
        }
        
        if (Input.GetKey(KeyCode.RightControl))
        {
            if (shield2 == 1)
            {
                shieldS2.Play();
                p2Shield.enabled = false;
                shield2 = 0;
                s2Over = false;
            }
        }

        if (!s2Over)
        {
            mS2.enabled = true;
            cS2.enabled = true;
            s2Time += 1;
            if (s2Time > 90)
            {
                s2Over = true;
                s2Time = 0;
            }
        }
        

        if (s1Over)
        {
            mS1.enabled = false;
            cS1.enabled = false;
        }
        
        if (s2Over)
        {
            mS2.enabled = false;
            cS2.enabled = false;
        }

        if (aOver)
        {
            PaddleControls.paddleSpeed1 = 7.0f;
            PaddleControls.paddleSpeed2 = 7.0f;
        }
    }
}

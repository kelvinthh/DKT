using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nevigateScript : MonoBehaviour {

    public GameObject left;
    public GameObject right;
    public GameObject retrn;
    public GameObject retrn2;
    [SerializeField]
    private Transform position1;
    [SerializeField]
    private Transform position2;
    [SerializeField]
    private Transform position3;
    private Transform targetpos;
    public int speed;
    public static bool move;
    public int num;
    public float movtime;

    // Use this for initialization
    void Start () {
        targetpos = position1;
        ReticleTimer.timelength = 1;
        movtime = 3;
	}


    public void triggered()
    {
        ReticleTimer.selected = true;
        movtime = 3;
    }

    public void nottriggered()
    {
        ReticleTimer.selected = false;
        ReticleTimer.timelength = 1f;
       
    }
    private void Update()
    {
        Debug.Log(move);
        Debug.Log(ReticleTimer.timelength);
      
        if (ReticleTimer.selected == true)
        {

            ReticleTimer.timelength = ReticleTimer.timelength - Time.deltaTime;

            if (ReticleTimer.timelength <= 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetpos.position, speed * Time.deltaTime);
                ReticleTimer.selectMade = true;
                ReticleTimer.selected = false;
                ReticleTimer.timelength = 1f;
                move = true;
            }
        }
        Movemment();
    }

    public void Sel(Collider other)
    {
        
        if (other.CompareTag("Left"))
        {
            MoveLeft();
            num = 1;
        }
        if (other.CompareTag("Right"))
        {
            MoveRight();
            num = 2;
        }
        if (other.CompareTag("Ret1"))
        {
            Return();
            num = 3;
        }
       
    }

  

    public void MoveLeft()
    {
       
        targetpos = position2;
       
        triggered();
    }

    public void MoveRight()
    {
        targetpos = position3;
       
        triggered();
    }

    public void Return()
    {
        targetpos = position1;
    
        triggered();
    }

    public void Movemment() {

        if (move == true)
        {
            movtime = movtime - Time.deltaTime;
            if (movtime > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetpos.position, speed * Time.deltaTime);
            }
            else move = false;
        }
        if (movtime < 0.1 && movtime > 0)
        {
            if (num == 1)
            {
                right.SetActive(false);
                left.SetActive(false);
                retrn.SetActive(true);
                retrn2.SetActive(false);
            }
            if (num == 2)
            {
                right.SetActive(false);
                left.SetActive(false);
                retrn.SetActive(false);
                retrn2.SetActive(true);
            }
            if (num == 3)
            {
                right.SetActive(true);
                left.SetActive(true);
                retrn.SetActive(false);
                retrn2.SetActive(false);
            }
        }
    }
}

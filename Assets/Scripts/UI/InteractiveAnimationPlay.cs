using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveAnimationPlay : MonoBehaviour, IInteractiveObject {

    [SerializeField] private bool isShortAction = false;

    private float gazeTimer = 3.0f;
    [SerializeField] private string animationName = "";
    Animator anim;

    private bool gazingAt = false;
    public void Action()
    {

    }

    public void GazeEnter()
    {
        anim.Play("buttonImpact");
    }

    public void GazeExit()
    {
        //anim.Play("New State");
    }

    public void GazeTimer()
    {
    }

    // Use this for initialization
    void Awake () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}
    void Start()
    {
        if (isShortAction)
        {
            gazeTimer = 1.5f;
        }
    }
}

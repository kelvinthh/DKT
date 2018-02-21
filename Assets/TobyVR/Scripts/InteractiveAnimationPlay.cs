using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveAnimationPlay : MonoBehaviour, IInteractiveObject {

    [SerializeField] private float gazeTimer = 1.0f;

    Animator anim;

    private bool gazingAt = false;
    public void Action()
    {

    }

    public void GazeEnter()
    {
        anim.Play("button enlarge");
    }

    public void GazeExit()
    {
        anim.Play("Entry");
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
}

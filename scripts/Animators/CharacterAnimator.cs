using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

    NavMeshAgent agent;
    Animator animator;

    const float smoothTime = .1f;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float speedPercent = agent.velocity.magnitude / agent.speed;

        if (speedPercent != 0)
        {
            agent.speed = 5f;
            animator.SetBool("moving", true);
        }
        else
        {
            agent.speed = 0f;
            animator.SetBool("moving", false);
        }
        
        
	}
}

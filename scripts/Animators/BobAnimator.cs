using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BobAnimator : MonoBehaviour {

    NavMeshAgent agent;
    Animator animator;
    BobController player;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = GetComponentInParent<BobController>();
        player.OnAttack += OnAttack;
    }

    void OnAttack()
    {
        animator.SetTrigger("attack");
    }
}

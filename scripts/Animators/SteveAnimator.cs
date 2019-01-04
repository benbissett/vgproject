using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SteveAnimator : MonoBehaviour {

    NavMeshAgent agent;
    Animator animator;
    SteveController player;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = GetComponentInParent<SteveController>();
        player.OnAttack += OnAttack;
    }

    void OnAttack()
    {
        animator.SetTrigger("attack");
    }
}

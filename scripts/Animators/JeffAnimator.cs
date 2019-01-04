using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JeffAnimator : MonoBehaviour
{

    NavMeshAgent agent;
    Animator animator;
    JeffController player;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = GetComponentInParent<JeffController>();
        player.OnAttack += OnAttack;
    }

    void OnAttack()
    {
        animator.SetTrigger("attack");
    }
}

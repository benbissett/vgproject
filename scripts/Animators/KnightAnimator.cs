using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnightAnimator : MonoBehaviour {

    NavMeshAgent agent;
    Animator animator;
    Player2Controller player;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = GetComponentInParent<Player2Controller>();
        player.OnAttack += OnAttack;
    }

    void OnAttack()
    {
        animator.SetTrigger("attack");
    }
}

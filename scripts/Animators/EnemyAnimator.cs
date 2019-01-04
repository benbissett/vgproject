using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour {

    NavMeshAgent agent;
    Animator animator;
    Enemy2Controller enemy;

    const float smoothTime = .1f;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        enemy = GetComponentInParent<Enemy2Controller>();
        enemy.OnAttack += OnAttack;
        enemy.OnDragonAttack += OnDragonAttack;
        enemy.OnDragonHeal += OnDragonHeal;
        enemy.OnMultiAttack += OnMultiAttack;
    }

    // Update is called once per frame
    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, smoothTime, Time.deltaTime);
    }

    void OnAttack()
    {
        animator.SetTrigger("attack");
    }

    void OnDragonAttack()
    {
        animator.SetTrigger("Claw Attack");
    }

    void OnDragonHeal()
    {
        animator.SetTrigger("Scream");
    }

    void OnMultiAttack()
    {
        animator.SetTrigger("Basic Attack");
    }
}

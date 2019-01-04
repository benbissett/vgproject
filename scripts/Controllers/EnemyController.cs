using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 10f;
    float timeToChangeDirection;
    public float fieldOfViewAngle = 90f;
    float randTime;
    public bool chasing;

    Transform target;

    Vector3 start;
    Vector3 end;
    Vector3 dest;

    NavMeshAgent agent;
    RaycastHit hit;

    // Use this for initialization
    void Start () {
        target = PlayerManager.instance.player.transform;
        start = transform.position;
        int randPos = Random.Range(0, 2);
        if (randPos == 0)
        {
            end = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
        }
        else
        {
            end = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
        }
        
        agent = GetComponent<NavMeshAgent>();
        dest = end;
        agent.destination = end;

        randTime = Random.Range(3f, 6f);
        timeToChangeDirection = randTime;

        agent.speed = Random.Range(1f, 2f);
    }
	
	// Update is called once per frame
	void Update () {
        timeToChangeDirection -= Time.deltaTime;
        Vector3 direction = target.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        if (angle < fieldOfViewAngle * 0.5f) { 
            if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, lookRadius))
            {
                if(hit.collider.gameObject.tag == "Player")
                {
                    chasing = true;
                    agent.SetDestination(target.transform.position);
                    
                }

                else
                {
                    chasing = false;
                    if (timeToChangeDirection <= 0)
                    {
                        if (dest == end)
                        {
                            dest = start;
                        }
                        else
                        {
                            dest = end;
                        }
                        agent.destination = dest;
                        timeToChangeDirection = randTime;
                    }
                }
            }
        }

        else
        {
            chasing = false;
            if (timeToChangeDirection <= 0)
            {
                if (dest == end)
                {
                    dest = start;
                }
                else
                {
                    dest = end;
                }
                agent.destination = dest;
                timeToChangeDirection = 5f;
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

    }
}

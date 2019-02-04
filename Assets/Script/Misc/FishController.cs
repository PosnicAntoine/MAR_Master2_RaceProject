using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishController : MonoBehaviour
{

    public float wanderingTime;
    public float wanderingRadius;
    public NavMeshAgent fishAgent;

    private float timer;


    // Use this for initialization
    void OnEnable()
    {
        timer = wanderingTime;
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderingTime)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderingRadius, -1);
            fishAgent.SetDestination(newPos);
            timer = 0;
        }


    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}

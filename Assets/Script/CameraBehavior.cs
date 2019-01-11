using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject player;
    public float damping = 10;
    public float height = 0.2f;
    public float distance = 0.5f;

    void LateUpdate()
    {
        Vector3 desiredPosition = player.transform.TransformPoint(0,-distance,height);
        if (desiredPosition.y > 0.1)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
            transform.LookAt(player.transform);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(desiredPosition.x, 1f, desiredPosition.z), Time.deltaTime * damping);
            transform.LookAt(player.transform);
        }
    }
}

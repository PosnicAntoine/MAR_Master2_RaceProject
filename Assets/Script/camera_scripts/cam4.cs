using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam4 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.gamemodeReplay)
        {
            GameManager.Instance.Enable4();
        }
    }

    private void Update()
    {
        transform.LookAt(GameManager.Instance.car.transform);
    }
}

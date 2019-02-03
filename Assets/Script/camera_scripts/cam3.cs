using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam3 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.gamemodeReplay)
        {
            GameManager.Instance.Enable3();
        }
    }

}

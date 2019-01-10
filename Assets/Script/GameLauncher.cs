using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject playerutils;
    private void Awake()
    {
       
    }

    public void PlayGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayRace();
            panel.SetActive(false);
            playerutils.SetActive(true);

        }
    }

    public void ReplayRace()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RePlay();
            panel.SetActive(false);
        }
    }

    public void GhostRace()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GhostRace();
            panel.SetActive(false);
        }
    }
}

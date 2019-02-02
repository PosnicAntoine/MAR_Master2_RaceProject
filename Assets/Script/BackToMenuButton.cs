using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenuButton : MonoBehaviour
{

    public GameObject MenuPanel;
    public GameObject ParentPanel;

    public GameObject PlayerUtils;

    public void BackToMenuAction(){
        MenuPanel.SetActive(true);
        PlayerUtils.SetActive(false);
        ParentPanel.SetActive(false);
    }
}

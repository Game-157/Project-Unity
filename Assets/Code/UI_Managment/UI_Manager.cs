using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [Header("UI Menus")]
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;

    public void Start()
    {
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
    }

    public void ShowWinMenu()
    {
        winMenu.SetActive(true);
    }

    public void ShowLoseMenu()
    {
        loseMenu.SetActive(true);
    }
}

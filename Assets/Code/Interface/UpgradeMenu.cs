using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }
}

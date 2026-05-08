using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetProgress(float progress)
    {
        slider.value = Mathf.Clamp01(progress);
    }
}

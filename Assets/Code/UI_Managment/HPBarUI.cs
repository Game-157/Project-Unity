using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private Transform target;
    private IHealthComponent health;

    public void Initialize(Transform target, IHealthComponent health)
    {
        this.target = target;
        this.health = health;
    }

    private void Update()
    {
        if (target == null || health == null)
            return;

        // позиция над персонажем
        Vector3 worldPos = target.position + Vector3.up * 5f;

        // смещение НАЗАД от камеры
        worldPos -= Camera.main.transform.forward * 1.5f;

        // перевод в экранные координаты
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        transform.position = screenPos;

        // обновление HP
        slider.value = health.Health / health.MaxHealth;
    }
}

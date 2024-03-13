using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image healthBarFill;

    private void OnEnable()
    {
        _character.onTakeDamage += UpdateHealthBar;
        _character.onHeal += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _character.onTakeDamage -= UpdateHealthBar;
        _character.onHeal -= UpdateHealthBar;
    }

    private void Start()
    {
        SetNameText(_character.DisplayName);
    }

    void SetNameText(string text)
    {
        nameText.text = text;
    }

    private void UpdateHealthBar()
    {
        float healthPercent = (float) _character.CurHp / _character.MaxHp;
        healthBarFill.fillAmount = healthPercent;
    }
}

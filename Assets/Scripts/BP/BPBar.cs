using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BPBar : MonoBehaviour
{
    public BPSystem bpSystem;
    [SerializeField] 
    private Image imgBar;
    private float _totalLife;
    public float GetTotalLife => _totalLife;

    private float _targetLife;
    private float _currentLife;
    public float GetCurrentLife => _currentLife;
    private float _lerpIndex;
    [SerializeField] 
    private float fillSpeed;

    private bool _filling;
    public UnityEvent onFilledEvent;

    public void StartUp()
    {
        _totalLife = bpSystem.totalBP;
        imgBar.fillAmount = 1.0f;
        _targetLife = _totalLife;
        _currentLife = _totalLife;
        _lerpIndex = 0.0f;
        
        _filling = false;
    }

    private void Update()
    {
        if (_filling)
        {
            _lerpIndex = Mathf.Min(_lerpIndex + fillSpeed * Time.deltaTime, 1.0f);
            _currentLife = Mathf.Lerp(_currentLife, _targetLife, _lerpIndex);
            imgBar.fillAmount = _currentLife / _totalLife;

            if (Mathf.Abs(_currentLife - _targetLife) <= 0.0f + Mathf.Epsilon)
            {
                _currentLife = _targetLife;
                _filling = false;
                _lerpIndex = 0;
                onFilledEvent.Invoke();
            }
        }
        UpdateBar();
    }

    public void UpdateBar()
    {
        _targetLife = bpSystem.currentBP;
        _filling = true;
    }

    public void AddToSystem(int add)
    {
        bpSystem.AddBP(add);
    }
}
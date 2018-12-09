using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    [SerializeField] private Slider _powerGauge;
    [SerializeField] private Text _timeTextLabel;

    private float _powerGaugeSpeed = 0.015f;
    private bool _shouldPowerGaugeMove = true;
    private bool _isPowerGaugeIncreasing = true;

    private float timeSinceStart = 0.0f;

    public void ResetGauge(){
        _powerGauge.value = _powerGauge.maxValue / 2;
    }

    public float GetPowerGaugeValue()
    {
        return _powerGauge.value;
    }

    public void SetPowerGauge(bool shouldStart){
        _shouldPowerGaugeMove = shouldStart;
        _powerGauge.gameObject.SetActive(shouldStart);
    }

    public float GetAccuracyForCurrentGaugeValue()
    {
        int powerGaugeValue = (int)(GetPowerGaugeValue() * 100);

        float result = 0.0f;

        if (powerGaugeValue >= 45 && powerGaugeValue < 55)
        {
            result = 100f;
        }
        else if (powerGaugeValue >= 35 && powerGaugeValue < 45 || powerGaugeValue >= 55 && powerGaugeValue < 65)
        {
            result = 90f;
        }
        else if (powerGaugeValue >= 25 && powerGaugeValue < 35 || powerGaugeValue >= 65 && powerGaugeValue < 75)
        {
            result = 80f;
        }
        else if (powerGaugeValue >= 15 && powerGaugeValue < 25 || powerGaugeValue >= 75 && powerGaugeValue < 85)
        {
            result = 75;
        }
        else if (powerGaugeValue >= 0 && powerGaugeValue < 15 || powerGaugeValue >= 85 && powerGaugeValue < 100)
        {
            result = 70f;
        }

        return result;

    }

    // Use this for initialization
    void Start () {
        _powerGauge.gameObject.SetActive(false);
	}
	
    private void HandlePowerGauge(){

        if(_isPowerGaugeIncreasing){
            _powerGauge.value += _powerGaugeSpeed;
            if(_powerGauge.value >= _powerGauge.maxValue){
                _isPowerGaugeIncreasing = false;
            }
        }
        else{
            _powerGauge.value -= _powerGaugeSpeed;
            if (_powerGauge.value <= _powerGauge.minValue)
            {
                _isPowerGaugeIncreasing = true;
            }
        }

    }

	// Update is called once per frame
	void FixedUpdate () {

        if(_shouldPowerGaugeMove){
            HandlePowerGauge();
        }

    }

    private void Update()
    {
        timeSinceStart += Time.deltaTime;
        var minutes = (int) timeSinceStart / 60;
        var seconds = (int)timeSinceStart - minutes * 60;
        _timeTextLabel.text = minutes + ":" + seconds.ToString();
    }
}

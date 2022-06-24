using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private Transform _plant;

    [SerializeField, Range(0.01f, 0.3f)]
    private float _changeScaleValue = 0.1f;

    [SerializeField, Range(0.01f, 5f)]
    private float _tickTime = 1f;
    private float _progress;

    private Vector3 _changeScaleVector;

    private Vector3 _startScale;
    private Vector3 _startPos;

    void Start()
    {
        _startPos = _plant.localPosition;
        _startScale = _plant.localScale;
        _changeScaleVector = new Vector3(0f, _changeScaleValue, 0f);
    }
    
    void Update()
    {
        _progress += Time.deltaTime;
        while (_progress >= _tickTime)
        {
            if (_plant.localScale.y < 1)
            {
                Vector3 tempScale = _plant.localScale + _changeScaleVector;
                Vector3 offset = _plant.localPosition + _changeScaleVector / 2f;
                _plant.localScale = tempScale;
                _plant.localPosition = offset;
                _progress -= _tickTime;
            }
            else
            {

                _plant.localScale = _startScale;
                _plant.localPosition = _startPos;

                _progress -= _tickTime;
            }
        }
    }
}

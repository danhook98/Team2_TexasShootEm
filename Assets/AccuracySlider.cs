using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class AccuracySlider : MonoBehaviour
{
    [SerializeField] private Slider accuracySlider;
    private float _accuracyScore;
    private float _accuracySliderValue;
    private float _valueChange = 0.05f;

    void Update()
    {
            if (Mathf.Approximately(accuracySlider.value, accuracySlider.maxValue))
            {
                _valueChange = -0.05f;
            }

            if (Mathf.Approximately(accuracySlider.value, accuracySlider.minValue))
            {
                _valueChange = 0.05f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _valueChange = 0;
                _accuracySliderValue = Mathf.Abs(accuracySlider.value); // Turn negatives into positives
                _accuracySliderValue = 1 - _accuracySliderValue; 
                

                _accuracyScore = _accuracySliderValue;
                Debug.Log(_accuracyScore);
            }

            // For Testing
            if (Input.GetKeyDown(KeyCode.T))
            {
                _valueChange = 0.05f;
            }
    }

    void FixedUpdate()
    {
        accuracySlider.value += _valueChange;
    }
}

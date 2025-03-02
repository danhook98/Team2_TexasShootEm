using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.Collections.Generic;

public class AccuracySlider : MonoBehaviour
{
    [SerializeField] private Slider accuracySlider;
    [SerializeField] private AnimationCurve lerpCurve;
    
    [SerializeField] private float baseValueChange = 0.05f;
    
    private float _accuracyScore;
    private float _accuracySliderValue;
    private float _valueChange; 
    private float _sliderSpeed;
    private float _difficultyMultiplier = 1f;
    private bool _isSliderPaused;
    
    private Dictionary<string, float> scoreResults = new Dictionary<string, float>();
    private float _perfectScoreRange = 0.9f;
    private float _goodScoreRange = 0.6f;
    private float _okayScoreRange = 0.3f;
    private float _badScoreRange;
    private float _missScoreRange;
    
    private void Start()
    {
        _valueChange = baseValueChange * _difficultyMultiplier;
        _isSliderPaused = false;
        AddNewScoreResults();
    }
    
    private void Update()
    {
        if (!_isSliderPaused)
        {
            CalculateValueChange();
        }
        
        // For scoring, score ranges from 0 to 1, closer to the middle of the bar closer to a value of 1.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isSliderPaused = true;
            
            // Calculate score when slider value is above or below the halfway mark.
            if (accuracySlider.value > 0.5f)
            {
                _accuracySliderValue = accuracySlider.value;
                _accuracySliderValue -= 0.5f;
                _accuracySliderValue = 1 - (2 * _accuracySliderValue);
            }

            if (accuracySlider.value < 0.5f)
            {
                _accuracySliderValue = accuracySlider.value;
                _accuracySliderValue = 2 * _accuracySliderValue;
            }
            
            _accuracyScore = _accuracySliderValue;
            Debug.Log(_accuracyScore);
        }

        // For Testing
        if (Input.GetKeyDown(KeyCode.T))
        {
            ResetAccuracySlider();
        }
    }

    private void FixedUpdate()
    {
        accuracySlider.value = Mathf.Clamp(accuracySlider.value + _sliderSpeed, -1f, 1f);
    }
    
    public void SetDifficulty(float newMultiplier)
    {
        _difficultyMultiplier = newMultiplier;
        _valueChange = baseValueChange * _difficultyMultiplier; 
    }

    private void CalculateValueChange()
    {
        float pingPongValue = Mathf.PingPong(Time.time * 0.5f, accuracySlider.maxValue);
        _valueChange = lerpCurve.Evaluate(pingPongValue);
        accuracySlider.value = _valueChange;
    }

    public void ResetAccuracySlider()
    {
        _isSliderPaused = false;
    }

    private void AddNewScoreResults()
    {
        scoreResults.Add("Perfect!", _perfectScoreRange);
        scoreResults.Add("Good!", _goodScoreRange);
        scoreResults.Add("Okay!", _okayScoreRange);
        scoreResults.Add("Bad!", _badScoreRange);
        scoreResults.Add("Miss!", _missScoreRange);
    }
}
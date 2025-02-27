using UnityEngine;
using UnityEngine.UI;

public class AccuracySlider : MonoBehaviour
{
    [SerializeField] private Slider accuracySlider;
    [SerializeField] private AnimationCurve lerpCurve;
    
    [SerializeField] private float BaseValueChange = 0.05f;
    
    private float _accuracyScore;
    private float _accuracySliderValue;
    private float _valueChange; 
    private float _sliderSpeed;
    private float _difficultyMultiplier = 1f;

    private void Start()
    {
        _valueChange = BaseValueChange * _difficultyMultiplier;
    }
    
    void Update()
    {
        // Animation curve uses 0 - 1 whereas the slider has a - 1 to 1 range, this deals with this issue by normalising the slider value.
        float normalizedValue = (accuracySlider.value + 1) / 2f;
        float curveValue = lerpCurve.Evaluate(normalizedValue);
        
        _sliderSpeed = curveValue * Mathf.Sign(_valueChange) * Mathf.Abs(_valueChange);
        
        float nextValue = accuracySlider.value + _sliderSpeed;
        
            // Changes speed from +ve to -ve by checking the next value of the slider. Setting the speed to 0 prevents it from going over the max and min.
            if (nextValue > 1f || nextValue < -1f)
            {
                _valueChange = -_valueChange;
                _sliderSpeed = 0;
            }
            
            // For scoring, score ranges from 0 to 1, closer to the middle of the bar closer to a value of 1.
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
                _valueChange = BaseValueChange * _difficultyMultiplier;
            } 
    }

    void FixedUpdate()
    {
        accuracySlider.value = Mathf.Clamp(accuracySlider.value + _sliderSpeed, -1f, 1f);
    }
    
    public void SetDifficulty(float newMultiplier)
    {
        _difficultyMultiplier = newMultiplier;
        _valueChange = BaseValueChange * _difficultyMultiplier; 
    }
}
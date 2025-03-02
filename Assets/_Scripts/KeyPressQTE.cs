using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TexasShootEm
{
    public class KeyPressQTE : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private int numberOfKeys = 2;
        
        private RandomKeyPressGenerator _keyGenerator;

        private List<Key> _queuedKeys;

        private void Awake()
        {
            _keyGenerator = new RandomKeyPressGenerator();
            _queuedKeys = new List<Key>();
        }

        private void OnEnable() => inputReader.OnDirectionalEvent += KeyPress;
        private void OnDisable() => inputReader.OnDirectionalEvent -= KeyPress;

        private void Update()
        {
            // FOR TESTING ONLY.
            if (Input.GetKeyDown(KeyCode.F))
            {
                RandomKeyPressGenerator.GenerateKeys(ref _queuedKeys, numberOfKeys);
                
                foreach (Key key in _queuedKeys)
                {
                    Debug.Log(key);
                }
            }
        }

        private void KeyPress(Vector2 input)
        {
            if (_queuedKeys.Count == 0) return; 
            
            var test = _keyGenerator.GetKeyFromDirection(input);

            if (test == _queuedKeys[0])
            {
                Debug.Log("Valid key pressed in sequence!");
                _queuedKeys.RemoveAt(0);
            }
        }
    }
}

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

        private void OnEnable() => inputReader.OnAimEvent += Aim;
        private void OnDisable() => inputReader.OnAimEvent -= Aim;

        private void Aim()
        {
            _keyGenerator.GenerateKeys(ref _queuedKeys, numberOfKeys);

            foreach (Key key in _queuedKeys)
            {
                Debug.Log(key);
            }
        }
    }
}

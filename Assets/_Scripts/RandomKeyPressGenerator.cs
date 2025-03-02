using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TexasShootEm
{
    public class RandomKeyPressGenerator : MonoBehaviour
    {
        [SerializeField] private int numberOfKeys = 2;

        private enum Key { Up, Down, Left, Right }
        private static readonly Vector2[] DirectionAxes = new Vector2[]
        {
            new Vector2(0f, 1f),
            new Vector2(0f, -1f),
            new Vector2(-1f, 0f),
            new Vector2(1f, 0f)
        };
        
        private Vector2 GetDirectionFromKey(Key key) => DirectionAxes[(int)key];
        
        private List<Key> _queuedKeys = new List<Key>();

        private void Start()
        {
            GenerateKeys();
        }

        [SerializeField] private Text _text; 
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GenerateKeys();
            }
        }

        private void GenerateKeys()
        {
            _queuedKeys.Clear();
            _text.text = "";
            
            for (int i = 0; i < numberOfKeys; i++)
            {
                Key randomKey = (Key) Random.Range(0, 4);
                _queuedKeys.Add(randomKey);
                _text.text += "\n" + randomKey.ToString();
            }
            
            foreach (Key key in _queuedKeys)
            {
                Debug.Log(key);
            }
        }
    }
}

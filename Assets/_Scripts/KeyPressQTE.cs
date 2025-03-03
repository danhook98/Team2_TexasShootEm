using System.Collections.Generic;
using UnityEngine;

namespace TexasShootEm
{
    public class KeyPressQTE : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        
        [Header("Key Press Variables")]
        [SerializeField] private int numberOfKeysToPress = 2;
        [SerializeField] private Vector2 spawnPosition = new Vector2(1.25f, 0.25f);

        [Header("Key Game Object")] 
        [SerializeField] private Arrow arrowPrefab;
        
        private RandomKeyPressGenerator _keyGenerator;
        private List<Key> _queuedKeys;
        private List<Arrow> _arrowObjects;
        
        private Camera _mainCamera;
        private Vector2 _startPosition;

        private void Awake()
        {
            _keyGenerator = new RandomKeyPressGenerator();
            _queuedKeys = new List<Key>();
            _arrowObjects = new List<Arrow>();
            
            _mainCamera = Camera.main;
            _startPosition = _mainCamera.ViewportToWorldPoint(spawnPosition);
        }

        private void OnEnable() => inputReader.OnDirectionalEvent += KeyPress;
        private void OnDisable() => inputReader.OnDirectionalEvent -= KeyPress;

        private void Update()
        {
            // FOR TESTING ONLY.
            if (Input.GetKeyDown(KeyCode.F))
            {
                GenerateKeys(numberOfKeysToPress);
            }
        }
        
        private void KeyPress(Vector2 input)
        {
            if (_queuedKeys.Count == 0) return; 
            
            var key = _keyGenerator.GetKeyFromDirection(input);

            if (key == _queuedKeys[0])
            {
                Debug.Log("Valid key pressed in sequence!");
                _queuedKeys.RemoveAt(0);
                Destroy(_arrowObjects[0].gameObject);
                _arrowObjects.RemoveAt(0);
            }
        }

        private void GenerateKeys(int numberOfKeys = 2)
        {
            RandomKeyPressGenerator.GenerateKeys(ref _queuedKeys, numberOfKeys);
            
            SpawnArrows(numberOfKeys);
                
            foreach (Key key in _queuedKeys)
            {
                Debug.Log(key);
            }
        }

        private void SpawnArrows(int arrowsToSpawn)
        {
            _arrowObjects.Clear();

            for (int i = 0; i < arrowsToSpawn; i++)
            {
                Vector3 pos = _startPosition + (Vector2.right * i);
                Arrow arrow = Instantiate(arrowPrefab, transform);
                arrow.SetPosition(pos);
                _arrowObjects.Add(arrow);
            }
        }
    }
}
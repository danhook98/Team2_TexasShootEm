using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TexasShootEm
{
    public class RandomKeyPressGenerator
    {
        private static readonly Vector2[] DirectionAxes = new Vector2[]
        {
            new Vector2(0f, 1f),
            new Vector2(0f, -1f),
            new Vector2(-1f, 0f),
            new Vector2(1f, 0f)
        };
        
        public Vector2 GetDirectionFromKey(Key key) => DirectionAxes[(int)key];
        public Key GetKeyFromDirection(Vector2 dir) => (Key) Array.FindIndex(DirectionAxes, direction 
                                                                                                => direction == dir);

        public static void GenerateKeys(ref List<Key> keys, int keyCount)
        {
            keys.Clear();
            
            for (int i = 0; i < keyCount; i++)
            {
                Key randomKey = (Key) Random.Range(0, 4);
                keys.Add(randomKey);
            }
        }
    }
    
    public enum Key { Up, Down, Left, Right }
}

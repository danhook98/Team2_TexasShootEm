using UnityEngine;

namespace TexasShootEm
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        
        private void Update()
        {
            transform.Translate(moveSpeed * Time.deltaTime * Vector2.left);
        }
        
        public void SetPosition(Vector2 pos) => transform.position = pos;
    }
}
using System.Collections;
using UnityEngine;

// I created this script with the help of the below tutorial. I have refactored the code to streamline it, and to also
// aid in my understanding of how it works.
//
// https://gamedesigntheory.blogspot.com/2010/09/controlling-aspect-ratio-in-unity.html

namespace TexasShootEm
{
    [RequireComponent(typeof(Camera))]
    public class AspectRatioLock : MonoBehaviour
    {
        [SerializeField] private Vector2 targetAspectRatio = new(16f, 9f);
        
        private Camera _camera;
        
        private float _desiredAspectRatio;

        private void Awake() => _camera = GetComponent<Camera>();

        private void Start()
        {
            _desiredAspectRatio = targetAspectRatio.x / targetAspectRatio.y;
            UpdateCameraRect();
            StartCoroutine(CheckForResolutionChange());
        }

        /// <summary>
        /// Continuously checks if the screen resolution has changed and then updates the camera rect if it has.
        /// </summary>
        private IEnumerator CheckForResolutionChange()
        {
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;
            
            WaitForSeconds delay = new WaitForSeconds(0.25f);

            while (true)
            {
                if (screenWidth != Screen.width || screenHeight != Screen.height)
                {
                    UpdateCameraRect();
                    screenWidth = Screen.width;
                    screenHeight = Screen.height;
                }

                yield return delay;
            }
        }

        /// <summary>
        /// Changes the attached camera's view rect to match the target aspect ratio set as one of the editor variables. 
        /// </summary>
        private void UpdateCameraRect()
        {
            // Calculate the current aspect ratio of the game resolution. 
            float aspectRatio = (float) Screen.width / (float) Screen.height;
            
            // current viewport height should be scaled by this amount
            float scaleHeight = aspectRatio / _desiredAspectRatio;
            
            // Get the rect of the camera. 
            Rect cameraRect = _camera.rect;

            switch (scaleHeight)
            {
                // If scale height is less than 1, then the screen is too tall compared to our desired aspect ratio, so we
                // need to add a letterbox (black bars at the top and bottom). 
                case < 1.0f:
                {
                    // Set the width of the camera rect to the full width of the screen.
                    cameraRect.width = 1.0f;

                    // Set the height of the camera rect to the calculated scale height, this ensures that the height
                    // matches our desired 16:9 aspect ratio. 
                    cameraRect.height = scaleHeight;

                    cameraRect.x = 0f;

                    // Reposition the rect to ensure it's centred on the Y axis.
                    cameraRect.y = (1.0f - scaleHeight) / 2.0f;
                    break;
                }
                // If the scale height is larger than 1, then the screen is too wide compared to our desired aspect ratio, so
                // we need to add a pillar box (black bars at the sides).
                case > 1.0f:
                {
                    // Calculate the new rect width. 
                    float scaleWidth = 1.0f / scaleHeight;
                
                    // Set the width of the camera rect to the calculated scale width, this ensures that the width matches
                    // our desired 16:9 aspect ratio. 
                    cameraRect.width = scaleWidth;
                
                    // Set the height of the camera rect to the full height of the screen.
                    cameraRect.height = 1f;
                
                    // Reposition the rect to ensure it's centred on the X axis.
                    cameraRect.x = (1.0f - scaleWidth) / 2.0f;
                
                    cameraRect.y = 0f;
                    break;
                }
            }

            // Update the camera's rect with the new one.
            _camera.rect = cameraRect;
        }
    }
}

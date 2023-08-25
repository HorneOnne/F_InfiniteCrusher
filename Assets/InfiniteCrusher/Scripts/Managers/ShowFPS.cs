using UnityEngine;

namespace InfiniteCrusher
{
    public class ShowFPS : MonoBehaviour
    {
        public float updateInterval = 0.5f; // Update every half second

        private float fpsAccumulator = 0f;
        private int frames = 0;
        private float timeLeft;

        private void Start()
        {
            timeLeft = updateInterval;
        }

        private void Update()
        {
            timeLeft -= Time.deltaTime;
            fpsAccumulator += Time.timeScale / Time.deltaTime;
            frames++;

            if (timeLeft <= 0)
            {
                float fps = fpsAccumulator / frames;
                string fpsText = "FPS: " + Mathf.RoundToInt(fps);

                timeLeft = updateInterval;
                fpsAccumulator = 0;
                frames = 0;

                Debug.Log(fpsText);
            }
        }
    }
}

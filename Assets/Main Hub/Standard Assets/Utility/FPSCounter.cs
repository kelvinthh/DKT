using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Utility
{
    [RequireComponent(typeof (Text))]
    public class FPSCounter : MonoBehaviour
    {
        const float fpsMeasurePeriod = 0.5f;
        private int m_FpsAccumulator = 0;
        private float m_FpsNextPeriod = 0;
        private int m_CurrentFps;
        private Text m_Text;
        public bool showFov = true;
        private Camera mainCam;
        private float m_currentFov = 0;


        private void Start()
        {
            m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
            m_Text = GetComponent<Text>();
            mainCam = Camera.main;
        }


        private void Update()
        {
            // measure average frames per second
            m_FpsAccumulator++;
            if (Time.realtimeSinceStartup > m_FpsNextPeriod)
            {
                if (showFov && mainCam) m_currentFov = mainCam.fieldOfView;
                m_CurrentFps = (int) (m_FpsAccumulator/fpsMeasurePeriod);
                m_FpsAccumulator = 0;
                m_FpsNextPeriod += fpsMeasurePeriod;
                if (showFov)
                {
                    m_Text.text = m_CurrentFps + " FPS    " + m_currentFov + " FOV";
                }
                else
                {
                    m_Text.text = m_CurrentFps + " FPS";
                }
            }
        }
    }
}

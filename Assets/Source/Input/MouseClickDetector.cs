using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Roguelike
{
    public class MouseClickDetector
    {
        private int     m_buttonIndex;
        private bool    m_clickStarted;
        private bool    m_uiClick;
        private Vector3 m_lastPosition;

        public event Action<MouseClickDetector> onButtonDown;
        public event Action<MouseClickDetector> onButtonUp;
        public event Action<MouseClickDetector> onDrag;

        public MouseClickDetector(int button)
        {
            m_buttonIndex = button;
        }

        public void Update()
        {
            if (Input.GetMouseButton(m_buttonIndex))
            {
                if (!m_clickStarted && IsPointOverUI(Input.mousePosition))
                {
                    m_uiClick = true;
                }

                if (!m_clickStarted && !m_uiClick && !IsPointOverUI(Input.mousePosition))
                {
                    m_clickStarted = true;
                    m_lastPosition = Input.mousePosition;

                    if (onButtonDown != null)
                    {
                        onButtonDown(this);
                    }
                }

                if (m_clickStarted && (Input.mousePosition != m_lastPosition))
                {
                    if (onDrag != null)
                    {
                        onDrag(this);
                    }
                }
            }
            else
            {
                if (m_clickStarted)
                {
                    if(onButtonUp != null)
                    {
                        onButtonUp(this);
                    }
                }

                m_clickStarted = false;
                m_uiClick = false;
            }
        }

        private bool IsPointOverUI(Vector2 point)
        {
            if (EventSystem.current != null)
            {
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                pointer.position = point;
                List<RaycastResult> raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointer, raycastResults);

                if (raycastResults.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

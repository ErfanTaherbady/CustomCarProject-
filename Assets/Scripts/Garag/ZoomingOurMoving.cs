using System;
using UnityEngine;
using Cinemachine;
namespace ErfanDeveloper
{
    public class ZoomingOurMoving : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera mainCamera;
        [SerializeField] private float zoomModifierSpeed = 0.1f,minZoom,MaxZoom;
        private float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;
        private Vector2 firstTouchPrevPos, secondTouchPrevPos;

        private void Update()
        {
            if (Input.touchCount == 2)
            {
                Touch firstTouch = Input.GetTouch(0);
                Touch secondTouch = Input.GetTouch(1);

                firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
                secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

                touchesCurPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
                touchesCurPosDifference =
                    (firstTouch.position - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;
                if (touchesPrevPosDifference > touchesCurPosDifference)
                    mainCamera.m_Lens.FieldOfView += zoomModifier;
                if (touchesPrevPosDifference < touchesCurPosDifference)
                    mainCamera.m_Lens.FieldOfView -= zoomModifier;
            }

            mainCamera.m_Lens.FieldOfView = Mathf.Clamp(mainCamera.m_Lens.FieldOfView, minZoom, MaxZoom);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

public class CameraControlTrigger : MonoBehaviour
{
    public CustomInspectorObject customInspectorObj;

    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (customInspectorObj.panCameraOnContact)
            {
                CameraManager.instance.PanCameraOnContact(customInspectorObj.panDistance, 
                    customInspectorObj.panTime, customInspectorObj.panDirection, false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 exitDirection = (collision.transform.position - _collider.bounds.center).normalized;
            if(customInspectorObj.swapCameras && customInspectorObj.cameraOnLeft != null && customInspectorObj.cameraOnRight != null)
            {
                CameraManager.instance.SwapCamera(customInspectorObj.cameraOnLeft, customInspectorObj.cameraOnRight, exitDirection);
            }

            if (customInspectorObj.panCameraOnContact)
            {
                CameraManager.instance.PanCameraOnContact(customInspectorObj.panDistance,
                    customInspectorObj.panTime, customInspectorObj.panDirection, true);
            }
        }
    }
}
[System.Serializable]
public class CustomInspectorObject
{
    public bool swapCameras = false;
    public bool panCameraOnContact = false;

    [HideInInspector] public CinemachineVirtualCamera cameraOnLeft;
    [HideInInspector] public CinemachineVirtualCamera cameraOnRight;

    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = .35f;
}
public enum PanDirection
{
    Up,
    Down,
    Left, 
    Right
}
[CustomEditor(typeof(CameraControlTrigger))]
public class MyScriptEditor: Editor
{
    CameraControlTrigger cameraControlTrigger;

    private void OnEnable()
    {
        cameraControlTrigger = (CameraControlTrigger)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (cameraControlTrigger.customInspectorObj.swapCameras)
        {
            cameraControlTrigger.customInspectorObj.cameraOnLeft = EditorGUILayout.ObjectField("Camera on left",
                cameraControlTrigger.customInspectorObj.cameraOnLeft, typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
            
            cameraControlTrigger.customInspectorObj.cameraOnRight = EditorGUILayout.ObjectField("Camera on right",
                cameraControlTrigger.customInspectorObj.cameraOnRight, typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
        }
        if (cameraControlTrigger.customInspectorObj.panCameraOnContact)
        {
            cameraControlTrigger.customInspectorObj.panDirection = (PanDirection)EditorGUILayout.EnumPopup("Camera Pan Direction",
                cameraControlTrigger.customInspectorObj.panDirection);

            cameraControlTrigger.customInspectorObj.panDistance = EditorGUILayout.FloatField("Pan Distance",
                cameraControlTrigger.customInspectorObj.panDistance);
            
            cameraControlTrigger.customInspectorObj.panTime = EditorGUILayout.FloatField("Pan Time",
                cameraControlTrigger.customInspectorObj.panTime);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(cameraControlTrigger);
        }
    }
}

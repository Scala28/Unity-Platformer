using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private CinemachineVirtualCamera[] _allCameras;
    [SerializeField] private Transform _playerTransform;

    [Header("Direction bias")]
    [SerializeField] private float _flipScreenTime = 0.5f;

    [Header("Lerp Y Damping during player fall/jump")]
    [SerializeField] private float _fallPanAmount = .25f;
    [SerializeField] private float _fallPanTime = .35f;
    public float _fallSpeedYDampingChangeTreshold = -15f;
    private float _normYPanAmount;

    public bool IsLerpingYDamping { get; private set; }
    public bool LerpedFromPlayerFalling { get; set; }

    private CinemachineVirtualCamera _currentCamera;
    private CinemachineFramingTransposer _framingTransposer;
    private Coroutine _cameraFaceDirection, _lerpYPan, _panCamera;

    private Player _player;

    private Vector2 _startTrackedObjectOffset;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _player = _playerTransform.gameObject.GetComponent<Player>();
        for(int i=0; i<_allCameras.Length; i++)
        {
            if (_allCameras[i].enabled)
            {
                _currentCamera = _allCameras[i];
                _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }
        _normYPanAmount = _framingTransposer.m_YDamping;
        _startTrackedObjectOffset = _framingTransposer.m_TrackedObjectOffset;
    }

    #region Direction Bias

    public void CallCameraFaceDirection()
    {
        _cameraFaceDirection = StartCoroutine(FacingDirectionBias());
    }
    private IEnumerator FacingDirectionBias()
    {
        float startPoint = _framingTransposer.m_ScreenX;
        float endPoint = DetermineEndPoint();

        float lerpedAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < _flipScreenTime)
        {
            elapsedTime += Time.deltaTime;

            lerpedAmount = Mathf.Lerp(startPoint, endPoint, (elapsedTime / _flipScreenTime));
            _framingTransposer.m_ScreenX = lerpedAmount;
            yield return null;
        }
    }

    private float DetermineEndPoint()
    {

        if (_player.FacingRight)
        {
            return 0.45f;
        }

        else
        {
            return 0.55f;
        }
    }

    #endregion

    #region Lerp Y Damping
    public void LerpYDamping(bool isPlayerFalling)
    {
        _lerpYPan = StartCoroutine(LerpYAction(isPlayerFalling));
    }
    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;

        float startDampAmount = _framingTransposer.m_YDamping;
        float endDampAmount = 0f;

        if (isPlayerFalling)
        {
            endDampAmount = _fallPanAmount;
            LerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = _normYPanAmount;
        }

        float elapsedTime = 0f;
        while(elapsedTime <= _fallPanTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / _fallPanTime));
            _framingTransposer.m_YDamping = lerpedPanAmount;
            yield return null;
        }
        IsLerpingYDamping = false;
    }
    #endregion

    #region EdgeDetection
    public void PanCameraOnContact(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        _panCamera = StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPos));
    }
    private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        Vector2 endPos = Vector2.zero;
        Vector2 startingPos = Vector2.zero;

        if (!panToStartingPos)
        {
            switch (panDirection)
            {
                case PanDirection.Up:
                    endPos = Vector2.up;
                    break;
                case PanDirection.Left:
                    endPos = Vector2.left;
                    break;
                case PanDirection.Down:
                    endPos = Vector2.down;
                    break;
                case PanDirection.Right:
                    endPos = Vector2.right;
                    break;
                default:
                    break;
            }
            endPos *= panDistance;
            startingPos = _startTrackedObjectOffset;
            endPos += startingPos;
        }
        else
        {
            startingPos = _framingTransposer.m_TrackedObjectOffset;
            endPos = _startTrackedObjectOffset;
        }

        float elapsedTime = 0f;
        while(elapsedTime <= panTime)
        {
            elapsedTime += Time.deltaTime;
            Vector3 panLerp = Vector3.Lerp(startingPos, endPos, (elapsedTime / panTime));
            _framingTransposer.m_TrackedObjectOffset = panLerp;

            yield return null;
        }
    }
    #endregion

    #region Swap Cameras
    public void SwapCamera(CinemachineVirtualCamera cameraFromLeft, CinemachineVirtualCamera cameraFromRight,
        Vector2 triggerExitDirection)
    {
        if(_currentCamera == cameraFromLeft && triggerExitDirection.x > 0f)
        {
            cameraFromRight.enabled = true;
            cameraFromLeft.enabled = false;

            _currentCamera = cameraFromRight;
            _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }else if (_currentCamera == cameraFromRight && triggerExitDirection.x < 0f)
        {
            cameraFromLeft.enabled = true;
            cameraFromRight.enabled = false;

            _currentCamera = cameraFromLeft;
            _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
    }
    #endregion
}

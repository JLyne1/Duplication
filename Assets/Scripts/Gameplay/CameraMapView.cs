using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraMapView : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [Header("Size Values")]
    [SerializeField] private float zoomedOutSize = 10f;
    [SerializeField] private float defaultSize = 4f;
    [SerializeField] private float zoomDuration = 0.5f;

    private PlayableCharacterManager playableCharacterManager;
    private bool isZoomedOut = false;

    private void Awake()
    {
        playableCharacterManager = FindObjectOfType<PlayableCharacterManager>();
        virtualCamera.m_Lens.OrthographicSize = defaultSize;
    }
    
    private void Start()
    {
        isZoomedOut = false;
    }

    public void SetZoomState(bool zoomedOut)
    {
        if (isZoomedOut != zoomedOut)
        {
            isZoomedOut = zoomedOut;
            StopAllCoroutines();
            StartCoroutine(ZoomCoroutine(isZoomedOut ? zoomedOutSize : defaultSize));
        }
    }

    private IEnumerator ZoomCoroutine(float targetSize)
    {
        float elapsedTime = 0f;
        float startSize = virtualCamera.m_Lens.OrthographicSize;

        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            float newSize = Mathf.MoveTowards(startSize, targetSize, t * Mathf.Abs(targetSize - startSize));
            virtualCamera.m_Lens.OrthographicSize = newSize;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = targetSize;
        
        if (isZoomedOut)
        {
            playableCharacterManager.SetControlsEnabling(playableCharacterManager.GetActivePlayer(), false);
        }
        else
        {
            playableCharacterManager.SetControlsEnabling(playableCharacterManager.GetActivePlayer(), true);
        }
    }
}
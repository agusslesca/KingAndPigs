using Unity.Cinemachine;
using UnityEngine;

public class CustomCameraOffset : MonoBehaviour
{
    public CinemachineCamera cinemachineCamera;
    public CinemachinePositionComposer positionComposer;

    private void Start()
    {
        positionComposer = cinemachineCamera.GetComponent<CinemachinePositionComposer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");

        positionComposer.TargetOffset.y = -1.8f;
    }
}

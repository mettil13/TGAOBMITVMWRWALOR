using Cinemachine;
using proceduralGeneration;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 startOffset;
    Vector3 lastOffset;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] float lerpSpeed;

    void Start()
    {
        cam.LookAt = RiverGeneration.instance.playerObj.transform;

        startOffset = transform.position - cam.LookAt.transform.position ;
        lastOffset = startOffset;
    }

    private void LateUpdate()
    {
        Vector3 desiredOffset =Quaternion.LookRotation(RiverGeneration.instance.playerBuoiancy.flowDirection, Vector3.up) * startOffset;

        lastOffset = Vector3.Slerp(lastOffset, desiredOffset, Time.deltaTime * lerpSpeed);

        transform.position = cam.LookAt.transform.position + lastOffset;
    }
}

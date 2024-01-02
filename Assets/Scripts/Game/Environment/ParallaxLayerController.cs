using UnityEngine;

public class ParallaxLayerController : MonoBehaviour
{
    [SerializeField] private Camera viewCamera;
    [SerializeField] private float cameraDeltaScalarX = 1f;
    [SerializeField] private float cameraDeltaScalarY = 1f;

    private Vector3 cameraStartPosition;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        cameraStartPosition = viewCamera.transform.position;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraDelta = viewCamera.transform.position - cameraStartPosition;
        transform.position = startPosition + new Vector3(cameraDeltaScalarX * cameraDelta.x, cameraDeltaScalarY * cameraDelta.y);
    }
}

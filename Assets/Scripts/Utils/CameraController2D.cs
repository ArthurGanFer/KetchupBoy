using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Camera))]
public class CameraController2D : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    public Tilemap tilemap;

    private Camera followCamera;
    private Vector3 offset;
    private Vector2 cameraHalfSize;
    private float leftBound;
    private float rightBound;
    private float bottomBound;

    // Start is called before the first frame update
    void OnEnable()
    {
        followCamera = GetComponent<Camera>();

        offset = transform.position - target.position;

        tilemap.CompressBounds();

        CalculateBounds();
        ClampCameraPostion();
    }

    private void CalculateBounds()
    {
        cameraHalfSize = new Vector2(followCamera.aspect * followCamera.orthographicSize, followCamera.orthographicSize);
        
        leftBound = tilemap.cellBounds.min.x + cameraHalfSize.x;
        rightBound = tilemap.cellBounds.max.x - cameraHalfSize.x;
        bottomBound = tilemap.cellBounds.min.y + cameraHalfSize.y;
    }

    void LateUpdate()
    {
        CalculateBounds();
        ClampCameraPostion();
    }

    private void ClampCameraPostion()
    {
        Vector3 targetCamPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        smoothedPos.x = Mathf.Clamp(smoothedPos.x, leftBound, rightBound);
        smoothedPos.y = Mathf.Clamp(smoothedPos.y, bottomBound, smoothedPos.y);

        transform.position = smoothedPos;
    }
}

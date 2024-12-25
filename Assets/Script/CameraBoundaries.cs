using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    public Camera mainCamera; // Gán camera chính ở đây

    private float cameraHeight;
    private float cameraWidth;

    void Start()
    {
        // Lấy kích thước camera
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        cameraHeight = mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;
    }

    void Update()
    {
        // Lấy vị trí hiện tại của vật thể
        Vector3 position = transform.position;

        // Tính toán giới hạn của camera
        float leftBound = mainCamera.transform.position.x - cameraWidth;
        float rightBound = mainCamera.transform.position.x + cameraWidth;
        float topBound = mainCamera.transform.position.y + cameraHeight;
        float bottomBound = mainCamera.transform.position.y - cameraHeight;

        // Giới hạn vị trí của vật thể
        position.x = Mathf.Clamp(position.x, leftBound, rightBound);
        position.y = Mathf.Clamp(position.y, bottomBound, topBound);

        // Cập nhật vị trí của vật thể
        transform.position = position;
    }
}

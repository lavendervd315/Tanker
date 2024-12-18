using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    public Camera mainCamera; // Camera chính
    public GameObject topWall, bottomWall, leftWall, rightWall; // Các t??ng
    public float wallThickness = 1f; // ?? dày c?a các b?c t??ng

    void Start()
    {
        CreateWalls();
    }

    void CreateWalls()
    {
        // Tính toán chi?u cao và chi?u r?ng màn hình t? camera
        float screenHeight = mainCamera.orthographicSize * 2;
        float screenWidth = screenHeight * mainCamera.aspect;

        // ??t các t??ng vào ?úng v? trí và kích th??c
        // Top Wall (T??ng trên)
        topWall.transform.position = new Vector3(0, mainCamera.transform.position.y + mainCamera.orthographicSize + wallThickness / 2, 0);
        topWall.transform.localScale = new Vector3(screenWidth, wallThickness, 1);

        // Bottom Wall (T??ng d??i)
        bottomWall.transform.position = new Vector3(0, mainCamera.transform.position.y - mainCamera.orthographicSize - wallThickness / 2, 0);
        bottomWall.transform.localScale = new Vector3(screenWidth, wallThickness, 1);

        // Left Wall (T??ng trái)
        leftWall.transform.position = new Vector3(mainCamera.transform.position.x - screenWidth / 2 - wallThickness / 2, 0, 0);
        leftWall.transform.localScale = new Vector3(wallThickness, screenHeight, 1);

        // Right Wall (T??ng ph?i)
        rightWall.transform.position = new Vector3(mainCamera.transform.position.x + screenWidth / 2 + wallThickness / 2, 0, 0);
        rightWall.transform.localScale = new Vector3(wallThickness, screenHeight, 1);
    }
}

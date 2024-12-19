using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    public Camera mainCamera; // Camera chính
    public GameObject topWall, bottomWall, leftWall, rightWall; // Các tường trên, dưới, trái, phải
    public float wallThickness = 0.5f; // Độ dày của các bức tường

    void Start()
    {
        CreateWalls();
    }

    void CreateWalls()
    {
        // Tính toán chiều cao và chiều rộng màn hình từ camera
        float screenHeight = mainCamera.orthographicSize * 2; // Chiều cao màn hình (theo đơn vị thế giới)
        float screenWidth = screenHeight * mainCamera.aspect; // Chiều rộng màn hình (theo đơn vị thế giới)

        // Đặt các tường vào đúng vị trí và kích thước
        // Tường trên (Top Wall)
        topWall.transform.position = new Vector3(0, mainCamera.transform.position.y + mainCamera.orthographicSize + wallThickness / 2, 0);
        topWall.transform.localScale = new Vector3(screenWidth, wallThickness, 1);

        // Tường dưới (Bottom Wall)
        bottomWall.transform.position = new Vector3(0, mainCamera.transform.position.y - mainCamera.orthographicSize - wallThickness / 2, 0);
        bottomWall.transform.localScale = new Vector3(screenWidth, wallThickness, 1);

        // Tường trái (Left Wall)
        leftWall.transform.position = new Vector3(mainCamera.transform.position.x - screenWidth / 2 - wallThickness / 2, 0, 0);
        leftWall.transform.localScale = new Vector3(wallThickness, screenHeight, 1);

        // Tường phải (Right Wall)
        rightWall.transform.position = new Vector3(mainCamera.transform.position.x + screenWidth / 2 + wallThickness / 2, 0, 0);
        rightWall.transform.localScale = new Vector3(wallThickness, screenHeight, 1);
    }
}

using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    public Camera mainCamera; // Camera ch�nh
    public GameObject topWall, bottomWall, leftWall, rightWall; // C�c t??ng
    public float wallThickness = 1f; // ?? d�y c?a c�c b?c t??ng

    void Start()
    {
        CreateWalls();
    }

    void CreateWalls()
    {
        // T�nh to�n chi?u cao v� chi?u r?ng m�n h�nh t? camera
        float screenHeight = mainCamera.orthographicSize * 2;
        float screenWidth = screenHeight * mainCamera.aspect;

        // ??t c�c t??ng v�o ?�ng v? tr� v� k�ch th??c
        // Top Wall (T??ng tr�n)
        topWall.transform.position = new Vector3(0, mainCamera.transform.position.y + mainCamera.orthographicSize + wallThickness / 2, 0);
        topWall.transform.localScale = new Vector3(screenWidth, wallThickness, 1);

        // Bottom Wall (T??ng d??i)
        bottomWall.transform.position = new Vector3(0, mainCamera.transform.position.y - mainCamera.orthographicSize - wallThickness / 2, 0);
        bottomWall.transform.localScale = new Vector3(screenWidth, wallThickness, 1);

        // Left Wall (T??ng tr�i)
        leftWall.transform.position = new Vector3(mainCamera.transform.position.x - screenWidth / 2 - wallThickness / 2, 0, 0);
        leftWall.transform.localScale = new Vector3(wallThickness, screenHeight, 1);

        // Right Wall (T??ng ph?i)
        rightWall.transform.position = new Vector3(mainCamera.transform.position.x + screenWidth / 2 + wallThickness / 2, 0, 0);
        rightWall.transform.localScale = new Vector3(wallThickness, screenHeight, 1);
    }
}

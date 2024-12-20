using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerTank; // Xe tăng người chơi
    public GameObject botPrefab; // Prefab của bot
    public Transform[] spawnPoints; // Các điểm spawn bot
    private int currentBotCount = 0; // Số bot hiện tại trong trận

    void Start()
    {
        SpawnBot(); // Spawn bot đầu tiên khi game bắt đầu
    }

    public void SpawnBot()
    {
        if (spawnPoints.Length == 0) return;

        // Chọn ngẫu nhiên một điểm spawn
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Tạo bot tại vị trí spawn
        Instantiate(botPrefab, spawnPoint.position, spawnPoint.rotation);
        currentBotCount++;
    }

    public void OnBotDestroyed()
    {
        currentBotCount--;
        if (currentBotCount <= 0)
        {
            // Spawn bot mới khi bot hiện tại bị tiêu diệt
            SpawnBot();
        }
    }
}

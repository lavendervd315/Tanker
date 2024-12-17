using UnityEngine;

public class BotScript : MonoBehaviour
{
    public GameObject tanker;// Tham chiếu đến người chơi
    public GameObject bot;
    public GameObject bulletPrefab;  // Prefab của viên đạn
    public Transform firePoint;      // Điểm xuất phát của viên đạn
    public float bulletSpeed = 1f;  // Tốc độ của viên đạn
    public float moveSpeed = 1f;     // Tốc độ di chuyển của bot
    public float attackRange = 10f;  // Tầm bắn của bot
    public float detectionRange = 15f; // Tầm phát hiện người chơi
    public float lastShootTime = 0f; // Thời gian lần bắn cuối cùng
    public float shootCooldown = 2f; // Thời gian trễ giữa các lần bắn (2 giây)
    public float health = 50f; // Máu ban đầu của kẻ địch


    private Rigidbody2D rb;          // Rigidbody2D của bot
    private Vector2 movement;        // Hướng di chuyển
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Tính toán hướng đi của bot đối với người chơi
        Vector2 directionToPlayer = (tanker.transform.position - transform.position).normalized;

        // Kiểm tra xem người chơi có nằm trong tầm phát hiện của bot không
        float distanceToPlayer = Vector2.Distance(transform.position, tanker.transform.position);

        if (distanceToPlayer < detectionRange)
        {
            // Di chuyển bot về phía người chơi
            movement = directionToPlayer;

            // Nếu bot có thể bắn, hãy bắn viên đạn
            if (distanceToPlayer <= attackRange && Time.time - lastShootTime >= shootCooldown)
            {
                Shoot();
                lastShootTime = Time.time; // Cập nhật thời gian bắn lần cuối
            }
        }

        // Xoay bot theo hướng di chuyển
        if (movement != Vector2.zero)
        {
            // Tính toán góc quay từ vector di chuyển
            float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90;

            // Lấy góc hiện tại của tank
            float currentAngle = bot.transform.eulerAngles.z;

            // Tính toán góc quay ngắn nhất
            float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

            // Quay tank theo góc quay ngắn nhất
            bot.transform.Rotate(0, 0, angleDifference);
        }
    }

    void FixedUpdate()
    {
        // Di chuyển bot bằng Rigidbody2D
        rb.linearVelocity = movement * moveSpeed;
    }

    void Shoot()
    {
        // Tạo viên đạn mới tại vị trí firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Lấy Rigidbody2D của viên đạn và thêm lực để nó di chuyển
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.linearVelocity = firePoint.up * bulletSpeed;
    }

    // Hàm nhận sát thương
    public void TakeDamage(float damage)
    {
        Destroy(gameObject);
    }
}

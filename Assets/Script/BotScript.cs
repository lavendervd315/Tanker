using UnityEngine;

public class BotScript : MonoBehaviour
{
    
    public GameObject bulletPrefab; // Prefab của viên đạn
    private GameController gameController;
    private GameObject tanker; // Tham chiếu đến người chơi

    public Transform firePoint; // Điểm xuất phát của viên đạn
    public float bulletSpeed = 10f; // Tốc độ của viên đạn
    public float moveSpeed = 1f; // Tốc độ di chuyển của bot
    public float attackRange = 10f; // Tầm bắn của bot
    public float detectionRange = 50f; // Tầm phát hiện người chơi
    public float lastShootTime = 0f; // Thời gian lần bắn cuối cùng
    public float shootCooldown = 2f; // Thời gian trễ giữa các lần bắn (2 giây)
    public float health = 20f; // Máu ban đầu của bot

    private Rigidbody2D rb; // Rigidbody2D của bot
    private Vector2 movement; // Hướng di chuyển

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = FindAnyObjectByType<GameController>();
        tanker = GameObject.FindWithTag("Tanker"); ;
    }

    void Update()
    {
        if (tanker == null) return;

        // Tính toán hướng di chuyển của bot
        Vector2 directionToPlayer = (tanker.transform.position - transform.position).normalized;

        // Kiểm tra khoảng cách tới người chơi
        float distanceToPlayer = Vector2.Distance(transform.position, tanker.transform.position);

        // Bot di chuyển về phía người chơi
        movement = directionToPlayer;

        // Kiểm tra và bắn nếu trong tầm tấn công
        if (distanceToPlayer <= attackRange && Time.time - lastShootTime >= shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }

        //if (distanceToPlayer < detectionRange)
        //{
        //    // Bot di chuyển về phía người chơi
        //    movement = directionToPlayer;

        //    // Kiểm tra và bắn nếu trong tầm tấn công
        //    if (distanceToPlayer <= attackRange && Time.time - lastShootTime >= shootCooldown)
        //    {
        //        Shoot();
        //        lastShootTime = Time.time;
        //    }
        //}
        //else
        //{
        //    // Không di chuyển khi người chơi ngoài tầm
        //    movement = Vector2.zero;
        //}

        // Xoay bot theo hướng di chuyển
        if (movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }
    }

    void FixedUpdate()
    {
        // Di chuyển bot
        rb.linearVelocity = movement * moveSpeed;
    }

    void Shoot()
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint chưa được gán trong BotScript!");
            return;
        }

        // Tạo viên đạn
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Lấy Rigidbody2D của viên đạn và thêm vận tốc
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.linearVelocity = firePoint.up * bulletSpeed;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (gameController != null)
        {
            gameController.OnBotDestroyed();
        }

        Destroy(gameObject);
    }
}

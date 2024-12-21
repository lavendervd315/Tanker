using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class TankScript : MonoBehaviour
{
    public GameObject tanker; // Tank
    public GameObject bulletPrefab; // Prefab của viên đạn
    public GameObject rocketPrefab; // Prefab của viên đạn
    public Transform firePoint; // Điểm xuất phát của viên đạn
    public float bulletSpeed = 100f; // Tốc độ của viên đạn
    public float moveSpeed = 10f; // Tốc độ di chuyển của tank
    public float maxHealth = 100f; // Máu tối đa
    public Slider healthBar; // Tham chiếu đến thanh máu (Slider)
    public AudioClip shootSound; // Âm thanh khi bắn đạn

    private float currentHealth = 100f;
    private Rigidbody2D rb; // Rigidbody2D của tank
    private Vector2 movement; // Vector lưu hướng di chuyển
    private AudioSource audioSource; // Nguồn phát âm thanh
    public VariableJoystick variableJoystick;

    void Start()
    {
        // Lấy Rigidbody2D từ tanker
        rb = tanker.GetComponent<Rigidbody2D>();

        // Thêm hoặc lấy AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Lấy input từ bàn phím (WASD hoặc mũi tên)
        float moveX = Input.GetAxisRaw("Horizontal"); // Trái/Phải
        float moveY = Input.GetAxisRaw("Vertical");   // Lên/Xuống

        // Gộp input từ joystick và bàn phím
        float joystickX = variableJoystick.Horizontal;
        float joystickY = variableJoystick.Vertical;

        // Ưu tiên joystick, nếu không có thì dùng bàn phím
        movement = new Vector2(
            joystickX != 0 ? joystickX : moveX,
            joystickY != 0 ? joystickY : moveY
        ).normalized;

        // Xoay tank theo hướng di chuyển
        if (movement != Vector2.zero)
        {
            // Tính toán góc quay từ vector di chuyển
            float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90;

            // Lấy góc hiện tại của tank
            float currentAngle = tanker.transform.eulerAngles.z;

            // Tính toán góc quay ngắn nhất
            float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

            // Quay mượt mà về hướng targetAngle với giới hạn tốc độ quay
            float smoothedAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * 10f);

            // Đảm bảo góc quay không quá 1 vòng (360 độ)
            if (Mathf.Abs(angleDifference) > 180f)
            {
                smoothedAngle = (smoothedAngle + 180f) % 360f;
            }

            tanker.transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
        }

        // Bắn đạn khi nhấn phím Chuột trái hoặc Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }


    void FixedUpdate()
    {
        // Di chuyển tank bằng Rigidbody2D
        rb.linearVelocity = movement * moveSpeed;
    }

    void Shoot()
    {
        // Phát âm thanh bắn đạn
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Tạo viên đạn chính giữa
        GameObject bulletCenter = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rbCenter = bulletCenter.GetComponent<Rigidbody2D>();
        SetupBullet(bulletCenter);

        // Tạo viên đạn song song (bên phải)
        Vector3 offsetRight = firePoint.right * 0.5f; // Khoảng cách song song (dịch sang phải)
        GameObject bulletssRight = Instantiate(bulletPrefab, firePoint.position + offsetRight, firePoint.rotation);
        Rigidbody2D rbssRight = bulletssRight.GetComponent<Rigidbody2D>();
        SetupBullet(bulletssRight);

        // Tạo viên đạn song song (bên trái)
        Vector3 offsetLeft = -firePoint.right * 0.5f; // Khoảng cách song song (dịch sang trái)
        GameObject bulletssLeft = Instantiate(bulletPrefab, firePoint.position + offsetLeft, firePoint.rotation);
        Rigidbody2D rbssLeft = bulletssLeft.GetComponent<Rigidbody2D>();
        SetupBullet(bulletssLeft);

        //// Tạo viên đạn chéo bên trái
        GameObject bulletLeft = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + 30));
        Rigidbody2D rbLeft = bulletLeft.GetComponent<Rigidbody2D>();
        rbLeft.linearVelocity = bulletLeft.transform.up * bulletSpeed;

        // Tạo viên đạn chéo bên phải
        GameObject bulletRight = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z - 30));
        Rigidbody2D rbRight = bulletRight.GetComponent<Rigidbody2D>();
        rbRight.linearVelocity = bulletRight.transform.up * bulletSpeed;

        // Tạo tên lửa đuổi
        GameObject rocket = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);

        // Gán mục tiêu cho tên lửa
        BotScript botScript = FindNearestBot(); // Tìm bot gần nhất
        if (botScript != null)
        {
            RocketScript rocketScript = rocket.GetComponent<RocketScript>();
            if (rocketScript != null)
            {
                rocketScript.target = botScript.transform; // Gán mục tiêu là đối tượng có BotScript
            }
        }
    }

    void SetupBullet(GameObject bullet)
    {
        // Ngăn viên đạn va chạm với tank
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        // Thiết lập vận tốc cho viên đạn
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.up * bulletSpeed;
        }
    }

    BotScript FindNearestBot()
    {
        BotScript[] bots = FindObjectsOfType<BotScript>(); // Lấy tất cả BotScript trong cảnh
        BotScript nearestBot = null;
        float minDistance = Mathf.Infinity;

        foreach (BotScript bot in bots)
        {
            float distance = Vector2.Distance(transform.position, bot.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestBot = bot;
            }
        }

        return nearestBot;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log("currentHealth=" + currentHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Debug.Log("Tank destroyed!");
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth; // Giá trị từ 0 đến 1
        }
    }
}

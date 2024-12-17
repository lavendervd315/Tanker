using UnityEngine;
using UnityEngine.UI;

public class TankScript : MonoBehaviour
{
    public GameObject tanker; // Tank
    public GameObject bulletPrefab; // Prefab của viên đạn
    public Transform firePoint; // Điểm xuất phát của viên đạn
    public float bulletSpeed = 100f; // Tốc độ của viên đạn
    public float moveSpeed = 10f; // Tốc độ di chuyển của tank
    public float maxHealth = 100f; // Máu tối đa
    public Slider healthBar; // Tham chiếu đến thanh máu (Slider)

    private float currentHealth = 100f;

    private Rigidbody2D rb; // Rigidbody2D của tank
    private Vector2 movement; // Vector lưu hướng di chuyển

    void Start()
    {
        // Lấy Rigidbody2D từ tanker
        rb = tanker.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Lấy input từ bàn phím (WASD hoặc mũi tên)
        float moveX = Input.GetAxisRaw("Horizontal"); // Trái/Phải
        float moveY = Input.GetAxisRaw("Vertical");   // Lên/Xuống

        // Tạo vector di chuyển
        movement = new Vector2(moveX, moveY).normalized;

        // Xoay tank theo hướng di chuyển
        if (movement != Vector2.zero)
        {
            // Tính toán góc quay từ vector di chuyển
            float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90;

            // Lấy góc hiện tại của tank
            float currentAngle = tanker.transform.eulerAngles.z;

            // Tính toán góc quay ngắn nhất
            float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

            // Quay tank theo góc quay ngắn nhất
            tanker.transform.Rotate(0, 0, angleDifference);
        }


        // Bắn đạn khi nhấn phím Chuột trái
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
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
        // Tạo viên đạn mới tại vị trí firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Lấy Rigidbody2D của viên đạn và thêm lực để nó di chuyển
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.linearVelocity = firePoint.up * bulletSpeed;
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

using UnityEngine;

public class RocketScript : MonoBehaviour
{
    public GameObject hitEffect; // Prefab của hiệu ứng va chạm
    public float damage = 20f;
    public float speed = 5f;           // Tốc độ tên lửa
    public float rotationSpeed = 200f; // Tốc độ xoay để đuổi theo mục tiêu
    public Transform target;           // Mục tiêu (vd: Tank, Enemy)


    void Update()
    {
        if (target == null)
        {
            // Nếu không có mục tiêu, tự hủy tên lửa
            Destroy(gameObject);
            return;
        }

        // Tính hướng tới mục tiêu
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        // Tính góc xoay để hướng tới mục tiêu
        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        // Xoay dần tên lửa
        transform.Rotate(0, 0, -rotateAmount * rotationSpeed * Time.deltaTime);

        // Di chuyển tên lửa về phía trước
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bot"))
        {
            Debug.Log("Bullet hit Bot!");

            // Gây sát thương cho kẻ địch
            BotScript bot = collision.GetComponent<BotScript>();
            if (bot != null)
            {
                bot.TakeDamage(damage);
            }

            // Hiển thị hiệu ứng va chạm
            if (hitEffect != null)
            {
                // Tạo hiệu ứng tại vị trí va chạm
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }

            // Hủy viên đạn sau khi chạm
            Destroy(gameObject);
        }

        if (collision.CompareTag("Wall"))
        {
            Debug.Log("Rocket hit Wall!");

            // Hủy viên đạn sau khi chạm
            Destroy(gameObject);
        }
    }
}

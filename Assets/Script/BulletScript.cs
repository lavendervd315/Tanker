using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float damage = 10; // Sát thương của viên đạn
    public GameObject hitEffect; // Prefab của hiệu ứng va chạm

    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu viên đạn chạm vào kẻ địch
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

        if (collision.CompareTag("Tanker"))
        {
            Debug.Log("Bullet hit Tank!");
            TankScript tank = collision.GetComponent<TankScript>();
            if (tank != null)
            {
                tank.TakeDamage(damage);
            }
            // Hủy viên đạn sau khi chạm
            Destroy(gameObject);
        }

        if (collision.CompareTag("Wall"))
        {
            Debug.Log("Bullet hit Wall!");

            // Hủy viên đạn sau khi chạm
            Destroy(gameObject);
        }
    }
}

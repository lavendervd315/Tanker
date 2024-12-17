using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public int damage = 10; // Sát thương của viên đạn
    public GameObject hitEffect; // Prefab của hiệu ứng va chạm

    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu viên đạn chạm vào kẻ địch
        if (other.CompareTag("Bot"))
        {
            Debug.Log("Bullet hit Bot!");

            // Gây sát thương cho kẻ địch
            BotScript bot = other.GetComponent<BotScript>();
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


        }

        if (other.CompareTag("Tanker"))
        {
            Debug.Log("Bullet hit Tank!");
            TankScript tank = other.GetComponent<TankScript>();
            if (tank != null)
            {
                tank.TakeDamage(damage);
            }
        }

        // Hủy viên đạn sau khi chạm
        Destroy(gameObject);
    }
}

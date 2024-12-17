using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float damage = 10f; // Sát thương của viên đạn

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

            // Hủy viên đạn sau khi chạm
            Destroy(gameObject);
        }

        // Kiểm tra nếu viên đạn chạm vào tường
        if (other.CompareTag("Wall"))
        {
            Debug.Log("Bullet hit Wall!");
            // Hủy viên đạn khi chạm vào tường
            Destroy(gameObject);
        }
    }
}

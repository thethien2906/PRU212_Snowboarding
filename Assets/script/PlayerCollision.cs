using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void onTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coins"))
        {
            Debug.Log("Coin collected!");
        }
    }
    
}

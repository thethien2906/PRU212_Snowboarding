using Unity.Cinemachine;
using UnityEngine;

public class CrashController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            Debug.Log("Crash");
        }
    }
}

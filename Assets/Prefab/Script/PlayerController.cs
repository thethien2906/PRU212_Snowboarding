using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public MapSpawnerController mapSpawner;
    [SerializeField] float rotationSpeed = 200f;
    [SerializeField] float jumpForce = 10f;  // Lực nhảy
    private bool isGrounded = false;  // Kiểm tra xem có đang trên mặt đất không

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (mapSpawner == null)
        {
            mapSpawner = FindFirstObjectByType<MapSpawnerController>();
        }
    }

    void FixedUpdate()
    {
        HandleRotation();
    }

    void Update()
    {
        HandleJump();
        mapSpawner.CheckAndSpawnNextMap(transform);
    }

    void HandleRotation()
    {
        float rotationInput = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationInput = 1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationInput = -1f;
        }

        rb.AddTorque(rotationInput * rotationSpeed * Time.fixedDeltaTime);
    }
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false; // Ngăn nhảy liên tục
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

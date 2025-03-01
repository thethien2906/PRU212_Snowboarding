using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public MapSpawnerController mapSpawner;
    [SerializeField] float rotationSpeed = 200f;
    [SerializeField] float jumpForce = 30f;  // Lực nhảy
    private bool isGrounded = false;  // Kiểm tra xem có đang trên mặt đất không
	private float rotationSum = 0f;  // Tổng số độ quay tích lũy
	private float previousRotation = 0f;  // Lưu góc quay trước đó
	private float lastRotationTime = 0f;  // Thời gian lần xoay gần nhất
	private const float comboDuration = 2f;  // Khoảng thời gian cho combo (2s)
    private int countCombo = 0;
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
        CheckRotationForScore();

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

	void CheckRotationForScore()
	{
		float currentRotation = transform.rotation.eulerAngles.z;
		float deltaRotation = Mathf.DeltaAngle(previousRotation, currentRotation);

		rotationSum += deltaRotation;
		previousRotation = currentRotation;

		if (Mathf.Abs(rotationSum) >= 360f)
		{
			float currentTime = Time.time;  // Lấy thời gian hiện tại

			if (currentTime - lastRotationTime <= comboDuration)
			{
				countCombo++;  // Nếu xoay trong 2s thì tăng combo
			}
			else
			{
				countCombo = 1;  // Nếu quá 2s thì reset combo
			}

			lastRotationTime = currentTime;  // Cập nhật thời gian lần xoay cuối
			ScoreCountScript.ScoreValue += (countCombo >= 2) ? 20 : 10;  // Cộng điểm

			Debug.Log($"Player xoay 360 độ! Combo: {countCombo}, Điểm: {ScoreCountScript.ScoreValue}");
			rotationSum = 0f;
		}
	}


	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
			rotationSum = 0f;
			isGrounded = true;
        }
    }

}

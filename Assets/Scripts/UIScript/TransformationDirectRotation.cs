using UnityEngine;
using UnityEngine.UI;

public class TransformationDirectRotation : MonoBehaviour
{
    [Header("Main Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public bool TouchJoystickInput;
    float moveX = 0;
    float moveZ = 0;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!TouchJoystickInput)
        {
            // Mendapatkan input horizontal dan vertical dari pemain
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
        }

        // Membuat vektor pergerakan berdasarkan input pemain
        Vector3 movement = new Vector3(moveX, 0f, moveZ);

        // Menggerakkan karakter berdasarkan vektor pergerakan
        rb.velocity = movement.normalized * moveSpeed;

        // Rotasi karakter sesuai arah pergerakan
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void SetInputX(Text value)
    {
        moveX = float.Parse(value.text);
    }
    public void SetInputY(Text value)
    {
        moveZ = float.Parse(value.text);
    }
}


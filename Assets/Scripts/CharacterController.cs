using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterController : MonoBehaviour
{
    [Header("Main Settings")]
    public Animator animator;
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public bool TouchJoystickInput;
    float moveX = 0;
    float moveZ = 0;

    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
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
        float speed = movement.magnitude; // Hitung kecepatan total (untuk Blend Tree)
        animator.SetFloat("Speed", speed);
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

    public void SetTrigger(string triggerName)
    {
        if (animator != null)
        {
            animator.SetTrigger(triggerName);

            // Jalankan coroutine untuk menunggu animasi selesai
            StartCoroutine(WaitForAnimationToEnd(triggerName));
        }
    }

    // Coroutine untuk menunggu animasi selesai
    private IEnumerator WaitForAnimationToEnd(string triggerName)
    {
        // Tunggu hingga animator masuk ke state yang menggunakan trigger
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        while (!currentState.IsTag(triggerName))
        {
            yield return null;
            currentState = animator.GetCurrentAnimatorStateInfo(0);
        }

        // Tunggu hingga animasi selesai
        while (currentState.normalizedTime < 1.0f)
        {
            yield return null;
            currentState = animator.GetCurrentAnimatorStateInfo(0);
        }

        // Setelah selesai, kembalikan ke locomotion dengan Speed
        animator.ResetTrigger(triggerName); // Pastikan trigger di-reset
        animator.SetFloat("Speed", 0);     // Atur kembali ke locomotion (Idle jika Speed = 0)
    }

}


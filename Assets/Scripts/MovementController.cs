using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public float speed;
    Animator animComponent;

    // 🔊 Звук шагов
    public AudioSource footstepSource;       // Источник звука
    public AudioClip[] footstepClips;        // Список звуков шагов
    public float stepInterval = 0.4f;        // Интервал между шагами
    private float stepTimer = 0f;            // Внутренний таймер

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animComponent = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 moveVector = Vector3.zero;

        // 🎮 Управление движением и анимацией
        if (Input.GetKey(KeyCode.W))
        {
            moveVector += new Vector3(0, 1, 0);
            animComponent.SetBool("WalkUp", true);
        }
        else { animComponent.SetBool("WalkUp", false); }

        if (Input.GetKey(KeyCode.A))
        {
            moveVector += new Vector3(-1, 0, 0);
            animComponent.SetBool("WalkLeft", true);
        }
        else { animComponent.SetBool("WalkLeft", false); }

        if (Input.GetKey(KeyCode.S))
        {
            moveVector += new Vector3(0, -1, 0);
            animComponent.SetBool("WalkDown", true);
        }
        else { animComponent.SetBool("WalkDown", false); }

        if (Input.GetKey(KeyCode.D))
        {
            moveVector += new Vector3(1, 0, 0);
            animComponent.SetBool("WalkRight", true);
        }
        else { animComponent.SetBool("WalkRight", false); }

        moveVector.Normalize();
        rigidbody.velocity = moveVector * speed;

        // 🧍 Если стоим — выключаем все анимации
        if (moveVector == Vector3.zero)
        {
            animComponent.SetBool("WalkLeft", false);
            animComponent.SetBool("WalkDown", false);
            animComponent.SetBool("WalkRight", false);
            animComponent.SetBool("WalkUp", false);
        }

        // 🔊 Воспроизведение шагов
        if (moveVector != Vector3.zero)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    // 🔁 Метод для проигрывания звука
    void PlayFootstep()
    {
        if (footstepClips.Length > 0 && footstepSource != null)
        {
            footstepSource.clip = footstepClips[Random.Range(0, footstepClips.Length)];
            footstepSource.Play();
        }
    }
}

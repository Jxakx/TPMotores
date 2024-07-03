using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaSaltoCubo : MonoBehaviour
{
    public float jumpForce = 5.0f; // Fuerza con la que el cubo salta hacia arriba
    public float fallSpeed = 10.0f; // Velocidad a la que el cubo vuelve al suelo

    private Vector3 originalPosition; // Posición original del cubo
    private bool isJumping = false; // Flag para controlar si el cubo está saltando

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(JumpAndFall());
        }
    }

    public IEnumerator JumpAndFall()
    {
        isJumping = true;

        // Salto rápido hacia arriba
        Vector3 jumpTarget = transform.position + Vector3.up * jumpForce;
        while (transform.position.y < jumpTarget.y)
        {
            transform.Translate(Vector3.up * jumpForce * Time.deltaTime, Space.World);
            yield return null;
        }

        // Caída suave de vuelta al suelo
        while (transform.position.y > originalPosition.y)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
            yield return null;
        }

        transform.position = originalPosition;
        isJumping = false;
    }
}


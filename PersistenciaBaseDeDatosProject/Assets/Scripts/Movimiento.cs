using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        // Obtener el componente Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Capturar el input de WASD
        float moveHorizontal = Input.GetAxis("Horizontal"); // A (-1) y D (1)
        float moveVertical = Input.GetAxis("Vertical"); // W (1) y S (-1)

        // Crear un vector de movimiento basado en las teclas presionadas
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    }

    void FixedUpdate()
    {
        // Aplicar la fuerza al Rigidbody para mover al jugador
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
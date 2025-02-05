using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 40;

    void Update()
    {
        // Mover el objeto hacia adelante
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // Destruir el coche si la posición x >= 680 (sentido original)
        if (transform.position.x >= 680)
        {
            Destroy(gameObject);
        }

        // Destruir el coche si la posición x <= 320 (sentido contrario)
        if (transform.position.x <= 320)
        {
            Destroy(gameObject);
        }
    }
}

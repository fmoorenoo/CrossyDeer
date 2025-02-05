using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(500f, 1f, 105f);
    public Vector3 newPosition = new Vector3(500f, 1f, 0f);
    public TextMeshProUGUI scoreText;

    private int score = 0;

    private Vector3 initialPosition;

    void Start()
    {
        // Establecer la posición inicial
        transform.position = startPosition;
        initialPosition = startPosition;

        // Buscar el texto de puntuación si no está asignado
        if (scoreText == null)
        {
            GameObject textObject = GameObject.Find("ScoreText");
            if (textObject != null)
            {
                scoreText = textObject.GetComponent<TextMeshProUGUI>();
            }
        }

        UpdateScoreText();
    }

    void Update()
    {
        
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Mover la moneda a la nueva posición
            transform.position = newPosition;
            initialPosition = newPosition; 


            // Incrementar el puntaje
            score++;

            // Actualizar el texto de puntuación
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}

using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveDistance = 10f;
    public float rotationSpeed = 100f;
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;

    // Límites de movimiento
    public float minZ = -15f;
    public float maxZ = 115f;
    public float minX = 435f;
    public float maxX = 565f;

    private bool isMoving = false;
    private bool isCrushed = false;

    private TextMeshProUGUI finishText;
    public GameObject uiObject; // Referencia al objeto UI

    void Start()
    {
        // Busca el objeto con el tag "Finish"
        GameObject finishObject = GameObject.FindGameObjectWithTag("Finish");

        // Buscar el objeto UI si no está asignado
        if (uiObject == null)
        {
            uiObject = GameObject.Find("UI");
        }
    }

    void Update()
    {
        if (!isMoving && !isCrushed)
        {
            CheckInput();
        }
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MovePlayer(Vector3.forward, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MovePlayer(Vector3.back, 180f);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MovePlayer(Vector3.left, -90f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MovePlayer(Vector3.right, 90f);
        }
    }

    void MovePlayer(Vector3 direction, float rotationAngle)
    {
        isMoving = true;
        StartCoroutine(RotateAndJump(direction, rotationAngle));
    }

    IEnumerator RotateAndJump(Vector3 direction, float rotationAngle)
    {
        float currentAngle = transform.eulerAngles.y;
        float rotationStep = rotationSpeed * Time.deltaTime;

        while (!Mathf.Approximately(currentAngle, rotationAngle))
        {
            currentAngle = Mathf.MoveTowardsAngle(currentAngle, rotationAngle, rotationStep);
            transform.rotation = Quaternion.Euler(0f, currentAngle, 0f);
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = transform.position + direction * moveDistance;

        // Restringir la posición a los límites definidos
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        float distance = Vector3.Distance(startPosition, targetPosition);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed / distance;
            Vector3 currentPosition = Vector3.Lerp(startPosition, targetPosition, t);
            float height = Mathf.Sin(t * Mathf.PI) * jumpHeight;
            currentPosition.y = startPosition.y + height;
            transform.position = currentPosition;

            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car") && !isCrushed)
        {
            isCrushed = true;
            StartCoroutine(CarCollision());
        }
    }

    IEnumerator CarCollision()
    {
        // 'Aplastar' el personaje al detectar una colisión con un coche
        transform.localScale = new Vector3(transform.localScale.x * 2f, transform.localScale.y * 0.2f, transform.localScale.z * 2f);

        GetComponent<Collider>().enabled = false;

        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        if (uiObject != null)
        {
            uiObject.transform.position = new Vector3(294f, 211f, 0f);
        }

        yield break;
    }
}

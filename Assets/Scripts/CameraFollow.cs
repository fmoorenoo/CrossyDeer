using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;            
    public Vector3 offset;              
    public Vector3 topViewOffset = new Vector3(0f, 50f, 0f); 
    public float transitionSpeed = 5f;   

    private bool isTopView = false;     
    private Vector3 targetPosition;      
    private Quaternion targetRotation; 

    void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }

        
        targetPosition = transform.position;
        targetRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.P))
        {
            // Activar vista desde arriba
            isTopView = true;

            targetPosition = player.position + topViewOffset;
            targetRotation = Quaternion.Euler(90f, 0f, 0f);
        }
        else
        {
            // Volver a la vista normal
            isTopView = false;

            targetPosition = player.position + offset;
            targetRotation = Quaternion.LookRotation(player.position - transform.position);
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * transitionSpeed);
    }
}


using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    [Header("Player Controlled game objects")]
    public GameObject Fox;
    public GameObject Crow;
    [Header("Player Switch Script")]
    public PlayerSwitch playerSwitch;
    [Header("Camera Settings")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;
    private Vector3 vel = Vector3.zero;
    
    private void FixedUpdate()
    {
        if(playerSwitch.isFox == true) // ganti ke fox
        {
            Vector3 targetPosition = Fox.transform.position + offset;
            targetPosition.z = transform.position.z;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
        } else if(playerSwitch.isFox == false) // ganti ke crow
        {
            Vector3 targetPosition = Crow.transform.position + offset;
            targetPosition.z = transform.position.z;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
        }
    }
}

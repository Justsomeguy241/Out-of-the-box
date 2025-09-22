using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMechanic : MonoBehaviour
{
    public GameObject Camera;

    public void HeavyRotation(InputAction.CallbackContext context)//sepertiPopcornYangMeletup-letup
    {
        Camera.transform.Rotate(0, 0, -90);
    }

    public void HeavyRotation2(InputAction.CallbackContext context)//sepertiPopcornYangMeletup-letup
    {
        Camera.transform.rotation = Quaternion.Euler(0, 0, 90);
    }
}

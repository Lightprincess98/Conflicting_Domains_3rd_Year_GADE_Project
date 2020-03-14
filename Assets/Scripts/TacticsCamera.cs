using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCamera : MonoBehaviour 
{
    //Rotates the camera 90 degrees to the left
    public void RotateLeft()
    {
        transform.Rotate(Vector3.up, 90, Space.Self);
    }

    //Rotate the camera 90 degrees to the right
    public void RotateRight()
    {
        transform.Rotate(Vector3.up, -90, Space.Self);
    }
}

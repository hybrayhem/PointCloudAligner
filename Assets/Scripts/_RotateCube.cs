using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Rotates the cube in the template scene
public class RotateCube : MonoBehaviour
{
    // [Tooltip ("Changes the rotation speed of the cube")]
    // public float rotateSpeed = 1f;

    // [Tooltip("Changes orientation of the cube")]
    // public Vector3 objectRotation;

    // //Called every frame the app is running
    // // Note that "*" represents multiplication
    // void Update()
    // {
    // //Change the rotation (by the defined orientation * the time that has passed * defined speed)
    //     transform.Rotate(objectRotation * Time.deltaTime * rotateSpeed); 
    // }

    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    
    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        
        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);
        
        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);
    }

}

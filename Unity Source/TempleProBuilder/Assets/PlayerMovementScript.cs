using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
      CharacterController controler;
    public float speed = 6f;
 
    public Transform CamObj;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
 
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    Vector3 velocity;
 
 
    // Start is called before the first frame update
    void Start()
    {
 
        controler = GetComponent<CharacterController>();
 
    }
 
    // Update is called once per frame
    void Update()
    {
 
       
 
         velocity.y += gravity * Time.deltaTime;
        controler.Move(velocity * Time.deltaTime);
 
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
 
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
 
        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg * CamObj.eulerAngles.y;
            //Atan is an angle that start's at 0 at the x axis but in Unity the 0 is forward
 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
 
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controler.Move(moveDir.normalized * speed * Time.deltaTime);
 
        }
    }
 
}

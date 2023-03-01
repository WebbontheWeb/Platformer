using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MarioController : MonoBehaviour
{
    public float acceleration = 20f; //150
    public float jumpForce = 20f;
    public float jumpBoost = 7.5f; //5
    public float maxSpeed = 6f;
    public float maxSprintSpeed;

    //other
    public TextMeshPro coinText;
    public TextMeshPro scoreText;

    private int coins = 0;
    private int score = 0;

    //keeping track of whether player can jump
    //if value is greater than zero can jump
    public int canJump = 10;
    private bool jump = false;
    //so Mario can only hit an object once while jumping
    //private bool jumpHit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //using key detection in update since its iffy in fixedUpdate
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump > 0) {
            jump = true;
        }
    }

    // Update is called consistently 
    void FixedUpdate()
    {
        float horizonalAxis = Input.GetAxis("Horizontal");
        Rigidbody body = GetComponent<Rigidbody>();
        //body.velocity += horizonalAxis * Vector3.right * Time.deltaTime * acceleration;
        body.AddForce(Vector3.right * acceleration * horizonalAxis, ForceMode.Force);

        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + 0.03f;

        //checking for ground below
        //so you get 100ms of coyote time
        if(Physics.Raycast(transform.position, Vector3.down, halfHeight)){
            canJump = 10;
            GetComponent<Animator>().SetBool("Jumping", false);
        } else {
            GetComponent<Animator>().SetBool("Jumping", true);
            canJump--;
        }

        //hitting things above

        RaycastHit hit;
        
        Debug.DrawLine(transform.position,  Vector3.up, Color.blue, halfHeight);
        if (Physics.Raycast(transform.position + new Vector3(0f, 1.7f, 0f), Vector3.up, out hit, 0.09f)){
            if(hit.transform.name == "Question(Clone)"){
                coins++;
                coinText.text = $"{coins}";
                score += 100;
            } else if (hit.transform.name == "Brick(Clone)"){
                Destroy(hit.transform.gameObject);
                score += 100;
            }

            //jumpHit = true;

            Debug.Log($"hit: {hit.transform.name}");
        }

        scoreText.text = $"{score}";

        //making sure it can't go too fast
        //body.velocity = new Vector3(Mathf.Clamp(body.velocity.x, -maxSpeed, maxSpeed), body.velocity.y, body.velocity.z);

        //Jump
        if(jump){
            //Debug.Log("Jump");
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
        }

        //going higher when jump is held
        //only when you're going up, doesn't work when Y velocity is negative
        if(Input.GetKey(KeyCode.Space) && body.velocity.y > 0){
            body.AddForce(Vector3.up * jumpBoost, ForceMode.Force);
        }

        float xVelocity;
        //clamping horizontal speed
        //can go quicker if sprinting
        if(Input.GetKey(KeyCode.LeftShift)){
            xVelocity = Mathf.Clamp(body.velocity.x, -maxSprintSpeed, maxSprintSpeed);
        } else {
            xVelocity = Mathf.Clamp(body.velocity.x, -maxSpeed, maxSpeed);
        }

        if(Mathf.Abs(horizonalAxis) < 0.1f){
            xVelocity *= 0.9f;
        }

        if(xVelocity >= 0){
            Quaternion direction = Quaternion.identity;
            direction.eulerAngles = new Vector3(0,90,0);
            transform.rotation = direction;
        } else {
            //transform.rotation = Quaternion.Euler(0, 90, 0);
            Quaternion direction = Quaternion.identity;
            direction.eulerAngles = new Vector3(0,-90,0);
            transform.rotation = direction;
        }

        //Debug.DrawLine(transform.position, transform.position + Vector3.down * halfHeight, Color.blue, 0f);
        body.velocity = new Vector3(xVelocity, body.velocity.y, body.velocity.z);

        float speed = body.velocity.magnitude;
        GetComponent<Animator>().SetFloat("Speed", speed);
    }
}

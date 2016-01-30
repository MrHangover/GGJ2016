using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    [Range(0f, 10f)]
    public float maxSpeed = 5f;
    [Range(0f, 50f)]
    public float acceleration = 2f;
    [Range(0f, 50f)]
    public float friction = 1f;
    public GameObject newMe;

    Vector2 input;
    Rigidbody2D body;
    Animator anim;
    Vector3 respawnPosition;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        respawnPosition = transform.position;
        newMe = gameObject;
    }

    // Update is called once per frame
    void Update () {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(input.sqrMagnitude > 1f)
        {
            input.Normalize();
        }
        
        if(input.sqrMagnitude > 0f)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        anim.SetFloat("ySpeed", input.y);
    }

    void FixedUpdate()
    {
        //Applying friction / stopping
        if (input.x == 0f)
        {
            if (Mathf.Abs(body.velocity.x) > Mathf.Abs(body.velocity.x * Time.fixedDeltaTime))
                body.velocity = new Vector2(body.velocity.x - friction * Mathf.Sign(body.velocity.x) * Time.fixedDeltaTime, body.velocity.y);
            else
                body.velocity = new Vector2(0f, body.velocity.y);
        }
        if (input.y == 0f)
        {
            if (Mathf.Abs(body.velocity.y) > Mathf.Abs(body.velocity.y * Time.fixedDeltaTime))
                body.velocity = new Vector2(body.velocity.x, body.velocity.y - friction * Mathf.Sign(body.velocity.y) * Time.fixedDeltaTime);
            else
                body.velocity = new Vector2(body.velocity.x, 0f);
        }

        //Applying movement / acceleration
        body.velocity = body.velocity + input * acceleration * Time.fixedDeltaTime;
        if (body.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            body.velocity = body.velocity.normalized * maxSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        
        Debug.Log("Trigger!");

        GameObject colObj = collide.gameObject;

        if(colObj.GetComponent<EnemyGeneric>().gameObject != null)
        {
            if(colObj.GetComponent<EnemyGeneric>().Kills())
            {
                Reset();
            }
        } else if  ( collide.gameObject.name== "Fireball(Clone)")
        {

            Reset();
        }
        else
        {
            Debug.Log("Hvad er det? "+collide.gameObject.name);
        }
        return;
    }
    
    void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}

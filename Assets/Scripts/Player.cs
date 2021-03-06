using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public SpriteRenderer render;
    public Animator anim;
    public Sprite lookRight, lookLeft, lookUp, lookDown, lvl1foreground;
    public Animation walkFront, walkBack, walkRight, walkLeft;
    public int hp = 4;
    private float time = 0, redTime = 0;
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    public Vector2 jumpHeight;
    public bool Floor;
    public Text txt;



    private void Start()
    {
        PlayerPrefs.SetInt("Points", 0);
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider2D>();

        

    }
    private void Update()
    {
        txt.text = ""+PlayerPrefs.GetInt("Points");
        if (Input.GetKeyDown(KeyCode.W)&& Floor==true)
        {
            Floor = false;
            GetComponent<Rigidbody2D>().AddForce(jumpHeight, ForceMode2D.Impulse);
        }
    }
    private void FixedUpdate()
    {

        
        /* if (hp == 0)
         {
             SceneManager.UnloadSceneAsync("Level 1");
             SceneManager.LoadSceneAsync("Game Over");
         }*/

        float x = Input.GetAxisRaw("Horizontal");


        // Reset MoveDelta
        moveDelta = new Vector3(x,0,0);

        // Change Sprite

        if (Input.GetButton("Fire1") || Input.GetButton("Fire2") || Input.GetButton("Fire3") || Input.GetButton("Jump"))
        {
            if (Input.GetButton("Fire1"))
            {
                anim.SetInteger("direction", 5);
            }
            else if (Input.GetButton("Fire2"))
            {
                anim.SetInteger("direction", 6);
            }
            else if (Input.GetButton("Fire3"))
            {
                anim.SetInteger("direction", 7);
            }
            else
            {
                anim.SetInteger("direction", 8);
            }
        } else if (moveDelta.y != 0 || moveDelta.x != 0)
        {
            if (moveDelta.y > 0)
            {
                anim.SetInteger("direction", 1);
            }
            else if (moveDelta.y < 0)
            {
                anim.SetInteger("direction", 3);
            }
            else if (moveDelta.x > 0)
            {
                anim.SetInteger("direction", 2);
            }
            else if (moveDelta.x < 0)
            {
                anim.SetInteger("direction", 4);
            }

        } else
        {
            anim.SetInteger("direction", 0);
        }

        // Collisions
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
       

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)

        {
            transform.Translate(4*moveDelta.x * Time.deltaTime, 0, 0);
        }

        // Fake3d
        if (transform.position.y > -0.58)
        {
            render.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Player");

        } else
        {
            render.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Player Above");
        }

        if (time > 0)
        {
            time += Time.deltaTime;
            redTime += Time.deltaTime;
            render.color = new Color(255, 0, 0, 128);
            if (redTime > 0.25 && redTime < 0.5) {
                render.color = new Color(255, 255, 255, 255);
            } else if (redTime > 0.5)
            {
                render.color = new Color(255, 0, 0, 128);
                redTime = 0;
            }
        }

        if (time > 1)
        {
            time = 0;
            render.color = new Color(255, 255, 255, 255);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") && time == 0)
        {
            hp = hp - 1 ;
            time += Time.deltaTime;
        }

        if (collision.gameObject.tag.Equals("Fruit") && hp<4)
        {
            hp += 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag.Equals("Fruit") )
        {
           
            Destroy(collision.gameObject);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Floor") && Floor == false)
        {
            Floor = true;
        }
    }

}   
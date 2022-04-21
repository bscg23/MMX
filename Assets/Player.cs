using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float firerate;
    [SerializeField] int jumforce;
    [SerializeField] GameObject bullet;           
    [SerializeField] GameObject point;
    AudioSource sound;
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip kill;

    Rigidbody2D mybody;
    Animator myanim;
    bool IsGrounded = true;
    float nextbullet = 0;


    // Start is called before the first frame update
    void Start()
    {
        mybody = GetComponent<Rigidbody2D>();
        myanim = GetComponent<Animator>();
        StartCoroutine(Micorutina());
        sound = GetComponent<AudioSource>();
    }

     IEnumerator Micorutina()
    {
        while (true)
        {
            Debug.Log("Esperando cuatro segundos");
            yield return new WaitForSeconds(4);
            Debug.Log("Pasaron cuatro segundos");
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position,
            Vector2.down,
            1.3f,
            LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 1.3f, Color.red);

        IsGrounded = (ray.collider != null);
        Jump();
        Fire();

    }
    
    void Fire()
    {
        if (Input.GetKey(KeyCode.Z) && Time.time >= nextbullet)
        {
            nextbullet = Time.time + firerate;
            myanim.SetLayerWeight(1, 1);
            Instantiate(bullet, point.transform.position, transform.rotation);
            sound.PlayOneShot(shoot, 1f);
        }
        else
        {
            myanim.SetLayerWeight(1, 0);
        }

    }


    void Jump()
    {
        if (IsGrounded)

        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("saltando!");
                mybody.AddForce(new Vector2(0, jumforce), ForceMode2D.Impulse);
            }
        }
        if (mybody.velocity.y != 0 && !IsGrounded)
            myanim.SetBool("Jumping", true);
        else 
            myanim.SetBool("Jumping", false);
    }

    private void FixedUpdate()
    {
        float dirH = Input.GetAxis("Horizontal");
        
        if (dirH != 0)
        {
            myanim.SetBool("Running", true);
            if (dirH < 0)
                transform.localScale = new Vector2(-1, 1);
            else
                transform.localScale = new Vector2(1, 1);

        }
        else
        
            myanim.SetBool("Running", false);

        mybody.velocity = new Vector2(dirH * speed, mybody.velocity.y);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            myanim.SetBool("Death", true);
            sound.PlayOneShot(kill, 1f);
        }
    }
}

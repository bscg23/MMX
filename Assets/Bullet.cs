using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float FireRate;
    [SerializeField] float speed;
    bool Impact = false;
    Rigidbody2D myBody;
    Animator myAnim;
    Vector2 dirBala;
    float dirH;
    float direction;
    float zzz = 0;


    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

      
        GameObject player = GameObject.Find("Player");
        Transform playerTransform = player.transform;
        direction = playerTransform.localScale.x;

        if (direction == 1)
        {
            dirBala = Vector2.left;
        }
        else if (direction == -1)
        {
            dirBala = Vector2.right;
        }
    }



    // Update is called once per frame
    void Update()
    {

        RaycastHit2D ray = Physics2D.Raycast(transform.position, dirBala, direction * 0.3f, LayerMask.GetMask("Ground", "EnemyT", "EnemyV"));
        Impact = (ray.collider != null);
        if (zzz!=0)
        {
            transform.position = new Vector3(zzz, transform.position.y, 0);
        }
        else if (Impact)
        {
             zzz = transform.position.x;
            myAnim.SetBool("Impact", true);
            Object.Destroy(gameObject, 0.5f);
        }
        else
        {
            transform.Translate(new Vector3(direction * speed * Time.deltaTime, 0, 0));
            myAnim.SetBool("Impact", false);
        }


    }

    private void FixedUpdate()
    {

    }



}
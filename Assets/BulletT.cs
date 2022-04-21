using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletT : MonoBehaviour
{

    [SerializeField] float speed;
    Animator myanim;
    // Start is called before the first frame update
    void Start()
    {
        myanim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //myanim.SetBool("Destroy", true);
            Object.Destroy(gameObject, 0.0f);
        }

    }
}
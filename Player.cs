using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//※6
using UnityEngine.SceneManagement;//※7

public class Player : MonoBehaviour
{

    private CharacterController characterController;//※1
    private Vector3 velocity;//※1
    public float walkSpeed;//※1
    private Animator animator;//※2
    public GameObject bullet;//※3
    public Transform muzzle;//※3
    public float speed = 1000;//※3
    private float PlayerHP;//※6
    public GameObject slider;//※6
    private Slider _slider;//※6
    private GameObject[] enemyObjects;//※8

    void Start()//ゲーム開始時に一度だけ実行する内容
    {
        characterController = GetComponent<CharacterController>();//※1
        animator = GetComponent<Animator>();//※2
        _slider = slider.GetComponent<Slider>();//※6
        PlayerHP = 5;//※6
    }

    void FixedUpdate()//ゲーム開始時に何度も実行する内容
    {
        if (characterController.isGrounded)//※1
        {
            velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));//※1
            if (velocity.magnitude > 0.1f)//※1
            {
                animator.SetBool("isRunning", true);//※2
                transform.LookAt(transform.position + velocity);//※1
            }
            else//※2
            {
                animator.SetBool("isRunning", false);//※2
            }
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;//※1
        characterController.Move(velocity * walkSpeed * Time.deltaTime);//※1

        if (Input.GetMouseButtonDown(0))//※3
        {
            StartCoroutine("shot");//※3
        }

        _slider.value = PlayerHP;//※6

        if (PlayerHP == 0)//※7
        {
            SceneManager.LoadScene("GameOver");//※7
            GetComponent<AudioSource>().Play();
        }
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");//※8
        if (enemyObjects.Length == 0)//※8
        {
            SceneManager.LoadScene("GameClear");//※8
        }
    }

    IEnumerator shot()//※3
    {
        animator.SetBool("Attack", true);//※3
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.3f);//※3
        Vector3 force;//※3
        GameObject bullets = GameObject.Instantiate(bullet) as GameObject;　//※3
        bullets.transform.position = muzzle.position;//※3
        force = this.gameObject.transform.forward * speed;//※3
        bullets.GetComponent<Rigidbody>().AddForce(force);//※3
        animator.SetBool("Attack", false);//※3
        Destroy(bullets.gameObject, 1); //※3
    }

    IEnumerator Damage()//※6
    {
        PlayerHP -= 1;//※6
        yield return new WaitForSeconds(0.5f);//※6
    }

    void OnTriggerEnter(Collider collision)//※6
    {
        if (collision.gameObject.tag == "Enemyattack")//※6
        {
            StartCoroutine("Damage");//※6
        }
    }
}
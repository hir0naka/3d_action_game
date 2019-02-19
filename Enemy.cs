using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//※4

public class Enemy : MonoBehaviour
{
    public GameObject target;//※4
    NavMeshAgent agent;//※4
    Animator animator;//※4
    public GameObject Enemyattack;//※4
    public Transform enemy;//※4
    public GameObject Explosion;//※5

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();//※4
        animator = GetComponent<Animator>();//※4
    }

    void Update()
    {
        agent.destination = target.transform.position;//※4
    }

    void OnTriggerEnter(Collider collision)//※4
    {
        if (collision.gameObject.tag == "Player")//※4
        {
            StartCoroutine("EnemyAttack");//※4
        }
        if (collision.gameObject.tag == "Ball")//※5
        {
            GameObject Explosions = GameObject.Instantiate(Explosion) as GameObject;//※5
            Explosions.transform.position = enemy.position;//※5
            Destroy(this.gameObject);//※5
            Destroy(Explosions.gameObject, 1);//※5
        }
    }

    IEnumerator EnemyAttack()//※4
    {
        animator.SetBool("EnemyAttack", true);//※4
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.05f);//※4
        GameObject Enemyattacks = GameObject.Instantiate(Enemyattack) as GameObject;//※4
        Enemyattacks.transform.position = enemy.position;//※4
        animator.SetBool("EnemyAttack", false);//※4
        Destroy(Enemyattacks.gameObject, 1);//※4
    }
}
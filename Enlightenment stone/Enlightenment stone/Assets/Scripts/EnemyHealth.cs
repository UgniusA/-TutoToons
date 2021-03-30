using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    int health;

    [SerializeField]
    int maxHealth;

    public Slider slider;
    public GameObject HealthBarUI;

    [SerializeField]
    int damage = 25;

    public Rigidbody rb;

    Animator _animator;

    bool isDead;

    public GameObject Drop;

    public Transform target;

    List<GameObject> list = new List<GameObject>();

    void Start()
    {
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
        slider.value = calculateHeatlh();
    }

    void Update()
    {
        slider.value = calculateHeatlh();

        if (health >= maxHealth)
        {
            HealthBarUI.SetActive(false);
        }
        else
        {
            HealthBarUI.SetActive(true);
        }
        if (health <= 0)
        {
            if (!isDead)
            {
                GetComponent<EnemyHealth>().enabled = false;
                GetComponent<EnemyAi>().enabled = false;
                StartCoroutine(StartDeath());
            }
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }


    float calculateHeatlh()
    {
        return (float)health / (float)maxHealth;
    }

    internal void addHealth(int amount)
    {
        health += amount;
    }

    void OnCollisionEnter(Collision collision)
    {
        IDamageable Player = collision.gameObject.GetComponent<IDamageable>();

        if (Player != null)
        {
            Player.TakeDamage(-damage);
        }
    }

    IEnumerator StartDeath()
    {
        isDead = true;
        _animator.SetTrigger("Death");
        yield return new WaitForSeconds(3.3f);
        Instantiate(Drop, transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
        GetComponent<EnemyHealth>().enabled = true;
        GetComponent<EnemyAi>().enabled = true;
        Destroy(gameObject);
    }
}




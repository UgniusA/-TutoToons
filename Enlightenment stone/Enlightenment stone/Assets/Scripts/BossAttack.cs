using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public LayerMask layerMask;
    public int damage = 50;
    public float range = 1f;

    public float sightRange = 5f;
    public float sightAngle = 90f;

    private GameObject Player;

    public Transform target;

    private bool isHitting = false;

    public ParticleSystem enemyattack;

    Animator _animator;

    HealthBarController healthBar;
    ArmorBarController armorBar;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        healthBar = Player.GetComponent<HealthBarController>();
        armorBar = Player.GetComponent<ArmorBarController>();
    }
    void Update()
    {
        transform.LookAt(target);
        Vector3 dist = (Player.transform.position + Vector3.up) - (transform.position + Vector3.up);
        if (dist.magnitude < sightRange)
        {
            float cosAngle = Vector3.Dot(dist.normalized, transform.forward.normalized);
            float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
            if (angle < sightAngle / 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, dist, out hit, range, layerMask))
                {
                    if (!isHitting)
                    {
                        enemyattack.Play();
                        if (Physics.Raycast(transform.position + Vector3.up, dist, out hit, range, layerMask))
                            Debug.Log("Hit");
                        _animator.SetTrigger("Attack");
                    }
                }
            }
        }
    }

    void Hit()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, forward, out hit, range, layerMask))

        {
            {
                if (armorBar.armor > 0)
                {
                    armorBar.TakeArmor(25);
                }
                else
                {
                    healthBar.TakeDamage(35);
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Not Hit");
        }
    }

    void EndHit()
    {
        isHitting = false;
    }

    void StartHit()
    {
        isHitting = true;
    }
}
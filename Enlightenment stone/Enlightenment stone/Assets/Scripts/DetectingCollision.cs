using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingCollision : MonoBehaviour
{
    public LayerMask layerMask;
    public int damage = 20;
    public float range = 1f;

    public float sightRange = 5f;
    public float sightAngle = 90f;

    private GameObject Player;

    private bool isHitting = false;

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
                    {if (Physics.Raycast(transform.position + Vector3.up, dist, out hit, range, layerMask))
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
                    armorBar.TakeArmor(20);
                }
                else
                {
                    healthBar.TakeDamage(20);
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
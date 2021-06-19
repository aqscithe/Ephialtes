using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class HealthManager : MonoBehaviour
{
    [Range(1, 2)]
    [SerializeField] public int maxHealth = 2;
    [HideInInspector] public int health = 0;
    [HideInInspector] public bool invincible = false;
    [HideInInspector] public bool stunned = false;
    [SerializeField] float invincibilityCooldown = 2f;
    [SerializeField] float stunCooldown = 3f;

    private Animator animator = null;
    private bool attacked = false;
    private bool dead = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        SetAnimatorValues();

        if (!invincible && !stunned)
            return;

        StartCoroutine(DisableStun());
        StartCoroutine(DisableInvicibility());
    }

    private void SetAnimatorValues()
    {
        animator.SetBool("IsDead", dead);
        animator.SetBool("Attacked", attacked);
        animator.SetBool("Stunned", stunned);
        animator.SetBool("IsInjured", (health < maxHealth));
    }

    IEnumerator DisableStun()
    {
        yield return new WaitForSeconds(stunCooldown);
        stunned = false;
    }

    IEnumerator DisableInvicibility()
    {
        yield return new WaitForSeconds(invincibilityCooldown);
        invincible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null)
            return;

        if (other.gameObject.tag.Contains("Enemy") && !invincible)
        {
            invincible = true;
            stunned = true;
            SubtractHealth(other.gameObject.transform.parent.GetComponent<Damage>().GetDamage());
            
        }
    }

    public void SubtractHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (transform.tag.Equals("Player"))
            {
                SetDead();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            attacked = true;
        }
            
    }

    private void SetDead()
    {
        dead = true;
        gameObject.GetComponent<MovePlayer>().dead = true;
    }

    public void SetAlive()
    {
        dead = false;
        gameObject.GetComponent<MovePlayer>().dead = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trap : MonoBehaviour
{
    [SerializeField] float trapTime = 2.5f;
    [SerializeField] float armTime = 3f;
    [SerializeField] float triggerRadius = 1f;

    private bool target = false;
    private bool arming = false;

    private InnerTrapTriggered innerTrap;
    private Damage dmg;
    private MovePlayer mp;
    

    private void Awake()
    {
        mp = FindObjectOfType<MovePlayer>();
        innerTrap = GetComponentInChildren<InnerTrapTriggered>();
        innerTrap.SetRadius(triggerRadius);
        dmg = GetComponent<Damage>();
    }

    private void Update()
    {
        if (target)
        {
            StartCoroutine(DisarmTrap());
        }
        else if(arming)
        {
            StartCoroutine(ArmTrap());
        }
    }

    IEnumerator ArmTrap()
    {
        yield return new WaitForSeconds(armTime);
        arming = false;
    }

    IEnumerator DisarmTrap()
    {
        yield return new WaitForSeconds(trapTime);
        target = false;
        arming = true;
        UnlockPlayerMovement();
    }

    private void LockPlayerMovement()
    {
        mp.trapped = true;
    }

    public void OnTriggerEnterChild(ref Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !arming)
        {
            target = true;
            other.gameObject.GetComponent<HealthManager>().SubtractHealth(dmg.GetDamage());
            LockPlayerMovement();
        }
    }

    private void UnlockPlayerMovement()
    {
        mp.trapped = false;
    }
}

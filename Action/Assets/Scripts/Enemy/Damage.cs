using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    [SerializeField] int damageDealt = 1;

    public int GetDamage() { return damageDealt; }
}

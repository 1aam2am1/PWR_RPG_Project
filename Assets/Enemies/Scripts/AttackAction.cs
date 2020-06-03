using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackAction : MonoBehaviour
{
    public abstract void Attack(float time);
    public virtual void TimeNotAttack(float time) { }
}

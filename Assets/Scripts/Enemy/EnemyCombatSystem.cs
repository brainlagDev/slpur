using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatSystem : MonoBehaviour
{
    public EnemyCombatSettings CombatSettings;
    [SerializeField] protected Animator Animator;
    [SerializeField] protected Transform AttackPoint;
    [SerializeField] protected LayerMask AttackLayer;
    public bool IsAttacking;

    public virtual void Attack()
    {
        if(!IsAttacking)
            Animator.SetTrigger("Attack0");
    }
    public virtual void SetIsAttacking(int i)
    {
        if (i == 1)
        {
            IsAttacking = true;
        }
        else IsAttacking = false;
    }
    public virtual void AttackMoment()
    {
        Collider2D[] damagedCreatures = Physics2D.OverlapCircleAll(AttackPoint.position, CombatSettings.AttackRadius);
        foreach (var creature in damagedCreatures)
        {
            foreach (var tag in CombatSettings.TagsToAttack)
            {
                if (creature.CompareTag(tag))
                {
                    Debug.Log("Attack");
                    creature.gameObject.GetComponent<Creature>().GetDamage(CombatSettings.LightAttackData);
                    //creature.SendMessage("GetDamage", CombatSettings.LightAttackData);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, CombatSettings.AttackRadius);
    }
}

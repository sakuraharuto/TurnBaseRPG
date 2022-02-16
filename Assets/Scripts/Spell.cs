using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public string spellName;
    public int power;
    public int manaCost;
    public enum SpellType{ Attack, Heal}
    public SpellType spellType;

    private Vector3 targetPosition; 

    private void Update() {
        if(targetPosition != Vector3.zero){   // There is a target
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 20f);   //From spell pos to target pos in speed 20f
            if(Vector3.Distance(transform.position, targetPosition) < 0.25){
                Destroy(this.gameObject, 1);
            }
        } else { // There is no target
            Destroy(this.gameObject);
        }
    }
    public void CastSpell(Character target){
        targetPosition = target.transform.position;

        if (spellType == SpellType.Attack){
            target.Hurt(power);
        } else if (spellType == SpellType.Heal){
            target.Heal(power);
        }
    }
}

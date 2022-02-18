using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public override void Die()
    {
        base.Die();
    }

    public void Act(){
        int dieRoll = Random.Range(0, 2);
        switch(dieRoll){
            case 0:
                Defend();
                break;
            case 1:
                // cast spell
                Spell spellToCast = GetRandomSpell();
                if(spellToCast.spellType == Spell.SpellType.Heal){
                    // get friendly weak unit
                }
                if(!CastSpell(spellToCast, null)){
                    // attack
                }
                break;
            case 2:
                // attack
                break;
        }
    }

    Spell GetRandomSpell(){
        return spells[Random.Range(0, (spells.Count - 1))];
    }
}

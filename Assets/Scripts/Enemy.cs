using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public override void Die()
    {
        base.Die();
        BattleController.Instance.characters[1].Remove(this);
        Debug.Log("Enemy die");
    }

    public void Act(){
        int dieRoll = Random.Range(0, 3);
        Character target = BattleController.Instance.GetRandomPlayer();
        switch(dieRoll){
            case 0:
                Defend();
                Debug.Log("Enemy defend");
                break;
            case 1:
                // cast spell
                Spell spellToCast = GetRandomSpell();
                //Debug.Log(spellToCast.spellType);
                if(spellToCast.spellType == Spell.SpellType.Heal){
                    // get friendly weak unit
                    target = BattleController.Instance.GetWeakestUnit();
                    Debug.Log("Enemy heal");
                }
                if(!CastSpell(spellToCast, target)){
                    // attack
                    BattleController.Instance.DoAttack(this, target);
                    Debug.Log("Enemy Attack");
                }
                break;
            case 2:
                // attack
                BattleController.Instance.DoAttack(this, target);
                Debug.Log("Enemy attack");
                break;
        }
    }

    Spell GetRandomSpell(){
        return spells[Random.Range(0, (spells.Count - 1))];
    }
}

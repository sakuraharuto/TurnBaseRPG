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
                Debug.Log("Enemy defend");
                Defend(); 
                break;
            case 1:
                // cast spell
                Spell spellToCast = GetRandomSpell();
                //Debug.Log(spellToCast.spellType);
                if(spellToCast.spellType == Spell.SpellType.Heal){
                    // get friendly weak unit
                    Debug.Log("Enemy heal");
                    target = BattleController.Instance.GetWeakestUnit();
                }
                if(!CastSpell(spellToCast, target)){
                    // attack
                    Debug.Log("Enemy cast spell");
                    BattleController.Instance.DoAttack(this, target);
                }
                break;
            case 2:
                // attack
                Debug.Log("Enemy attack");
                BattleController.Instance.DoAttack(this, target);
                break;
        }
    }

    Spell GetRandomSpell(){
        return spells[Random.Range(0, (spells.Count - 1))];
    }
}

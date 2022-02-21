using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : Character
{
    public override void Die()
    {
        base.Die();
        BattleController.Instance.characters[0].Remove(this);
    }
}

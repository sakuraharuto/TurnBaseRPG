using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpwanPoint : MonoBehaviour
{
    public Character Spwan(Character character){
        Character characterToSpwan = Instantiate<Character>(character, this.transform);
        return characterToSpwan;
    }
}

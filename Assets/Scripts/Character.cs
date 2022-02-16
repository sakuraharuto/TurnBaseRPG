using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int health;
    public int maxHealth;
    public int attackPower;
    public int defencePower;
    public int manaPoint;
    public List<Spell> spells;

    public void Hurt(int amount){
        int damageAmount = Random.Range(0, 1) * (amount - defencePower); // Either do 0 damage or full damage
        health = Mathf.Max(health - damageAmount, 0); // Ensure health never go below 0
        if (health == 0){
            // Character die
            Die();
        }
    }

    public void Heal(int amount){
        int healAmount = (int)(amount + (maxHealth * .33f)); // The more max HP you have, the more heal you received
        health = Mathf.Min(health + healAmount, maxHealth); // Ensure health not above max health 
    }

    public void Defend(){
        defencePower += (int)(defencePower * .33f);
    }

    public void Die(){
        Destroy(this.gameObject);
    }
}

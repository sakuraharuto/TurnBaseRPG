using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{   
    public static BattleController Instance {get; set;}
    public Dictionary<int, List<Character>> characters = new Dictionary<int, List<Character>>();  // 0 for player, 1 for enemy. For example, characters[1][0] means first character on enemy team
    public int characterTurnIndex;
    public int activeTurn;
    public Spell playerSelectedSpell;
    public bool playerIsAttack;
    [SerializeField] private BattleSpwanPoint[] spwanPoints;
    [SerializeField] private BattleUIController uiController;

    private void Start() {
        if (Instance != null && Instance != this){
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
        characters.Add(0, new List<Character>());
        characters.Add(1, new List<Character>());

        FindObjectOfType<BattleLauncher>().Launch();  // Call Launch function from BattleLauncher class instaed of BattleController class
    }

    public Character GetRandomPlayer(){
        return characters[0][Random.Range(0, characters[0].Count - 1)];
    }

    public Character GetWeakestUnit(){
        Character weakestUnit = characters[1][0];
        foreach(Character character in characters[1]){
            if(character.health < weakestUnit.health){
                weakestUnit = character;
            }
        }

        return weakestUnit;
    }

    private void nextTurn(){
        // If we are in turn 0, set activeTurn to 1. If its not 0, which means we are in turn 1, set activeTurn to 0. This means after we moved, enemies move, than we move again.
        activeTurn = activeTurn == 0 ? 1 : 0;  
    }

    private void nextAct(){
        // If we have one friendly and one enemy alive
        if (characters[0].Count > 0 && characters[1].Count > 0){
            if (characterTurnIndex < (characters[activeTurn].Count - 1)){
                characterTurnIndex++;
            } else {
                nextTurn();
                characterTurnIndex = 0;
            }

            switch(activeTurn){
                case 0:
                // Player team move, update UI
                uiController.ToggleActionState(true);
                uiController.BuildSpellList(GetCurrentCharacter().spells);
                break;

                case 1:
                // Enemy team move, update UI
                StartCoroutine(PerformAct());
                uiController.ToggleActionState(false);
                break;
            }
        } else {
            Debug.Log("Battle finish"); // Battle finished, elimated all enemies or we all dead, back to town scene or gameover maybe? 
        }
    }

    IEnumerator PerformAct(){
        yield return new WaitForSeconds(.75f);
        if(GetCurrentCharacter().health > 0){
            GetCurrentCharacter().GetComponent<Enemy>().Act();
        }
        uiController.UpdateCharacterUI();
        yield return new WaitForSeconds(1f);
        nextAct();
    }

    public void SelectCharacter(Character character){
        if(playerIsAttack){
            DoAttack(GetCurrentCharacter(), character);
        } else if(playerSelectedSpell != null){
            if(GetCurrentCharacter().CastSpell(playerSelectedSpell, character)){
                uiController.UpdateCharacterUI();
                nextAct();
            } else {
                Debug.LogWarning("Not enough mana to cast spell");
            }
        }
    }

    public void DoAttack(Character attacker, Character target){
        target.Hurt(attacker.attackPower);
    }
    public void StartBattle(List<Character> players, List<Character> enemies){
        for(int i = 0; i < players.Count; i++){
            characters[0].Add(spwanPoints[i + 3].Spwan(players[i]));     // Spawn point(3,4,5) belong to players, so use i + 3
        }

        for(int i = 0; i < enemies.Count; i++){ 
            characters[1].Add(spwanPoints[i].Spwan(enemies[i]));  // Spwan point(0,1,2) belong to enemies
        }
    }

    public Character GetCurrentCharacter(){
        return characters[activeTurn][characterTurnIndex]; // Return the character that currently moving
    }
}

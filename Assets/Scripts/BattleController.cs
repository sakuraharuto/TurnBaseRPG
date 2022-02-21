using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{   
    public static BattleController Instance {get; set;}
    public Dictionary<int, List<Character>> characters = new Dictionary<int, List<Character>>();  // 0 for player, 1 for enemy
    public int characterTurnIndex;
    public int activeTurn;
    public Spell playerSelectedSpell;
    public bool playerIsAttack;
    [SerializeField] private BattleSpwanPoint[] spwanPoints;

    private void Start() {
        if (Instance != null && Instance != this){
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
        characters.Add(0, new List<Character>());
        characters.Add(1, new List<Character>());
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
        activeTurn = activeTurn == 0 ? 1 : 0;  // If we are in turn 0, set activeTurn to 1. If its not 0, which means we are in turn 1, set activeTurn to 0
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
                // UI stuff
                break;

                case 1:
                //UI stuff and action
                StartCoroutine(PerformAct());
                break;
            }
        } else {
            Debug.Log("Battle finish"); // One side all dead, back to town scene maybe? 
        }
    }

    IEnumerator PerformAct(){
        yield return new WaitForSeconds(.75f);
        if(characters[activeTurn][characterTurnIndex].health > 0){
            characters[activeTurn][characterTurnIndex].GetComponent<Enemy>().Act();
        }
        yield return new WaitForSeconds(1f);
        nextAct();
    }

    public void SelectCharacter(Character character){
        if(playerIsAttack){
            DoAttack(characters[activeTurn][characterTurnIndex], character);
        } else if(playerSelectedSpell != null){
            if(characters[activeTurn][characterTurnIndex].CastSpell(playerSelectedSpell, character)){
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
}

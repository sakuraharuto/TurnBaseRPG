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
    public void StartBattle(List<Character> players, List<Character> enemies){
        for(int i = 0; i < players.Count; i++){
            characters[0].Add(spwanPoints[i + 3].Spwan(players[i]));     // Spawn point(3,4,5) belong to players, so use i + 3
        }

        for(int i = 0; i < enemies.Count; i++){ 
            characters[1].Add(spwanPoints[i].Spwan(enemies[i]));  // Spwan point(0,1,2) belong to enemies
        }
    }
}

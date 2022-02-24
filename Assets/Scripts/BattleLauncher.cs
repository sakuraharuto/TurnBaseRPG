using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleLauncher : MonoBehaviour
{   
    public List<Character> listOfPlayers {get; set;}
    public List<Character> listOfEnemies {get; set;}

    void Awake() {
        DontDestroyOnLoad(this);
    }

    public void PrepareBattle(List<Character> enemies, List<Character> players){
        listOfPlayers = players;
        listOfEnemies = enemies;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");  // Load the battle scene, name must be the same as the scene's name, case senstive
    }

    public void Launch(){
        BattleController.Instance.StartBattle(listOfPlayers, listOfEnemies);
    }
    
}

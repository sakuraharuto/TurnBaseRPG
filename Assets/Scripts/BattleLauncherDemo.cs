using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLauncherDemo : MonoBehaviour
{
    [SerializeField] private List<Character> PlayersTeam, EnemiesTeam;
    [SerializeField] private BattleLauncher launcher;

    public void Launch(){
        launcher.PrepareBattle(EnemiesTeam, PlayersTeam);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardUI : MonoBehaviour
{
    public Text ScoreboardText;
    public class PlayerStats {
        public Player Player;
        public string Name;
        public int DamageDealt;
        public int DamageTaken;

        public PlayerStats(Player player, string name) {
            Player = player;
            Name = name;
            DamageDealt = 0;
            DamageTaken = 0;
        }
    }
    
    private List<PlayerStats> PlayerScores = new List<PlayerStats>();

    public static ScoreboardUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        Player[] players = GameObject.FindObjectsOfType<Player>();
        foreach (var player in players)
        {
            PlayerScores.Add(new PlayerStats(player, player.gameObject.name));
        }
        
        UpdateScoreboard();
    }


    void UpdateScoreboard() {
        if (ScoreboardText == null)
            return;
        
        string newText = "Damage Dealt:\n";

        foreach (var score in PlayerScores)
        {
            newText += score.Name + " : " + score.DamageDealt + "\n";
        }

        newText.Remove(newText.Length - 2, 2);

        ScoreboardText.text = newText;
    }

    public void UpdatePlayerDamage(int damage, Player player) {
        // PlayerStats playerStat = FindPlayerStats(player);
        //
        // playerStat.DamageDealt += damage;
        //
        // UpdateScoreboard();
    }

     // private PlayerStats FindPlayerStats(Player player)
     // {
     //     foreach (var playerScore in PlayerScores)
     //     {
     //         if (playerScore.Player == player)
     //             return playerScore;
     //     }
     //
     //     throw new Exception("Couldn't find player");
     // }
}

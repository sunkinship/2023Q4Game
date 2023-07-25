using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class InitializeLevelData : MonoBehaviour
{
    [SerializeField]
    private int level = 0;

    public Player player;

    private void Awake()
    {
        currentLevel = level;
        if (gameState == GameState.story)
        {
            player.playerData = Instance.playerData[abilityStateStory];
        }
        else
        {
            if (level == 1)
                player.playerData = Instance.playerData[level];
            else if (level == 2)
            {
                if (GetAbilityState() > 1)
                    player.playerData = Instance.playerData[level];
                else
                    player.playerData = Instance.playerData[1];
            }
            else if (level == 3)
            {
                if (GetAbilityState() > 2)
                    player.playerData = Instance.playerData[level];
                else if (GetAbilityState() > 1)
                    player.playerData = Instance.playerData[2];
                else
                    player.playerData = Instance.playerData[1];
            }
        }
        Destroy(gameObject);
    }
}
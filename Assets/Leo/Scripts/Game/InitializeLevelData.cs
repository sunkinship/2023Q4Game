using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevelData : MonoBehaviour
{
    [SerializeField]
    private int level = 0;

    public Player player;

    private void Awake()
    {
        player.playerData = GameManager.Instance.playerData[GameManager.abilityState];
        Destroy(gameObject);
    }
}

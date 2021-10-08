using System.Collections;
using System.Collections.Generic;
using BotScheduler.Gameplay.Level;
using BotScheduler.Managers;
using UnityEngine;


namespace BotScheduler.Actors {
    [RequireComponent(typeof(PlayerManager))]
    [RequireComponent(typeof(LevelManager))]
    public class ManagersActor : MonoBehaviour {
        [SerializeField]
        private LevelConfiguration levelConfiguration;

        private PlayerManager playerManager;
        private LevelManager levelManager;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            levelManager = GetComponent<LevelManager>();
        }

        private void Start()
        {
            playerManager.levelConfiguration = levelConfiguration;
            levelManager.levelConfiguration = levelConfiguration;
        }
    }
}
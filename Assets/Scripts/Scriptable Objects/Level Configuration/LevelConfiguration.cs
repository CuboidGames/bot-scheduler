using System.Collections.Generic;
using BotScheduler.Gameplay.Commands;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/Level Configuration", order = 1)]
public class LevelConfiguration : ScriptableObject
{
  public int queueSize = 1;
  public List<CommandType> commands;
}
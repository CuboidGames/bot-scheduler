using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/Available Level Commands", order = 2)]
public class AvailableLevelCommands : ScriptableObject
{
    public List<AvailableLevelCommand> levelCommands = new List<AvailableLevelCommand>();
}


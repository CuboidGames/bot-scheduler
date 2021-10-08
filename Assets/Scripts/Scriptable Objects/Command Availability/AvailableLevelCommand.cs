using BotScheduler.Gameplay.Commands;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/Available Level Command", order = 1)]
public class AvailableLevelCommand : ScriptableObject
{
    public CommandType commandType;
    public int count;

    public BaseCommand GetCommand()
    {
        return CommandFactory.GetCommand(commandType);
    }
}
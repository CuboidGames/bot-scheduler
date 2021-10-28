using System.Threading.Tasks;

namespace BotScheduler.Systems.Schedule
{
    public interface ISchedulable {
        Task Run();
    }
}
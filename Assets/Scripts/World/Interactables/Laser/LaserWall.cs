using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotScheduler.Utils;
using UnityEngine;

namespace BotScheduler.World
{
    public class LaserWall : MonoBehaviour
    {
        List<ObjectScale> lasers;

        void Awake()
        {
            lasers = new List<ObjectScale>(GetComponentsInChildren<ObjectScale>());
        }

        async public void SetActive(bool active) {
            foreach (var laser in lasers) {
                laser.SetActive(active);
                await Task.Delay(100);
            }
        }
    }
}
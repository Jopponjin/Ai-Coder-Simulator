using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class Timer : MonoBehaviour
    {
        float seconds;
        float timeStamp;

        public Timer(float seconds)
        {
            Set(seconds);
        }

        public void Set(float seconds)
        {
            this.seconds = seconds;
            this.timeStamp = Time.time;
        }

        public bool ExpireReset()
        {
            if (Time.time >= timeStamp + seconds)
            {
                Set(seconds);
                return true;
            }
            return false;
        }
    }
}


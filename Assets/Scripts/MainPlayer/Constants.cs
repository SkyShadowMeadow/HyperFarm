using UnityEngine;

namespace MainPlayer
{
    public static class Constants
    {
        public static readonly int IsRunning = Animator.StringToHash("isRunning");
        public static readonly int IsIdling = Animator.StringToHash("isIdling");
        public static readonly int IsHarvesting = Animator.StringToHash("isHarvesting");

        public static readonly float MinMagnitude = 0.01f;
    }
}
﻿using UnityEngine;

namespace Gameplay.Player.Abilities.Configs
{
    [CreateAssetMenu(menuName = "Configs/AbilityConfig", fileName = "AbilityConfigSO")]
    public class AbilityConfigSO : ScriptableObject
    {
        public float AbilityDuration;
    }
}
using System;
using Gameplay.Player.Models;
using UnityEngine;

namespace Gameplay.Player.Interfaces
{
    public interface ICollide
    {
        event Action<GameObject> OnCollide;
        void Init(PlayerModel playerModel);
    }
}
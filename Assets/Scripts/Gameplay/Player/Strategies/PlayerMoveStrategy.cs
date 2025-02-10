using System;
using Gameplay.Player.Interfaces;
using Gameplay.Player.Models;
using UnityEngine;

namespace Gameplay.Player.Strategies
{
    public sealed class PlayerMoveStrategy : MonoBehaviour, IMove
    {
        private PlayerModel _playerModel;
        private IInput _input;
        
        public void Init(PlayerModel playerModel, IInput input)
        {
            _playerModel = playerModel;
            _input = input;
            _input.GetX().OnChanged += UpdateX;
        }

        private void UpdateX(float newX)
        {
            if(_playerModel.IsDead) return;
            // Unlock the movement of the player
            
            if (!_playerModel.IsMovable) _playerModel.IsMovable = true;
            
            // Convert newX to the scale from level min to level max
            
            // Update player's X position
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        private void Update()
        {
            if(!_playerModel.IsMovable) return;
            
            transform.position += new Vector3(0, _playerModel.MoveYSpeed, 0) * Time.deltaTime;
        }
    }
}
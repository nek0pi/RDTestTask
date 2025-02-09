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
            // TODO Before the first input, the player should be in the middle of the screen and not moving.
            _playerModel = playerModel;
            _input = input;
            _input.GetX().OnChanged += Move;
        }

        private void Move(float xValue)
        {
            transform.position += new Vector3(xValue, _playerModel.MoveYSpeed, 0);
        }
    }
}
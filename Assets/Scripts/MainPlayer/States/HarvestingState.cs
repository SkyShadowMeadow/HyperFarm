using UnityEngine;

namespace MainPlayer.States
{
    public class HarvestingState : IState
    {
        private readonly PlayerInput _playerInput;
        private readonly Animator _animator;
        private readonly PlayerStateHandler _playerStateHandler;
        private readonly Player _player;
        private Vector2 _movementVector;

        public HarvestingState(PlayerInput playerInput, Animator animator, PlayerStateHandler playerStateHandler, Player player)
        {
            _playerInput = playerInput;
            _animator = animator;
            _playerStateHandler = playerStateHandler;
            _player = player;
        }
        public void OnEnter()
        {
            _animator.SetBool(Constants.IsHarvesting, true);
            _player.ShowWeapon();
        }

        public void Loop()
        {
            if (!_player.CanHarvesting())
            {
                OnExit(_playerStateHandler.IdlingState);
            }
        }

        public void OnExit(IState nextState)
        {
            _animator.SetBool(Constants.IsHarvesting ,false);
            _playerStateHandler.ChangeState(nextState);
        }
    }
}

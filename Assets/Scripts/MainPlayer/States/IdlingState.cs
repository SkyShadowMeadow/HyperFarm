using UnityEngine;

namespace MainPlayer.States
{
    public class IdlingState : IState
    {
        private readonly PlayerInput _playerInput;
        private readonly Animator _animator;
        private readonly PlayerStateHandler _playerStateHandler;
        private readonly Player _player;
        public IdlingState(PlayerInput playerInput, Animator animator, PlayerStateHandler playerStateHandler, Player player)
        {
            _playerInput = playerInput;
            _animator = animator;
            _playerStateHandler = playerStateHandler;
            _player = player;
        }
        public void OnEnter()
        {
            _animator.SetBool(Constants.IsIdling, true);
            _player.HideWeapon();
        }

        public void Loop()
        {
            if(IsMovementStarted() && !_player.StackIsUnloading())
                OnExit(_playerStateHandler.RunningState);
        }
        
        public void OnExit(IState nextState)
        {
            _animator.SetBool(Constants.IsIdling, false);
            _playerStateHandler.ChangeState(nextState);
        }

        private bool IsMovementStarted()
            => _playerInput.InputAxes().sqrMagnitude > Constants.MinMagnitude;
    }
}

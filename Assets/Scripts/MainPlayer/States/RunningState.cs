using UnityEngine;

namespace MainPlayer.States
{
    public class RunningState : IState
    {
        private readonly PlayerInput _playerInput;
        private readonly Animator _animator;
        private readonly PlayerStateHandler _playerStateHandler;
        private readonly Player _player;
        private Vector2 _movementVector;

        public RunningState(PlayerInput playerInput, Animator animator, PlayerStateHandler playerStateHandler, Player player)
        {
            _playerInput = playerInput;
            _animator = animator;
            _playerStateHandler = playerStateHandler;
            _player = player;
        }
        
        public void OnEnter()
        {
            _animator.SetBool(Constants.IsRunning, true);
            _player.GetComponent<WheatStack>().Sway();
        }

        public void Loop()
        {
            if (_player.StackIsUnloading())
                OnExit(_playerStateHandler.IdlingState);
            if(IsMoving() && !_player.CanHarvesting())
                _player.Move();
            else if(_player.CanHarvesting() && !_player.StackIsFull() && !_player.StackIsUnloading())
                OnExit(_playerStateHandler.HarvestingState);
            else if(_player.StackIsFull() && IsMoving())
                _player.Move();
            else if (!_player.CanHarvesting() && !IsMoving())
                OnExit(_playerStateHandler.IdlingState);
            else if (_player.CanHarvesting() && !IsMoving() && _player.StackIsFull())
                OnExit(_playerStateHandler.IdlingState);
        }
        

        public void OnExit(IState nextState)
        {
            _player.GetComponent<WheatStack>().StopSwaying();
            _animator.SetBool(Constants.IsRunning, false);
            _playerStateHandler.ChangeState(nextState);
        }
        private bool IsMoving()
            => _playerInput.InputAxes().sqrMagnitude > Constants.MinMagnitude;
    }
}

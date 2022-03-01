using UnityEngine;

namespace MainPlayer.States
{
    public class PlayerStateHandler : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private Animator _animator;
        private IState _currentState;
        private Player _player;

        public IdlingState IdlingState { get; private set; } 
        public RunningState RunningState { get; private set; } 
        public HarvestingState HarvestingState { get; private set; } 

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _animator = GetComponent<Animator>();
            _player = GetComponent<Player>();
            IdlingState = new IdlingState(_playerInput, _animator, this, _player);
            RunningState = new RunningState(_playerInput, _animator, this, _player);
            HarvestingState = new HarvestingState(_playerInput, _animator, this, _player);
        }

        private void Start()
        {
            _currentState = IdlingState;
            _currentState.OnEnter();
        }

        private void Update()
        {
            _currentState.Loop();
        }

        public void ChangeState(IState nextState)
        {
            _currentState = nextState;
            _currentState.OnEnter();
        }
    }
}

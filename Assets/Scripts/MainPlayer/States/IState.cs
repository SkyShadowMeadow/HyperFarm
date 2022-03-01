
namespace MainPlayer.States
{
    public interface IState
    {
        void OnEnter();
        void Loop();
        void OnExit(IState nextState);
    }
}

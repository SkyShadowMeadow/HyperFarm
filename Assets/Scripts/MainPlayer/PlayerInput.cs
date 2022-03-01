using UnityEngine;

namespace MainPlayer
{
    public class PlayerInput : MonoBehaviour
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        public Vector2 InputAxes()
            => new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}

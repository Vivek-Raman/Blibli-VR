using UnityEngine;

namespace Quinbay.Player
{
    public class MovePlayerBasedOnGesture : MonoBehaviour
    {
        [SerializeField] private CharacterController playerController;
        [SerializeField] private float moveSpeed = 1.3f;

        private bool moveForward = false;

        private const float SIMULATION_RATE = 60f;

        public void SetForceMoveForward(bool value)
        {
            this.moveForward = value;
        }

        private void Update()
        {
            if (!moveForward) return;
            playerController.Move(
                moveSpeed * Time.deltaTime * SIMULATION_RATE * playerController.transform.GetChild(0).forward);
        }
    }
}
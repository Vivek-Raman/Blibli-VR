using UnityEngine;

namespace Quinbay.Player
{
    public class GestureSupportedOVRPlayerController : OVRPlayerController
    {
        public void SetForceMoveForward(bool value)
        {
            Debug.Log("Set forceMoveForward to " + value.ToString());
            base.forceMoveForward = value;
        }
    }
}
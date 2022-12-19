using System;
using Oculus.Interaction;
using UnityEngine;

namespace Quinbay.Selection
{
    public class GrabSelector : MonoBehaviour, ISelector
    {
        public event Action WhenSelected;
        public event Action WhenUnselected;
        
        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.Touch.PrimaryThumbstick))
            {
                WhenSelected?.Invoke();
            } 
            else if (OVRInput.GetUp(OVRInput.Touch.PrimaryThumbstick))
            {
                WhenUnselected?.Invoke();
            }
        }

        
    }
}
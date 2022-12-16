using System.Collections.Generic;
using Quinbay.Data;
using UnityEngine;

namespace Quinbay.Space
{
    public class SpaceManager : MonoBehaviour, ISpaceManager
    {
        [SerializeField] private List<BlibliSpace> spaces = new();

        public List<BlibliSpace> Spaces => spaces;

        public void SelectSpace()
        {
            throw new System.NotImplementedException();
        }
    }
}
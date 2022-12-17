using System;
using System.Collections.Generic;
using Quinbay.Data;
using UnityEngine;

namespace Quinbay.Space
{
    public class SpaceManager : MonoBehaviour, ISpaceManager
    {
        [SerializeField] private List<BlibliSpace> spaces = new();

        public List<BlibliSpace> Spaces => spaces;

        private Dictionary<string, BlibliSpace> _spaceMap = new();

        private void Awake()
        {
            _spaceMap = new Dictionary<string, BlibliSpace>();
            spaces.ForEach(space => _spaceMap.Add(space.SceneName, space));
        }

        public void SelectSpace(string spaceName)
        {
            if (!_spaceMap.ContainsKey(spaceName))
            {
                Debug.LogError("Space list does not contain requested space: " + spaceName);
                return;
            } 
            SelectSpace(_spaceMap[spaceName]);
        }

        public void SelectSpace(BlibliSpace space)
        {
            // TODO: load scene
        }
    }
}
using UnityEngine;

namespace Quinbay.Data
{
    [CreateAssetMenu]
    public class BlibliSpace : ScriptableObject
    {
        [SerializeField] private string spaceName;
        [SerializeField] private string sceneName;

        public string SpaceName => spaceName;
        public string SceneName => sceneName;
    }
}
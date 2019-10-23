using UnityEngine;

namespace Game
{
    public class GameSceneController : MonoBehaviour, IGame
    {
        [SerializeField] private TurretSample m_Turret = null;
        
        public IModelController ActiveModel => m_Turret;
    }
}


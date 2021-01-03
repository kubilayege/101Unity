using Core;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {

        private int _score;

        public void Scored()
        {
            _score++;
            Debug.Log(_score);
        }
    }
}

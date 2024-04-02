using AlbertoGarrido.Platformer.Intefaces;
using UnityEngine;

namespace AlbertoGarrido.Platformer
{
    public class Coin : MonoBehaviour, ICollectable
    {
        public void Collect()
        {
            Destroy(gameObject);
        }
    }
}
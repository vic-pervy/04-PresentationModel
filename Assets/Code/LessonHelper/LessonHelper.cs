using UnityEngine;
using Zenject;

namespace Code
{
    public sealed class LessonHelper : MonoBehaviour
    {
        [SerializeField] private long _current;
        private MoneyStorage _moneyStorage;
        private GemStorage _gemStorage;

        [Inject]
        private void Construct(MoneyStorage moneyStorage, GemStorage gemStorage)
        {
            _gemStorage = gemStorage;
            _moneyStorage = moneyStorage;
        }

        public void AddMoney()
        {
            _moneyStorage.Add(_current);
        }

        public void SpendMoney()
        {
            _moneyStorage.Spend(_current);
        }

        public void AddGem()
        {
            _gemStorage.Add(_current);
        }

        public void SpendGem()
        {
            _gemStorage.Spend(_current);
        }
    }
}

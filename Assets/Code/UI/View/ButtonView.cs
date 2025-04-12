using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public sealed class ButtonView : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        public Button Button => _button;
    }
}

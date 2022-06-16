using UnityEngine;
using UnityEngine.UI;

namespace PurchaseView.Scripts
{
    [RequireComponent(typeof(InputField))]
    internal sealed class FieldCheck : MonoBehaviour
    {
        [SerializeField] private InputField thisField;

        public bool CheckIsNullOrEmpty() => 
            string.IsNullOrEmpty(thisField.text);
    }
}

using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace Core
{
    public static class TMP_TextExtension
    {
        public static void ResetComponent(this TMP_Text text)
        {
            text.rectTransform.transform.SetPositionAndRotation(Vector3.zero, quaternion.identity);
            text.rectTransform.position = Vector3.zero;
        }
    }
}
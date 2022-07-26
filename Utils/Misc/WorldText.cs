using UnityEngine;

namespace Utils.Misc
{
    public class WorldText
    {
        public static TextMesh CreateWorldText(string text, Transform parent = null,
            Vector3 location = default, int fontSize = 40, Color? color = null,
            TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left,
            int sortingOrder = 5000)
        {
            if (color == null) color = Color.white;
            
            return CreateWorldText(parent, text, location, textAnchor, textAlignment, fontSize, (Color) color, sortingOrder);
        }

        private static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition,
            TextAnchor textAnchor, TextAlignment alignement, int fontSize, Color color, int sortingOrder)
        {
            GameObject gameObject = new GameObject("World Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = alignement;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }
    }
}
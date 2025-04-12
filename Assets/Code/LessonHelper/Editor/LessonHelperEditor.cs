using UnityEditor;
using UnityEngine;

namespace Code
{
    [CustomEditor(typeof(LessonHelper))]
    public sealed class LessonHelperEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var lessonHelper = (LessonHelper) target;
            
            if (GUILayout.Button("Add Money"))
            {
                lessonHelper.AddMoney();
            }
            
            if (GUILayout.Button("Spend Money"))
            {
                lessonHelper.SpendMoney();
            }
            
            if (GUILayout.Button("Add Gem"))
            {
                lessonHelper.AddGem();
            }
            
            if (GUILayout.Button("Spend Gem"))
            {
                lessonHelper.SpendGem();
            }
        }
    }
}

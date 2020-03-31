using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace StudioNeeco {
    public class NeverAloneEditor : OdinEditorWindow
    {
        [InlineEditor(Expanded = true)]
        public NeverAloneEditorData editorData; 

        void Awake()
        {
            this.editorData = Resources.Load<NeverAloneEditorData>("NeverAloneEditorData");
        }

        //********************************
        // !+ Menu Setup
        //********************************

        [MenuItem("Tools/Never Alone Editor")]
        private static void OpenWindow()
        {
            GetWindow<NeverAloneEditor>().Show();
        }
    }
}

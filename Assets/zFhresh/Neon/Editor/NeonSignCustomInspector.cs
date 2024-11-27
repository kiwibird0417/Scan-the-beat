using UnityEditor;
using UnityEngine;
using zFhresh.Neon;

namespace zFhresh.Neon {
    
    [CustomEditor(typeof(LedPanelScript))]
    public class NeonSignCustomInspector : Editor
    {

        SerializedProperty CustomText;
        SerializedProperty CustomTexture;

        SerializedProperty ColorAnimate;
        SerializedProperty text;
        SerializedProperty color;
        SerializedProperty color_2;
        SerializedProperty MoveSpeed; 
        SerializedProperty fontSize;
        // Textmeshpro font
        SerializedProperty font;

        SerializedProperty material;
        SerializedProperty OneShootCamera;
        SerializedProperty renderTexture;

        SerializedProperty quality;

        SerializedProperty AutoUpdate;

        GUISkin _Skin;

        bool CustomGroup = true;
        bool ReferenceGroup = false;

        bool SettingsGroup = true;
        Texture2D Banner;
        private void OnEnable()
        {
            CustomText = serializedObject.FindProperty("CustomText");
            CustomTexture = serializedObject.FindProperty("CustomTexture");
            text = serializedObject.FindProperty("text");
            color = serializedObject.FindProperty("color");
            MoveSpeed = serializedObject.FindProperty("MoveSpeed");
            fontSize = serializedObject.FindProperty("fontSize");
            font = serializedObject.FindProperty("font");
            material = serializedObject.FindProperty("material");
            OneShootCamera = serializedObject.FindProperty("OneShootCamera");
            renderTexture = serializedObject.FindProperty("renderTexture");
            ColorAnimate = serializedObject.FindProperty("ColorAnimation");
            color_2 = serializedObject.FindProperty("color_2");
            quality = serializedObject.FindProperty("quality");
            AutoUpdate = serializedObject.FindProperty("AutoUpdate");

            _Skin = Resources.Load<GUISkin>("GUI_Skin");
            Banner = (Texture2D)Resources.Load("Banner");
            //Banner = (Texture2D)Resources.Load("Neon/Image/Banner.jpg");
            // Banner = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Neon/Image/Banner.jpg", typeof(Texture2D));
        }

        public override void OnInspectorGUI()
        {
            LedPanelScript _ledPanelScript = (LedPanelScript)target;

            serializedObject.Update();

            using (new GUILayout.VerticalScope(_Skin.customStyles[0])) {
            
                // Check Textmeshpro is installed
                
                EditorGUI.PrefixLabel(new Rect(25, 45, 100, 15), 0, new GUIContent());
                // Change group background color
                // GUI.backgroundColor = new Color32(0, 0, 0, 0);
                // Show texture2D İmage to center
                GUI.DrawTexture(new Rect(125, 12.5f, 100, 100), Banner, ScaleMode.ScaleToFit, true, 0);
                EditorGUILayout.Space(110);
                
                EditorGUILayout.HelpBox("Textmeshpro is required for this script. Please install Textmeshpro from Package Manager", MessageType.Warning);
                // Show texture2D İmage
                


                CustomGroup = EditorGUILayout.BeginFoldoutHeaderGroup(CustomGroup, "Customize");

                if (CustomGroup) {
                    EditorGUILayout.PropertyField(CustomText);
                    if(_ledPanelScript.CustomText) {
                        EditorGUILayout.PropertyField(text);
                        EditorGUILayout.PropertyField(fontSize);
                        EditorGUILayout.PropertyField(font);
                        EditorGUILayout.PropertyField(ColorAnimate);
                        EditorGUILayout.PropertyField(color);
                        if(_ledPanelScript.ColorAnimation) {
                            EditorGUILayout.PropertyField(color_2);
                        }
                        EditorGUILayout.PropertyField(MoveSpeed);
                        
                    }
                    else {
                        EditorGUILayout.PropertyField(CustomTexture);
                        EditorGUILayout.HelpBox("If you want to make your own custom texture, check the documentation", MessageType.Warning);
                        EditorGUILayout.PropertyField(ColorAnimate);
                        EditorGUILayout.PropertyField(color);
                        if(_ledPanelScript.ColorAnimation) {
                            EditorGUILayout.PropertyField(color_2);
                        }
                        EditorGUILayout.PropertyField(MoveSpeed);
                    }
                }
                
                EditorGUILayout.EndFoldoutHeaderGroup();

                SettingsGroup = EditorGUILayout.BeginFoldoutHeaderGroup(SettingsGroup, "Settings");

                if (SettingsGroup) {
                    EditorGUILayout.PropertyField(quality);
                }
                
                EditorGUILayout.EndFoldoutHeaderGroup();



                ReferenceGroup = EditorGUILayout.BeginFoldoutHeaderGroup(ReferenceGroup, "Reference for Debug");

                if (ReferenceGroup) {
                    EditorGUILayout.PropertyField(material);
                    EditorGUILayout.PropertyField(OneShootCamera);
                    EditorGUILayout.PropertyField(renderTexture);
                }
                
                EditorGUILayout.EndFoldoutHeaderGroup();


                EditorGUILayout.Space(10);

                // EditorGUILayout.LabelField("Neon Refrance", EditorStyles.boldLabel);


                // if play mode show button for custom inspector
                if (Application.isPlaying) {
                    if (GUILayout.Button("Update Customize")) {
                        if (_ledPanelScript.CustomText) {
                            _ledPanelScript.SetTextureText();
                        }
                        else {
                            _ledPanelScript.SetCustomTexture();
                        }
                    }
                }

                    Repaint();
                serializedObject.ApplyModifiedProperties();
            }
        }
    }

}
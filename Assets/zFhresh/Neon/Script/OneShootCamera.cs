using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace zFhresh.Neon
{
    public class OneShootCamera : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] TextMeshPro TMP_Text;

        public void OneShootRender(RenderTexture renderTex) {
            SetRenderTexture(renderTex);
            _camera.Render();
        }
        void SetRenderTexture(RenderTexture renderTex) {
            _camera.targetTexture = renderTex;
        }
        public void ChangeText(string text) {
            TMP_Text.text = text;
        }
        public void ChangeText(string text, TMP_FontAsset font) {
            TMP_Text.text = text;
            TMP_Text.font = font;
        }
        public void ChangeText(string text, TMP_FontAsset font, float fontSize) {
            TMP_Text.text = text;
            TMP_Text.font = font;
            TMP_Text.fontSize = fontSize;
        }
    }
}

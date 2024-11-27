using UnityEngine;
using TMPro;

namespace zFhresh.Neon
{
    public class LedPanelScript : MonoBehaviour
    {
        // [Header("Led Panel Settings")]
        public bool CustomText = true;
        public bool ColorAnimation = true;

        public bool AutoUpdate = true;
        [Tooltip("If CustomText is false, this texture will be used \n Check documentation for texture settings")]
        [SerializeField]Texture2D CustomTexture;
        [SerializeField]string text;

        [ColorUsage(true, true)]
        [SerializeField] Color color;
        [ColorUsage(true, true)]
        [SerializeField] Color color_2;

        [Range(-1.0f, 1.0f)]
        [Tooltip("Move speed of the texture")]
        [SerializeField] float MoveSpeed = -0.1f;

        //[Header("Led Panel Text Settings")]
        [Range(0.0f, 24.0f)]
        [SerializeField] float fontSize = 12;
        // Textmeshpro font
        [SerializeField] TMP_FontAsset font;

        public enum Quality {
            Low,
            Medium,
            High,
            veryHigh,
            Max,
            veryMax
        }

        [SerializeField] Quality quality = Quality.High;
        // [Header("Led Panel References")]
        [SerializeField] Material material;
        [SerializeField] GameObject OneShootCamera;
        [SerializeField] RenderTexture renderTexture;

        
        // material properti block
        MaterialPropertyBlock _propBlock;
        void Awake()
        {
            
            SetAllVariables();
        }
        void Start()
        {        
            SetQuality();
            if (CustomText) {
                Invoke("SetTextureText", 0.5f);
            }
            else {
                Invoke("SetCustomTexture", 0.5f);
            }
            
            

            //material.SetTexture("_NeonTexture", TM_Text.mainTexture);
        }
        void OnValidate()
        {
            // if not playmode
            if (!AutoUpdate) return;

            if (CustomText) {
                if(Application.isPlaying) return;

                Texture2D _texture = Resources.Load<Texture2D>("_CustomText");
                if (_texture != null) {
                    SetProperties(_texture, color, MoveSpeed);
                }
                else {
                    Debug.LogWarning("CustomText is true but _CustomText texture is not found in Resources folder");
                }
                
            }
            else {
                // if(Application.isPlaying) return;
                SetCustomTexture();
            }
            SetQuality();
        }
        void SetQuality() {
            switch (quality) {
                case Quality.Low:
                    renderTexture = new RenderTexture(128, 128, 24);
                    break;
                case Quality.Medium:
                    renderTexture = new RenderTexture(256, 256, 24);
                    break;
                case Quality.High:
                    renderTexture = new RenderTexture(512, 512, 24);
                    break;
                case Quality.veryHigh:
                    renderTexture = new RenderTexture(1024, 1024, 24);
                    break;
                case Quality.Max:
                    renderTexture = new RenderTexture(2048, 2048, 24);
                    break;
                case Quality.veryMax:
                    renderTexture = new RenderTexture(4096, 4096, 24);
                    break;
            }
        }
        void SetAllVariables() {
            material = GetComponent<Renderer>().sharedMaterial;
        }
        // trigger from inspector
        [ContextMenu("SetTextureText")]
        public void SetTextureText() {

            Debug.Log("SetTextureText");
            SetQuality();
            // Create one shoot camera ( One shoot camera is a camera that will be destroyed after 1 frame )
            GameObject _OneShootCamera = Instantiate(OneShootCamera, transform.position + Vector3.up * 3000, Quaternion.identity);
            
            OneShootCamera _OneShootCameraScript = _OneShootCamera.GetComponent<OneShootCamera>();

            _OneShootCameraScript.ChangeText(text, font, fontSize);

            _OneShootCameraScript.OneShootRender(renderTexture);


            // rendertexture to texture2d
            Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

            RenderTexture.active = renderTexture;

            
            // read pixels from rendertexture
            tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);

            // apply pixels to texture2d
            tex.Apply();



            RenderTexture.active = null; // JC: added to avoid errors


            // Set texture with material property block
            if (_propBlock == null)
                _propBlock = new MaterialPropertyBlock();
            
            SetProperties(tex, color, MoveSpeed);

            Destroy(_OneShootCamera);
        }
        [ContextMenu("SetCustomTexture")]
        public void SetCustomTexture() {
            if (material== null) {
                SetAllVariables();
            }

            // material.SetTexture("_NeonTexture", CustomTexture);
            // material.SetColor("_NeonColor", color);
            // material.SetFloat("_NeonMoveSpeed", MoveSpeed);
            // Set texture with material property block
            SetProperties(CustomTexture, color, MoveSpeed);
        }
        
        public void SetProperties(Texture2D _texture) {
            if (_propBlock == null)
                _propBlock = new MaterialPropertyBlock();

            _propBlock.SetTexture("_NeonTexture", _texture);

            GetComponent<Renderer>().SetPropertyBlock(_propBlock);
            
        }
        public void SetProperties(Color _color) {
            if (_propBlock == null)
                _propBlock = new MaterialPropertyBlock();

            _propBlock.SetColor("_NeonColor", _color);

            GetComponent<Renderer>().SetPropertyBlock(_propBlock);
            
        }
        public void SetProperties(float _MoveSpeed) {
            if (_propBlock == null)
                _propBlock = new MaterialPropertyBlock();

            _propBlock.SetFloat("_NeonMoveSpeed", _MoveSpeed);

            GetComponent<Renderer>().SetPropertyBlock(_propBlock);
            
        }
        public void SetProperties(Texture2D _texture, Color _color, float _MoveSpeed) {
            if (_propBlock == null)
                _propBlock = new MaterialPropertyBlock();

            _propBlock.SetTexture("_NeonTexture", _texture);
            _propBlock.SetColor("_NeonColor", _color);
            _propBlock.SetFloat("_NeonMoveSpeed", _MoveSpeed);

            GetComponent<Renderer>().SetPropertyBlock(_propBlock);

        }
        
        #region  ChangeText
        public void ChangeText(string _text) {
            if (!CustomText) {
                CustomText = true;
            }
            text = _text;
            SetTextureText();
        }
        public void ChangeText(string _text, Color _color) {
            if (!CustomText) {
                CustomText = true;
            }
            text = _text;
            color = _color;
            SetTextureText();
        }
        
        public void ChangeText(string _text,Color _color,  float _fontSize) {
            if (!CustomText) {
                CustomText = true;
            }
            text = _text;
            fontSize = _fontSize;
            color = _color;
            SetTextureText();

        }
        /// <summary>
        /// Change led panel text 
        /// </summary>
        /// <param name="_text"></param>
        /// <param name="_color"></param>
        /// <param name="_fontSize"></param>
        /// <param name="Font"></param>
        public void ChangeText(string _text, Color _color,  float _fontSize, TMP_FontAsset Font) {
            if (!CustomText) {
                CustomText = true;
            }
            text = _text;
            fontSize = _fontSize;
            color = _color;
            font = Font;
            SetTextureText();
        }
        #endregion
        #region  ChangeTexture
        /// <summary>
        /// Change led panel texture if led is custom text mode it will change to custom texture mode
        /// </summary>
        /// <param name="_texture"></param>
        public void ChangeTexture(Texture2D _texture) {
            if (CustomText) {
                CustomText = false;
            }
            CustomTexture = _texture;
            SetCustomTexture();
        }

        #endregion
        #region  ChangeSpeed
        /// <summary>
        ///  Change led panel move speed 
        /// </summary>
        /// <param name="_speed">Speed set between -1 to 1 </param>
        public void ChangeSpeed(float _speed) {
            MoveSpeed = _speed;
            SetProperties(MoveSpeed);
            
        }
        #endregion
        #region  ChangeColor
        /// <summary>
        /// Change led panel color
        /// </summary>
        /// <param name="_color"> Set Color </param>
        public void ChangeColor(Color _color) {
            color = _color;
            SetProperties(color);
        }
        #endregion

        // void SetOneShootCameraText(String text , GameObject OneShootCamera) {
        //     TextMeshPro TM_Text = OneShootCamera.GetComponentInChildren<TextMeshPro>();
        //     TM_Text.text = text;
        // }
        // IEnumerator SetTexture(GameObject OneShootCamera)
        // {
            
        // }

        #region  Getter
        /// <summary>
        /// Get Led Panel Text
        /// </summary>
        /// <returns>Return to String</returns>
        public string GetText() {
            return text;
        }
        /// <summary>
        /// Get Led Panel Font Size
        /// </summary>
        /// <returns>Return to float</returns>
        public Color GetColor() {
            return color;
        }
        /// <summary>
        /// Get Led Panel Font Size
        /// </summary>
        /// <returns>Return to float</returns>
        public float GetMoveSpeed() {
            return MoveSpeed;
        }
        /// <summary>
        /// Get Led Panel Texture
        /// </summary>
        /// <returns>Return to Texture2D</returns>
        public Texture2D GetTexture() {
            return CustomTexture;
        }
        #endregion
        void Update()
        {
            if (ColorAnimation) {
                //material.SetColor("_NeonColor", Color.Lerp(color, color_2, Mathf.PingPong(Time.time, 2)));
                SetProperties(Color.Lerp(color, color_2, Mathf.PingPong(Time.time, 2)));
            }
        }
    }
}
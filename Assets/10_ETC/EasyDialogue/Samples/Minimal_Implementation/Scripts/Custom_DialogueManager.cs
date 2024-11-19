using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EasyDialogue;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace EasyDialogue.Samples
{
    /// <summary>
    /// The only script in the Minimal_Implementation Scene, this houses all interactions with the user and EasyDialogue System
    /// </summary>
    /// 

    public class Custom_DialogueManager : MonoBehaviour
    {
        #region Attributes
        [SerializeField] private TMP_Text textBox;
        [SerializeField] private TMP_Text characterName;
        [SerializeField] private UnityEngine.UI.Image characterImage;
        [SerializeField] private TMP_Text[] playerChoices;
        [SerializeField] EasyDialogue.EasyDialogueGraph graphToPlay;

        [SerializeField] bool startWithOverrideCharacter;
        [SerializeField, Tooltip("This will only be used if \"startWithOverrideCharacter\" is ticked on")] Character overrideCharacter;

        private item_data[] items = new item_data[3]
        {
            new item_data("coffe", "coffee", ItemType.Consumable),
            new item_data("sword", "sword", ItemType.Equippable),
            new item_data("watter_ballon", "water ballon", ItemType.Throwable)
        };

        private EasyDialogueGraph currentGraph;
        public bool HasDialogueGraph() => currentGraph != null;
        private EasyDialogueManager easyDialogueManager;
        private Canvas myCanvas;

        #endregion
        //================================================================
        //1. Trigger로 대화를 넘겨보도록 하자.
        public InputActionReference triggerL;
        public InputActionReference triggerR;


        //================================================================
        #region Setup And Input
        void Awake()
        {
            if (SceneManager.GetActiveScene().name == "Story") // 특정 씬에서만 유지
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject); // 다른 씬에서는 삭제
            }
        }


        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 이벤트 등록
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // 씬 로드 이벤트 해제
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != "Story") // Story 씬이 아닌 경우
            {
                Destroy(gameObject); // 해당 오브젝트 삭제
            }
        }


        private void Start()
        {
            myCanvas = GetComponent<Canvas>();
            easyDialogueManager = FindObjectOfType<EasyDialogueManager>();
            easyDialogueManager.OnDialogueStarted += (EasyDialogueGraph _graph, dialogue_line _dl) => { Debug.Log($"{_dl.text} was said by {_dl.character}"); };
            easyDialogueManager.OnDialogueProgressed += OnDialogueProgressedHandler;
            easyDialogueManager.OnDialogueEnded += (EasyDialogueGraph _graph) => SceneManager.LoadScene("Main"); ;
            InitializeDialogue();

            triggerL.action.Enable();
            triggerL.action.performed += context => DialogueStart();

            triggerR.action.Enable();
            triggerR.action.performed += context => DialogueStart();
        }

        void OnDestroy()
        {
            triggerL.action.performed -= context => DialogueStart();
            triggerR.action.performed -= context => DialogueStart();
        }



        //================================================================================
        private void OnDialogueProgressedHandler(EasyDialogueGraph _graph, dialogue_line _line)
        {
            Debug.Log($"{_line.text} was said by {_line.character}");
        }

        /// <summary>
        /// Handle user input
        /// </summary>
        /// 

        //트리거 버튼을 누르면 대화가 시작된다.
        public void DialogueStart()
        {
            if (HasDialogueGraph())
            {
                //Progress Dialogue if no player response, or select response 1.
                GetNextDialogue();
            }
            else
            {
                StartDialogueEncounter(ref graphToPlay);
            }
        }

        #endregion

        #region Main Functionality

        /// <summary>
        /// Called to start the dialogue with the given graph.
        /// </summary>
        /// <param name="_dialogueGraph"></param>
        public void StartDialogueEncounter(ref EasyDialogueGraph _dialogueGraph)
        {
            currentGraph = _dialogueGraph;
            currentGraph.localGraphContext = SetupCustomGraphContext();
            if (startWithOverrideCharacter)
            {
                currentGraph.AddOverrideCharacter(ref overrideCharacter);
            }
            dialogue_line dialogue = easyDialogueManager.StartDialogueEncounter(ref _dialogueGraph);
            DisplayDialogue(ref dialogue);
        }

        /// <summary>
        /// Get's the next dialogue, from the 1,2,3 inputs as well as button clicks.
        /// </summary>
        /// <param name="_choiceIndex"></param>
        public void GetNextDialogue(int _choiceIndex = 0)
        {
            if (!HasDialogueGraph()) return;
            dialogue_line dialogue;
            if (easyDialogueManager.GetNextDialogue(ref currentGraph, out dialogue, (ushort)_choiceIndex))
            {
                DisplayDialogue(ref dialogue);
            }
            else
            {
                InitializeDialogue();
            }
        }

        /// <summary>
        /// Abruptly ends or kills the current dialogue session.
        /// </summary>
        public void EndDialogue()
        {
            if (easyDialogueManager.EndDialogueEncounter(ref currentGraph))
            {
                InitializeDialogue();
            }
        }

        #endregion

        //All of the following functions are for the presentation and setting of UI.
        #region Helper Functions

        private CustomGraphContext SetupCustomGraphContext()
        {
            CustomGraphContext cgc = new CustomGraphContext();
            cgc.heldItem = items[Random.Range(0, items.Length)];
            return cgc;
        }


        private void InitializeDialogue()
        {
            characterImage.sprite = null;
            currentGraph = null;
            textBox.text = "Initialized text box, please start a dialogue";
            characterName.text = "The mystical asset creator";
            myCanvas.enabled = false;
            for (int i = 0;
                i < playerChoices.Length;
                ++i)
            {
                playerChoices[i].text = "Tmp player choice";
            }
            HidePlayerResponses();
        }

        private void DisplayDialogue(ref dialogue_line _dialogue)
        {
            ShowCharacterDialogue(_dialogue.character, _dialogue.text);
            if (_dialogue.HasPlayerResponses())
            {
                ShowPlayerResponses(_dialogue.playerResponces);
            }
            else
            {
                HidePlayerResponses();
            }
        }

        private void ShowCharacterDialogue(Character _character, string _text)
        {
            myCanvas.enabled = true;
            textBox.text = _text;
            characterName.text = _character.displayName;
            characterImage.sprite = _character.portrait;
        }

        private void HidePlayerResponses()
        {
            for (int i = 0;
            i < playerChoices.Length;
            ++i)
            {
                playerChoices[i].text = "No option avalible";
                playerChoices[i].transform.parent.parent.gameObject.SetActive(false);
            }
        }

        private void ShowPlayerResponses(string[] _responses)
        {
            for (int i = 0;
                i < _responses.Length;
                ++i)
            {
                playerChoices[i].text = _responses[i];
                playerChoices[i].transform.parent.parent.gameObject.SetActive(true);
            }
        }

        #endregion
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

namespace Doublsb.Dialog
{
    public class DialogManager : MonoBehaviour
    {
        #region Public Fields
        
        [Header("Game Objects")]
        public GameObject Printer;
        public GameObject Characters;

        [Header("UI Objects")]
        public Text Printer_Text;

        [Header("Audio Objects")]
        public AudioSource SEAudio;
        public AudioSource CallAudio;

        [Header("Preference")]
        public float Delay = 0.1f;

        [Header("Selector")]
        public GameObject Selector;
        public GameObject SelectorItem;
        public Text SelectorItemText;

        [HideInInspector]
        public State state;

        [HideInInspector]
        public string Result;
        
        #endregion

        #region Private Fields
        
        private Character _currentCharacter;
        private DialogData _currentData;

        private float _currentDelay;
        private float _lastDelay;
        private Coroutine _textingRoutine;
        private Coroutine _printingRoutine;
        
        #endregion
        
        #region Show & Hide
        public void Show(DialogData Data)
        {
            _currentData = Data;
            FindCharacter(Data.Character);

            if(_currentCharacter != null)
                Emote("Normal");

            _textingRoutine = StartCoroutine(Activate());
        }

        public void Show(List<DialogData> Data)
        {
            StartCoroutine(ActivateList(Data));
        }

        public void Click_Window()
        {
            switch (state)
            {
                case State.Active:
                    StartCoroutine(Skip()); break;

                case State.Wait:
                    if(_currentData.SelectList.Count <= 0) 
                        Hide(); 
                    else if (_currentData.SelectList.Count > 6)
                        SceneManager.LoadScene(2);
                    break;
            }
        }

        public void Hide()
        {
            if(_textingRoutine != null)
                StopCoroutine(_textingRoutine);

            if(_printingRoutine != null)
                StopCoroutine(_printingRoutine);

            Printer.SetActive(false);
            Characters.SetActive(false);
            Selector.SetActive(false);

            state = State.Deactivate;

            if (_currentData.Callback != null)
            {
                _currentData.Callback.Invoke();
                _currentData.Callback = null;
            }
        }
        #endregion

        #region Selector

        public void Select(int index)
        {
            Result = _currentData.SelectList.GetByIndex(index).Key;
            Hide();
        }

        #endregion

        #region Sound

        public void Play_ChatSE()
        {
            if (_currentCharacter != null)
            {
                SEAudio.clip = _currentCharacter.ChatSE[UnityEngine.Random.Range(0, _currentCharacter.ChatSE.Length)];
                SEAudio.Play();
            }
        }

        public void Play_CallSE(string SEname)
        {
            if (_currentCharacter != null)
            {
                var FindSE
                    = Array.Find(_currentCharacter.CallSE, (SE) => SE.name == SEname);

                CallAudio.clip = FindSE;
                CallAudio.Play();
            }
        }

        #endregion

        #region Speed

        public void Set_Speed(string speed)
        {
            switch (speed)
            {
                case "up":
                    _currentDelay -= 0.25f;
                    if (_currentDelay <= 0) _currentDelay = 0.001f;
                    break;

                case "down":
                    _currentDelay += 0.25f;
                    break;

                case "init":
                    _currentDelay = Delay;
                    break;

                default:
                    _currentDelay = float.Parse(speed);
                    break;
            }

            _lastDelay = _currentDelay;
        }

        #endregion

        
        private void FindCharacter(string name)
        {
            if (name != string.Empty)
            {
                Transform Child = Characters.transform.Find(name);
                if (Child != null) _currentCharacter = Child.GetComponent<Character>();
            }
        }

        private void Initialize ()
        {
            _currentDelay = Delay;
            _lastDelay = 0.1f;
            Printer_Text.text = string.Empty;

            Printer.SetActive(true);

            Characters.SetActive(_currentCharacter != null);
            foreach (Transform item in Characters.transform) item.gameObject.SetActive(false);
            if(_currentCharacter != null) _currentCharacter.gameObject.SetActive(true);
        }

        private void InitSelector()
        {
            ClearSelector();

            if (_currentData.SelectList.Count > 0)
            {
                Selector.SetActive(true);

                for (int i = 0; i < _currentData.SelectList.Count; i++)
                {
                    AddSelectorItem(i);
                }
            }
                
            else Selector.SetActive(false);
        }

        private void ClearSelector()
        {
            for (int i = 1; i < Selector.transform.childCount; i++)
            {
                Destroy(Selector.transform.GetChild(i).gameObject);
            }
        }

        private void AddSelectorItem (int index)
        {
            SelectorItemText.text = _currentData.SelectList.GetByIndex(index).Value;

            var NewItem = Instantiate(SelectorItem, Selector.transform);
            NewItem.GetComponent<Button>().onClick.AddListener(() => Select(index));
            NewItem.SetActive(true);
        }

        #region Show Text

        private IEnumerator ActivateList (List<DialogData> DataList)
        {
            state = State.Active;

            foreach (var Data in DataList)
            {
                Show(Data);
                InitSelector();

                while (state != State.Deactivate) { yield return null; }
            }
        }

        private IEnumerator Activate()
        {
            Initialize();

            state = State.Active;

            foreach (var item in _currentData.Commands)
            {
                switch (item.Command)
                {
                    case Command.print:
                        yield return _printingRoutine = StartCoroutine(Print(item.Context));
                        break;

                    case Command.color:
                        _currentData.Format.Color = item.Context;
                        break;

                    case Command.emote:
                        Emote(item.Context);
                        break;

                    case Command.size:
                        _currentData.Format.Resize(item.Context);
                        break;

                    case Command.sound:
                        Play_CallSE(item.Context);
                        break;

                    case Command.speed:
                        Set_Speed(item.Context);
                        break;

                    case Command.click:
                        yield return WaitInput();
                        break;

                    case Command.close:
                        Hide();
                        yield break;

                    case Command.wait:
                        yield return new WaitForSeconds(float.Parse(item.Context));
                        break;
                }
            }

            state = State.Wait;
        }

        private IEnumerator WaitInput()
        {
            while (!Input.GetMouseButtonDown(0)) yield return null;
            _currentDelay = _lastDelay;
        }

        private IEnumerator Print (string Text)
        {
            _currentData.PrintText += _currentData.Format.OpenTagger;

            for (int i = 0; i < Text.Length; i++)
            {
                _currentData.PrintText += Text[i];
                Printer_Text.text = _currentData.PrintText + _currentData.Format.CloseTagger;

                if (Text[i] != ' ') Play_ChatSE();
                if (_currentDelay != 0) yield return new WaitForSeconds(_currentDelay);
            }

            _currentData.PrintText += _currentData.Format.CloseTagger;
        }

        public void Emote (string Text)
        {
            _currentCharacter.GetComponent<Image>().sprite = _currentCharacter.Emotion.Data[Text];
        }

        private IEnumerator Skip ()
        {
            if (_currentData.isSkippable)
            {
                _currentDelay = 0;
                while (state != State.Wait) yield return null;
                _currentDelay = Delay;
            }
        }

        #endregion

    }
}
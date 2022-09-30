using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using Lean.Localization;
using UnityEngine.Events;
using System;

namespace AdditionalFunctions
{
    public class AlertSystem : MonoBehaviour, IDisposable
	{
        [SerializeField] private GameObject _mainWindow;
        [Header("Texts")] [SerializeField] private TMP_Text _textHeader;
        [SerializeField] private TMP_Text _textMessage;
        [Header("Buttons")] [SerializeField] private Button _btnOK;
        [SerializeField] private Button _btnCancel;

        [Header("Button Actions")] private UnityAction _actionOk;
        private UnityAction _actionCancel;

        private static AlertSystem instance;
        public static AlertSystem Instance => instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance);
            }
        }

        private void SetText(string message, string header = null)
        {
            if (string.IsNullOrEmpty(header))
            {
                _textHeader.gameObject.SetActive(false);
            }
            else
            {
                _textHeader.gameObject.SetActive(true);
                _textHeader.text = header;
                _textHeader.autoSizeTextContainer = true;
            }

            _textMessage.text = message;
            _textMessage.autoSizeTextContainer = true;
        }

        private void ShowAlert()
        {
            _mainWindow.SetActive(true);

            StartCoroutine(SetSizePanel());
        }
        
        private void ShowAlert(string nameOk)
        {
            _btnOK.gameObject.SetActive(true);
			//var translation = LeanLocalization.GetTranslationText(nameOk);
			//if (!string.IsNullOrEmpty(translation))
			//	_btnOK.GetComponentInChildren<TMP_Text>().text = translation;
			//else
				_btnOK.GetComponentInChildren<TMP_Text>().text = nameOk;
			_btnCancel.gameObject.SetActive(false);

            ShowAlert();
        }
        
        private void ShowAlert(string nameOk, string nameCancel)
        {
            _btnOK.gameObject.SetActive(true);
			//var translationOk = LeanLocalization.GetTranslationText(nameOk);
			//if (!string.IsNullOrEmpty(translationOk))
			//	_btnOK.GetComponentInChildren<TMP_Text>().text = translationOk;
			//else
				_btnOK.GetComponentInChildren<TMP_Text>().text = nameOk;

			_btnCancel.gameObject.SetActive(true);
			//var translationCancel = LeanLocalization.GetTranslationText(nameCancel);
			//if (!string.IsNullOrEmpty(translationCancel))
			//	_btnCancel.GetComponentInChildren<TMP_Text>().text = translationCancel;
			//else
				_btnCancel.GetComponentInChildren<TMP_Text>().text = nameCancel;

			ShowAlert();
        }

        private IEnumerator SetSizePanel()
        {
            //в одном кадре не успевает получить новый размер
            yield return null;
            var panel = _mainWindow.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
            var newHeight = panel.rect.height;

            var shadowPanel = _mainWindow.transform.GetChild(0).GetComponent<RectTransform>();
            var newSize = shadowPanel.sizeDelta;

            newSize.y = newHeight;
            shadowPanel.sizeDelta = newSize;
        }

        private void HideAlert()
        {
			_mainWindow.SetActive(false);
            _actionOk = null;
            _actionCancel = null;
        }

        public void ShowMessage(string message, string header = null)
        {
            SetText(message, header);

            ShowAlert();
        }

        public void ShowMessageOk(string message, string nameOk, UnityAction actionOk, string header = null)
        {
            SetText(message, header);

            _actionOk = actionOk;

            ShowAlert(nameOk);
        }

		public void ShowMessageOk(string message, string nameOk, string header = null)
		{
			SetText(message, header);

			ShowAlert(nameOk);
		}

		public void ShowMessageOkCancel(string message, string nameOk, string nameCancel, UnityAction actionOk, UnityAction actionCancel, string header = null)
        {
            SetText(message, header);

            _actionOk = actionOk;
            _actionCancel = actionCancel;
            
            ShowAlert(nameOk, nameCancel);
        }

        public void OnClickButtonOk()
        {
            _actionOk?.Invoke();
            HideAlert();
        }

        public void OnClickButtonCancel()
        {
            _actionCancel?.Invoke();
            HideAlert();
        }

		public void Dispose()
		{
			instance = null;
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu1 : MonoBehaviour
{
	private UIDocument _document;
	private Button _button;

	private void Awake()
	{
		_document = GetComponent<UIDocument>();
		_button = _document.rootVisualElement.Q("StartGameButton") as Button;
		_button.RegisterCallback<ClickEvent>(OnPlayGameClick);
	}

	private void OnPlayGameClick(ClickEvent evt)
	{
		Debug.Log("Play button pressed");
	}
}

/**************************************************************************/
/** 	© 2016 NULLcode Studio. License: CC 0.
/** 	Разработано специально для http://null-code.ru/
/** 	WebMoney: R209469863836. Z126797238132, E274925448496, U157628274347
/** 	Яндекс.Деньги: 410011769316504
/**************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;

public class MysqlConnect : MonoBehaviour {

	[SerializeField] private InputField userName;
	[SerializeField] private InputField userPass;
	[SerializeField] private InputField userEmail;
	[SerializeField] private Text messageText;
    [SerializeField] private GameObject messageObject;

    [SerializeField] private Button register;
	[SerializeField] private Button login;

	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject loginMenu;

    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject panelBlured;

    [SerializeField] private string loginURL = "http://ec2-3-139-95-157.us-east-2.compute.amazonaws.com/login.php";
	[SerializeField] private string registerURL = "http://ec2-3-139-95-157.us-east-2.compute.amazonaws.com/register.php";

	void Start()
	{
        if (DataHolder.logged)
        {
            loginMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
	}

	void Awake()
	{
		userPass.contentType = InputField.ContentType.Password;
		register.onClick.AddListener(() => {Register();});
		login.onClick.AddListener(() => {Login();});
	}

	bool IsValidEmail(string email) // валидация email
	{
		return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
	}

	bool IsValid(string value, int min, int max, string field) // валидация имени и пароля
	{
		if(value.Length < min)
		{
			Message("В поле [ " + field + " ] недостаточно символов.");
			return false;
		}
		else if(value.Length > max)
		{
			Message("В поле [ " + field + " ] превышен максимум символов.");
			return false;
		}
		else if(Regex.IsMatch(value, @"[^\w\.@-]"))
		{
			Message("В поле [ " + field + " ] содержаться недопустимые символы.");
			return false;
		}

		return true;
	}

	void Message(string text)
	{
        messageObject.SetActive(true);
		messageText.text = text;
        if (text == "")
            messageText.text = "Ошибка ввода";
		Debug.Log(this + " --> " + text);
	}

	void Login()
	{
		if(!IsValid(userName.text, 3, 15, "Имя") || !IsValid(userPass.text, 6, 20, "Пароль")) return;

		WWWForm form = new WWWForm();
		form.AddField("name", userName.text);
		form.AddField("password", userPass.text);
		WWW www = new WWW(loginURL, form);
		StartCoroutine(LoginFunc(www));
	}

	void Register()
	{
		if(!IsValidEmail(userEmail.text))
		{
			Message("Email адрес указан не верно!");
			return;
		}

		if(!IsValid(userName.text, 3, 15, "Имя") || !IsValid(userPass.text, 6, 20, "Пароль")) return;

		WWWForm form = new WWWForm();
		form.AddField("name", userName.text);
		form.AddField("password", userPass.text);
		form.AddField("email", userEmail.text);
		WWW www = new WWW(registerURL, form);
		StartCoroutine(RegisterFunc(www));
	}

	IEnumerator LoginFunc(WWW www)
	{
		yield return www;
        
		if(www.error == null)
		{
			if(string.Compare(www.text, "Success!") == 0) // получаем в ответе слово-ключ из файла login.php
			{
				Message("Успешный вход!");
				loginMenu.SetActive(false);
				mainMenu.SetActive(true);
                panel.SetActive(true);
                panelBlured.SetActive(false);
                DataHolder.name = userName.text;
                DataHolder.logged = true;
            }
			else
			{
				Message(www.text);
			}
		}
		else
		{
			Message("Error: " + www.error);
		}
	}

	IEnumerator RegisterFunc(WWW www)
	{
		yield return www;

		if(www.error == null)
		{
			if (string.Compare(www.text, "User allready exists!") == 0)
			{
				Message("Такой пользователь уже есть.");
			}
			else
			{
				Message("Пользователь успешно добавлен в базу.");
				loginMenu.SetActive(false);
				mainMenu.SetActive(true);
                panel.SetActive(true);
                panelBlured.SetActive(false);
                DataHolder.name = userName.text;
                DataHolder.logged = true;
            }
		}
		else
		{
			Message("Error: " + www.error);
		}
	}

	public void AddTriggers(Button button, Text text)
	{
		var eventPEnter = new EventTrigger.Entry();
		var eventPExit = new EventTrigger.Entry();
		var eventPClick = new EventTrigger.Entry();
		var trig = button.gameObject.GetComponent<EventTrigger>();

		eventPEnter.eventID = EventTriggerType.PointerEnter;
		eventPEnter.callback.AddListener((data) => {
			text.color = new Color(0, 0, 0, 255);
			button.image.color = new Color(255, 2555, 255, 255);
		});
		trig.triggers.Add(eventPEnter);

		eventPExit.eventID = EventTriggerType.PointerExit;
		eventPExit.callback.AddListener((x) => {
			text.color = new Color(255, 255, 255, 255);
			button.image.color = new Color(255, 255, 255, 0);
		});
		trig.triggers.Add(eventPExit);

		eventPClick.eventID = EventTriggerType.PointerClick;
		eventPClick.callback.AddListener((x) => {
			text.color = new Color(255, 255, 255, 255);
			button.image.color = new Color(255, 255, 255, 0);
		});
		trig.triggers.Add(eventPClick);
	}
    public void Quit()
    {
        Application.Quit();
    }
}

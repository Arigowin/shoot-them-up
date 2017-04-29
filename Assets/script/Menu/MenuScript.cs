using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

	[SerializeField] private Button MyStartButton = null;
	[SerializeField] private Button MyQuitButton = null;

	void Start ()
	{
		MyStartButton.onClick.AddListener (() => {
			ClickStart ();
		});
		MyQuitButton.onClick.AddListener (() => {
			ClickQuit ();
		});
	}

	void ClickStart()
	{
		SceneManager.LoadScene("shoot_them_up", LoadSceneMode.Single);
	}

	void ClickQuit()
	{
		Application.Quit ();
	}

}

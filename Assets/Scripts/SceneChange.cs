using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	public void PlayGame() 
	{
		SceneManager.LoadScene("GyyuGameScene");
	}
}

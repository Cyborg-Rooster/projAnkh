using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    [SerializeField] FadeController fade;
    // Start is called before the first frame update
    void Start()
    {
        if (!Database.CheckifXMLExist()) Database.CreateDatabase();
        fade.StartCoroutine(fade.StartFadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectOption(int index)
    {
        StartCoroutine(Select(index));
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("sceTest");
    }

    public void ShowPontuations()
    {
        SceneManager.LoadScene("scePontuation");
    }

    public void Close()
    {
        Application.Quit();
    }

    IEnumerator Select(int index)
    {
        yield return fade.StartFadeOut();

        if (index == 0) StartNewGame();
        else if (index == 1) ShowPontuations();
        else if (index == 2) Close();
    }
}

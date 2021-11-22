using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PontuationController : MonoBehaviour
{
    [SerializeField] GameObject[] pontuationIndex;
    [SerializeField] FadeController fade;

    float[] points = new float[8];

    int index = -1;
    int value = 0;
    Text userName;

    // Start is called before the first frame update
    void Start()
    {
        value = PhaseController.points;
        UpdateValues();
        StartCoroutine("PontuationViewer");
    }

    IEnumerator PontuationViewer()
    {
        yield return fade.StartFadeIn();
        yield return WaitForReturnKey();
        yield return new WaitForSeconds(1);
        yield return fade.StartFadeOut();
        SceneManager.LoadScene("sceMain");
    }

    public void UpdateValues()
    {
        PlayerData data;
        for (int i = 0; i < pontuationIndex.Length; i++)
        {
            data = DatabaseController.Read(i.ToString());
            points[i] = data.Value;
            pontuationIndex[i].transform.GetChild(0).GetComponent<Text>().text = data.Name;
            pontuationIndex[i].transform.GetChild(1).GetComponent<Text>().text = data.Value.ToString();
        }
        if (PhaseController.points != 0) StartConfiguration();
    }

    void StartConfiguration()
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (PhaseController.points > points[i])
            {
                index = i;
                ReajustValues(i);

                userName = pontuationIndex[i].transform.GetChild(0).GetComponent<Text>();
                userName.text = "";
                pontuationIndex[i].transform.GetChild(1).GetComponent<Text>().text = value.ToString();
                pontuationIndex[i].GetComponent<BlinkController>().StartBlink();
                break;
            }
        }
    }

    public void SaveInput()
    {
        pontuationIndex[index].GetComponent<BlinkController>().StopBlink();
        PlayerData data = new PlayerData()
        {
            ID = index,
            Name = userName.text,
            Value = value
        };
        if (DatabaseController.CheckIfExist(index.ToString())) DatabaseController.Update(data);
        else DatabaseController.Create(data);
    }

    IEnumerator WaitForReturnKey()
    {
        bool waiting = true;
        while(waiting)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.Backspace) && index != -1)
                {
                    if (userName.text.Length > 0) userName.text = userName.text.Remove(userName.text.Length - 1, 1);
                }

                else if (Input.GetKeyDown(KeyCode.Space) && index != -1) userName.text += " ";

                else if (Input.GetKeyDown(KeyCode.Return)) 
                { 
                    if (index != -1) SaveInput();
                    waiting = false;
                }

                else
                {
                    foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                    {
                        if (Input.GetKey(vKey))
                            if(Array.Exists(Data.Characters, k => k == vKey) && userName.text.Length < 9 && index != -1) userName.text += vKey.ToString();
                    }
                }
            }

            yield return null;
        }
    }

    public void ReajustValues(int _index)
    {
        PlayerData data;
        for (int i = 7; i > _index; i--)
        {
            data = new PlayerData()
            {
                ID = i,
                Name = pontuationIndex[i - 1].transform.GetChild(0).GetComponent<Text>().text,
                Value = int.Parse(pontuationIndex[i - 1].transform.GetChild(1).GetComponent<Text>().text)
            };

            DatabaseController.Update(data);
        }

        PhaseController.RestoreStaticAttributes();
        UpdateValues();
    }

    
}

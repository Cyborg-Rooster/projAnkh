using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

class PhaseController : MonoBehaviour
{
    [SerializeField] Text txtPoints;
    [SerializeField] Text txtLifes;
    [SerializeField] Text txtEnemies;
    [SerializeField] Text txtMiddle;
    [SerializeField] FadeController fade;
    [SerializeField] PlayerController player;

    UIManager ui;

    public static int phaseCount = 1;
    public static int points = 0;
    public static int enemyCount = 6;
    public static int lifes = 3;
    public bool Started = false;
    bool finish = false;

    private void Start()
    {
        ui = new UIManager()
        {
            TxtEnemies = txtEnemies,
            TxtPoints = txtPoints,
            TxtLifes = txtLifes
        };

        ui.UpdateValues(points, enemyCount, lifes);
        StartCoroutine("StartPhase");
    }

    private void Update()
    {
        if (!finish && enemyCount == 0) Finish(false);
    }

    public void AddPoints(int value)
    {
        points = ui.AddValue(points, value);
        ui.UpdateValues(points, enemyCount, lifes);
    }

    public void SubEnemy(int value)
    {
        enemyCount = ui.SubValue(enemyCount, value);
        ui.UpdateValues(points, enemyCount, lifes);
    }

    public void SubLife(int value)
    {
        lifes = ui.SubValue(lifes, value);
        ui.UpdateValues(points, enemyCount, lifes);
    }

    public void Finish(bool dead)
    {
        finish = true;
        if (dead) StartCoroutine("OnLose");
        else StartCoroutine("OnWin");
    }

    public static void RestoreStaticAttributes()
    {
        phaseCount = 1;
        points = 0;
        enemyCount = 6;
        lifes = 3;
        Data.EnemySpeed = 0.8f;
    }

    IEnumerator OnWin()
    {
        Started = false;
        player.StopAndTurnForward();

        yield return new WaitForSeconds(1);

        txtMiddle.text = "Você Ganhou!";
        txtMiddle.gameObject.SetActive(true);
        Data.EnemySpeed += 0.2f;
        phaseCount++;
        GetComponent<ChangeColorController>().ChangeColor();

        yield return FinishPhase();
    }

    IEnumerator OnLose()
    {
        SubLife(1);

        yield return new WaitForSeconds(1);

        txtMiddle.text = "Você Morreu!";
        txtMiddle.gameObject.SetActive(true);

        yield return FinishPhase();
    }

    IEnumerator StartPhase()
    {
        yield return fade.StartFadeIn();
        for (int i = 3; i > 0; i--)
        {
            ui.ChangeTextBoxValue(txtMiddle, i.ToString());
            yield return new WaitForSeconds(1);
        }

        Started = true;
        ui.ChangeTextBoxValue(txtMiddle, "Começar!");

        yield return new WaitForSeconds(2);
        txtMiddle.gameObject.SetActive(false);
    }

    IEnumerator FinishPhase()
    {
        yield return new WaitForSeconds(2);

        yield return fade.StartFadeOut();

        yield return new WaitForSeconds(1);

        enemyCount = Data.EnemyCount;
        AnkhManager.AnkhCatched = false;

        if(lifes > 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else SceneManager.LoadScene("scePontuation");
    }

}

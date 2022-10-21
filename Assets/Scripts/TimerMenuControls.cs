using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerMenuControls : MonoBehaviour
{
    public GameObject panelTimerAccessButtons;

    public GameObject prefabTimerAccessButton;
    public Transform pointTimerButtonsSpawnStart;
    public float buttonApperaringDelay = 0.2f;
    GameObject[] buttonsTimerAccess;
    int indexCurrentStartAnimationButton;

    public GameObject prefabTimerPanel;
    public Transform pointTimerPanelSpawn;
    GameObject panelCurrentTimer;

    void Start()
    {
        GameData.instance.SpawnTimers(GameData.instance.timersCount);
        buttonsTimerAccess = new GameObject[GameData.instance.timersCount];
        Vector3 pos;
        for (int i=0; i < GameData.instance.timersCount; i++)
        {
            pos = pointTimerButtonsSpawnStart.position + new Vector3(0, -15* i);
            buttonsTimerAccess[i] = Instantiate(prefabTimerAccessButton, pos, Quaternion.identity, pointTimerButtonsSpawnStart);
            buttonsTimerAccess[i].GetComponentInChildren<TimerAccessButton>().SetButtonValue(i);
            int local_i = i;
            buttonsTimerAccess[i].GetComponentInChildren<Button>().onClick.AddListener(() => OnTimerAccessButtonPress(local_i));
        }
        ShowAllTimerButtons();
    }

    IEnumerator StartTimerAccessButtonAppearingAnimation(Animator animator)
    {
        panelTimerAccessButtons.SetActive(true);
        animator.SetTrigger("ListOfButtonsAppear");
        yield return new WaitForSeconds(buttonApperaringDelay);
        indexCurrentStartAnimationButton++;
        if (indexCurrentStartAnimationButton < buttonsTimerAccess.Length)
            StartCoroutine(StartTimerAccessButtonAppearingAnimation(buttonsTimerAccess[indexCurrentStartAnimationButton].GetComponent<Animator>()));           
    }

    IEnumerator StartTimerAccessButtonDissappearingAnimation(Animator animator)
    {
        animator.SetTrigger("ListOfButtonsDissappear");
        yield return new WaitForSeconds(buttonApperaringDelay);
        indexCurrentStartAnimationButton--;
        if (indexCurrentStartAnimationButton >= 0)
            StartCoroutine(StartTimerAccessButtonDissappearingAnimation(buttonsTimerAccess[indexCurrentStartAnimationButton].GetComponent<Animator>()));
        else
            panelTimerAccessButtons.SetActive(false);
    }

    void HideAllTimerButtons()
    {
        indexCurrentStartAnimationButton = buttonsTimerAccess.Length - 1;
        StartCoroutine(StartTimerAccessButtonDissappearingAnimation(buttonsTimerAccess[indexCurrentStartAnimationButton].GetComponent<Animator>()));
    }

    public void ShowAllTimerButtons()
    {
        indexCurrentStartAnimationButton = 0;
        StartCoroutine(StartTimerAccessButtonAppearingAnimation(buttonsTimerAccess[indexCurrentStartAnimationButton].GetComponent<Animator>()));
    }

    void OnTimerAccessButtonPress(int button_value)
    {
        GameData.instance.SetCurrentTimer(button_value);
        HideAllTimerButtons();
        panelCurrentTimer = Instantiate(prefabTimerPanel, pointTimerPanelSpawn.position, Quaternion.identity, pointTimerPanelSpawn);
        panelCurrentTimer.GetComponent<TimerPanelControls>().SetTimerSelectionPanelLink(this);
    }

    void Update()
    {
        
    }
}

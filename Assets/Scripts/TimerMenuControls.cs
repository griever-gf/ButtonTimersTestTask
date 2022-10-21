using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerMenuControls : MonoBehaviour
{
    public GameObject panelTimerAccessButtons;

    public GameObject prefabTimerAccessButton;
    public Transform pointTimerButtonsSpawnStart;
    public int timerButtonsCount = 3; //need to move to Game Data
    public float buttonApperaringDelay = 0.2f;
    GameObject[] buttonsTimerAccess;
    int indexCurrentStartAnimationButton;

    public GameObject prefabTimerPanel;
    public Transform pointTimerPanelSpawn;
    GameObject panelCurrentTimer;

    void Start()
    {
        GameData.instance.SpawnTimers(timerButtonsCount);
        buttonsTimerAccess = new GameObject[timerButtonsCount];
        Vector3 pos;
        for (int i=0; i < timerButtonsCount; i++)
        {
            pos = pointTimerButtonsSpawnStart.position + new Vector3(0, -15* i);
            buttonsTimerAccess[i] = Instantiate(prefabTimerAccessButton, pos, Quaternion.identity, pointTimerButtonsSpawnStart);
            buttonsTimerAccess[i].GetComponentInChildren<TimerAccessButton>().SetTimerNumberText(i+1);
            buttonsTimerAccess[i].GetComponentInChildren<Button>().onClick.AddListener(OnTimerAccessButtonPress);
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

    void OnTimerAccessButtonPress()
    {
        HideAllTimerButtons();
        panelCurrentTimer = Instantiate(prefabTimerPanel, pointTimerPanelSpawn.position, Quaternion.identity, pointTimerPanelSpawn);
        panelCurrentTimer.GetComponent<TimerPanelControls>().SetTimerSelectionPanelLink(this);
    }

    void Update()
    {
        
    }
}

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
    //GameObject[] buttonsTimerAccess;
    List <GameObject> buttonsTimerAccess;
    int indexCurrentStartAnimationButton;

    public GameObject prefabTimerPanel;
    public Transform pointTimerPanelSpawn;
    GameObject panelCurrentTimer;

    public GameObject panelAddRemoveTimers;

    void Start()
    {
        buttonsTimerAccess = new List<GameObject>();
        for (int i=0; i < GameData.instance.timersCount; i++)
            SpawnTimer(i);
        ShowAllTimerButtons();
    }
    
    void SpawnTimer(int idx)
    {
        GameData.instance.AddTimer(idx);
        Vector3 pos = pointTimerButtonsSpawnStart.position + new Vector3(0, -15 * idx);
        buttonsTimerAccess.Add(Instantiate(prefabTimerAccessButton, pos, Quaternion.identity, pointTimerButtonsSpawnStart));
        buttonsTimerAccess[idx].GetComponentInChildren<TimerAccessButton>().UpdateButtonName(idx);
        buttonsTimerAccess[idx].GetComponentInChildren<Button>().onClick.AddListener(() => OnTimerAccessButtonPress(idx));
    }

    void DestroyTimer(int idx)
    {
        GameData.instance.RemoveTimer(idx);
        Destroy(buttonsTimerAccess[idx]);
        buttonsTimerAccess.RemoveAt(idx);
    }

    IEnumerator StartTimerAccessButtonAppearingAnimation(Animator animator)
    {
        panelTimerAccessButtons.SetActive(true);
        animator.SetTrigger("ListOfButtonsAppear");
        yield return new WaitForSeconds(buttonApperaringDelay);
        indexCurrentStartAnimationButton++;
        if (indexCurrentStartAnimationButton < buttonsTimerAccess.Count)
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
        indexCurrentStartAnimationButton = buttonsTimerAccess.Count - 1;
        StartCoroutine(StartTimerAccessButtonDissappearingAnimation(buttonsTimerAccess[indexCurrentStartAnimationButton].GetComponent<Animator>()));
        panelAddRemoveTimers.SetActive(false);
    }

    public void ShowAllTimerButtons()
    {
        indexCurrentStartAnimationButton = 0;
        StartCoroutine(StartTimerAccessButtonAppearingAnimation(buttonsTimerAccess[indexCurrentStartAnimationButton].GetComponent<Animator>()));
        panelAddRemoveTimers.SetActive(true);
    }

    void OnTimerAccessButtonPress(int button_value)
    {
        GameData.instance.SetCurrentTimer(button_value);
        HideAllTimerButtons();
        panelCurrentTimer = Instantiate(prefabTimerPanel, pointTimerPanelSpawn.position, Quaternion.identity, pointTimerPanelSpawn);
        panelCurrentTimer.GetComponent<TimerPanelControls>().SetTimerSelectionPanelLink(this);
    }

    public void SetFinishedColorToTimerButton()
    {
        int btn_idx = GameData.instance.GetFinishedTimerIndex();
        buttonsTimerAccess[btn_idx].GetComponentInChildren<TimerAccessButton>().SetButtonViewTimerComplete();
    }

    public void SetDefaultColorToCurrentTimerButton()
    {
        buttonsTimerAccess[GameData.instance.GetCurrentTimerIndex()].GetComponentInChildren<TimerAccessButton>().SetButtonViewTimerDefault();
    }

    public void AddNewTimerButton()
    {
        GameData.instance.timersCount++;
        int current_idx = GameData.instance.timersCount - 1;
        SpawnTimer(current_idx);
        StartCoroutine(StartTimerAccessButtonAppearingAnimation(buttonsTimerAccess[current_idx].GetComponent<Animator>()));
    }

    public void RemoveLastTimerButton()
    {
        int current_idx = GameData.instance.timersCount - 1;
        DestroyTimer(current_idx);
        GameData.instance.timersCount--;
    }

    void Update()
    {
        
    }
}

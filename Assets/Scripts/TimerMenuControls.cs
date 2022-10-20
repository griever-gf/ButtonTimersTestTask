using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerMenuControls : MonoBehaviour
{
    public GameObject prefabTimerAccessButton;
    public Transform pointTimerButtonsSpawnStart;
    public int timerButtonsCount = 3; //need to move to Game Data
    public float buttonApperaringDelay = 0.2f;
    GameObject[] buttonsTimerAccess;
    int indexCurrentStartAnimationButton;

    void Start()
    {
        buttonsTimerAccess = new GameObject[timerButtonsCount];
        Vector3 pos;
        for (int i=0; i < timerButtonsCount; i++)
        {
            pos = pointTimerButtonsSpawnStart.transform.position + new Vector3(0, -15* i);
            buttonsTimerAccess[i] = Instantiate(prefabTimerAccessButton, pos, Quaternion.identity, pointTimerButtonsSpawnStart);
            buttonsTimerAccess[i].GetComponentInChildren<TimerAccessButton>().SetTimerNumberText(i+1);
            buttonsTimerAccess[i].GetComponentInChildren<Button>().onClick.AddListener(HideAllTimerButtons);
        }
        indexCurrentStartAnimationButton = 0;
        StartCoroutine(StartTimerAccessButtonAppearingAnimation(buttonsTimerAccess[indexCurrentStartAnimationButton].GetComponent<Animator>()));
    }

    IEnumerator StartTimerAccessButtonAppearingAnimation(Animator animator)
    {
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
    }

    void HideAllTimerButtons()
    {
        indexCurrentStartAnimationButton = buttonsTimerAccess.Length - 1;
        StartCoroutine(StartTimerAccessButtonDissappearingAnimation(buttonsTimerAccess[indexCurrentStartAnimationButton].GetComponent<Animator>()));
    }

    void Update()
    {
        
    }
}

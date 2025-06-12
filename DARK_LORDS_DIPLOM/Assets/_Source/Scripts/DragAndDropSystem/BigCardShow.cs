using System.Collections;
using UnityEngine;

public class BigCardShow : MonoBehaviour
{
    [SerializeField] public GameObject BigCardFirst;
    [SerializeField] public GameObject BigCardSecond;

    public bool isMouseOver = false;
    public bool CanTriggerFirst = true;
    public bool CanTriggerSecond = false;
    private Coroutine logCoroutine;

    private void Start()
    {
        BigCardFirst.SetActive(false);
        BigCardSecond.SetActive(false);
    }

    private void Update()
    {
        // Не нужно ничего делать в Update
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;

        // Запускаем корутину для вывода в лог через 5 секунд только один раз
        if (logCoroutine == null && CanTriggerFirst)
        {
            logCoroutine = StartCoroutine(LogAfterDelayFirst(1f));
        }

        if (logCoroutine == null && CanTriggerSecond)
        {
            logCoroutine = StartCoroutine(LogAfterDelaySecond(1f));
        }
    }

    private void OnMouseExit()
    {
        isMouseOver = false;

        // Останавливаем корутину, если она запущена
        if (logCoroutine != null)
        {
            StopCoroutine(logCoroutine);
            logCoroutine = null;
        }

        BigCardFirst.SetActive(false);
        BigCardSecond.SetActive(false);
    }

    private IEnumerator LogAfterDelayFirst(float delay)
    {
        yield return new WaitForSeconds(delay);
        BigCardFirst.SetActive(true);

        // Сбрасываем корутину после завершения
        logCoroutine = null;
    }

    private IEnumerator LogAfterDelaySecond(float delay)
    {
        yield return new WaitForSeconds(delay);
        BigCardSecond.SetActive(true);

        // Сбрасываем корутину после завершения
        logCoroutine = null;
    }
}

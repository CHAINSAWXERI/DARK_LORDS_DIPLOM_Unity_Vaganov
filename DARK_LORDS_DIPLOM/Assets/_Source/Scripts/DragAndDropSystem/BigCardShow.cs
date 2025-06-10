using System.Collections;
using UnityEngine;

public class BigCardShow : MonoBehaviour
{
    [SerializeField] public GameObject BigCard;

    public bool isMouseOver = false;
    public bool CanTrigger = true;
    private Coroutine logCoroutine;

    private void Start()
    {
        BigCard.SetActive(false);
    }

    private void Update()
    {
        // Не нужно ничего делать в Update
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;

        // Запускаем корутину для вывода в лог через 5 секунд только один раз
        if (logCoroutine == null && CanTrigger)
        {
            logCoroutine = StartCoroutine(LogAfterDelay(1f));
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

        BigCard.SetActive(false);
    }

    private IEnumerator LogAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        BigCard.SetActive(true);

        // Сбрасываем корутину после завершения
        logCoroutine = null;
    }
}

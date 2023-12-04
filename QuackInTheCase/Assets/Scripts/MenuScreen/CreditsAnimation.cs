using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreditsAnimation : MonoBehaviour
{

    private float speed;
    private bool rollCredits = false;
    private Vector3 initialPosition = new Vector3(0f, -1000f, 0f);

    [SerializeField] private AudioSource BGMusic;

    void Update()
    {
        if (rollCredits)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetMouseButton(0))
        {
            speed = 300;
        }
        else
        {
            speed = 40;
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.0f);
        rollCredits = true;
    }

    public void SetCredits()
    {
        BGMusic.Stop();
        GetComponent<RectTransform>().anchoredPosition = initialPosition;
        StartCoroutine(Delay());
    }

    public void StopCredits()
    {
        rollCredits = false;
        BGMusic.Play();
    }
}

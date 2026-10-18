
using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float duracion = 0.15f;
    public float intensidad = 0.15f;

    private Vector3 posicionInicial;
    private Coroutine temblorActual;

    void LateUpdate()
    {
        if (temblorActual == null)
        {
            posicionInicial = transform.localPosition;
        }
    }

    public void Temblar()
    {
        if (temblorActual != null)
        {
            StopCoroutine(temblorActual);
            transform.localPosition = posicionInicial;
        }

        temblorActual = StartCoroutine(TemblarCoroutine());
    }

    IEnumerator TemblarCoroutine()
    {
        posicionInicial = transform.localPosition;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            float x = Random.Range(-1f, 1f) * intensidad;
            float y = Random.Range(-1f, 1f) * intensidad;

            transform.localPosition = posicionInicial + new Vector3(x, y, 0f);

            tiempo += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = posicionInicial;
        temblorActual = null;
    }
}


using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Referencia UI")]
    public GameObject panelPausa;

    private bool juegoPausado = false;

    void Start()
    {
        Time.timeScale = 1f;
        juegoPausado = false;

        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) ||
            Input.GetKeyDown(KeyCode.P))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        if (panelPausa != null)
        {
            panelPausa.SetActive(true);
        }

        Time.timeScale = 0f;
        juegoPausado = true;
    }

    public void Reanudar()
    {
        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }

        Time.timeScale = 1f;
        juegoPausado = false;
    }
}

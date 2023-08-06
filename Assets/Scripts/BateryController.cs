using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BateryController : MonoBehaviour
{

    [SerializeField] private AudioSource collectCoin_SFX; // Campo donde se enlaza el componente de audio desde Unity

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Moneda");
        collectCoin_SFX.Play();  // Sonido
        StartCoroutine(goNextLevel(collectCoin_SFX.clip.length)); // Pasa el nivel con un "delay" para que alcance a sonar el audio
        gameObject.GetComponent<Renderer>().enabled = false;      // La moneda desaparece
    }

    // Aquí está la lógica del paso de nivel después de la pausa suficiente para que suene el audio
    private IEnumerator goNextLevel(float delay)
    {

        yield return new WaitForSeconds(delay);
        Debug.Log("CogisteUnaMoneda");

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlasher : MonoBehaviour
{
    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] private float _flashTime = .25f;
    [SerializeField] private float _intensity = 5;

    private SpriteRenderer spriteRenderer;
    private Material material;
    private Color flashColor;
    private Coroutine damageFlashCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }
    public void Flash()
    {
        damageFlashCoroutine = StartCoroutine(DamageFlash());
    }
    private IEnumerator DamageFlash()
    {
        float factor = Mathf.Pow(2, _intensity);
        flashColor = new Color(_flashColor.r * factor, _flashColor.g * factor, _flashColor.b * factor);
        material.SetColor("_FlashColor", flashColor);

        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while(elapsedTime < _flashTime)
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(1f, 0, (elapsedTime / _flashTime));
            material.SetFloat("_FlashAmount", currentFlashAmount);  

            yield return null;
        }
    }
}

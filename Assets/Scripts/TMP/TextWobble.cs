using UnityEngine;
using TMPro;
using System.Collections;

public class TextWobble : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public bool shouldWobble = false;
    public float wobbleIntensity = 5f;
    public float wobbleSpeed = 5f;
    private Color originalColor;
    private Vector3 originalPosition;
    private bool isWobbling = false;

    void Start()
    {
        if (textMeshPro != null)
        {
            originalColor = textMeshPro.color;
            originalPosition = textMeshPro.transform.localPosition;
        }
    }

    void Update()
    {
        if (shouldWobble)
        {
            WobbleText();
        }
    }

    public void StartWobble()
    {
        isWobbling = true;
        shouldWobble = true;
    }

    private void StopWobble()
    {
        shouldWobble = false;
        isWobbling = false;
    }

    private void WobbleText()
    {
        if (textMeshPro == null) return;

        textMeshPro.color = Color.yellow;
        textMeshPro.ForceMeshUpdate();
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue;

            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            int vertexIndex = charInfo.vertexIndex;
            Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            float waveOffset = Mathf.Sin(Time.time * wobbleSpeed + i) * wobbleIntensity;
            for (int j = 0; j < 4; j++)
            {
                vertices[vertexIndex + j] += new Vector3(0, waveOffset, 0);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }

    public void TriggerGlitchEffect(float duration)
    {
        StartCoroutine(GlitchEffect(duration));
    }

    private IEnumerator GlitchEffect(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            textMeshPro.color = Color.red;
            textMeshPro.transform.localPosition = originalPosition + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
            yield return new WaitForSeconds(0.05f);
            textMeshPro.transform.localPosition = originalPosition;
            elapsed += 0.05f;
        }
        textMeshPro.color = originalColor;
        textMeshPro.transform.localPosition = originalPosition;
    }
}

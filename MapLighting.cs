using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLighting : MonoBehaviour
{
    public Color targetAmbientLightColor = Color.black;
    public float transitionDuration = 5f;
    public AnimationCurve ambientIntensityCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);
    public bool enableFog = true;
    public Color targetFogColor = Color.black;
    public float targetFogDensity = 0.02f;

    private float initialAmbientIntensity;
    private Color initialAmbientLightColor;
    private Color initialFogColor;
    private float initialFogDensity;
    private bool isTransitioning = false;

    void Start()
    {
        // Save the initial settings
        initialAmbientIntensity = RenderSettings.ambientIntensity;
        initialAmbientLightColor = RenderSettings.ambientLight;
        initialFogColor = RenderSettings.fogColor;
        initialFogDensity = RenderSettings.fogDensity;

        // Set the initial fog settings
        RenderSettings.fog = enableFog;
    }

    void Update()
    {
        // Example trigger condition: player enters a certain area, or a game event occurs
        if (Input.GetKeyDown(KeyCode.L) && !isTransitioning)
        {
            StartCoroutine(ChangeEnvironmentLighting());
        }
    }

    IEnumerator ChangeEnvironmentLighting()
    {
        isTransitioning = true;
        float timer = 0;

        while (timer <= transitionDuration)
        {
            float progress = timer / transitionDuration;
            float curveProgress = ambientIntensityCurve.Evaluate(progress); // Use the curve to adjust the progress
            RenderSettings.ambientIntensity = Mathf.Lerp(initialAmbientIntensity, 0f, curveProgress);
            RenderSettings.ambientLight = Color.Lerp(initialAmbientLightColor, targetAmbientLightColor, curveProgress);
            RenderSettings.fogColor = Color.Lerp(initialFogColor, targetFogColor, curveProgress);
            RenderSettings.fogDensity = Mathf.Lerp(initialFogDensity, targetFogDensity, curveProgress);

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the final values are set
        RenderSettings.ambientIntensity = 0f;
        RenderSettings.ambientLight = targetAmbientLightColor;
        RenderSettings.fogColor = targetFogColor;
        RenderSettings.fogDensity = targetFogDensity;

        isTransitioning = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class Graphics_Quality : MonoBehaviour
{
    public RenderPipelineAsset[] qualityLevels; // Πίνακας με τα επίπεδα ποιότητας γραφικών
    public TMP_Dropdown dropdown; // Dropdown μενού για επιλογή ποιότητας γραφικών

    private void Start()
    {
        // Ορισμός της τιμής του dropdown στο τρέχον επίπεδο ποιότητας
        dropdown.value = QualitySettings.GetQualityLevel();
        // Ορισμός της τιμής V-Sync σε 1 (ενεργοποίηση V-Sync) για το αρχικό επίπεδο ποιότητας
        QualitySettings.vSyncCount = 1;
    }

    public void ChangeLevel(int value)
    {
        // Αλλαγή του επιπέδου ποιότητας
        QualitySettings.SetQualityLevel(value);
        // Αλλαγή της αποδοτικής γραφικής ροής
        QualitySettings.renderPipeline = qualityLevels[value];
        // Ενεργοποίηση V-Sync για το τρέχον επίπεδο ποιότητας
        QualitySettings.vSyncCount = 1;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string title;
    private Text txt;

    private void Awake() {
        txt = GetComponent<Text>();
    }

    private void Update() {
        UpdateVolume();
    }

    private void UpdateVolume(){
        float volumePercent = PlayerPrefs.GetFloat(volumeName) * 100;
        txt.text = title + ((int)volumePercent).ToString();
    }
}

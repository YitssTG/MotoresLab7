using UnityEngine;
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSettings[] audioSettings;

    private float[] _savedVolumes;
    private int _dataLength;

    private void Awake()
    {
        _dataLength = audioSettings.Length;

        _savedVolumes = new float[_dataLength];
    }
    private void OnEnable()
    {
        for (int i = 0; i < _dataLength; i++)
        {
            _savedVolumes[i] = audioSettings[i].VolumeScaled;
        }
    }
    private void OnDisable()
    {

    }
    public void RevertChanges()
    {
        for (int i = 0; i < _dataLength; i++)
        {
            audioSettings[i].UpdateVolume(_savedVolumes[i]);
        }
    }
    public void ApplyChange()
    {
        for (int i = 0; i < _dataLength; i++)
        {
            audioSettings[i].SaveDataFile();

            _savedVolumes[i] = audioSettings[i].VolumeScaled;
        }
    }
}
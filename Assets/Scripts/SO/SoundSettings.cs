using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSound
{
    public SceneName sceneName;
    public List<AudioClip> backgroundMusic; 
}

[CreateAssetMenu(fileName = "SoundSettings", menuName = "GameSettings/SoundSettings", order = 11)]
public class SoundSettings : ScriptableObject
{
    [Range(0f, 1)]
    public float masterVolume;
    [Range(0f, 1)]
    public float effectVolume;
    [Range(0f, 1)]
    public float musicVolume;

    public List<LevelSound> levelSounds;
}

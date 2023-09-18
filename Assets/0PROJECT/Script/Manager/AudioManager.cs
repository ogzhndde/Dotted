using UnityEngine;

/// <summary>   
/// The class that controls all sound events in the game.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public AudioSource audioPlay;
    public AudioSource soundPlay;
    

   private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnPlaySound, OnPlaySound);
        EventManager.AddHandler(GameEvent.OnPlaySoundVolume, OnPlaySoundVolume);
        EventManager.AddHandler(GameEvent.OnPlaySoundPitch, OnPlaySoundPitch);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnPlaySound, OnPlaySound);
        EventManager.RemoveHandler(GameEvent.OnPlaySoundVolume, OnPlaySoundVolume);
        EventManager.RemoveHandler(GameEvent.OnPlaySoundPitch, OnPlaySoundPitch);
    }


    private void OnPlaySound(object value)
    {
        audioPlay.pitch = 1;
        audioPlay.clip = Resources.Load<AudioClip>((string)value);
        audioPlay.PlayOneShot(audioPlay.clip);
    }

    private void OnPlaySoundVolume(object value, object volume)
    {
        audioPlay.volume = (float)volume;
        audioPlay.clip = Resources.Load<AudioClip>((string)value);
        audioPlay.PlayOneShot(audioPlay.clip);
    }

    private void OnPlaySoundPitch(object value, object pitch)
    {
        float pitchValue = (float)pitch;
        audioPlay.pitch = pitchValue > 2.5f ? 2.5f : pitchValue;

        audioPlay.clip = Resources.Load<AudioClip>((string)value);
        audioPlay.PlayOneShot(audioPlay.clip);
    }

    private void OnPlaySoundBg(object value, object volume)
    {
        soundPlay.volume = (float)volume;

        soundPlay.clip = Resources.Load<AudioClip>((string)value);
        soundPlay.PlayOneShot(soundPlay.clip);
    }

  








}

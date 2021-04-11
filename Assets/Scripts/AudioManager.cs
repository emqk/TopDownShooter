using UnityEngine;

public static class AudioManager
{
    public static void PlayClip2D(AudioClip clip)
    {
        GameObject audioInstance = new GameObject("AudioPlayer");
        AudioSource audioSource = audioInstance.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0;
        audioSource.PlayOneShot(clip);
        GameObject.Destroy(audioInstance, clip.length);
    }
}

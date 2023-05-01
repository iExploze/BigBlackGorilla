using UnityEngine;

public class HeartbeatController : MonoBehaviour
{
    public string monsterTag = "Monster";
    public float maxDistance = 10f;
    public AudioSource heartbeatAudioSource;
    public AudioSource scaryMusicAudioSource;
    public AnimationCurve volumeCurve;
    public AnimationCurve pitchCurve;

    private void Update()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(monsterTag);
        float minDistance = maxDistance;

        foreach (GameObject monster in monsters)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }

        float t = 1f - Mathf.Clamp01(minDistance / maxDistance);
        heartbeatAudioSource.volume = volumeCurve.Evaluate(t);
        heartbeatAudioSource.pitch = pitchCurve.Evaluate(t);

        // Control the volume of the scary music based on proximity to the monster
        float scaryMusicVolume = 1f - heartbeatAudioSource.volume;
        scaryMusicAudioSource.volume = scaryMusicVolume;
    }
}

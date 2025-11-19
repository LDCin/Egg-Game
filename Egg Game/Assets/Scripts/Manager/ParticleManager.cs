using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleManager : Singleton<ParticleManager>
{
    [SerializeField] private ParticleSystem _bubbleParticle;
    [SerializeField] private ParticleSystem _cloudParticle;
    public void Start()
    {
        PlayBubbleParticle();
        StopCloudParticle();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            PlayBubbleParticle();
            StopCloudParticle();
        }
        else if (scene.name == "Game")
        {
            StopBubbleParticle();
            PlayCloudParticle();
        }
    }
    public void PlayBubbleParticle()
    {
        _bubbleParticle.Play();
    }
    public void StopBubbleParticle()
    {
        _bubbleParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
    public void PlayCloudParticle()
    {
        _cloudParticle.Play();
    }
    public void StopCloudParticle()
    {
        _cloudParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}

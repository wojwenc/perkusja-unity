using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Button))]
public class Drum : MonoBehaviour {

    private AudioSource audioSource;
    private Button btn;
    public int drumID;
    [SerializeField]
    Recorder recorder;



    public void HitSound() {
        audioSource.Play();
    }

    public void Hit() {
        HitSound();
        recorder.AddDrumHit(drumID);
    }

    public void Disable() {
        btn.interactable = false;
        btn.onClick.RemoveAllListeners();
    }

    public void Enable() {
        btn.interactable = true;
        btn.onClick.AddListener(Hit);
    }

    void OnEnable() {
        Recorder.onStartedPlaying += Disable;
        Recorder.onStoppedPlaying += Enable;
    }

    private void OnDisable() {
        Recorder.onStartedPlaying -= Disable;
        Recorder.onStoppedPlaying -= Enable;
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
        btn = GetComponent<Button>();
        Enable();
	}



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recorder : MonoBehaviour {
    [SerializeField]
    private Button playBtn;
    [SerializeField]
    private Button recordBtn;

    [SerializeField]
    private Text playBtnTxt;
    [SerializeField]
    private Text recordBtnTxt;

    private bool recorded = false;
    private bool isPlaying = false;
    private bool isRecording = false;

    public delegate void RecorderAction();
    public static event RecorderAction onStartedPlaying;
    public static event RecorderAction onStoppedPlaying;

    private float timeBetweenHits = 0.0f;

    [SerializeField]
    private List<float> recording = new List<float>();

    [SerializeField]
    private List<AudioSource> audioSources = new List<AudioSource>();

    IEnumerator PlayRecording() {
        audioSources[(int)recording[1]].Play();
        for (int i = 2; i < recording.Count; i+=2) {
            yield return new WaitForSeconds(recording[i]);
            audioSources[(int)recording[i+1]].Play();
        }

        TogglePlaying();

    }

    void Start() {
        playBtn.onClick.AddListener(TogglePlaying);
        recordBtn.onClick.AddListener(ToggleRecording);

        playBtn.interactable = false;
    }

    public void AddDrumHit(int drumID) {
        if (isRecording) {
            recording.Add(timeBetweenHits);
            recording.Add(drumID);
            timeBetweenHits = 0.0f;
        }
    }

    void ToggleRecording() {
        if (!isPlaying) {
            if(isRecording) {
                isRecording = false;
                playBtn.interactable = true;
                recordBtnTxt.text = "Nagraj";

            } else {
                isRecording = true;
                playBtn.interactable = false;
                recording.Clear();
                recordBtnTxt.text = "Zatrzymaj Nagrywanie";
            }

        }
    }

    void TogglePlaying() {
        if (!isRecording) {
            if (isPlaying) {
                isPlaying = false;
                recordBtn.interactable = true;
                playBtnTxt.text = "Odtwórz";
                onStoppedPlaying();
                StopAllCoroutines();
            } else {
                isPlaying = true;
                recordBtn.interactable = false;
                playBtnTxt.text = "Stop";
                onStartedPlaying();
                StartCoroutine(PlayRecording());
            }
        }
    }

    private void Update() {
        if(isRecording) {
            timeBetweenHits += Time.deltaTime;
        }
    }


}

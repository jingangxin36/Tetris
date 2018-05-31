using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {
    public AudioClip cursor;
    public AudioClip drop;
    public AudioClip control;
    public AudioClip lineClear;

    private AudioSource[] mAudioSources;

    private bool mIsMute;

    protected override void Awake() {
        base.Awake();
        mAudioSources = GetComponents<AudioSource>();
    }

    public void PlayCursor() {
        PlayAudio(cursor, 1);
    }
    public void PlayDrop() {
        PlayAudio(drop, 0);
    }
    public void PlayControl() {
        PlayAudio(control,1);
    }
    public void PlayLineClear() {
        PlayAudio(lineClear,2);
    }

    private void PlayAudio(AudioClip clip, int index) {
        if (mIsMute) return;
        mAudioSources[index].clip = clip;
        mAudioSources[index].Play();
    }

    public void SetMute(bool isMute) {
        mIsMute = isMute;
        if (isMute == false) {
            PlayCursor();
        }
    }

}

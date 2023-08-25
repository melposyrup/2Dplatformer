using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	// bgm settings
	[SerializeField] AudioSource bgmAudioSource;
	[SerializeField] List<BGMSoundData> bgmSoundDatas;

	// se settings
	[SerializeField] AudioSource seAudioSource;
	[SerializeField] List<SESoundData> seSoundDatas;



	public float masterVolume = 1;
	public float bgmMasterVolume = 0.6f;
	public float seMasterVolume = 0.8f;

	public static SoundManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else { Destroy(gameObject); }
	}


	public void PlayBGM(BGMSoundData.BGM bgm)
	{
		BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
		bgmAudioSource.clip = data.audioClip;
		bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
		bgmAudioSource.Play();
	}
	public void SetBGMVolume(BGMSoundData.BGM bgm, float v)
	{
		BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
		bgmAudioSource.clip = data.audioClip;
		Mathf.Clamp(v, 0.0f, 1.0f);
		bgmAudioSource.volume = v;
	}
	public void StopBGM(BGMSoundData.BGM bgm)
	{
		bgmAudioSource.Stop();
	}


	public void PlaySE(SESoundData.SE se)
	{
		SESoundData data = seSoundDatas.Find(data => data.se == se);
		seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
		seAudioSource.PlayOneShot(data.audioClip);
	}
	public void PlaySEnoRepeat(SESoundData.SE se)
	{
		SESoundData data = seSoundDatas.Find(data => data.se == se);
		if (!seAudioSource.isPlaying)
		{
			seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
			seAudioSource.PlayOneShot(data.audioClip);
		}
	}

}


[System.Serializable]
public class BGMSoundData
{
	public enum BGM
	{
		// changed by scene
		TitleScene,
		PlayScene,
		GameClear,
		// add tags here
	}

	public BGM bgm;
	public AudioClip audioClip;
	[Range(0, 1)]
	public float volume = 1;
}

[System.Serializable]
public class SESoundData
{
	public enum SE
	{
		// sound effect
		Enter,
		Move,
		Jump,
		Land,
		Attack,
		Climb,
		Heal,
		Die,
		Hurt,
		GameOver,
		Kill,
		// add tags here
	}

	public SE se;
	public AudioClip audioClip;
	[Range(0, 1)]
	public float volume = 1f;
}


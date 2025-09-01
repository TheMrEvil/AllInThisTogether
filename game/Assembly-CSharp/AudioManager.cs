using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using VellumMusicSystem;

// Token: 0x02000029 RID: 41
public class AudioManager : MonoBehaviour
{
	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000127 RID: 295 RVA: 0x0000D002 File Offset: 0x0000B202
	private AudioSource otherAmbient
	{
		get
		{
			if (!(this.curAmbient == this.AmbientA))
			{
				return this.AmbientA;
			}
			return this.AmbientB;
		}
	}

	// Token: 0x06000128 RID: 296 RVA: 0x0000D024 File Offset: 0x0000B224
	private void Awake()
	{
		AudioManager.availableSources = new Queue<AudioManager.AudioPoolItem>();
		AudioManager.inuseSources = new List<AudioManager.AudioPoolItem>();
		AudioManager.audioNodes = new Dictionary<AudioEffectNode, AudioSource>();
		AudioManager.instance = this;
		this.curAmbient = this.AmbientB;
		for (int i = 0; i < 72; i++)
		{
			GameObject gameObject = new GameObject("AudioSource-" + (i + 1).ToString());
			gameObject.transform.SetParent(base.transform);
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.outputAudioMixerGroup = this.SFXGroup;
			AudioManager.availableSources.Enqueue(new AudioManager.AudioPoolItem(audioSource));
		}
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.GameplayStateChanged));
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(this.SceneChanged));
		GameplayManager.OnGenereChanged = (Action<GenreTree>)Delegate.Combine(GameplayManager.OnGenereChanged, new Action<GenreTree>(this.OnGenreChanged));
		this.LoadSettings();
		this.Mixer.SetFloat("SFX_Volume", 0f);
		this.Mixer.SetFloat("Ambient_Volume", 0f);
	}

	// Token: 0x06000129 RID: 297 RVA: 0x0000D14D File Offset: 0x0000B34D
	private void Start()
	{
		AudioManager.GoToMenuMusic();
		this.SceneChanged();
		base.StartCoroutine("StartFadeIn");
	}

	// Token: 0x0600012A RID: 298 RVA: 0x0000D166 File Offset: 0x0000B366
	private IEnumerator StartFadeIn()
	{
		float t = 0f;
		float sfxVol = Settings.GetFloat(SystemSetting.SFXVolume, 65f) / 100f;
		float ambVol = Settings.GetFloat(SystemSetting.AmbientVolume, 65f) / 100f;
		while (t < 1f)
		{
			t += Time.deltaTime * 0.33f;
			t = Mathf.Clamp01(t);
			float num = t;
			if (sfxVol > 0f)
			{
				float value = AudioManager.LinearToDb(num * sfxVol);
				this.Mixer.SetFloat("SFX_Volume", value);
			}
			if (ambVol > 0f)
			{
				float value2 = AudioManager.LinearToDb(num * ambVol);
				this.Mixer.SetFloat("Ambient_Volume", value2);
			}
			yield return true;
		}
		this.DoneIntro = true;
		this.Music.StartMusicPlayer(this.M_Library);
		yield break;
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0000D178 File Offset: 0x0000B378
	private void Update()
	{
		this.FadeAmbient();
		this.UpdateMusicOverride();
		this.UpdateGameplayFilters();
		for (int i = AudioManager.inuseSources.Count - 1; i >= 0; i--)
		{
			if (!AudioManager.inuseSources[i].IsAvailable)
			{
				AudioManager.availableSources.Enqueue(AudioManager.inuseSources[i]);
				AudioManager.inuseSources.RemoveAt(i);
			}
		}
		foreach (string text in AudioManager.AudioNodeCD.GetKeys<string, float>())
		{
			Dictionary<string, float> audioNodeCD = AudioManager.AudioNodeCD;
			string key = text;
			audioNodeCD[key] -= Time.deltaTime;
			if (AudioManager.AudioNodeCD[text] <= 0f)
			{
				AudioManager.AudioNodeCD.Remove(text);
			}
		}
		foreach (AudioClip audioClip in AudioManager.AudioClipCD.GetKeys<AudioClip, float>())
		{
			Dictionary<AudioClip, float> audioClipCD = AudioManager.AudioClipCD;
			AudioClip key2 = audioClip;
			audioClipCD[key2] -= Time.deltaTime;
			if (AudioManager.AudioClipCD[audioClip] <= 0f)
			{
				AudioManager.AudioClipCD.Remove(audioClip);
			}
		}
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0000D2E4 File Offset: 0x0000B4E4
	private void SceneChanged()
	{
		this.LoadSceneAmbient();
		if (MapManager.InLobbyScene)
		{
			AudioManager.GoToMenuMusic();
			return;
		}
		this.ExitCombatMusic();
	}

	// Token: 0x0600012D RID: 301 RVA: 0x0000D300 File Offset: 0x0000B500
	private void GameplayStateChanged(GameState from, GameState to)
	{
		if (RaidManager.IsInRaid)
		{
			return;
		}
		if (to == GameState.InWave)
		{
			this.EnterCombatMusic(true, -1f);
			AudioManager.PlaySFX2D(this.OnWaveStarted.GetRandomClip(-1), 1f, 0.1f);
		}
		if (to == GameState.Reward_PreEnemy)
		{
			this.EnterCombatMusic(false, -1f);
		}
		if (from == GameState.InWave)
		{
			this.ExitCombatMusic();
		}
		if (to == GameState.Hub_Bindings)
		{
			AudioManager.PlaySFX2D(this.OnBindingStart, 1f, 0.1f);
		}
	}

	// Token: 0x0600012E RID: 302 RVA: 0x0000D375 File Offset: 0x0000B575
	private void OnGenreChanged(GenreTree tree)
	{
		if (!TutorialManager.InTutorial)
		{
			AudioManager.PlaySFX2D(this.GenreChangedSFX, 1f, 0.1f);
		}
	}

	// Token: 0x0600012F RID: 303 RVA: 0x0000D394 File Offset: 0x0000B594
	private void UpdateGameplayFilters()
	{
		float num = -80f;
		bool @bool = Settings.GetBool(SystemSetting.Audio_DangerFX, true);
		if (PlayerControl.myInstance != null && PlayerControl.myInstance.Health.LowHealthDanger && @bool)
		{
			num = 0f;
		}
		float num2 = (num > this.dangerLowpass) ? 8f : 3f;
		this.dangerLowpass = Mathf.Lerp(this.dangerLowpass, num, Time.deltaTime * num2);
		this.dangerLowpass = Mathf.MoveTowards(this.dangerLowpass, num, Time.deltaTime * 0.5f);
		this.Mixer.SetFloat("DangerLowpass", this.dangerLowpass);
	}

	// Token: 0x06000130 RID: 304 RVA: 0x0000D43C File Offset: 0x0000B63C
	public static void PlayUISecondaryAction()
	{
		AudioManager.PlayInterfaceSFX(AudioManager.instance.SecondaryAction, 1f, 0f);
	}

	// Token: 0x06000131 RID: 305 RVA: 0x0000D458 File Offset: 0x0000B658
	public static void PlayUIBook(bool didOpen)
	{
		AudioClip clip = AudioManager.instance.UI_BookClose;
		if (didOpen)
		{
			clip = AudioManager.instance.UI_BookOpen;
		}
		AudioManager.PlayInterfaceSFX(clip, 1f, 0f);
	}

	// Token: 0x06000132 RID: 306 RVA: 0x0000D490 File Offset: 0x0000B690
	public static void OnPlayerDamage(DamageInfo dmg)
	{
		bool flag = dmg.TotalAmount < 7f;
		bool flag2 = dmg.TotalAmount > 15f;
		if (dmg.TotalAmount == 0f)
		{
			AudioManager.PlaySFX2D(AudioManager.instance.AbsorbedDamage, 1f, 0.1f);
			return;
		}
		if (dmg.ShieldAmount > dmg.Amount)
		{
			if (flag)
			{
				AudioManager.PlaySFX2D(AudioManager.instance.ShieldDamageLight.GetRandomClip(-1), 0.66f, 0.1f);
				return;
			}
			if (flag2)
			{
				AudioManager.PlaySFX2D(AudioManager.instance.ShieldDamageHeavy.GetRandomClip(-1), 1f, 0.1f);
				return;
			}
			AudioManager.PlaySFX2D(AudioManager.instance.ShieldDamageMed.GetRandomClip(-1), 1f, 0.1f);
			return;
		}
		else
		{
			if (flag)
			{
				AudioManager.PlaySFX2D(AudioManager.instance.HealthDamageLight.GetRandomClip(-1), 0.66f, 0.1f);
				return;
			}
			if (flag2)
			{
				AudioManager.PlaySFX2D(AudioManager.instance.HealthDamageHeavy.GetRandomClip(-1), 1f, 0.1f);
				return;
			}
			AudioManager.PlaySFX2D(AudioManager.instance.HealthDamageMed.GetRandomClip(-1), 1f, 0.1f);
			return;
		}
	}

	// Token: 0x06000133 RID: 307 RVA: 0x0000D5BC File Offset: 0x0000B7BC
	private static AudioManager.AudioPoolItem NextSource()
	{
		if (AudioManager.availableSources.Count > 0)
		{
			AudioManager.AudioPoolItem audioPoolItem = AudioManager.availableSources.Dequeue();
			AudioManager.inuseSources.Add(audioPoolItem);
			return audioPoolItem;
		}
		AudioManager.AudioPoolItem audioPoolItem2 = AudioManager.inuseSources[0];
		AudioManager.inuseSources.RemoveAt(0);
		AudioManager.inuseSources.Add(audioPoolItem2);
		return audioPoolItem2;
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000D614 File Offset: 0x0000B814
	public static AudioSource PlayClipAtPoint(AudioClip clip, Vector3 point, float volume = 1f, float pitch = 1f, float threed = 1f, float minDist = 10f, float maxDist = 250f)
	{
		if (!AudioManager.CanPlay(clip))
		{
			return null;
		}
		AudioManager.AudioPoolItem audioPoolItem = AudioManager.NextSource();
		AudioSource source = audioPoolItem.source;
		source.transform.position = point;
		source.clip = clip;
		source.volume = volume;
		source.spatialBlend = threed;
		source.minDistance = minDist;
		source.maxDistance = maxDist;
		source.pitch = pitch;
		source.dopplerLevel = 0f;
		source.outputAudioMixerGroup = AudioManager.instance.SFXGroup;
		source.Play();
		audioPoolItem.playTime = Time.realtimeSinceStartup;
		return source;
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000D69C File Offset: 0x0000B89C
	public static void PlaySFX2D(AudioClip clip, float vol = 1f, float cd = 0.1f)
	{
		if (clip == null)
		{
			return;
		}
		AudioManager.PlayClipAtPoint(clip, Vector3.zero, vol, 1f, 0f, 10f, 250f);
		AudioManager.ClipPlayed(clip, cd);
	}

	// Token: 0x06000136 RID: 310 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
	public static void PlayLoudSFX2D(AudioClip clip, float vol = 1f, float cd = 0.1f)
	{
		if (clip == null)
		{
			return;
		}
		AudioManager.PlayLoudClipAtPoint(clip, Vector3.zero, vol, 1f, 0f, 10f, 250f);
		AudioManager.ClipPlayed(clip, cd);
	}

	// Token: 0x06000137 RID: 311 RVA: 0x0000D704 File Offset: 0x0000B904
	public static void PlayInterfaceSFX(AudioClip clip, float volume = 1f, float pitch = 0f)
	{
		if (clip == null)
		{
			return;
		}
		AudioSource audioSource = AudioManager.PlayClipAtPoint(clip, Vector3.zero, volume, (pitch > 0f) ? pitch : UnityEngine.Random.Range(0.95f, 1.05f), 0f, 10f, 250f);
		if (audioSource != null)
		{
			audioSource.outputAudioMixerGroup = AudioManager.instance.InterfaceGroup;
		}
	}

	// Token: 0x06000138 RID: 312 RVA: 0x0000D76C File Offset: 0x0000B96C
	public static AudioSource PlayLoudClipAtPoint(AudioClip clip, Vector3 point, float volume = 1f, float pitch = 1f, float threed = 1f, float minDist = 10f, float maxDist = 250f)
	{
		AudioSource audioSource = AudioManager.PlayClipAtPoint(clip, point, volume, pitch, threed, minDist, maxDist);
		if (audioSource != null)
		{
			audioSource.outputAudioMixerGroup = AudioManager.instance.LoudSFXGroup;
		}
		return audioSource;
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0000D7A3 File Offset: 0x0000B9A3
	public static void AudioNodeActivated(string guid, float cd = 0.1f)
	{
		if (cd <= 0f)
		{
			return;
		}
		if (!AudioManager.AudioNodeCD.ContainsKey(guid))
		{
			AudioManager.AudioNodeCD.Add(guid, cd);
		}
	}

	// Token: 0x0600013A RID: 314 RVA: 0x0000D7C7 File Offset: 0x0000B9C7
	public static void ClipPlayed(AudioClip clip, float cd = 0.075f)
	{
		if (cd <= 0f || clip == null)
		{
			return;
		}
		if (!AudioManager.AudioClipCD.ContainsKey(clip))
		{
			AudioManager.AudioClipCD.Add(clip, cd);
		}
	}

	// Token: 0x0600013B RID: 315 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
	public static bool CanPlay(string guid)
	{
		return !AudioManager.AudioNodeCD.ContainsKey(guid);
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0000D804 File Offset: 0x0000BA04
	public static bool CanPlay(AudioClip clip)
	{
		return !AudioManager.AudioClipCD.ContainsKey(clip);
	}

	// Token: 0x0600013D RID: 317 RVA: 0x0000D814 File Offset: 0x0000BA14
	public static void ConnectAudioNode(AudioEffectNode node, AudioSource src)
	{
		if (AudioManager.audioNodes.ContainsKey(node))
		{
			AudioManager.CancelAudioNode(node, true);
		}
		AudioManager.audioNodes.Add(node, src);
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0000D838 File Offset: 0x0000BA38
	public static void CancelAudioNode(AudioEffectNode node, bool cancelFollow)
	{
		if (!AudioManager.audioNodes.ContainsKey(node))
		{
			return;
		}
		AudioSource audioSource = AudioManager.audioNodes[node];
		AudioManager.audioNodes.Remove(node);
		if (audioSource == null)
		{
			return;
		}
		audioSource.Stop();
		if (cancelFollow)
		{
			LockFollow component = audioSource.GetComponent<LockFollow>();
			if (component == null)
			{
				return;
			}
			component.ReleaseImmediate();
		}
	}

	// Token: 0x0600013F RID: 319 RVA: 0x0000D88E File Offset: 0x0000BA8E
	private void LoadSceneAmbient()
	{
		this.LoadAmbient(Scene_Settings.instance.AmbientAudio);
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
	public void LoadAmbient(AudioClip ambientClip)
	{
		AudioSource otherAmbient = this.otherAmbient;
		otherAmbient.volume = 0f;
		otherAmbient.clip = ambientClip;
		otherAmbient.Play();
		this.curAmbient = otherAmbient;
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
	private void FadeAmbient()
	{
		if (this.otherAmbient.volume > 0f && this.otherAmbient.isPlaying)
		{
			this.otherAmbient.volume -= Time.deltaTime * 0.3f;
			if (this.otherAmbient.volume <= 0f)
			{
				this.otherAmbient.Stop();
			}
		}
		if (this.curAmbient.volume < 1f)
		{
			this.curAmbient.volume += Time.deltaTime * 0.3f;
		}
	}

	// Token: 0x06000142 RID: 322 RVA: 0x0000D96C File Offset: 0x0000BB6C
	public static void GoToMenuMusic()
	{
		if (!AudioManager.instance.Music.IsPlaying)
		{
			AudioManager.instance.Music.StartMusicPlayer(AudioManager.instance.M_Library);
			return;
		}
		AudioManager.instance.Music.SetClipSet(AudioManager.instance.M_Library);
	}

	// Token: 0x06000143 RID: 323 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
	public void EnterCombatMusic(bool postEnemy, float fadeOverride = -1f)
	{
		if (!RaidManager.IsInRaid)
		{
			if (postEnemy)
			{
				if (WaveManager.WaveConfig != null && WaveManager.WaveConfig.chapterType == GenreWaveNode.ChapterType.Boss)
				{
					if (this.Music.IsPlaying)
					{
						this.Music.RestartMusicPlayer(this.M_BossFight, -1f);
						return;
					}
					this.Music.StartMusicPlayer(this.M_BossFight);
					return;
				}
			}
			else
			{
				GenreWaveNode nextWaveInfo = WaveManager.NextWaveInfo;
				if (nextWaveInfo != null && nextWaveInfo.chapterType == GenreWaveNode.ChapterType.Boss)
				{
					this.Music.StopMusicPlayer();
					return;
				}
				this.Music.SetClipSet(this.M_CombatNormal);
			}
			return;
		}
		AudioManager.RaidMusic raidMusic = AudioManager.GetRaidMusic();
		VMS_ClipSet vms_ClipSet = raidMusic.NormalBoss;
		if (RaidDB.IsFinalEncounter(raidMusic.Raid, RaidManager.instance.CurrentEncounter))
		{
			vms_ClipSet = raidMusic.FinalBoss;
		}
		UnityEngine.Debug.Log("Starting Raid Boss music: " + this.Music.IsPlaying.ToString());
		if (this.Music.IsPlaying)
		{
			this.Music.RestartMusicPlayer(vms_ClipSet, fadeOverride);
			return;
		}
		this.Music.StartMusicPlayer(vms_ClipSet);
	}

	// Token: 0x06000144 RID: 324 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
	public void ExitCombatMusic()
	{
		if (MapManager.InLobbyScene)
		{
			return;
		}
		if (RaidManager.IsInRaid)
		{
			if (AudioManager.GetRaidMusic() != null)
			{
				AudioManager.instance.Music.SetClipSet(AudioManager.GetRaidMusic().Filler);
				return;
			}
		}
		else
		{
			AudioManager.instance.Music.SetClipSet(AudioManager.instance.M_BetweenWaves);
		}
	}

	// Token: 0x06000145 RID: 325 RVA: 0x0000DB2C File Offset: 0x0000BD2C
	private void UpdateMusicOverride()
	{
		if (this.mOverride > 0f && this.ShouldOverrideMusic())
		{
			this.mOverride -= 50f * Time.deltaTime;
		}
		else if (this.mOverride < 50f && !this.ShouldOverrideMusic())
		{
			this.mOverride = Mathf.Clamp(this.mOverride + 50f * Time.deltaTime, 0f, 50f);
		}
		float value = AudioManager.LinearToDb(this.mOverride / 50f);
		this.Mixer.SetFloat("MusicOverride_Volume", value);
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0000DBC8 File Offset: 0x0000BDC8
	private bool ShouldOverrideMusic()
	{
		return GameplayManager.CurState == GameState.Reward_Enemy || GameplayManager.CurState == GameState.Reward_PreEnemy;
	}

	// Token: 0x06000147 RID: 327 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
	public static float TimeToNextBar()
	{
		double currentMusicTime = VMS_Player.CurrentMusicTime;
		double currentBarTime = AudioManager.instance.Music.GetCurrentBarTime();
		return (float)(currentBarTime - currentMusicTime % currentBarTime);
	}

	// Token: 0x06000148 RID: 328 RVA: 0x0000DC09 File Offset: 0x0000BE09
	public static float CurrentBeatLength()
	{
		AudioManager audioManager = AudioManager.instance;
		return (float)((audioManager != null) ? audioManager.Music.GetCurrentBeatLength() : 10.0);
	}

	// Token: 0x06000149 RID: 329 RVA: 0x0000DC2C File Offset: 0x0000BE2C
	private static AudioManager.RaidMusic GetRaidMusic()
	{
		if (RaidManager.instance == null || AudioManager.instance == null)
		{
			return null;
		}
		RaidDB.RaidType currentRaid = RaidManager.instance.CurrentRaid;
		foreach (AudioManager.RaidMusic raidMusic in AudioManager.instance.RaidInfo)
		{
			if (raidMusic.Raid == currentRaid)
			{
				return raidMusic;
			}
		}
		return AudioManager.instance.RaidInfo[0];
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0000DCC4 File Offset: 0x0000BEC4
	public static float DistanceDelay(float distance)
	{
		return distance / 343f;
	}

	// Token: 0x0600014B RID: 331 RVA: 0x0000DCD0 File Offset: 0x0000BED0
	public static void SetupSFXObject(GameObject root, bool isOtherPlayer)
	{
		if (root == null)
		{
			return;
		}
		foreach (AudioSource audioSource in root.GetAllComponents<AudioSource>())
		{
			audioSource.outputAudioMixerGroup = (isOtherPlayer ? AudioManager.instance.OtherPlayerSFXGroup : AudioManager.instance.SFXGroup);
			if (isOtherPlayer)
			{
				audioSource.spatialBlend = Mathf.Max(audioSource.spatialBlend, 0.8f);
			}
		}
	}

	// Token: 0x0600014C RID: 332 RVA: 0x0000DD60 File Offset: 0x0000BF60
	private void LoadSettings()
	{
		this.ChangeVolumeSetting(SystemSetting.MasterVolume, Settings.GetFloat(SystemSetting.MasterVolume, 0f));
		this.ChangeVolumeSetting(SystemSetting.SFXVolume, Settings.GetFloat(SystemSetting.SFXVolume, 0f));
		this.ChangeVolumeSetting(SystemSetting.AmbientVolume, Settings.GetFloat(SystemSetting.AmbientVolume, 0f));
		this.ChangeVolumeSetting(SystemSetting.InterfaceVolume, Settings.GetFloat(SystemSetting.InterfaceVolume, 0f));
		this.ChangeVolumeSetting(SystemSetting.MusicVolume, Settings.GetFloat(SystemSetting.MusicVolume, 0f));
	}

	// Token: 0x0600014D RID: 333 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
	public void ChangeVolumeSetting(SystemSetting setting, float value)
	{
		string text = null;
		switch (setting)
		{
		case SystemSetting.MasterVolume:
			text = "Master";
			break;
		case SystemSetting.MusicVolume:
			text = "Music";
			break;
		case SystemSetting.AmbientVolume:
			text = "Ambient";
			if (!this.DoneIntro)
			{
				return;
			}
			break;
		case SystemSetting.SFXVolume:
			text = "SFX";
			if (!this.DoneIntro)
			{
				return;
			}
			break;
		case SystemSetting.InterfaceVolume:
			text = "Interface";
			break;
		}
		if (text == null)
		{
			return;
		}
		float value2 = AudioManager.LinearToDb(value / 100f);
		this.Mixer.SetFloat(text + "_Volume", value2);
	}

	// Token: 0x0600014E RID: 334 RVA: 0x0000DE66 File Offset: 0x0000C066
	public static float LinearToDb(float linearVolume)
	{
		if (linearVolume < 0.01f)
		{
			return -80f;
		}
		return Mathf.Log10(linearVolume) * 40f;
	}

	// Token: 0x0600014F RID: 335 RVA: 0x0000DE82 File Offset: 0x0000C082
	public static float DbToLinear(float dbVolume)
	{
		if (dbVolume <= -80f)
		{
			return 0f;
		}
		return Mathf.Pow(10f, dbVolume / 40f);
	}

	// Token: 0x06000150 RID: 336 RVA: 0x0000DEA4 File Offset: 0x0000C0A4
	public float GetMusicVolume()
	{
		float num;
		this.Mixer.GetFloat("Music_Volume", out num);
		float num2;
		this.Mixer.GetFloat("Master_Volume", out num2);
		num = AudioManager.DbToLinear(num);
		num2 = AudioManager.DbToLinear(num2);
		return num * num2;
	}

	// Token: 0x06000151 RID: 337 RVA: 0x0000DEE8 File Offset: 0x0000C0E8
	public AudioManager()
	{
	}

	// Token: 0x06000152 RID: 338 RVA: 0x0000DEFB File Offset: 0x0000C0FB
	// Note: this type is marked as 'beforefieldinit'.
	static AudioManager()
	{
	}

	// Token: 0x0400014A RID: 330
	public static AudioManager instance;

	// Token: 0x0400014B RID: 331
	private static Queue<AudioManager.AudioPoolItem> availableSources = new Queue<AudioManager.AudioPoolItem>();

	// Token: 0x0400014C RID: 332
	private static List<AudioManager.AudioPoolItem> inuseSources = new List<AudioManager.AudioPoolItem>();

	// Token: 0x0400014D RID: 333
	private static Dictionary<AudioEffectNode, AudioSource> audioNodes = new Dictionary<AudioEffectNode, AudioSource>();

	// Token: 0x0400014E RID: 334
	public const int SOURCE_COUNT = 72;

	// Token: 0x0400014F RID: 335
	private static int sourceID = 0;

	// Token: 0x04000150 RID: 336
	[Header("Volume Levels")]
	public AudioMixer Mixer;

	// Token: 0x04000151 RID: 337
	public AudioMixerGroup InterfaceGroup;

	// Token: 0x04000152 RID: 338
	public AudioMixerGroup SFXGroup;

	// Token: 0x04000153 RID: 339
	public AudioMixerGroup OtherPlayerSFXGroup;

	// Token: 0x04000154 RID: 340
	public AudioMixerGroup LoudSFXGroup;

	// Token: 0x04000155 RID: 341
	public VMS_Player Music;

	// Token: 0x04000156 RID: 342
	public VMS_ClipSet M_Library;

	// Token: 0x04000157 RID: 343
	public VMS_ClipSet M_BetweenWaves;

	// Token: 0x04000158 RID: 344
	public VMS_ClipSet M_CombatNormal;

	// Token: 0x04000159 RID: 345
	public VMS_ClipSet M_CombatSplice;

	// Token: 0x0400015A RID: 346
	public VMS_ClipSet M_CombatRaving;

	// Token: 0x0400015B RID: 347
	public VMS_ClipSet M_CombatTangent;

	// Token: 0x0400015C RID: 348
	public VMS_ClipSet M_BossFight;

	// Token: 0x0400015D RID: 349
	public List<AudioManager.RaidMusic> RaidInfo;

	// Token: 0x0400015E RID: 350
	[Header("Ambient SFX")]
	public AudioSource AmbientA;

	// Token: 0x0400015F RID: 351
	public AudioSource AmbientB;

	// Token: 0x04000160 RID: 352
	private AudioSource curAmbient;

	// Token: 0x04000161 RID: 353
	[Header("Game Cycle SFX")]
	public List<AudioClip> OnWaveCompleted;

	// Token: 0x04000162 RID: 354
	public List<AudioClip> OnWaveStarted;

	// Token: 0x04000163 RID: 355
	public AudioClip GenreChangedSFX;

	// Token: 0x04000164 RID: 356
	public AudioClip OnBindingStart;

	// Token: 0x04000165 RID: 357
	public AudioClip VignetteCompletedSFX;

	// Token: 0x04000166 RID: 358
	public AudioClip UI_BookOpen;

	// Token: 0x04000167 RID: 359
	public AudioClip UI_BookClose;

	// Token: 0x04000168 RID: 360
	public AudioClip SecondaryAction;

	// Token: 0x04000169 RID: 361
	public AudioClip PageShredded;

	// Token: 0x0400016A RID: 362
	[Header("Misc SFX")]
	public List<AudioClip> KilledEnemy;

	// Token: 0x0400016B RID: 363
	[Header("Damage Taken")]
	public List<AudioClip> ShieldDamageLight;

	// Token: 0x0400016C RID: 364
	public List<AudioClip> ShieldDamageMed;

	// Token: 0x0400016D RID: 365
	public List<AudioClip> ShieldDamageHeavy;

	// Token: 0x0400016E RID: 366
	public List<AudioClip> HealthDamageLight;

	// Token: 0x0400016F RID: 367
	public List<AudioClip> HealthDamageMed;

	// Token: 0x04000170 RID: 368
	public List<AudioClip> HealthDamageHeavy;

	// Token: 0x04000171 RID: 369
	public AudioClip AbsorbedDamage;

	// Token: 0x04000172 RID: 370
	private static Dictionary<string, float> AudioNodeCD = new Dictionary<string, float>();

	// Token: 0x04000173 RID: 371
	private static Dictionary<AudioClip, float> AudioClipCD = new Dictionary<AudioClip, float>();

	// Token: 0x04000174 RID: 372
	private bool DoneIntro;

	// Token: 0x04000175 RID: 373
	private float dangerLowpass;

	// Token: 0x04000176 RID: 374
	public float mOverride = 100f;

	// Token: 0x020003F7 RID: 1015
	public class AudioPoolItem
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06002082 RID: 8322 RVA: 0x000C0712 File Offset: 0x000BE912
		public bool IsAvailable
		{
			get
			{
				return this.source.isPlaying && Time.realtimeSinceStartup - this.playTime > 0.25f;
			}
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x000C0736 File Offset: 0x000BE936
		public AudioPoolItem(AudioSource src)
		{
			this.source = src;
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x000C0745 File Offset: 0x000BE945
		public static implicit operator AudioSource(AudioManager.AudioPoolItem item)
		{
			return item.source;
		}

		// Token: 0x0400210D RID: 8461
		public AudioSource source;

		// Token: 0x0400210E RID: 8462
		public float playTime;
	}

	// Token: 0x020003F8 RID: 1016
	[Serializable]
	public class RaidMusic
	{
		// Token: 0x06002085 RID: 8325 RVA: 0x000C074D File Offset: 0x000BE94D
		public RaidMusic()
		{
		}

		// Token: 0x0400210F RID: 8463
		public RaidDB.RaidType Raid;

		// Token: 0x04002110 RID: 8464
		public VMS_ClipSet Filler;

		// Token: 0x04002111 RID: 8465
		public VMS_ClipSet NormalBoss;

		// Token: 0x04002112 RID: 8466
		public VMS_ClipSet FinalBoss;
	}

	// Token: 0x020003F9 RID: 1017
	[CompilerGenerated]
	private sealed class <StartFadeIn>d__47 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002086 RID: 8326 RVA: 0x000C0755 File Offset: 0x000BE955
		[DebuggerHidden]
		public <StartFadeIn>d__47(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000C0764 File Offset: 0x000BE964
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x000C0768 File Offset: 0x000BE968
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			AudioManager audioManager = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
			}
			else
			{
				this.<>1__state = -1;
				t = 0f;
				sfxVol = Settings.GetFloat(SystemSetting.SFXVolume, 65f) / 100f;
				ambVol = Settings.GetFloat(SystemSetting.AmbientVolume, 65f) / 100f;
			}
			if (t >= 1f)
			{
				audioManager.DoneIntro = true;
				audioManager.Music.StartMusicPlayer(audioManager.M_Library);
				return false;
			}
			t += Time.deltaTime * 0.33f;
			t = Mathf.Clamp01(t);
			float num2 = t;
			if (sfxVol > 0f)
			{
				float value = AudioManager.LinearToDb(num2 * sfxVol);
				audioManager.Mixer.SetFloat("SFX_Volume", value);
			}
			if (ambVol > 0f)
			{
				float value2 = AudioManager.LinearToDb(num2 * ambVol);
				audioManager.Mixer.SetFloat("Ambient_Volume", value2);
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x000C08A7 File Offset: 0x000BEAA7
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000C08AF File Offset: 0x000BEAAF
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x000C08B6 File Offset: 0x000BEAB6
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002113 RID: 8467
		private int <>1__state;

		// Token: 0x04002114 RID: 8468
		private object <>2__current;

		// Token: 0x04002115 RID: 8469
		public AudioManager <>4__this;

		// Token: 0x04002116 RID: 8470
		private float <t>5__2;

		// Token: 0x04002117 RID: 8471
		private float <sfxVol>5__3;

		// Token: 0x04002118 RID: 8472
		private float <ambVol>5__4;
	}
}

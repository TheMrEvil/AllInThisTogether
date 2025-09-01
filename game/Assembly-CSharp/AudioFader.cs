using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000028 RID: 40
public class AudioFader : AudioPlayer
{
	// Token: 0x06000122 RID: 290 RVA: 0x0000CE5C File Offset: 0x0000B05C
	public override void Play()
	{
		if (this.Options == null || this.Options.Count == 0)
		{
			return;
		}
		if (this.routine != null)
		{
			base.StopCoroutine(this.routine);
		}
		this.routine = base.StartCoroutine(this.FadeInRoutine());
		this.didExit = false;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0000CEAC File Offset: 0x0000B0AC
	public override void Stop()
	{
		if (this.source == null)
		{
			return;
		}
		if (this.didExit)
		{
			return;
		}
		if (this.routine != null)
		{
			base.StopCoroutine(this.routine);
		}
		this.routine = base.StartCoroutine(this.FadeOutRoutine());
		this.didExit = true;
	}

	// Token: 0x06000124 RID: 292 RVA: 0x0000CEFE File Offset: 0x0000B0FE
	private IEnumerator FadeInRoutine()
	{
		AudioClip randomClip = this.Options.GetRandomClip(-1);
		if (!AudioManager.CanPlay(randomClip))
		{
			yield break;
		}
		if (this.source == null)
		{
			base.AddSource();
		}
		this.source.clip = randomClip;
		if (this.RepeatCooldown > 0f)
		{
			AudioManager.ClipPlayed(randomClip, this.RepeatCooldown);
		}
		float time = 0f;
		if (this.IsLoop)
		{
			time = (this.RandomStart ? UnityEngine.Random.Range(0f, this.source.clip.length) : this.Offset);
		}
		this.source.time = time;
		this.source.loop = this.IsLoop;
		this.source.pitch = UnityEngine.Random.Range(this.PitchRange.x, this.PitchRange.y);
		this.source.minDistance = this.MinMaxRange.x;
		this.source.maxDistance = this.MinMaxRange.y;
		this.source.spatialBlend = this.SpatialBlend;
		this.source.volume = this.FadeInCurve.Evaluate(0f) * this.Volume;
		this.source.Play();
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime / Mathf.Max(this.FadeDuration, 0.01f);
			this.source.volume = this.FadeInCurve.Evaluate(t) * this.Volume;
			yield return true;
			if (this.GroupOverride != null && this.source.outputAudioMixerGroup != this.GroupOverride)
			{
				this.source.outputAudioMixerGroup = this.GroupOverride;
			}
		}
		yield break;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x0000CF0D File Offset: 0x0000B10D
	private IEnumerator FadeOutRoutine()
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime / Mathf.Max(this.FadeDuration, 0.01f);
			this.source.volume = this.FadeOutCurve.Evaluate(t) * this.Volume;
			yield return true;
		}
		this.source.Stop();
		yield break;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x0000CF1C File Offset: 0x0000B11C
	public AudioFader()
	{
	}

	// Token: 0x0400013C RID: 316
	public float FadeDuration = 0.5f;

	// Token: 0x0400013D RID: 317
	public AnimationCurve FadeInCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x0400013E RID: 318
	public AnimationCurve FadeOutCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 0f)
	});

	// Token: 0x0400013F RID: 319
	[Range(0f, 1f)]
	public float Volume = 1f;

	// Token: 0x04000140 RID: 320
	[Range(0f, 1f)]
	public float SpatialBlend = 1f;

	// Token: 0x04000141 RID: 321
	public Vector2 MinMaxRange = new Vector2(15f, 250f);

	// Token: 0x04000142 RID: 322
	public Vector2 PitchRange = new Vector2(1f, 1f);

	// Token: 0x04000143 RID: 323
	public bool IsLoop = true;

	// Token: 0x04000144 RID: 324
	public bool RandomStart = true;

	// Token: 0x04000145 RID: 325
	public float Offset;

	// Token: 0x04000146 RID: 326
	public float RepeatCooldown;

	// Token: 0x04000147 RID: 327
	public AudioMixerGroup GroupOverride;

	// Token: 0x04000148 RID: 328
	private bool didExit;

	// Token: 0x04000149 RID: 329
	private Coroutine routine;

	// Token: 0x020003F5 RID: 1013
	[CompilerGenerated]
	private sealed class <FadeInRoutine>d__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002076 RID: 8310 RVA: 0x000C03EC File Offset: 0x000BE5EC
		[DebuggerHidden]
		public <FadeInRoutine>d__16(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000C03FB File Offset: 0x000BE5FB
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000C0400 File Offset: 0x000BE600
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			AudioFader audioFader = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				if (audioFader.GroupOverride != null && audioFader.source.outputAudioMixerGroup != audioFader.GroupOverride)
				{
					audioFader.source.outputAudioMixerGroup = audioFader.GroupOverride;
				}
			}
			else
			{
				this.<>1__state = -1;
				AudioClip randomClip = audioFader.Options.GetRandomClip(-1);
				if (!AudioManager.CanPlay(randomClip))
				{
					return false;
				}
				if (audioFader.source == null)
				{
					audioFader.AddSource();
				}
				audioFader.source.clip = randomClip;
				if (audioFader.RepeatCooldown > 0f)
				{
					AudioManager.ClipPlayed(randomClip, audioFader.RepeatCooldown);
				}
				float time = 0f;
				if (audioFader.IsLoop)
				{
					time = (audioFader.RandomStart ? UnityEngine.Random.Range(0f, audioFader.source.clip.length) : audioFader.Offset);
				}
				audioFader.source.time = time;
				audioFader.source.loop = audioFader.IsLoop;
				audioFader.source.pitch = UnityEngine.Random.Range(audioFader.PitchRange.x, audioFader.PitchRange.y);
				audioFader.source.minDistance = audioFader.MinMaxRange.x;
				audioFader.source.maxDistance = audioFader.MinMaxRange.y;
				audioFader.source.spatialBlend = audioFader.SpatialBlend;
				audioFader.source.volume = audioFader.FadeInCurve.Evaluate(0f) * audioFader.Volume;
				audioFader.source.Play();
				t = 0f;
			}
			if (t >= 1f)
			{
				return false;
			}
			t += Time.deltaTime / Mathf.Max(audioFader.FadeDuration, 0.01f);
			audioFader.source.volume = audioFader.FadeInCurve.Evaluate(t) * audioFader.Volume;
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x000C061E File Offset: 0x000BE81E
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x000C0626 File Offset: 0x000BE826
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x000C062D File Offset: 0x000BE82D
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002105 RID: 8453
		private int <>1__state;

		// Token: 0x04002106 RID: 8454
		private object <>2__current;

		// Token: 0x04002107 RID: 8455
		public AudioFader <>4__this;

		// Token: 0x04002108 RID: 8456
		private float <t>5__2;
	}

	// Token: 0x020003F6 RID: 1014
	[CompilerGenerated]
	private sealed class <FadeOutRoutine>d__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600207C RID: 8316 RVA: 0x000C0635 File Offset: 0x000BE835
		[DebuggerHidden]
		public <FadeOutRoutine>d__17(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000C0644 File Offset: 0x000BE844
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x000C0648 File Offset: 0x000BE848
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			AudioFader audioFader = this;
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
			}
			if (t >= 1f)
			{
				audioFader.source.Stop();
				return false;
			}
			t += Time.deltaTime / Mathf.Max(audioFader.FadeDuration, 0.01f);
			audioFader.source.volume = audioFader.FadeOutCurve.Evaluate(t) * audioFader.Volume;
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x000C06FB File Offset: 0x000BE8FB
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000C0703 File Offset: 0x000BE903
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x000C070A File Offset: 0x000BE90A
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002109 RID: 8457
		private int <>1__state;

		// Token: 0x0400210A RID: 8458
		private object <>2__current;

		// Token: 0x0400210B RID: 8459
		public AudioFader <>4__this;

		// Token: 0x0400210C RID: 8460
		private float <t>5__2;
	}
}

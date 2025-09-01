using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Audio
{
	// Token: 0x02000023 RID: 35
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioClipPlayable.bindings.h")]
	[RequiredByNativeCode]
	[StaticAccessor("AudioClipPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Audio/Public/Director/AudioClipPlayable.h")]
	public struct AudioClipPlayable : IPlayable, IEquatable<AudioClipPlayable>
	{
		// Token: 0x0600017C RID: 380 RVA: 0x00002FCC File Offset: 0x000011CC
		public static AudioClipPlayable Create(PlayableGraph graph, AudioClip clip, bool looping)
		{
			PlayableHandle handle = AudioClipPlayable.CreateHandle(graph, clip, looping);
			AudioClipPlayable audioClipPlayable = new AudioClipPlayable(handle);
			bool flag = clip != null;
			if (flag)
			{
				audioClipPlayable.SetDuration((double)clip.length);
			}
			return audioClipPlayable;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000300C File Offset: 0x0000120C
		private static PlayableHandle CreateHandle(PlayableGraph graph, AudioClip clip, bool looping)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AudioClipPlayable.InternalCreateAudioClipPlayable(ref graph, clip, looping, ref @null);
			PlayableHandle result;
			if (flag)
			{
				result = PlayableHandle.Null;
			}
			else
			{
				result = @null;
			}
			return result;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00003040 File Offset: 0x00001240
		internal AudioClipPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AudioClipPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AudioClipPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000307C File Offset: 0x0000127C
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00003094 File Offset: 0x00001294
		public static implicit operator Playable(AudioClipPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000030B4 File Offset: 0x000012B4
		public static explicit operator AudioClipPlayable(Playable playable)
		{
			return new AudioClipPlayable(playable.GetHandle());
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000030D4 File Offset: 0x000012D4
		public bool Equals(AudioClipPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000030F8 File Offset: 0x000012F8
		public AudioClip GetClip()
		{
			return AudioClipPlayable.GetClipInternal(ref this.m_Handle);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00003115 File Offset: 0x00001315
		public void SetClip(AudioClip value)
		{
			AudioClipPlayable.SetClipInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00003128 File Offset: 0x00001328
		public bool GetLooped()
		{
			return AudioClipPlayable.GetLoopedInternal(ref this.m_Handle);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00003145 File Offset: 0x00001345
		public void SetLooped(bool value)
		{
			AudioClipPlayable.SetLoopedInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00003158 File Offset: 0x00001358
		internal float GetVolume()
		{
			return AudioClipPlayable.GetVolumeInternal(ref this.m_Handle);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00003178 File Offset: 0x00001378
		internal void SetVolume(float value)
		{
			bool flag = value < 0f || value > 1f;
			if (flag)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable volume outside of range (0.0 - 1.0): " + value.ToString());
			}
			AudioClipPlayable.SetVolumeInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000031C4 File Offset: 0x000013C4
		internal float GetStereoPan()
		{
			return AudioClipPlayable.GetStereoPanInternal(ref this.m_Handle);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000031E4 File Offset: 0x000013E4
		internal void SetStereoPan(float value)
		{
			bool flag = value < -1f || value > 1f;
			if (flag)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable stereo pan outside of range (-1.0 - 1.0): " + value.ToString());
			}
			AudioClipPlayable.SetStereoPanInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00003230 File Offset: 0x00001430
		internal float GetSpatialBlend()
		{
			return AudioClipPlayable.GetSpatialBlendInternal(ref this.m_Handle);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00003250 File Offset: 0x00001450
		internal void SetSpatialBlend(float value)
		{
			bool flag = value < 0f || value > 1f;
			if (flag)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable spatial blend outside of range (0.0 - 1.0): " + value.ToString());
			}
			AudioClipPlayable.SetSpatialBlendInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000329C File Offset: 0x0000149C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("IsPlaying() has been deprecated. Use IsChannelPlaying() instead (UnityUpgradable) -> IsChannelPlaying()", true)]
		public bool IsPlaying()
		{
			return this.IsChannelPlaying();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000032B4 File Offset: 0x000014B4
		public bool IsChannelPlaying()
		{
			return AudioClipPlayable.GetIsChannelPlayingInternal(ref this.m_Handle);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000032D4 File Offset: 0x000014D4
		public double GetStartDelay()
		{
			return AudioClipPlayable.GetStartDelayInternal(ref this.m_Handle);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000032F1 File Offset: 0x000014F1
		internal void SetStartDelay(double value)
		{
			AudioClipPlayable.SetStartDelayInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00003304 File Offset: 0x00001504
		public double GetPauseDelay()
		{
			return AudioClipPlayable.GetPauseDelayInternal(ref this.m_Handle);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00003324 File Offset: 0x00001524
		internal void GetPauseDelay(double value)
		{
			double pauseDelayInternal = AudioClipPlayable.GetPauseDelayInternal(ref this.m_Handle);
			bool flag = this.m_Handle.GetPlayState() == PlayState.Playing && (value < 0.05 || (pauseDelayInternal != 0.0 && pauseDelayInternal < 0.05));
			if (flag)
			{
				throw new ArgumentException("AudioClipPlayable.pauseDelay: Setting new delay when existing delay is too small or 0.0 (" + pauseDelayInternal.ToString() + "), audio system will not be able to change in time");
			}
			AudioClipPlayable.SetPauseDelayInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000033A6 File Offset: 0x000015A6
		public void Seek(double startTime, double startDelay)
		{
			this.Seek(startTime, startDelay, 0.0);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000033BC File Offset: 0x000015BC
		public void Seek(double startTime, double startDelay, [DefaultValue("0")] double duration)
		{
			AudioClipPlayable.SetStartDelayInternal(ref this.m_Handle, startDelay);
			bool flag = duration > 0.0;
			if (flag)
			{
				double num = startDelay + duration;
				bool flag2 = num >= this.m_Handle.GetDuration();
				if (flag2)
				{
					this.m_Handle.SetDone(true);
				}
				this.m_Handle.SetDuration(duration + startTime);
				AudioClipPlayable.SetPauseDelayInternal(ref this.m_Handle, startDelay + duration);
			}
			else
			{
				this.m_Handle.SetDone(true);
				this.m_Handle.SetDuration(double.MaxValue);
				AudioClipPlayable.SetPauseDelayInternal(ref this.m_Handle, 0.0);
			}
			this.m_Handle.SetTime(startTime);
			this.m_Handle.Play();
		}

		// Token: 0x06000195 RID: 405
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioClip GetClipInternal(ref PlayableHandle hdl);

		// Token: 0x06000196 RID: 406
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetClipInternal(ref PlayableHandle hdl, AudioClip clip);

		// Token: 0x06000197 RID: 407
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetLoopedInternal(ref PlayableHandle hdl);

		// Token: 0x06000198 RID: 408
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLoopedInternal(ref PlayableHandle hdl, bool looped);

		// Token: 0x06000199 RID: 409
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetVolumeInternal(ref PlayableHandle hdl);

		// Token: 0x0600019A RID: 410
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVolumeInternal(ref PlayableHandle hdl, float volume);

		// Token: 0x0600019B RID: 411
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetStereoPanInternal(ref PlayableHandle hdl);

		// Token: 0x0600019C RID: 412
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStereoPanInternal(ref PlayableHandle hdl, float stereoPan);

		// Token: 0x0600019D RID: 413
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetSpatialBlendInternal(ref PlayableHandle hdl);

		// Token: 0x0600019E RID: 414
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSpatialBlendInternal(ref PlayableHandle hdl, float spatialBlend);

		// Token: 0x0600019F RID: 415
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIsChannelPlayingInternal(ref PlayableHandle hdl);

		// Token: 0x060001A0 RID: 416
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetStartDelayInternal(ref PlayableHandle hdl);

		// Token: 0x060001A1 RID: 417
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStartDelayInternal(ref PlayableHandle hdl, double delay);

		// Token: 0x060001A2 RID: 418
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetPauseDelayInternal(ref PlayableHandle hdl);

		// Token: 0x060001A3 RID: 419
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPauseDelayInternal(ref PlayableHandle hdl, double delay);

		// Token: 0x060001A4 RID: 420
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalCreateAudioClipPlayable(ref PlayableGraph graph, AudioClip clip, bool looping, ref PlayableHandle handle);

		// Token: 0x060001A5 RID: 421
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidateType(ref PlayableHandle hdl);

		// Token: 0x04000067 RID: 103
		private PlayableHandle m_Handle;
	}
}

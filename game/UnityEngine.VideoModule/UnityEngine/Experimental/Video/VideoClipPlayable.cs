using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;
using UnityEngine.Video;

namespace UnityEngine.Experimental.Video
{
	// Token: 0x02000002 RID: 2
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("VideoClipPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Video/Public/VideoClip.h")]
	[NativeHeader("Modules/Video/Public/ScriptBindings/VideoClipPlayable.bindings.h")]
	[NativeHeader("Modules/Video/Public/Director/VideoClipPlayable.h")]
	public struct VideoClipPlayable : IPlayable, IEquatable<VideoClipPlayable>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static VideoClipPlayable Create(PlayableGraph graph, VideoClip clip, bool looping)
		{
			PlayableHandle handle = VideoClipPlayable.CreateHandle(graph, clip, looping);
			VideoClipPlayable videoClipPlayable = new VideoClipPlayable(handle);
			bool flag = clip != null;
			if (flag)
			{
				videoClipPlayable.SetDuration(clip.length);
			}
			return videoClipPlayable;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000208C File Offset: 0x0000028C
		private static PlayableHandle CreateHandle(PlayableGraph graph, VideoClip clip, bool looping)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !VideoClipPlayable.InternalCreateVideoClipPlayable(ref graph, clip, looping, ref @null);
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

		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		internal VideoClipPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<VideoClipPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an VideoClipPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002114 File Offset: 0x00000314
		public static implicit operator Playable(VideoClipPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002134 File Offset: 0x00000334
		public static explicit operator VideoClipPlayable(Playable playable)
		{
			return new VideoClipPlayable(playable.GetHandle());
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002154 File Offset: 0x00000354
		public bool Equals(VideoClipPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002178 File Offset: 0x00000378
		public VideoClip GetClip()
		{
			return VideoClipPlayable.GetClipInternal(ref this.m_Handle);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002195 File Offset: 0x00000395
		public void SetClip(VideoClip value)
		{
			VideoClipPlayable.SetClipInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021A8 File Offset: 0x000003A8
		public bool GetLooped()
		{
			return VideoClipPlayable.GetLoopedInternal(ref this.m_Handle);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021C5 File Offset: 0x000003C5
		public void SetLooped(bool value)
		{
			VideoClipPlayable.SetLoopedInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021D8 File Offset: 0x000003D8
		public bool IsPlaying()
		{
			return VideoClipPlayable.GetIsPlayingInternal(ref this.m_Handle);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021F8 File Offset: 0x000003F8
		public double GetStartDelay()
		{
			return VideoClipPlayable.GetStartDelayInternal(ref this.m_Handle);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002215 File Offset: 0x00000415
		internal void SetStartDelay(double value)
		{
			this.ValidateStartDelayInternal(value);
			VideoClipPlayable.SetStartDelayInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002230 File Offset: 0x00000430
		public double GetPauseDelay()
		{
			return VideoClipPlayable.GetPauseDelayInternal(ref this.m_Handle);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002250 File Offset: 0x00000450
		internal void GetPauseDelay(double value)
		{
			double pauseDelayInternal = VideoClipPlayable.GetPauseDelayInternal(ref this.m_Handle);
			bool flag = this.m_Handle.GetPlayState() == PlayState.Playing && (value < 0.05 || (pauseDelayInternal != 0.0 && pauseDelayInternal < 0.05));
			if (flag)
			{
				throw new ArgumentException("VideoClipPlayable.pauseDelay: Setting new delay when existing delay is too small or 0.0 (" + pauseDelayInternal.ToString() + "), Video system will not be able to change in time");
			}
			VideoClipPlayable.SetPauseDelayInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022D2 File Offset: 0x000004D2
		public void Seek(double startTime, double startDelay)
		{
			this.Seek(startTime, startDelay, 0.0);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022E8 File Offset: 0x000004E8
		public void Seek(double startTime, double startDelay, [DefaultValue("0")] double duration)
		{
			this.ValidateStartDelayInternal(startDelay);
			VideoClipPlayable.SetStartDelayInternal(ref this.m_Handle, startDelay);
			bool flag = duration > 0.0;
			if (flag)
			{
				this.m_Handle.SetDuration(duration + startTime);
				VideoClipPlayable.SetPauseDelayInternal(ref this.m_Handle, startDelay + duration);
			}
			else
			{
				this.m_Handle.SetDuration(double.MaxValue);
				VideoClipPlayable.SetPauseDelayInternal(ref this.m_Handle, 0.0);
			}
			this.m_Handle.SetTime(startTime);
			this.m_Handle.Play();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002384 File Offset: 0x00000584
		private void ValidateStartDelayInternal(double startDelay)
		{
			double startDelayInternal = VideoClipPlayable.GetStartDelayInternal(ref this.m_Handle);
			bool flag = this.IsPlaying() && (startDelay < 0.05 || (startDelayInternal >= 1E-05 && startDelayInternal < 0.05));
			if (flag)
			{
				Debug.LogWarning("VideoClipPlayable.StartDelay: Setting new delay when existing delay is too small or 0.0 (" + startDelayInternal.ToString() + "), Video system will not be able to change in time");
			}
		}

		// Token: 0x06000014 RID: 20
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoClip GetClipInternal(ref PlayableHandle hdl);

		// Token: 0x06000015 RID: 21
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetClipInternal(ref PlayableHandle hdl, VideoClip clip);

		// Token: 0x06000016 RID: 22
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetLoopedInternal(ref PlayableHandle hdl);

		// Token: 0x06000017 RID: 23
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLoopedInternal(ref PlayableHandle hdl, bool looped);

		// Token: 0x06000018 RID: 24
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIsPlayingInternal(ref PlayableHandle hdl);

		// Token: 0x06000019 RID: 25
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetStartDelayInternal(ref PlayableHandle hdl);

		// Token: 0x0600001A RID: 26
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStartDelayInternal(ref PlayableHandle hdl, double delay);

		// Token: 0x0600001B RID: 27
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetPauseDelayInternal(ref PlayableHandle hdl);

		// Token: 0x0600001C RID: 28
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPauseDelayInternal(ref PlayableHandle hdl, double delay);

		// Token: 0x0600001D RID: 29
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalCreateVideoClipPlayable(ref PlayableGraph graph, VideoClip clip, bool looping, ref PlayableHandle handle);

		// Token: 0x0600001E RID: 30
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidateType(ref PlayableHandle hdl);

		// Token: 0x04000001 RID: 1
		private PlayableHandle m_Handle;
	}
}

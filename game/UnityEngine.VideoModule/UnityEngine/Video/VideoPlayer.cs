using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Video
{
	// Token: 0x0200000C RID: 12
	[RequiredByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Video/Public/VideoPlayer.h")]
	public sealed class VideoPlayer : Behaviour
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47
		// (set) Token: 0x06000030 RID: 48
		public extern VideoSource source { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000031 RID: 49
		// (set) Token: 0x06000032 RID: 50
		[NativeName("VideoUrl")]
		public extern string url { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000033 RID: 51
		// (set) Token: 0x06000034 RID: 52
		[NativeName("VideoClip")]
		public extern VideoClip clip { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000035 RID: 53
		// (set) Token: 0x06000036 RID: 54
		public extern VideoRenderMode renderMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000037 RID: 55
		// (set) Token: 0x06000038 RID: 56
		[NativeHeader("Runtime/Camera/Camera.h")]
		public extern Camera targetCamera { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000039 RID: 57
		// (set) Token: 0x0600003A RID: 58
		[NativeHeader("Runtime/Graphics/RenderTexture.h")]
		public extern RenderTexture targetTexture { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003B RID: 59
		// (set) Token: 0x0600003C RID: 60
		[NativeHeader("Runtime/Graphics/Renderer.h")]
		public extern Renderer targetMaterialRenderer { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003D RID: 61
		// (set) Token: 0x0600003E RID: 62
		public extern string targetMaterialProperty { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003F RID: 63
		// (set) Token: 0x06000040 RID: 64
		public extern VideoAspectRatio aspectRatio { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000041 RID: 65
		// (set) Token: 0x06000042 RID: 66
		public extern float targetCameraAlpha { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000043 RID: 67
		// (set) Token: 0x06000044 RID: 68
		public extern Video3DLayout targetCamera3DLayout { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000045 RID: 69
		[NativeHeader("Runtime/Graphics/Texture.h")]
		public extern Texture texture { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000046 RID: 70
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Prepare();

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000047 RID: 71
		public extern bool isPrepared { [NativeName("IsPrepared")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000048 RID: 72
		// (set) Token: 0x06000049 RID: 73
		public extern bool waitForFirstFrame { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004A RID: 74
		// (set) Token: 0x0600004B RID: 75
		public extern bool playOnAwake { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600004C RID: 76
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play();

		// Token: 0x0600004D RID: 77
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Pause();

		// Token: 0x0600004E RID: 78
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004F RID: 79
		public extern bool isPlaying { [NativeName("IsPlaying")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000050 RID: 80
		public extern bool isPaused { [NativeName("IsPaused")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000051 RID: 81
		public extern bool canSetTime { [NativeName("CanSetTime")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000052 RID: 82
		// (set) Token: 0x06000053 RID: 83
		[NativeName("SecPosition")]
		public extern double time { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000054 RID: 84
		// (set) Token: 0x06000055 RID: 85
		[NativeName("FramePosition")]
		public extern long frame { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000056 RID: 86
		public extern double clockTime { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000057 RID: 87
		public extern bool canStep { [NativeName("CanStep")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StepForward();

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000059 RID: 89
		public extern bool canSetPlaybackSpeed { [NativeName("CanSetPlaybackSpeed")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005A RID: 90
		// (set) Token: 0x0600005B RID: 91
		public extern float playbackSpeed { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005C RID: 92
		// (set) Token: 0x0600005D RID: 93
		[NativeName("Loop")]
		public extern bool isLooping { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005E RID: 94
		public extern bool canSetTimeSource { [NativeName("CanSetTimeSource")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005F RID: 95
		// (set) Token: 0x06000060 RID: 96
		public extern VideoTimeSource timeSource { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000061 RID: 97
		// (set) Token: 0x06000062 RID: 98
		public extern VideoTimeReference timeReference { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000063 RID: 99
		// (set) Token: 0x06000064 RID: 100
		public extern double externalReferenceTime { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000065 RID: 101
		public extern bool canSetSkipOnDrop { [NativeName("CanSetSkipOnDrop")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000066 RID: 102
		// (set) Token: 0x06000067 RID: 103
		public extern bool skipOnDrop { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000068 RID: 104
		public extern ulong frameCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000069 RID: 105
		public extern float frameRate { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006A RID: 106
		[NativeName("Duration")]
		public extern double length { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006B RID: 107
		public extern uint width { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600006C RID: 108
		public extern uint height { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600006D RID: 109
		public extern uint pixelAspectRatioNumerator { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600006E RID: 110
		public extern uint pixelAspectRatioDenominator { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006F RID: 111
		public extern ushort audioTrackCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000070 RID: 112
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetAudioLanguageCode(ushort trackIndex);

		// Token: 0x06000071 RID: 113
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ushort GetAudioChannelCount(ushort trackIndex);

		// Token: 0x06000072 RID: 114
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetAudioSampleRate(ushort trackIndex);

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000073 RID: 115
		public static extern ushort controlledAudioTrackMaxCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002500 File Offset: 0x00000700
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002518 File Offset: 0x00000718
		public ushort controlledAudioTrackCount
		{
			get
			{
				return this.GetControlledAudioTrackCount();
			}
			set
			{
				int controlledAudioTrackMaxCount = (int)VideoPlayer.controlledAudioTrackMaxCount;
				bool flag = (int)value > controlledAudioTrackMaxCount;
				if (flag)
				{
					throw new ArgumentException(string.Format("Cannot control more than {0} tracks.", controlledAudioTrackMaxCount), "value");
				}
				this.SetControlledAudioTrackCount(value);
			}
		}

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ushort GetControlledAudioTrackCount();

		// Token: 0x06000077 RID: 119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetControlledAudioTrackCount(ushort value);

		// Token: 0x06000078 RID: 120
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnableAudioTrack(ushort trackIndex, bool enabled);

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsAudioTrackEnabled(ushort trackIndex);

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600007A RID: 122
		// (set) Token: 0x0600007B RID: 123
		public extern VideoAudioOutputMode audioOutputMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600007C RID: 124
		public extern bool canSetDirectAudioVolume { [NativeName("CanSetDirectAudioVolume")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600007D RID: 125
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetDirectAudioVolume(ushort trackIndex);

		// Token: 0x0600007E RID: 126
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDirectAudioVolume(ushort trackIndex, float volume);

		// Token: 0x0600007F RID: 127
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetDirectAudioMute(ushort trackIndex);

		// Token: 0x06000080 RID: 128
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDirectAudioMute(ushort trackIndex, bool mute);

		// Token: 0x06000081 RID: 129
		[NativeHeader("Modules/Audio/Public/AudioSource.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AudioSource GetTargetAudioSource(ushort trackIndex);

		// Token: 0x06000082 RID: 130
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTargetAudioSource(ushort trackIndex, AudioSource source);

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000083 RID: 131 RVA: 0x00002558 File Offset: 0x00000758
		// (remove) Token: 0x06000084 RID: 132 RVA: 0x00002590 File Offset: 0x00000790
		public event VideoPlayer.EventHandler prepareCompleted
		{
			[CompilerGenerated]
			add
			{
				VideoPlayer.EventHandler eventHandler = this.prepareCompleted;
				VideoPlayer.EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					VideoPlayer.EventHandler value2 = (VideoPlayer.EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<VideoPlayer.EventHandler>(ref this.prepareCompleted, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				VideoPlayer.EventHandler eventHandler = this.prepareCompleted;
				VideoPlayer.EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					VideoPlayer.EventHandler value2 = (VideoPlayer.EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<VideoPlayer.EventHandler>(ref this.prepareCompleted, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000085 RID: 133 RVA: 0x000025C8 File Offset: 0x000007C8
		// (remove) Token: 0x06000086 RID: 134 RVA: 0x00002600 File Offset: 0x00000800
		public event VideoPlayer.EventHandler loopPointReached
		{
			[CompilerGenerated]
			add
			{
				VideoPlayer.EventHandler eventHandler = this.loopPointReached;
				VideoPlayer.EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					VideoPlayer.EventHandler value2 = (VideoPlayer.EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<VideoPlayer.EventHandler>(ref this.loopPointReached, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				VideoPlayer.EventHandler eventHandler = this.loopPointReached;
				VideoPlayer.EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					VideoPlayer.EventHandler value2 = (VideoPlayer.EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<VideoPlayer.EventHandler>(ref this.loopPointReached, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000087 RID: 135 RVA: 0x00002638 File Offset: 0x00000838
		// (remove) Token: 0x06000088 RID: 136 RVA: 0x00002670 File Offset: 0x00000870
		public event VideoPlayer.EventHandler started
		{
			[CompilerGenerated]
			add
			{
				VideoPlayer.EventHandler eventHandler = this.started;
				VideoPlayer.EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					VideoPlayer.EventHandler value2 = (VideoPlayer.EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<VideoPlayer.EventHandler>(ref this.started, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				VideoPlayer.EventHandler eventHandler = this.started;
				VideoPlayer.EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					VideoPlayer.EventHandler value2 = (VideoPlayer.EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<VideoPlayer.EventHandler>(ref this.started, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000089 RID: 137 RVA: 0x000026A8 File Offset: 0x000008A8
		// (remove) Token: 0x0600008A RID: 138 RVA: 0x000026E0 File Offset: 0x000008E0
		public event VideoPlayer.EventHandler frameDropped
		{
			[CompilerGenerated]
			add
			{
				VideoPlayer.EventHandler eventHandler = this.frameDropped;
				VideoPlayer.EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					VideoPlayer.EventHandler value2 = (VideoPlayer.EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<VideoPlayer.EventHandler>(ref this.frameDropped, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				VideoPlayer.EventHandler eventHandler = this.frameDropped;
				VideoPlayer.EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					VideoPlayer.EventHandler value2 = (VideoPlayer.EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<VideoPlayer.EventHandler>(ref this.frameDropped, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600008B RID: 139 RVA: 0x00002718 File Offset: 0x00000918
		// (remove) Token: 0x0600008C RID: 140 RVA: 0x00002750 File Offset: 0x00000950
		public event VideoPlayer.ErrorEventHandler errorReceived
		{
			[CompilerGenerated]
			add
			{
				VideoPlayer.ErrorEventHandler errorEventHandler = this.errorReceived;
				VideoPlayer.ErrorEventHandler errorEventHandler2;
				do
				{
					errorEventHandler2 = errorEventHandler;
					VideoPlayer.ErrorEventHandler value2 = (VideoPlayer.ErrorEventHandler)Delegate.Combine(errorEventHandler2, value);
					errorEventHandler = Interlocked.CompareExchange<VideoPlayer.ErrorEventHandler>(ref this.errorReceived, value2, errorEventHandler2);
				}
				while (errorEventHandler != errorEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				VideoPlayer.ErrorEventHandler errorEventHandler = this.errorReceived;
				VideoPlayer.ErrorEventHandler errorEventHandler2;
				do
				{
					errorEventHandler2 = errorEventHandler;
					VideoPlayer.ErrorEventHandler value2 = (VideoPlayer.ErrorEventHandler)Delegate.Remove(errorEventHandler2, value);
					errorEventHandler = Interlocked.CompareExchange<VideoPlayer.ErrorEventHandler>(ref this.errorReceived, value2, errorEventHandler2);
				}
				while (errorEventHandler != errorEventHandler2);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600008D RID: 141 RVA: 0x00002788 File Offset: 0x00000988
		// (remove) Token: 0x0600008E RID: 142 RVA: 0x000027C0 File Offset: 0x000009C0
		public event VideoPlayer.EventHandler seekCompleted
		{
			[CompilerGenerated]
			add
			{
				VideoPlayer.EventHandler eventHandler = this.seekCompleted;
				VideoPlayer.EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					VideoPlayer.EventHandler value2 = (VideoPlayer.EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<VideoPlayer.EventHandler>(ref this.seekCompleted, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				VideoPlayer.EventHandler eventHandler = this.seekCompleted;
				VideoPlayer.EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					VideoPlayer.EventHandler value2 = (VideoPlayer.EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<VideoPlayer.EventHandler>(ref this.seekCompleted, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600008F RID: 143 RVA: 0x000027F8 File Offset: 0x000009F8
		// (remove) Token: 0x06000090 RID: 144 RVA: 0x00002830 File Offset: 0x00000A30
		public event VideoPlayer.TimeEventHandler clockResyncOccurred
		{
			[CompilerGenerated]
			add
			{
				VideoPlayer.TimeEventHandler timeEventHandler = this.clockResyncOccurred;
				VideoPlayer.TimeEventHandler timeEventHandler2;
				do
				{
					timeEventHandler2 = timeEventHandler;
					VideoPlayer.TimeEventHandler value2 = (VideoPlayer.TimeEventHandler)Delegate.Combine(timeEventHandler2, value);
					timeEventHandler = Interlocked.CompareExchange<VideoPlayer.TimeEventHandler>(ref this.clockResyncOccurred, value2, timeEventHandler2);
				}
				while (timeEventHandler != timeEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				VideoPlayer.TimeEventHandler timeEventHandler = this.clockResyncOccurred;
				VideoPlayer.TimeEventHandler timeEventHandler2;
				do
				{
					timeEventHandler2 = timeEventHandler;
					VideoPlayer.TimeEventHandler value2 = (VideoPlayer.TimeEventHandler)Delegate.Remove(timeEventHandler2, value);
					timeEventHandler = Interlocked.CompareExchange<VideoPlayer.TimeEventHandler>(ref this.clockResyncOccurred, value2, timeEventHandler2);
				}
				while (timeEventHandler != timeEventHandler2);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000091 RID: 145
		// (set) Token: 0x06000092 RID: 146
		public extern bool sendFrameReadyEvents { [NativeName("AreFrameReadyEventsEnabled")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("EnableFrameReadyEvents")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000093 RID: 147 RVA: 0x00002868 File Offset: 0x00000A68
		// (remove) Token: 0x06000094 RID: 148 RVA: 0x000028A0 File Offset: 0x00000AA0
		public event VideoPlayer.FrameReadyEventHandler frameReady
		{
			[CompilerGenerated]
			add
			{
				VideoPlayer.FrameReadyEventHandler frameReadyEventHandler = this.frameReady;
				VideoPlayer.FrameReadyEventHandler frameReadyEventHandler2;
				do
				{
					frameReadyEventHandler2 = frameReadyEventHandler;
					VideoPlayer.FrameReadyEventHandler value2 = (VideoPlayer.FrameReadyEventHandler)Delegate.Combine(frameReadyEventHandler2, value);
					frameReadyEventHandler = Interlocked.CompareExchange<VideoPlayer.FrameReadyEventHandler>(ref this.frameReady, value2, frameReadyEventHandler2);
				}
				while (frameReadyEventHandler != frameReadyEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				VideoPlayer.FrameReadyEventHandler frameReadyEventHandler = this.frameReady;
				VideoPlayer.FrameReadyEventHandler frameReadyEventHandler2;
				do
				{
					frameReadyEventHandler2 = frameReadyEventHandler;
					VideoPlayer.FrameReadyEventHandler value2 = (VideoPlayer.FrameReadyEventHandler)Delegate.Remove(frameReadyEventHandler2, value);
					frameReadyEventHandler = Interlocked.CompareExchange<VideoPlayer.FrameReadyEventHandler>(ref this.frameReady, value2, frameReadyEventHandler2);
				}
				while (frameReadyEventHandler != frameReadyEventHandler2);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000028D8 File Offset: 0x00000AD8
		[RequiredByNativeCode]
		private static void InvokePrepareCompletedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.prepareCompleted != null;
			if (flag)
			{
				source.prepareCompleted(source);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002900 File Offset: 0x00000B00
		[RequiredByNativeCode]
		private static void InvokeFrameReadyCallback_Internal(VideoPlayer source, long frameIdx)
		{
			bool flag = source.frameReady != null;
			if (flag)
			{
				source.frameReady(source, frameIdx);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000292C File Offset: 0x00000B2C
		[RequiredByNativeCode]
		private static void InvokeLoopPointReachedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.loopPointReached != null;
			if (flag)
			{
				source.loopPointReached(source);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002954 File Offset: 0x00000B54
		[RequiredByNativeCode]
		private static void InvokeStartedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.started != null;
			if (flag)
			{
				source.started(source);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000297C File Offset: 0x00000B7C
		[RequiredByNativeCode]
		private static void InvokeFrameDroppedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.frameDropped != null;
			if (flag)
			{
				source.frameDropped(source);
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000029A4 File Offset: 0x00000BA4
		[RequiredByNativeCode]
		private static void InvokeErrorReceivedCallback_Internal(VideoPlayer source, string errorStr)
		{
			bool flag = source.errorReceived != null;
			if (flag)
			{
				source.errorReceived(source, errorStr);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000029D0 File Offset: 0x00000BD0
		[RequiredByNativeCode]
		private static void InvokeSeekCompletedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.seekCompleted != null;
			if (flag)
			{
				source.seekCompleted(source);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000029F8 File Offset: 0x00000BF8
		[RequiredByNativeCode]
		private static void InvokeClockResyncOccurredCallback_Internal(VideoPlayer source, double seconds)
		{
			bool flag = source.clockResyncOccurred != null;
			if (flag)
			{
				source.clockResyncOccurred(source, seconds);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002A21 File Offset: 0x00000C21
		public VideoPlayer()
		{
		}

		// Token: 0x04000022 RID: 34
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private VideoPlayer.EventHandler prepareCompleted;

		// Token: 0x04000023 RID: 35
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VideoPlayer.EventHandler loopPointReached;

		// Token: 0x04000024 RID: 36
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VideoPlayer.EventHandler started;

		// Token: 0x04000025 RID: 37
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private VideoPlayer.EventHandler frameDropped;

		// Token: 0x04000026 RID: 38
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VideoPlayer.ErrorEventHandler errorReceived;

		// Token: 0x04000027 RID: 39
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private VideoPlayer.EventHandler seekCompleted;

		// Token: 0x04000028 RID: 40
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VideoPlayer.TimeEventHandler clockResyncOccurred;

		// Token: 0x04000029 RID: 41
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VideoPlayer.FrameReadyEventHandler frameReady;

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x0600009F RID: 159
		public delegate void EventHandler(VideoPlayer source);

		// Token: 0x0200000E RID: 14
		// (Invoke) Token: 0x060000A3 RID: 163
		public delegate void ErrorEventHandler(VideoPlayer source, string message);

		// Token: 0x0200000F RID: 15
		// (Invoke) Token: 0x060000A7 RID: 167
		public delegate void FrameReadyEventHandler(VideoPlayer source, long frameIdx);

		// Token: 0x02000010 RID: 16
		// (Invoke) Token: 0x060000AB RID: 171
		public delegate void TimeEventHandler(VideoPlayer source, double seconds);
	}
}

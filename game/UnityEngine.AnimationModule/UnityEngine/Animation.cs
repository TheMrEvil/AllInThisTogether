using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	[NativeHeader("Modules/Animation/Animation.h")]
	public sealed class Animation : Behaviour, IEnumerable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000014 RID: 20
		// (set) Token: 0x06000015 RID: 21
		public extern AnimationClip clip { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000016 RID: 22
		// (set) Token: 0x06000017 RID: 23
		public extern bool playAutomatically { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24
		// (set) Token: 0x06000019 RID: 25
		public extern WrapMode wrapMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600001A RID: 26
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		// Token: 0x0600001B RID: 27 RVA: 0x00002065 File Offset: 0x00000265
		public void Stop(string name)
		{
			this.StopNamed(name);
		}

		// Token: 0x0600001C RID: 28
		[NativeName("Stop")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StopNamed(string name);

		// Token: 0x0600001D RID: 29
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Rewind();

		// Token: 0x0600001E RID: 30 RVA: 0x00002070 File Offset: 0x00000270
		public void Rewind(string name)
		{
			this.RewindNamed(name);
		}

		// Token: 0x0600001F RID: 31
		[NativeName("Rewind")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RewindNamed(string name);

		// Token: 0x06000020 RID: 32
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Sample();

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33
		public extern bool isPlaying { [NativeName("IsPlaying")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000022 RID: 34
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsPlaying(string name);

		// Token: 0x17000005 RID: 5
		public AnimationState this[string name]
		{
			get
			{
				return this.GetState(name);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002098 File Offset: 0x00000298
		[ExcludeFromDocs]
		public bool Play()
		{
			return this.Play(PlayMode.StopSameLayer);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000020B4 File Offset: 0x000002B4
		public bool Play([DefaultValue("PlayMode.StopSameLayer")] PlayMode mode)
		{
			return this.PlayDefaultAnimation(mode);
		}

		// Token: 0x06000026 RID: 38
		[NativeName("Play")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool PlayDefaultAnimation(PlayMode mode);

		// Token: 0x06000027 RID: 39 RVA: 0x000020D0 File Offset: 0x000002D0
		[ExcludeFromDocs]
		public bool Play(string animation)
		{
			return this.Play(animation, PlayMode.StopSameLayer);
		}

		// Token: 0x06000028 RID: 40
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Play(string animation, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x06000029 RID: 41 RVA: 0x000020EA File Offset: 0x000002EA
		[ExcludeFromDocs]
		public void CrossFade(string animation)
		{
			this.CrossFade(animation, 0.3f);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000020FA File Offset: 0x000002FA
		[ExcludeFromDocs]
		public void CrossFade(string animation, float fadeLength)
		{
			this.CrossFade(animation, fadeLength, PlayMode.StopSameLayer);
		}

		// Token: 0x0600002B RID: 43
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CrossFade(string animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x0600002C RID: 44 RVA: 0x00002107 File Offset: 0x00000307
		[ExcludeFromDocs]
		public void Blend(string animation)
		{
			this.Blend(animation, 1f);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002117 File Offset: 0x00000317
		[ExcludeFromDocs]
		public void Blend(string animation, float targetWeight)
		{
			this.Blend(animation, targetWeight, 0.3f);
		}

		// Token: 0x0600002E RID: 46
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Blend(string animation, [DefaultValue("1.0F")] float targetWeight, [DefaultValue("0.3F")] float fadeLength);

		// Token: 0x0600002F RID: 47 RVA: 0x00002128 File Offset: 0x00000328
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation)
		{
			return this.CrossFadeQueued(animation, 0.3f);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002148 File Offset: 0x00000348
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength)
		{
			return this.CrossFadeQueued(animation, fadeLength, QueueMode.CompleteOthers);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002164 File Offset: 0x00000364
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength, QueueMode queue)
		{
			return this.CrossFadeQueued(animation, fadeLength, queue, PlayMode.StopSameLayer);
		}

		// Token: 0x06000032 RID: 50
		[FreeFunction("AnimationBindings::CrossFadeQueuedImpl", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimationState CrossFadeQueued(string animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x06000033 RID: 51 RVA: 0x00002180 File Offset: 0x00000380
		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation)
		{
			return this.PlayQueued(animation, QueueMode.CompleteOthers);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000219C File Offset: 0x0000039C
		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation, QueueMode queue)
		{
			return this.PlayQueued(animation, queue, PlayMode.StopSameLayer);
		}

		// Token: 0x06000035 RID: 53
		[FreeFunction("AnimationBindings::PlayQueuedImpl", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimationState PlayQueued(string animation, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x06000036 RID: 54 RVA: 0x000021B7 File Offset: 0x000003B7
		public void AddClip(AnimationClip clip, string newName)
		{
			this.AddClip(clip, newName, int.MinValue, int.MaxValue);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000021CD File Offset: 0x000003CD
		[ExcludeFromDocs]
		public void AddClip(AnimationClip clip, string newName, int firstFrame, int lastFrame)
		{
			this.AddClip(clip, newName, firstFrame, lastFrame, false);
		}

		// Token: 0x06000038 RID: 56
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddClip([NotNull("NullExceptionObject")] AnimationClip clip, string newName, int firstFrame, int lastFrame, [DefaultValue("false")] bool addLoopFrame);

		// Token: 0x06000039 RID: 57
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveClip([NotNull("NullExceptionObject")] AnimationClip clip);

		// Token: 0x0600003A RID: 58 RVA: 0x000021DD File Offset: 0x000003DD
		public void RemoveClip(string clipName)
		{
			this.RemoveClipNamed(clipName);
		}

		// Token: 0x0600003B RID: 59
		[NativeName("RemoveClip")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RemoveClipNamed(string clipName);

		// Token: 0x0600003C RID: 60
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetClipCount();

		// Token: 0x0600003D RID: 61 RVA: 0x000021E8 File Offset: 0x000003E8
		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(AnimationPlayMode mode)
		{
			return this.PlayDefaultAnimation((PlayMode)mode);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002204 File Offset: 0x00000404
		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(string animation, AnimationPlayMode mode)
		{
			return this.Play(animation, (PlayMode)mode);
		}

		// Token: 0x0600003F RID: 63
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SyncLayer(int layer);

		// Token: 0x06000040 RID: 64 RVA: 0x00002220 File Offset: 0x00000420
		public IEnumerator GetEnumerator()
		{
			return new Animation.Enumerator(this);
		}

		// Token: 0x06000041 RID: 65
		[FreeFunction("AnimationBindings::GetState", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern AnimationState GetState(string name);

		// Token: 0x06000042 RID: 66
		[FreeFunction("AnimationBindings::GetStateAtIndex", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern AnimationState GetStateAtIndex(int index);

		// Token: 0x06000043 RID: 67
		[NativeName("GetAnimationStateCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetStateCount();

		// Token: 0x06000044 RID: 68 RVA: 0x00002238 File Offset: 0x00000438
		public AnimationClip GetClip(string name)
		{
			AnimationState state = this.GetState(name);
			bool flag = state;
			AnimationClip result;
			if (flag)
			{
				result = state.clip;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000045 RID: 69
		// (set) Token: 0x06000046 RID: 70
		public extern bool animatePhysics { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000047 RID: 71
		// (set) Token: 0x06000048 RID: 72
		[Obsolete("Use cullingType instead")]
		public extern bool animateOnlyIfVisible { [FreeFunction("AnimationBindings::GetAnimateOnlyIfVisible", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("AnimationBindings::SetAnimateOnlyIfVisible", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000049 RID: 73
		// (set) Token: 0x0600004A RID: 74
		public extern AnimationCullingType cullingType { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002268 File Offset: 0x00000468
		// (set) Token: 0x0600004C RID: 76 RVA: 0x0000227E File Offset: 0x0000047E
		public Bounds localBounds
		{
			[NativeName("GetLocalAABB")]
			get
			{
				Bounds result;
				this.get_localBounds_Injected(out result);
				return result;
			}
			[NativeName("SetLocalAABB")]
			set
			{
				this.set_localBounds_Injected(ref value);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002288 File Offset: 0x00000488
		public Animation()
		{
		}

		// Token: 0x0600004E RID: 78
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localBounds_Injected(out Bounds ret);

		// Token: 0x0600004F RID: 79
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_localBounds_Injected(ref Bounds value);

		// Token: 0x0200000E RID: 14
		private sealed class Enumerator : IEnumerator
		{
			// Token: 0x06000050 RID: 80 RVA: 0x00002291 File Offset: 0x00000491
			internal Enumerator(Animation outer)
			{
				this.m_Outer = outer;
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000051 RID: 81 RVA: 0x000022AC File Offset: 0x000004AC
			public object Current
			{
				get
				{
					return this.m_Outer.GetStateAtIndex(this.m_CurrentIndex);
				}
			}

			// Token: 0x06000052 RID: 82 RVA: 0x000022D0 File Offset: 0x000004D0
			public bool MoveNext()
			{
				int stateCount = this.m_Outer.GetStateCount();
				this.m_CurrentIndex++;
				return this.m_CurrentIndex < stateCount;
			}

			// Token: 0x06000053 RID: 83 RVA: 0x00002305 File Offset: 0x00000505
			public void Reset()
			{
				this.m_CurrentIndex = -1;
			}

			// Token: 0x04000017 RID: 23
			private Animation m_Outer;

			// Token: 0x04000018 RID: 24
			private int m_CurrentIndex = -1;
		}
	}
}

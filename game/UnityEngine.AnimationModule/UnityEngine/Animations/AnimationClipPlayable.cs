using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200004B RID: 75
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationClipPlayable.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationClipPlayable.h")]
	[StaticAccessor("AnimationClipPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	public struct AnimationClipPlayable : IPlayable, IEquatable<AnimationClipPlayable>
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x00004868 File Offset: 0x00002A68
		public static AnimationClipPlayable Create(PlayableGraph graph, AnimationClip clip)
		{
			PlayableHandle handle = AnimationClipPlayable.CreateHandle(graph, clip);
			return new AnimationClipPlayable(handle);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00004888 File Offset: 0x00002A88
		private static PlayableHandle CreateHandle(PlayableGraph graph, AnimationClip clip)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationClipPlayable.CreateHandleInternal(graph, clip, ref @null);
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

		// Token: 0x060002E3 RID: 739 RVA: 0x000048BC File Offset: 0x00002ABC
		internal AnimationClipPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationClipPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationClipPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000048F8 File Offset: 0x00002AF8
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00004910 File Offset: 0x00002B10
		public static implicit operator Playable(AnimationClipPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00004930 File Offset: 0x00002B30
		public static explicit operator AnimationClipPlayable(Playable playable)
		{
			return new AnimationClipPlayable(playable.GetHandle());
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00004950 File Offset: 0x00002B50
		public bool Equals(AnimationClipPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00004974 File Offset: 0x00002B74
		public AnimationClip GetAnimationClip()
		{
			return AnimationClipPlayable.GetAnimationClipInternal(ref this.m_Handle);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00004994 File Offset: 0x00002B94
		public bool GetApplyFootIK()
		{
			return AnimationClipPlayable.GetApplyFootIKInternal(ref this.m_Handle);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000049B1 File Offset: 0x00002BB1
		public void SetApplyFootIK(bool value)
		{
			AnimationClipPlayable.SetApplyFootIKInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000049C4 File Offset: 0x00002BC4
		public bool GetApplyPlayableIK()
		{
			return AnimationClipPlayable.GetApplyPlayableIKInternal(ref this.m_Handle);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000049E1 File Offset: 0x00002BE1
		public void SetApplyPlayableIK(bool value)
		{
			AnimationClipPlayable.SetApplyPlayableIKInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000049F4 File Offset: 0x00002BF4
		internal bool GetRemoveStartOffset()
		{
			return AnimationClipPlayable.GetRemoveStartOffsetInternal(ref this.m_Handle);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00004A11 File Offset: 0x00002C11
		internal void SetRemoveStartOffset(bool value)
		{
			AnimationClipPlayable.SetRemoveStartOffsetInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00004A24 File Offset: 0x00002C24
		internal bool GetOverrideLoopTime()
		{
			return AnimationClipPlayable.GetOverrideLoopTimeInternal(ref this.m_Handle);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00004A41 File Offset: 0x00002C41
		internal void SetOverrideLoopTime(bool value)
		{
			AnimationClipPlayable.SetOverrideLoopTimeInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00004A54 File Offset: 0x00002C54
		internal bool GetLoopTime()
		{
			return AnimationClipPlayable.GetLoopTimeInternal(ref this.m_Handle);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00004A71 File Offset: 0x00002C71
		internal void SetLoopTime(bool value)
		{
			AnimationClipPlayable.SetLoopTimeInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00004A84 File Offset: 0x00002C84
		internal float GetSampleRate()
		{
			return AnimationClipPlayable.GetSampleRateInternal(ref this.m_Handle);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00004AA1 File Offset: 0x00002CA1
		internal void SetSampleRate(float value)
		{
			AnimationClipPlayable.SetSampleRateInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00004AB1 File Offset: 0x00002CB1
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, AnimationClip clip, ref PlayableHandle handle)
		{
			return AnimationClipPlayable.CreateHandleInternal_Injected(ref graph, clip, ref handle);
		}

		// Token: 0x060002F6 RID: 758
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationClip GetAnimationClipInternal(ref PlayableHandle handle);

		// Token: 0x060002F7 RID: 759
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetApplyFootIKInternal(ref PlayableHandle handle);

		// Token: 0x060002F8 RID: 760
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetApplyFootIKInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060002F9 RID: 761
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetApplyPlayableIKInternal(ref PlayableHandle handle);

		// Token: 0x060002FA RID: 762
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetApplyPlayableIKInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060002FB RID: 763
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetRemoveStartOffsetInternal(ref PlayableHandle handle);

		// Token: 0x060002FC RID: 764
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRemoveStartOffsetInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060002FD RID: 765
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetOverrideLoopTimeInternal(ref PlayableHandle handle);

		// Token: 0x060002FE RID: 766
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetOverrideLoopTimeInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060002FF RID: 767
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetLoopTimeInternal(ref PlayableHandle handle);

		// Token: 0x06000300 RID: 768
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLoopTimeInternal(ref PlayableHandle handle, bool value);

		// Token: 0x06000301 RID: 769
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetSampleRateInternal(ref PlayableHandle handle);

		// Token: 0x06000302 RID: 770
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSampleRateInternal(ref PlayableHandle handle, float value);

		// Token: 0x06000303 RID: 771
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, AnimationClip clip, ref PlayableHandle handle);

		// Token: 0x04000146 RID: 326
		private PlayableHandle m_Handle;
	}
}

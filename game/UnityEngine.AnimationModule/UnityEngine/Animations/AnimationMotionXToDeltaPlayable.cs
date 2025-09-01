using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200004F RID: 79
	[StaticAccessor("AnimationMotionXToDeltaPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationMotionXToDeltaPlayable.bindings.h")]
	internal struct AnimationMotionXToDeltaPlayable : IPlayable, IEquatable<AnimationMotionXToDeltaPlayable>
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00005548 File Offset: 0x00003748
		public static AnimationMotionXToDeltaPlayable Null
		{
			get
			{
				return AnimationMotionXToDeltaPlayable.m_NullPlayable;
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00005560 File Offset: 0x00003760
		public static AnimationMotionXToDeltaPlayable Create(PlayableGraph graph)
		{
			PlayableHandle handle = AnimationMotionXToDeltaPlayable.CreateHandle(graph);
			return new AnimationMotionXToDeltaPlayable(handle);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00005580 File Offset: 0x00003780
		private static PlayableHandle CreateHandle(PlayableGraph graph)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationMotionXToDeltaPlayable.CreateHandleInternal(graph, ref @null);
			PlayableHandle result;
			if (flag)
			{
				result = PlayableHandle.Null;
			}
			else
			{
				@null.SetInputCount(1);
				result = @null;
			}
			return result;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000055BC File Offset: 0x000037BC
		private AnimationMotionXToDeltaPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationMotionXToDeltaPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationMotionXToDeltaPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000055F8 File Offset: 0x000037F8
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00005610 File Offset: 0x00003810
		public static implicit operator Playable(AnimationMotionXToDeltaPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00005630 File Offset: 0x00003830
		public static explicit operator AnimationMotionXToDeltaPlayable(Playable playable)
		{
			return new AnimationMotionXToDeltaPlayable(playable.GetHandle());
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00005650 File Offset: 0x00003850
		public bool Equals(AnimationMotionXToDeltaPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00005674 File Offset: 0x00003874
		public bool IsAbsoluteMotion()
		{
			return AnimationMotionXToDeltaPlayable.IsAbsoluteMotionInternal(ref this.m_Handle);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00005691 File Offset: 0x00003891
		public void SetAbsoluteMotion(bool value)
		{
			AnimationMotionXToDeltaPlayable.SetAbsoluteMotionInternal(ref this.m_Handle, value);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000056A1 File Offset: 0x000038A1
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationMotionXToDeltaPlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x060003A6 RID: 934
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAbsoluteMotionInternal(ref PlayableHandle handle);

		// Token: 0x060003A7 RID: 935
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAbsoluteMotionInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060003A8 RID: 936 RVA: 0x000056AB File Offset: 0x000038AB
		// Note: this type is marked as 'beforefieldinit'.
		static AnimationMotionXToDeltaPlayable()
		{
		}

		// Token: 0x060003A9 RID: 937
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x0400014C RID: 332
		private PlayableHandle m_Handle;

		// Token: 0x0400014D RID: 333
		private static readonly AnimationMotionXToDeltaPlayable m_NullPlayable = new AnimationMotionXToDeltaPlayable(PlayableHandle.Null);
	}
}

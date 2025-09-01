using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000055 RID: 85
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationRemoveScalePlayable.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("AnimationRemoveScalePlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/Director/AnimationRemoveScalePlayable.h")]
	[RequiredByNativeCode]
	internal struct AnimationRemoveScalePlayable : IPlayable, IEquatable<AnimationRemoveScalePlayable>
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00005BE4 File Offset: 0x00003DE4
		public static AnimationRemoveScalePlayable Null
		{
			get
			{
				return AnimationRemoveScalePlayable.m_NullPlayable;
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00005BFC File Offset: 0x00003DFC
		public static AnimationRemoveScalePlayable Create(PlayableGraph graph, int inputCount)
		{
			PlayableHandle handle = AnimationRemoveScalePlayable.CreateHandle(graph, inputCount);
			return new AnimationRemoveScalePlayable(handle);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00005C1C File Offset: 0x00003E1C
		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationRemoveScalePlayable.CreateHandleInternal(graph, ref @null);
			PlayableHandle result;
			if (flag)
			{
				result = PlayableHandle.Null;
			}
			else
			{
				@null.SetInputCount(inputCount);
				result = @null;
			}
			return result;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00005C58 File Offset: 0x00003E58
		internal AnimationRemoveScalePlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationRemoveScalePlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationRemoveScalePlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00005C94 File Offset: 0x00003E94
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00005CAC File Offset: 0x00003EAC
		public static implicit operator Playable(AnimationRemoveScalePlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00005CCC File Offset: 0x00003ECC
		public static explicit operator AnimationRemoveScalePlayable(Playable playable)
		{
			return new AnimationRemoveScalePlayable(playable.GetHandle());
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00005CEC File Offset: 0x00003EEC
		public bool Equals(AnimationRemoveScalePlayable other)
		{
			return this.Equals(other.GetHandle());
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00005D16 File Offset: 0x00003F16
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationRemoveScalePlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00005D20 File Offset: 0x00003F20
		// Note: this type is marked as 'beforefieldinit'.
		static AnimationRemoveScalePlayable()
		{
		}

		// Token: 0x060003F5 RID: 1013
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x04000153 RID: 339
		private PlayableHandle m_Handle;

		// Token: 0x04000154 RID: 340
		private static readonly AnimationRemoveScalePlayable m_NullPlayable = new AnimationRemoveScalePlayable(PlayableHandle.Null);
	}
}

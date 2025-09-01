using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200004D RID: 77
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Animation/Director/AnimationLayerMixerPlayable.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationLayerMixerPlayable.bindings.h")]
	[StaticAccessor("AnimationLayerMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	public struct AnimationLayerMixerPlayable : IPlayable, IEquatable<AnimationLayerMixerPlayable>
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00005138 File Offset: 0x00003338
		public static AnimationLayerMixerPlayable Null
		{
			get
			{
				return AnimationLayerMixerPlayable.m_NullPlayable;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00005150 File Offset: 0x00003350
		public static AnimationLayerMixerPlayable Create(PlayableGraph graph, int inputCount = 0)
		{
			return AnimationLayerMixerPlayable.Create(graph, inputCount, true);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000516C File Offset: 0x0000336C
		public static AnimationLayerMixerPlayable Create(PlayableGraph graph, int inputCount, bool singleLayerOptimization)
		{
			PlayableHandle handle = AnimationLayerMixerPlayable.CreateHandle(graph, inputCount);
			AnimationLayerMixerPlayable result = new AnimationLayerMixerPlayable(handle, singleLayerOptimization);
			return result;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00005190 File Offset: 0x00003390
		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationLayerMixerPlayable.CreateHandleInternal(graph, ref @null);
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

		// Token: 0x06000380 RID: 896 RVA: 0x000051CC File Offset: 0x000033CC
		internal AnimationLayerMixerPlayable(PlayableHandle handle, bool singleLayerOptimization = true)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationLayerMixerPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationLayerMixerPlayable.");
				}
				AnimationLayerMixerPlayable.SetSingleLayerOptimizationInternal(ref handle, singleLayerOptimization);
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00005210 File Offset: 0x00003410
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00005228 File Offset: 0x00003428
		public static implicit operator Playable(AnimationLayerMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00005248 File Offset: 0x00003448
		public static explicit operator AnimationLayerMixerPlayable(Playable playable)
		{
			return new AnimationLayerMixerPlayable(playable.GetHandle(), true);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00005268 File Offset: 0x00003468
		public bool Equals(AnimationLayerMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000528C File Offset: 0x0000348C
		public bool IsLayerAdditive(uint layerIndex)
		{
			bool flag = (ulong)layerIndex >= (ulong)((long)this.m_Handle.GetInputCount());
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layerIndex", string.Format("layerIndex {0} must be in the range of 0 to {1}.", layerIndex, this.m_Handle.GetInputCount() - 1));
			}
			return AnimationLayerMixerPlayable.IsLayerAdditiveInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000052F0 File Offset: 0x000034F0
		public void SetLayerAdditive(uint layerIndex, bool value)
		{
			bool flag = (ulong)layerIndex >= (ulong)((long)this.m_Handle.GetInputCount());
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layerIndex", string.Format("layerIndex {0} must be in the range of 0 to {1}.", layerIndex, this.m_Handle.GetInputCount() - 1));
			}
			AnimationLayerMixerPlayable.SetLayerAdditiveInternal(ref this.m_Handle, layerIndex, value);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00005350 File Offset: 0x00003550
		public void SetLayerMaskFromAvatarMask(uint layerIndex, AvatarMask mask)
		{
			bool flag = (ulong)layerIndex >= (ulong)((long)this.m_Handle.GetInputCount());
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layerIndex", string.Format("layerIndex {0} must be in the range of 0 to {1}.", layerIndex, this.m_Handle.GetInputCount() - 1));
			}
			bool flag2 = mask == null;
			if (flag2)
			{
				throw new ArgumentNullException("mask");
			}
			AnimationLayerMixerPlayable.SetLayerMaskFromAvatarMaskInternal(ref this.m_Handle, layerIndex, mask);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000053C6 File Offset: 0x000035C6
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationLayerMixerPlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x06000389 RID: 905
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsLayerAdditiveInternal(ref PlayableHandle handle, uint layerIndex);

		// Token: 0x0600038A RID: 906
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLayerAdditiveInternal(ref PlayableHandle handle, uint layerIndex, bool value);

		// Token: 0x0600038B RID: 907
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSingleLayerOptimizationInternal(ref PlayableHandle handle, bool value);

		// Token: 0x0600038C RID: 908
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLayerMaskFromAvatarMaskInternal(ref PlayableHandle handle, uint layerIndex, AvatarMask mask);

		// Token: 0x0600038D RID: 909 RVA: 0x000053D0 File Offset: 0x000035D0
		// Note: this type is marked as 'beforefieldinit'.
		static AnimationLayerMixerPlayable()
		{
		}

		// Token: 0x0600038E RID: 910
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x04000148 RID: 328
		private PlayableHandle m_Handle;

		// Token: 0x04000149 RID: 329
		private static readonly AnimationLayerMixerPlayable m_NullPlayable = new AnimationLayerMixerPlayable(PlayableHandle.Null, true);
	}
}

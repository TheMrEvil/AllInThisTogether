using System;
using System.Runtime.CompilerServices;
using UnityEngine.Animations;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Experimental.Animations
{
	// Token: 0x0200003E RID: 62
	[StaticAccessor("AnimationPlayableOutputExtensionsBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/AnimatorDefines.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableOutputExtensions.bindings.h")]
	public static class AnimationPlayableOutputExtensions
	{
		// Token: 0x0600028C RID: 652 RVA: 0x00004368 File Offset: 0x00002568
		public static AnimationStreamSource GetAnimationStreamSource(this AnimationPlayableOutput output)
		{
			return AnimationPlayableOutputExtensions.InternalGetAnimationStreamSource(output.GetHandle());
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00004386 File Offset: 0x00002586
		public static void SetAnimationStreamSource(this AnimationPlayableOutput output, AnimationStreamSource streamSource)
		{
			AnimationPlayableOutputExtensions.InternalSetAnimationStreamSource(output.GetHandle(), streamSource);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00004398 File Offset: 0x00002598
		public static ushort GetSortingOrder(this AnimationPlayableOutput output)
		{
			return (ushort)AnimationPlayableOutputExtensions.InternalGetSortingOrder(output.GetHandle());
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000043B7 File Offset: 0x000025B7
		public static void SetSortingOrder(this AnimationPlayableOutput output, ushort sortingOrder)
		{
			AnimationPlayableOutputExtensions.InternalSetSortingOrder(output.GetHandle(), (int)sortingOrder);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000043C8 File Offset: 0x000025C8
		[NativeThrows]
		private static AnimationStreamSource InternalGetAnimationStreamSource(PlayableOutputHandle output)
		{
			return AnimationPlayableOutputExtensions.InternalGetAnimationStreamSource_Injected(ref output);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000043D1 File Offset: 0x000025D1
		[NativeThrows]
		private static void InternalSetAnimationStreamSource(PlayableOutputHandle output, AnimationStreamSource streamSource)
		{
			AnimationPlayableOutputExtensions.InternalSetAnimationStreamSource_Injected(ref output, streamSource);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000043DB File Offset: 0x000025DB
		[NativeThrows]
		private static int InternalGetSortingOrder(PlayableOutputHandle output)
		{
			return AnimationPlayableOutputExtensions.InternalGetSortingOrder_Injected(ref output);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000043E4 File Offset: 0x000025E4
		[NativeThrows]
		private static void InternalSetSortingOrder(PlayableOutputHandle output, int sortingOrder)
		{
			AnimationPlayableOutputExtensions.InternalSetSortingOrder_Injected(ref output, sortingOrder);
		}

		// Token: 0x06000294 RID: 660
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationStreamSource InternalGetAnimationStreamSource_Injected(ref PlayableOutputHandle output);

		// Token: 0x06000295 RID: 661
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetAnimationStreamSource_Injected(ref PlayableOutputHandle output, AnimationStreamSource streamSource);

		// Token: 0x06000296 RID: 662
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalGetSortingOrder_Injected(ref PlayableOutputHandle output);

		// Token: 0x06000297 RID: 663
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetSortingOrder_Injected(ref PlayableOutputHandle output, int sortingOrder);
	}
}

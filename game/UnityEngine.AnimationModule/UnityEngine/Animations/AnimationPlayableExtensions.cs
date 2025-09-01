using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	// Token: 0x02000051 RID: 81
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Animation/Director/AnimationPlayableExtensions.h")]
	[NativeHeader("Modules/Animation/AnimationClip.h")]
	public static class AnimationPlayableExtensions
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x000058B4 File Offset: 0x00003AB4
		public static void SetAnimatedProperties<U>(this U playable, AnimationClip clip) where U : struct, IPlayable
		{
			PlayableHandle handle = playable.GetHandle();
			AnimationPlayableExtensions.SetAnimatedPropertiesInternal(ref handle, clip);
		}

		// Token: 0x060003C2 RID: 962
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetAnimatedPropertiesInternal(ref PlayableHandle playable, AnimationClip animatedProperties);
	}
}

using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000026 RID: 38
	[NativeHeader("Modules/Animation/OptimizeTransformHierarchy.h")]
	public class AnimatorUtility
	{
		// Token: 0x06000205 RID: 517
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void OptimizeTransformHierarchy([NotNull("NullExceptionObject")] GameObject go, string[] exposedTransforms);

		// Token: 0x06000206 RID: 518
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeoptimizeTransformHierarchy([NotNull("NullExceptionObject")] GameObject go);

		// Token: 0x06000207 RID: 519 RVA: 0x000037B9 File Offset: 0x000019B9
		public AnimatorUtility()
		{
		}
	}
}

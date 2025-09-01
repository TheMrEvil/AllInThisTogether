using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200003C RID: 60
	[ExcludeFromObjectFactory]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/RuntimeAnimatorController.h")]
	public class RuntimeAnimatorController : Object
	{
		// Token: 0x0600028A RID: 650 RVA: 0x000039EB File Offset: 0x00001BEB
		protected RuntimeAnimatorController()
		{
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600028B RID: 651
		public extern AnimationClip[] animationClips { [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}

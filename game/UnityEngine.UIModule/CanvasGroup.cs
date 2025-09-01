using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/UI/CanvasGroup.h")]
	[NativeClass("UI::CanvasGroup")]
	public sealed class CanvasGroup : Behaviour, ICanvasRaycastFilter
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2
		// (set) Token: 0x06000003 RID: 3
		[NativeProperty("Alpha", false, TargetType.Function)]
		public extern float alpha { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4
		// (set) Token: 0x06000005 RID: 5
		[NativeProperty("Interactable", false, TargetType.Function)]
		public extern bool interactable { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6
		// (set) Token: 0x06000007 RID: 7
		[NativeProperty("BlocksRaycasts", false, TargetType.Function)]
		public extern bool blocksRaycasts { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8
		// (set) Token: 0x06000009 RID: 9
		[NativeProperty("IgnoreParentGroups", false, TargetType.Function)]
		public extern bool ignoreParentGroups { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600000A RID: 10 RVA: 0x00002050 File Offset: 0x00000250
		public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return this.blocksRaycasts;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002068 File Offset: 0x00000268
		public CanvasGroup()
		{
		}
	}
}

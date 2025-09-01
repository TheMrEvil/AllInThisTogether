using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000F RID: 15
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/AnimationState.h")]
	public sealed class AnimationState : TrackedReference
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000054 RID: 84
		// (set) Token: 0x06000055 RID: 85
		public extern bool enabled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000056 RID: 86
		// (set) Token: 0x06000057 RID: 87
		public extern float weight { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000058 RID: 88
		// (set) Token: 0x06000059 RID: 89
		public extern WrapMode wrapMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005A RID: 90
		// (set) Token: 0x0600005B RID: 91
		public extern float time { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92
		// (set) Token: 0x0600005D RID: 93
		public extern float normalizedTime { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005E RID: 94
		// (set) Token: 0x0600005F RID: 95
		public extern float speed { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000060 RID: 96
		// (set) Token: 0x06000061 RID: 97
		public extern float normalizedSpeed { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000062 RID: 98
		public extern float length { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000063 RID: 99
		// (set) Token: 0x06000064 RID: 100
		public extern int layer { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000065 RID: 101
		public extern AnimationClip clip { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000066 RID: 102
		// (set) Token: 0x06000067 RID: 103
		public extern string name { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000068 RID: 104
		// (set) Token: 0x06000069 RID: 105
		public extern AnimationBlendMode blendMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600006A RID: 106 RVA: 0x0000230F File Offset: 0x0000050F
		[ExcludeFromDocs]
		public void AddMixingTransform(Transform mix)
		{
			this.AddMixingTransform(mix, true);
		}

		// Token: 0x0600006B RID: 107
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddMixingTransform([NotNull("NullExceptionObject")] Transform mix, [DefaultValue("true")] bool recursive);

		// Token: 0x0600006C RID: 108
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveMixingTransform([NotNull("NullExceptionObject")] Transform mix);

		// Token: 0x0600006D RID: 109 RVA: 0x0000231B File Offset: 0x0000051B
		public AnimationState()
		{
		}
	}
}

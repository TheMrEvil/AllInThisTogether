using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	// Token: 0x02000037 RID: 55
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	[MovedFrom(true, "UnityEditor.Animations", "UnityEditor", null)]
	[NativeHeader("Modules/Animation/AvatarMask.h")]
	public sealed class AvatarMask : Object
	{
		// Token: 0x06000245 RID: 581 RVA: 0x00003DBC File Offset: 0x00001FBC
		public AvatarMask()
		{
			AvatarMask.Internal_Create(this);
		}

		// Token: 0x06000246 RID: 582
		[FreeFunction("AnimationBindings::CreateAvatarMask")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] AvatarMask self);

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00003DD0 File Offset: 0x00001FD0
		[Obsolete("AvatarMask.humanoidBodyPartCount is deprecated, use AvatarMaskBodyPart.LastBodyPart instead.")]
		public int humanoidBodyPartCount
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x06000248 RID: 584
		[NativeMethod("GetBodyPart")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetHumanoidBodyPartActive(AvatarMaskBodyPart index);

		// Token: 0x06000249 RID: 585
		[NativeMethod("SetBodyPart")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetHumanoidBodyPartActive(AvatarMaskBodyPart index, bool value);

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600024A RID: 586
		// (set) Token: 0x0600024B RID: 587
		public extern int transformCount { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600024C RID: 588 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public void AddTransformPath(Transform transform)
		{
			this.AddTransformPath(transform, true);
		}

		// Token: 0x0600024D RID: 589
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddTransformPath([NotNull("ArgumentNullException")] Transform transform, [DefaultValue("true")] bool recursive);

		// Token: 0x0600024E RID: 590 RVA: 0x00003DF0 File Offset: 0x00001FF0
		public void RemoveTransformPath(Transform transform)
		{
			this.RemoveTransformPath(transform, true);
		}

		// Token: 0x0600024F RID: 591
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveTransformPath([NotNull("ArgumentNullException")] Transform transform, [DefaultValue("true")] bool recursive);

		// Token: 0x06000250 RID: 592
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetTransformPath(int index);

		// Token: 0x06000251 RID: 593
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTransformPath(int index, string path);

		// Token: 0x06000252 RID: 594
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetTransformWeight(int index);

		// Token: 0x06000253 RID: 595
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTransformWeight(int index, float weight);

		// Token: 0x06000254 RID: 596 RVA: 0x00003DFC File Offset: 0x00001FFC
		public bool GetTransformActive(int index)
		{
			return this.GetTransformWeight(index) > 0.5f;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00003E1C File Offset: 0x0000201C
		public void SetTransformActive(int index, bool value)
		{
			this.SetTransformWeight(index, value ? 1f : 0f);
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000256 RID: 598
		internal extern bool hasFeetIK { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000257 RID: 599 RVA: 0x00003E38 File Offset: 0x00002038
		internal void Copy(AvatarMask other)
		{
			for (AvatarMaskBodyPart avatarMaskBodyPart = AvatarMaskBodyPart.Root; avatarMaskBodyPart < AvatarMaskBodyPart.LastBodyPart; avatarMaskBodyPart++)
			{
				this.SetHumanoidBodyPartActive(avatarMaskBodyPart, other.GetHumanoidBodyPartActive(avatarMaskBodyPart));
			}
			this.transformCount = other.transformCount;
			for (int i = 0; i < other.transformCount; i++)
			{
				this.SetTransformPath(i, other.GetTransformPath(i));
				this.SetTransformActive(i, other.GetTransformActive(i));
			}
		}
	}
}

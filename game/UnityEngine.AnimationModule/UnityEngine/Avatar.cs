using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000030 RID: 48
	[NativeHeader("Modules/Animation/Avatar.h")]
	[UsedByNativeCode]
	public class Avatar : Object
	{
		// Token: 0x06000208 RID: 520 RVA: 0x000039EB File Offset: 0x00001BEB
		private Avatar()
		{
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000209 RID: 521
		public extern bool isValid { [NativeMethod("IsValid")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600020A RID: 522
		public extern bool isHuman { [NativeMethod("IsHuman")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000039F8 File Offset: 0x00001BF8
		public HumanDescription humanDescription
		{
			get
			{
				HumanDescription result;
				this.get_humanDescription_Injected(out result);
				return result;
			}
		}

		// Token: 0x0600020C RID: 524
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetMuscleMinMax(int muscleId, float min, float max);

		// Token: 0x0600020D RID: 525
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetParameter(int parameterId, float value);

		// Token: 0x0600020E RID: 526 RVA: 0x00003A10 File Offset: 0x00001C10
		internal float GetAxisLength(int humanId)
		{
			return this.Internal_GetAxisLength(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00003A30 File Offset: 0x00001C30
		internal Quaternion GetPreRotation(int humanId)
		{
			return this.Internal_GetPreRotation(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00003A50 File Offset: 0x00001C50
		internal Quaternion GetPostRotation(int humanId)
		{
			return this.Internal_GetPostRotation(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00003A70 File Offset: 0x00001C70
		internal Quaternion GetZYPostQ(int humanId, Quaternion parentQ, Quaternion q)
		{
			return this.Internal_GetZYPostQ(HumanTrait.GetBoneIndexFromMono(humanId), parentQ, q);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00003A90 File Offset: 0x00001C90
		internal Quaternion GetZYRoll(int humanId, Vector3 uvw)
		{
			return this.Internal_GetZYRoll(HumanTrait.GetBoneIndexFromMono(humanId), uvw);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00003AB0 File Offset: 0x00001CB0
		internal Vector3 GetLimitSign(int humanId)
		{
			return this.Internal_GetLimitSign(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x06000214 RID: 532
		[NativeMethod("GetAxisLength")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern float Internal_GetAxisLength(int humanId);

		// Token: 0x06000215 RID: 533 RVA: 0x00003AD0 File Offset: 0x00001CD0
		[NativeMethod("GetPreRotation")]
		internal Quaternion Internal_GetPreRotation(int humanId)
		{
			Quaternion result;
			this.Internal_GetPreRotation_Injected(humanId, out result);
			return result;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00003AE8 File Offset: 0x00001CE8
		[NativeMethod("GetPostRotation")]
		internal Quaternion Internal_GetPostRotation(int humanId)
		{
			Quaternion result;
			this.Internal_GetPostRotation_Injected(humanId, out result);
			return result;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00003B00 File Offset: 0x00001D00
		[NativeMethod("GetZYPostQ")]
		internal Quaternion Internal_GetZYPostQ(int humanId, Quaternion parentQ, Quaternion q)
		{
			Quaternion result;
			this.Internal_GetZYPostQ_Injected(humanId, ref parentQ, ref q, out result);
			return result;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00003B1C File Offset: 0x00001D1C
		[NativeMethod("GetZYRoll")]
		internal Quaternion Internal_GetZYRoll(int humanId, Vector3 uvw)
		{
			Quaternion result;
			this.Internal_GetZYRoll_Injected(humanId, ref uvw, out result);
			return result;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00003B38 File Offset: 0x00001D38
		[NativeMethod("GetLimitSign")]
		internal Vector3 Internal_GetLimitSign(int humanId)
		{
			Vector3 result;
			this.Internal_GetLimitSign_Injected(humanId, out result);
			return result;
		}

		// Token: 0x0600021A RID: 538
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_humanDescription_Injected(out HumanDescription ret);

		// Token: 0x0600021B RID: 539
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetPreRotation_Injected(int humanId, out Quaternion ret);

		// Token: 0x0600021C RID: 540
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetPostRotation_Injected(int humanId, out Quaternion ret);

		// Token: 0x0600021D RID: 541
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetZYPostQ_Injected(int humanId, ref Quaternion parentQ, ref Quaternion q, out Quaternion ret);

		// Token: 0x0600021E RID: 542
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetZYRoll_Injected(int humanId, ref Vector3 uvw, out Quaternion ret);

		// Token: 0x0600021F RID: 543
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetLimitSign_Injected(int humanId, out Vector3 ret);
	}
}

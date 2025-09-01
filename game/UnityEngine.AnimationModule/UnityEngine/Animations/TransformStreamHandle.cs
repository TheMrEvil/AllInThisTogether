using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x0200005A RID: 90
	[NativeHeader("Modules/Animation/Director/AnimationStreamHandles.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public struct TransformStreamHandle
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x000062DC File Offset: 0x000044DC
		public bool IsValid(AnimationStream stream)
		{
			return this.IsValidInternal(ref stream);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000062F8 File Offset: 0x000044F8
		private bool IsValidInternal(ref AnimationStream stream)
		{
			return stream.isValid && this.createdByNative && this.hasHandleIndex;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00006324 File Offset: 0x00004524
		private bool createdByNative
		{
			get
			{
				return this.animatorBindingsVersion > 0U;
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00006340 File Offset: 0x00004540
		private bool IsSameVersionAsStream(ref AnimationStream stream)
		{
			return this.animatorBindingsVersion == stream.animatorBindingsVersion;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00006360 File Offset: 0x00004560
		private bool hasHandleIndex
		{
			get
			{
				return this.handleIndex != -1;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00006380 File Offset: 0x00004580
		private bool hasSkeletonIndex
		{
			get
			{
				return this.skeletonIndex != -1;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000063A8 File Offset: 0x000045A8
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0000639E File Offset: 0x0000459E
		internal uint animatorBindingsVersion
		{
			get
			{
				return this.m_AnimatorBindingsVersion;
			}
			private set
			{
				this.m_AnimatorBindingsVersion = value;
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000063C0 File Offset: 0x000045C0
		public void Resolve(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000063CC File Offset: 0x000045CC
		public bool IsResolved(AnimationStream stream)
		{
			return this.IsResolvedInternal(ref stream);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000063E8 File Offset: 0x000045E8
		private bool IsResolvedInternal(ref AnimationStream stream)
		{
			return this.IsValidInternal(ref stream) && this.IsSameVersionAsStream(ref stream) && this.hasSkeletonIndex;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00006418 File Offset: 0x00004618
		private void CheckIsValidAndResolve(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			bool flag = this.IsResolvedInternal(ref stream);
			if (!flag)
			{
				bool flag2 = !this.createdByNative || !this.hasHandleIndex;
				if (flag2)
				{
					throw new InvalidOperationException("The TransformStreamHandle is invalid. Please use proper function to create the handle.");
				}
				bool flag3 = !this.IsSameVersionAsStream(ref stream) || (this.hasHandleIndex && !this.hasSkeletonIndex);
				if (flag3)
				{
					this.ResolveInternal(ref stream);
				}
				bool flag4 = this.hasHandleIndex && !this.hasSkeletonIndex;
				if (flag4)
				{
					throw new InvalidOperationException("The TransformStreamHandle cannot be resolved.");
				}
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000064B0 File Offset: 0x000046B0
		public Vector3 GetPosition(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetPositionInternal(ref stream);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000064D3 File Offset: 0x000046D3
		public void SetPosition(AnimationStream stream, Vector3 position)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetPositionInternal(ref stream, position);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000064EC File Offset: 0x000046EC
		public Quaternion GetRotation(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetRotationInternal(ref stream);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000650F File Offset: 0x0000470F
		public void SetRotation(AnimationStream stream, Quaternion rotation)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetRotationInternal(ref stream, rotation);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00006528 File Offset: 0x00004728
		public Vector3 GetLocalPosition(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetLocalPositionInternal(ref stream);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000654B File Offset: 0x0000474B
		public void SetLocalPosition(AnimationStream stream, Vector3 position)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalPositionInternal(ref stream, position);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00006564 File Offset: 0x00004764
		public Quaternion GetLocalRotation(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetLocalRotationInternal(ref stream);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00006587 File Offset: 0x00004787
		public void SetLocalRotation(AnimationStream stream, Quaternion rotation)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalRotationInternal(ref stream, rotation);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000065A0 File Offset: 0x000047A0
		public Vector3 GetLocalScale(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetLocalScaleInternal(ref stream);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000065C3 File Offset: 0x000047C3
		public void SetLocalScale(AnimationStream stream, Vector3 scale)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalScaleInternal(ref stream, scale);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000065DC File Offset: 0x000047DC
		public bool GetPositionReadMask(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetPositionReadMaskInternal(ref stream);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00006600 File Offset: 0x00004800
		public bool GetRotationReadMask(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetRotationReadMaskInternal(ref stream);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00006624 File Offset: 0x00004824
		public bool GetScaleReadMask(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetScaleReadMaskInternal(ref stream);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00006647 File Offset: 0x00004847
		public void GetLocalTRS(AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.GetLocalTRSInternal(ref stream, out position, out rotation, out scale);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00006660 File Offset: 0x00004860
		public void SetLocalTRS(AnimationStream stream, Vector3 position, Quaternion rotation, Vector3 scale, bool useMask)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalTRSInternal(ref stream, position, rotation, scale, useMask);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000667B File Offset: 0x0000487B
		public void GetGlobalTR(AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.GetGlobalTRInternal(ref stream, out position, out rotation);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00006692 File Offset: 0x00004892
		public void SetGlobalTR(AnimationStream stream, Vector3 position, Quaternion rotation, bool useMask)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetGlobalTRInternal(ref stream, position, rotation, useMask);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000066AB File Offset: 0x000048AB
		[NativeMethod(Name = "Resolve", IsThreadSafe = true)]
		private void ResolveInternal(ref AnimationStream stream)
		{
			TransformStreamHandle.ResolveInternal_Injected(ref this, ref stream);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000066B4 File Offset: 0x000048B4
		[NativeMethod(Name = "TransformStreamHandleBindings::GetPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Vector3 GetPositionInternal(ref AnimationStream stream)
		{
			Vector3 result;
			TransformStreamHandle.GetPositionInternal_Injected(ref this, ref stream, out result);
			return result;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000066CB File Offset: 0x000048CB
		[NativeMethod(Name = "TransformStreamHandleBindings::SetPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetPositionInternal(ref AnimationStream stream, Vector3 position)
		{
			TransformStreamHandle.SetPositionInternal_Injected(ref this, ref stream, ref position);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000066D8 File Offset: 0x000048D8
		[NativeMethod(Name = "TransformStreamHandleBindings::GetRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Quaternion GetRotationInternal(ref AnimationStream stream)
		{
			Quaternion result;
			TransformStreamHandle.GetRotationInternal_Injected(ref this, ref stream, out result);
			return result;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000066EF File Offset: 0x000048EF
		[NativeMethod(Name = "TransformStreamHandleBindings::SetRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetRotationInternal(ref AnimationStream stream, Quaternion rotation)
		{
			TransformStreamHandle.SetRotationInternal_Injected(ref this, ref stream, ref rotation);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000066FC File Offset: 0x000048FC
		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Vector3 GetLocalPositionInternal(ref AnimationStream stream)
		{
			Vector3 result;
			TransformStreamHandle.GetLocalPositionInternal_Injected(ref this, ref stream, out result);
			return result;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00006713 File Offset: 0x00004913
		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalPositionInternal(ref AnimationStream stream, Vector3 position)
		{
			TransformStreamHandle.SetLocalPositionInternal_Injected(ref this, ref stream, ref position);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00006720 File Offset: 0x00004920
		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Quaternion GetLocalRotationInternal(ref AnimationStream stream)
		{
			Quaternion result;
			TransformStreamHandle.GetLocalRotationInternal_Injected(ref this, ref stream, out result);
			return result;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00006737 File Offset: 0x00004937
		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalRotationInternal(ref AnimationStream stream, Quaternion rotation)
		{
			TransformStreamHandle.SetLocalRotationInternal_Injected(ref this, ref stream, ref rotation);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00006744 File Offset: 0x00004944
		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalScaleInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Vector3 GetLocalScaleInternal(ref AnimationStream stream)
		{
			Vector3 result;
			TransformStreamHandle.GetLocalScaleInternal_Injected(ref this, ref stream, out result);
			return result;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000675B File Offset: 0x0000495B
		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalScaleInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalScaleInternal(ref AnimationStream stream, Vector3 scale)
		{
			TransformStreamHandle.SetLocalScaleInternal_Injected(ref this, ref stream, ref scale);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00006766 File Offset: 0x00004966
		[NativeMethod(Name = "TransformStreamHandleBindings::GetPositionReadMaskInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private bool GetPositionReadMaskInternal(ref AnimationStream stream)
		{
			return TransformStreamHandle.GetPositionReadMaskInternal_Injected(ref this, ref stream);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000676F File Offset: 0x0000496F
		[NativeMethod(Name = "TransformStreamHandleBindings::GetRotationReadMaskInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private bool GetRotationReadMaskInternal(ref AnimationStream stream)
		{
			return TransformStreamHandle.GetRotationReadMaskInternal_Injected(ref this, ref stream);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00006778 File Offset: 0x00004978
		[NativeMethod(Name = "TransformStreamHandleBindings::GetScaleReadMaskInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private bool GetScaleReadMaskInternal(ref AnimationStream stream)
		{
			return TransformStreamHandle.GetScaleReadMaskInternal_Injected(ref this, ref stream);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00006781 File Offset: 0x00004981
		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalTRSInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void GetLocalTRSInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			TransformStreamHandle.GetLocalTRSInternal_Injected(ref this, ref stream, out position, out rotation, out scale);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000678E File Offset: 0x0000498E
		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalTRSInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalTRSInternal(ref AnimationStream stream, Vector3 position, Quaternion rotation, Vector3 scale, bool useMask)
		{
			TransformStreamHandle.SetLocalTRSInternal_Injected(ref this, ref stream, ref position, ref rotation, ref scale, useMask);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000679F File Offset: 0x0000499F
		[NativeMethod(Name = "TransformStreamHandleBindings::GetGlobalTRInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void GetGlobalTRInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			TransformStreamHandle.GetGlobalTRInternal_Injected(ref this, ref stream, out position, out rotation);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000067AA File Offset: 0x000049AA
		[NativeMethod(Name = "TransformStreamHandleBindings::SetGlobalTRInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetGlobalTRInternal(ref AnimationStream stream, Vector3 position, Quaternion rotation, bool useMask)
		{
			TransformStreamHandle.SetGlobalTRInternal_Injected(ref this, ref stream, ref position, ref rotation, useMask);
		}

		// Token: 0x06000469 RID: 1129
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		// Token: 0x0600046A RID: 1130
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x0600046B RID: 1131
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position);

		// Token: 0x0600046C RID: 1132
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		// Token: 0x0600046D RID: 1133
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Quaternion rotation);

		// Token: 0x0600046E RID: 1134
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x0600046F RID: 1135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position);

		// Token: 0x06000470 RID: 1136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		// Token: 0x06000471 RID: 1137
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Quaternion rotation);

		// Token: 0x06000472 RID: 1138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalScaleInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x06000473 RID: 1139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalScaleInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 scale);

		// Token: 0x06000474 RID: 1140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetPositionReadMaskInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		// Token: 0x06000475 RID: 1141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetRotationReadMaskInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		// Token: 0x06000476 RID: 1142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetScaleReadMaskInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		// Token: 0x06000477 RID: 1143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalTRSInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale);

		// Token: 0x06000478 RID: 1144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalTRSInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position, ref Quaternion rotation, ref Vector3 scale, bool useMask);

		// Token: 0x06000479 RID: 1145
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalTRInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 position, out Quaternion rotation);

		// Token: 0x0600047A RID: 1146
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalTRInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position, ref Quaternion rotation, bool useMask);

		// Token: 0x0400016B RID: 363
		private uint m_AnimatorBindingsVersion;

		// Token: 0x0400016C RID: 364
		private int handleIndex;

		// Token: 0x0400016D RID: 365
		private int skeletonIndex;
	}
}

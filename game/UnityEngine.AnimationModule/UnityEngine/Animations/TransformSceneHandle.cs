using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x0200005C RID: 92
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationSceneHandles.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public struct TransformSceneHandle
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x00006BEC File Offset: 0x00004DEC
		public bool IsValid(AnimationStream stream)
		{
			return stream.isValid && this.createdByNative && this.hasTransformSceneHandleDefinitionIndex && this.HasValidTransform(ref stream);
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00006C24 File Offset: 0x00004E24
		private bool createdByNative
		{
			get
			{
				return this.valid > 0U;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00006C40 File Offset: 0x00004E40
		private bool hasTransformSceneHandleDefinitionIndex
		{
			get
			{
				return this.transformSceneHandleDefinitionIndex != -1;
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00006C60 File Offset: 0x00004E60
		private void CheckIsValid(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			bool flag = !this.createdByNative || !this.hasTransformSceneHandleDefinitionIndex;
			if (flag)
			{
				throw new InvalidOperationException("The TransformSceneHandle is invalid. Please use proper function to create the handle.");
			}
			bool flag2 = !this.HasValidTransform(ref stream);
			if (flag2)
			{
				throw new NullReferenceException("The transform is invalid.");
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00006CB4 File Offset: 0x00004EB4
		public Vector3 GetPosition(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetPositionInternal(ref stream);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetPosition(AnimationStream stream, Vector3 position)
		{
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00006CD8 File Offset: 0x00004ED8
		public Vector3 GetLocalPosition(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetLocalPositionInternal(ref stream);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetLocalPosition(AnimationStream stream, Vector3 position)
		{
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00006CFC File Offset: 0x00004EFC
		public Quaternion GetRotation(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetRotationInternal(ref stream);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetRotation(AnimationStream stream, Quaternion rotation)
		{
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00006D20 File Offset: 0x00004F20
		public Quaternion GetLocalRotation(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetLocalRotationInternal(ref stream);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetLocalRotation(AnimationStream stream, Quaternion rotation)
		{
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00006D44 File Offset: 0x00004F44
		public Vector3 GetLocalScale(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetLocalScaleInternal(ref stream);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00006D67 File Offset: 0x00004F67
		public void GetLocalTRS(AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			this.CheckIsValid(ref stream);
			this.GetLocalTRSInternal(ref stream, out position, out rotation, out scale);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00006D80 File Offset: 0x00004F80
		public void GetGlobalTR(AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			this.CheckIsValid(ref stream);
			this.GetGlobalTRInternal(ref stream, out position, out rotation);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetLocalScale(AnimationStream stream, Vector3 scale)
		{
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00006D97 File Offset: 0x00004F97
		[ThreadSafe]
		private bool HasValidTransform(ref AnimationStream stream)
		{
			return TransformSceneHandle.HasValidTransform_Injected(ref this, ref stream);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00006DA0 File Offset: 0x00004FA0
		[NativeMethod(Name = "TransformSceneHandleBindings::GetPositionInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetPositionInternal(ref AnimationStream stream)
		{
			Vector3 result;
			TransformSceneHandle.GetPositionInternal_Injected(ref this, ref stream, out result);
			return result;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00006DB8 File Offset: 0x00004FB8
		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalPositionInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetLocalPositionInternal(ref AnimationStream stream)
		{
			Vector3 result;
			TransformSceneHandle.GetLocalPositionInternal_Injected(ref this, ref stream, out result);
			return result;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00006DD0 File Offset: 0x00004FD0
		[NativeMethod(Name = "TransformSceneHandleBindings::GetRotationInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion GetRotationInternal(ref AnimationStream stream)
		{
			Quaternion result;
			TransformSceneHandle.GetRotationInternal_Injected(ref this, ref stream, out result);
			return result;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00006DE8 File Offset: 0x00004FE8
		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalRotationInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion GetLocalRotationInternal(ref AnimationStream stream)
		{
			Quaternion result;
			TransformSceneHandle.GetLocalRotationInternal_Injected(ref this, ref stream, out result);
			return result;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00006E00 File Offset: 0x00005000
		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalScaleInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetLocalScaleInternal(ref AnimationStream stream)
		{
			Vector3 result;
			TransformSceneHandle.GetLocalScaleInternal_Injected(ref this, ref stream, out result);
			return result;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00006E17 File Offset: 0x00005017
		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalTRSInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void GetLocalTRSInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			TransformSceneHandle.GetLocalTRSInternal_Injected(ref this, ref stream, out position, out rotation, out scale);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00006E24 File Offset: 0x00005024
		[NativeMethod(Name = "TransformSceneHandleBindings::GetGlobalTRInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void GetGlobalTRInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			TransformSceneHandle.GetGlobalTRInternal_Injected(ref this, ref stream, out position, out rotation);
		}

		// Token: 0x060004B7 RID: 1207
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasValidTransform_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream);

		// Token: 0x060004B8 RID: 1208
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPositionInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x060004B9 RID: 1209
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPositionInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x060004BA RID: 1210
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotationInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		// Token: 0x060004BB RID: 1211
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalRotationInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		// Token: 0x060004BC RID: 1212
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalScaleInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x060004BD RID: 1213
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalTRSInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale);

		// Token: 0x060004BE RID: 1214
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalTRInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 position, out Quaternion rotation);

		// Token: 0x04000172 RID: 370
		private uint valid;

		// Token: 0x04000173 RID: 371
		private int transformSceneHandleDefinitionIndex;
	}
}

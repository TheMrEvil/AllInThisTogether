using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x0200005D RID: 93
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/Director/AnimationSceneHandles.h")]
	public struct PropertySceneHandle
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x00006E30 File Offset: 0x00005030
		public bool IsValid(AnimationStream stream)
		{
			return this.IsValidInternal(ref stream);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00006E4C File Offset: 0x0000504C
		private bool IsValidInternal(ref AnimationStream stream)
		{
			return stream.isValid && this.createdByNative && this.hasHandleIndex && this.HasValidTransform(ref stream);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00006E80 File Offset: 0x00005080
		private bool createdByNative
		{
			get
			{
				return this.valid > 0U;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00006E9C File Offset: 0x0000509C
		private bool hasHandleIndex
		{
			get
			{
				return this.handleIndex != -1;
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00006EBA File Offset: 0x000050BA
		public void Resolve(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			this.ResolveInternal(ref stream);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00006ED0 File Offset: 0x000050D0
		public bool IsResolved(AnimationStream stream)
		{
			return this.IsValidInternal(ref stream) && this.IsBound(ref stream);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00006EF8 File Offset: 0x000050F8
		private void CheckIsValid(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			bool flag = !this.createdByNative || !this.hasHandleIndex;
			if (flag)
			{
				throw new InvalidOperationException("The PropertySceneHandle is invalid. Please use proper function to create the handle.");
			}
			bool flag2 = !this.HasValidTransform(ref stream);
			if (flag2)
			{
				throw new NullReferenceException("The transform is invalid.");
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00006F4C File Offset: 0x0000514C
		public float GetFloat(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetFloatInternal(ref stream);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetFloat(AnimationStream stream, float value)
		{
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00006F70 File Offset: 0x00005170
		public int GetInt(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetIntInternal(ref stream);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetInt(AnimationStream stream, int value)
		{
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00006F94 File Offset: 0x00005194
		public bool GetBool(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetBoolInternal(ref stream);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetBool(AnimationStream stream, bool value)
		{
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00006FB7 File Offset: 0x000051B7
		[ThreadSafe]
		private bool HasValidTransform(ref AnimationStream stream)
		{
			return PropertySceneHandle.HasValidTransform_Injected(ref this, ref stream);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00006FC0 File Offset: 0x000051C0
		[ThreadSafe]
		private bool IsBound(ref AnimationStream stream)
		{
			return PropertySceneHandle.IsBound_Injected(ref this, ref stream);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00006FC9 File Offset: 0x000051C9
		[NativeMethod(Name = "Resolve", IsThreadSafe = true)]
		private void ResolveInternal(ref AnimationStream stream)
		{
			PropertySceneHandle.ResolveInternal_Injected(ref this, ref stream);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00006FD2 File Offset: 0x000051D2
		[NativeMethod(Name = "GetFloat", IsThreadSafe = true)]
		private float GetFloatInternal(ref AnimationStream stream)
		{
			return PropertySceneHandle.GetFloatInternal_Injected(ref this, ref stream);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00006FDB File Offset: 0x000051DB
		[NativeMethod(Name = "GetInt", IsThreadSafe = true)]
		private int GetIntInternal(ref AnimationStream stream)
		{
			return PropertySceneHandle.GetIntInternal_Injected(ref this, ref stream);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00006FE4 File Offset: 0x000051E4
		[NativeMethod(Name = "GetBool", IsThreadSafe = true)]
		private bool GetBoolInternal(ref AnimationStream stream)
		{
			return PropertySceneHandle.GetBoolInternal_Injected(ref this, ref stream);
		}

		// Token: 0x060004D2 RID: 1234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasValidTransform_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		// Token: 0x060004D3 RID: 1235
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsBound_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		// Token: 0x060004D4 RID: 1236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		// Token: 0x060004D5 RID: 1237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloatInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		// Token: 0x060004D6 RID: 1238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIntInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		// Token: 0x060004D7 RID: 1239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBoolInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		// Token: 0x04000174 RID: 372
		private uint valid;

		// Token: 0x04000175 RID: 373
		private int handleIndex;
	}
}

using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000058 RID: 88
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/Director/AnimationStream.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStream.bindings.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public struct AnimationStream
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00005F8C File Offset: 0x0000418C
		internal uint animatorBindingsVersion
		{
			get
			{
				return this.m_AnimatorBindingsVersion;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00005FA4 File Offset: 0x000041A4
		public bool isValid
		{
			get
			{
				return this.m_AnimatorBindingsVersion >= 2U && this.constant != IntPtr.Zero && this.input != IntPtr.Zero && this.output != IntPtr.Zero && this.workspace != IntPtr.Zero && this.animationHandleBinder != IntPtr.Zero;
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000601C File Offset: 0x0000421C
		internal void CheckIsValid()
		{
			bool flag = !this.isValid;
			if (flag)
			{
				throw new InvalidOperationException("The AnimationStream is invalid.");
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00006044 File Offset: 0x00004244
		public float deltaTime
		{
			get
			{
				this.CheckIsValid();
				return this.GetDeltaTime();
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00006064 File Offset: 0x00004264
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x00006083 File Offset: 0x00004283
		public Vector3 velocity
		{
			get
			{
				this.CheckIsValid();
				return this.GetVelocity();
			}
			set
			{
				this.CheckIsValid();
				this.SetVelocity(value);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00006098 File Offset: 0x00004298
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x000060B7 File Offset: 0x000042B7
		public Vector3 angularVelocity
		{
			get
			{
				this.CheckIsValid();
				return this.GetAngularVelocity();
			}
			set
			{
				this.CheckIsValid();
				this.SetAngularVelocity(value);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000060CC File Offset: 0x000042CC
		public Vector3 rootMotionPosition
		{
			get
			{
				this.CheckIsValid();
				return this.GetRootMotionPosition();
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x000060EC File Offset: 0x000042EC
		public Quaternion rootMotionRotation
		{
			get
			{
				this.CheckIsValid();
				return this.GetRootMotionRotation();
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000610C File Offset: 0x0000430C
		public bool isHumanStream
		{
			get
			{
				this.CheckIsValid();
				return this.GetIsHumanStream();
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000612C File Offset: 0x0000432C
		public AnimationHumanStream AsHuman()
		{
			this.CheckIsValid();
			bool flag = !this.GetIsHumanStream();
			if (flag)
			{
				throw new InvalidOperationException("Cannot create an AnimationHumanStream for a generic rig.");
			}
			return this.GetHumanStream();
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00006164 File Offset: 0x00004364
		public int inputStreamCount
		{
			get
			{
				this.CheckIsValid();
				return this.GetInputStreamCount();
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00006184 File Offset: 0x00004384
		public AnimationStream GetInputStream(int index)
		{
			this.CheckIsValid();
			return this.InternalGetInputStream(index);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000061A4 File Offset: 0x000043A4
		public float GetInputWeight(int index)
		{
			this.CheckIsValid();
			return this.InternalGetInputWeight(index);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000061C4 File Offset: 0x000043C4
		public void CopyAnimationStreamMotion(AnimationStream animationStream)
		{
			this.CheckIsValid();
			animationStream.CheckIsValid();
			this.CopyAnimationStreamMotionInternal(animationStream);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000061DE File Offset: 0x000043DE
		private void ReadSceneTransforms()
		{
			this.CheckIsValid();
			this.InternalReadSceneTransforms();
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000061EF File Offset: 0x000043EF
		private void WriteSceneTransforms()
		{
			this.CheckIsValid();
			this.InternalWriteSceneTransforms();
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00006200 File Offset: 0x00004400
		[NativeMethod(Name = "AnimationStreamBindings::CopyAnimationStreamMotion", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void CopyAnimationStreamMotionInternal(AnimationStream animationStream)
		{
			AnimationStream.CopyAnimationStreamMotionInternal_Injected(ref this, ref animationStream);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000620A File Offset: 0x0000440A
		[NativeMethod(IsThreadSafe = true)]
		private float GetDeltaTime()
		{
			return AnimationStream.GetDeltaTime_Injected(ref this);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00006212 File Offset: 0x00004412
		[NativeMethod(IsThreadSafe = true)]
		private bool GetIsHumanStream()
		{
			return AnimationStream.GetIsHumanStream_Injected(ref this);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000621C File Offset: 0x0000441C
		[NativeMethod(Name = "AnimationStreamBindings::GetVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetVelocity()
		{
			Vector3 result;
			AnimationStream.GetVelocity_Injected(ref this, out result);
			return result;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00006232 File Offset: 0x00004432
		[NativeMethod(Name = "AnimationStreamBindings::SetVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void SetVelocity(Vector3 velocity)
		{
			AnimationStream.SetVelocity_Injected(ref this, ref velocity);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000623C File Offset: 0x0000443C
		[NativeMethod(Name = "AnimationStreamBindings::GetAngularVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetAngularVelocity()
		{
			Vector3 result;
			AnimationStream.GetAngularVelocity_Injected(ref this, out result);
			return result;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00006252 File Offset: 0x00004452
		[NativeMethod(Name = "AnimationStreamBindings::SetAngularVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void SetAngularVelocity(Vector3 velocity)
		{
			AnimationStream.SetAngularVelocity_Injected(ref this, ref velocity);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000625C File Offset: 0x0000445C
		[NativeMethod(Name = "AnimationStreamBindings::GetRootMotionPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetRootMotionPosition()
		{
			Vector3 result;
			AnimationStream.GetRootMotionPosition_Injected(ref this, out result);
			return result;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00006274 File Offset: 0x00004474
		[NativeMethod(Name = "AnimationStreamBindings::GetRootMotionRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion GetRootMotionRotation()
		{
			Quaternion result;
			AnimationStream.GetRootMotionRotation_Injected(ref this, out result);
			return result;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000628A File Offset: 0x0000448A
		[NativeMethod(IsThreadSafe = true)]
		private int GetInputStreamCount()
		{
			return AnimationStream.GetInputStreamCount_Injected(ref this);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00006294 File Offset: 0x00004494
		[NativeMethod(Name = "GetInputStream", IsThreadSafe = true)]
		private AnimationStream InternalGetInputStream(int index)
		{
			AnimationStream result;
			AnimationStream.InternalGetInputStream_Injected(ref this, index, out result);
			return result;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000062AB File Offset: 0x000044AB
		[NativeMethod(Name = "GetInputWeight", IsThreadSafe = true)]
		private float InternalGetInputWeight(int index)
		{
			return AnimationStream.InternalGetInputWeight_Injected(ref this, index);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000062B4 File Offset: 0x000044B4
		[NativeMethod(IsThreadSafe = true)]
		private AnimationHumanStream GetHumanStream()
		{
			AnimationHumanStream result;
			AnimationStream.GetHumanStream_Injected(ref this, out result);
			return result;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000062CA File Offset: 0x000044CA
		[NativeMethod(Name = "ReadSceneTransforms", IsThreadSafe = true)]
		private void InternalReadSceneTransforms()
		{
			AnimationStream.InternalReadSceneTransforms_Injected(ref this);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000062D2 File Offset: 0x000044D2
		[NativeMethod(Name = "WriteSceneTransforms", IsThreadSafe = true)]
		private void InternalWriteSceneTransforms()
		{
			AnimationStream.InternalWriteSceneTransforms_Injected(ref this);
		}

		// Token: 0x0600042B RID: 1067
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyAnimationStreamMotionInternal_Injected(ref AnimationStream _unity_self, ref AnimationStream animationStream);

		// Token: 0x0600042C RID: 1068
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetDeltaTime_Injected(ref AnimationStream _unity_self);

		// Token: 0x0600042D RID: 1069
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIsHumanStream_Injected(ref AnimationStream _unity_self);

		// Token: 0x0600042E RID: 1070
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVelocity_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		// Token: 0x0600042F RID: 1071
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVelocity_Injected(ref AnimationStream _unity_self, ref Vector3 velocity);

		// Token: 0x06000430 RID: 1072
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAngularVelocity_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		// Token: 0x06000431 RID: 1073
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAngularVelocity_Injected(ref AnimationStream _unity_self, ref Vector3 velocity);

		// Token: 0x06000432 RID: 1074
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRootMotionPosition_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		// Token: 0x06000433 RID: 1075
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRootMotionRotation_Injected(ref AnimationStream _unity_self, out Quaternion ret);

		// Token: 0x06000434 RID: 1076
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetInputStreamCount_Injected(ref AnimationStream _unity_self);

		// Token: 0x06000435 RID: 1077
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetInputStream_Injected(ref AnimationStream _unity_self, int index, out AnimationStream ret);

		// Token: 0x06000436 RID: 1078
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float InternalGetInputWeight_Injected(ref AnimationStream _unity_self, int index);

		// Token: 0x06000437 RID: 1079
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetHumanStream_Injected(ref AnimationStream _unity_self, out AnimationHumanStream ret);

		// Token: 0x06000438 RID: 1080
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalReadSceneTransforms_Injected(ref AnimationStream _unity_self);

		// Token: 0x06000439 RID: 1081
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalWriteSceneTransforms_Injected(ref AnimationStream _unity_self);

		// Token: 0x0400015B RID: 347
		private uint m_AnimatorBindingsVersion;

		// Token: 0x0400015C RID: 348
		private IntPtr constant;

		// Token: 0x0400015D RID: 349
		private IntPtr input;

		// Token: 0x0400015E RID: 350
		private IntPtr output;

		// Token: 0x0400015F RID: 351
		private IntPtr workspace;

		// Token: 0x04000160 RID: 352
		private IntPtr inputStreamAccessor;

		// Token: 0x04000161 RID: 353
		private IntPtr animationHandleBinder;

		// Token: 0x04000162 RID: 354
		internal const int InvalidIndex = -1;
	}
}

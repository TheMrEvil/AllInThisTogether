using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000039 RID: 57
	[NativeHeader("Modules/Animation/HumanPoseHandler.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	public class HumanPoseHandler : IDisposable
	{
		// Token: 0x06000259 RID: 601
		[FreeFunction("AnimationBindings::CreateHumanPoseHandler")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_CreateFromRoot(Avatar avatar, Transform root);

		// Token: 0x0600025A RID: 602
		[FreeFunction("AnimationBindings::CreateHumanPoseHandler", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_CreateFromJointPaths(Avatar avatar, string[] jointPaths);

		// Token: 0x0600025B RID: 603
		[FreeFunction("AnimationBindings::DestroyHumanPoseHandler")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x0600025C RID: 604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetHumanPose(out Vector3 bodyPosition, out Quaternion bodyRotation, [Out] float[] muscles);

		// Token: 0x0600025D RID: 605
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetHumanPose(ref Vector3 bodyPosition, ref Quaternion bodyRotation, float[] muscles);

		// Token: 0x0600025E RID: 606
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetInternalHumanPose(out Vector3 bodyPosition, out Quaternion bodyRotation, [Out] float[] muscles);

		// Token: 0x0600025F RID: 607
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetInternalHumanPose(ref Vector3 bodyPosition, ref Quaternion bodyRotation, float[] muscles);

		// Token: 0x06000260 RID: 608
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void GetInternalAvatarPose(void* avatarPose, int avatarPoseLength);

		// Token: 0x06000261 RID: 609
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void SetInternalAvatarPose(void* avatarPose, int avatarPoseLength);

		// Token: 0x06000262 RID: 610 RVA: 0x00003F68 File Offset: 0x00002168
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				HumanPoseHandler.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00003FAC File Offset: 0x000021AC
		public HumanPoseHandler(Avatar avatar, Transform root)
		{
			this.m_Ptr = IntPtr.Zero;
			bool flag = root == null;
			if (flag)
			{
				throw new ArgumentNullException("HumanPoseHandler root Transform is null");
			}
			bool flag2 = avatar == null;
			if (flag2)
			{
				throw new ArgumentNullException("HumanPoseHandler avatar is null");
			}
			bool flag3 = !avatar.isValid;
			if (flag3)
			{
				throw new ArgumentException("HumanPoseHandler avatar is invalid");
			}
			bool flag4 = !avatar.isHuman;
			if (flag4)
			{
				throw new ArgumentException("HumanPoseHandler avatar is not human");
			}
			this.m_Ptr = HumanPoseHandler.Internal_CreateFromRoot(avatar, root);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00004038 File Offset: 0x00002238
		public HumanPoseHandler(Avatar avatar, string[] jointPaths)
		{
			this.m_Ptr = IntPtr.Zero;
			bool flag = jointPaths == null;
			if (flag)
			{
				throw new ArgumentNullException("HumanPoseHandler jointPaths array is null");
			}
			bool flag2 = avatar == null;
			if (flag2)
			{
				throw new ArgumentNullException("HumanPoseHandler avatar is null");
			}
			bool flag3 = !avatar.isValid;
			if (flag3)
			{
				throw new ArgumentException("HumanPoseHandler avatar is invalid");
			}
			bool flag4 = !avatar.isHuman;
			if (flag4)
			{
				throw new ArgumentException("HumanPoseHandler avatar is not human");
			}
			this.m_Ptr = HumanPoseHandler.Internal_CreateFromJointPaths(avatar, jointPaths);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000040C0 File Offset: 0x000022C0
		public void GetHumanPose(ref HumanPose humanPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			this.GetHumanPose(out humanPose.bodyPosition, out humanPose.bodyRotation, humanPose.muscles);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00004110 File Offset: 0x00002310
		public void SetHumanPose(ref HumanPose humanPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			this.SetHumanPose(ref humanPose.bodyPosition, ref humanPose.bodyRotation, humanPose.muscles);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00004160 File Offset: 0x00002360
		public void GetInternalHumanPose(ref HumanPose humanPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			this.GetInternalHumanPose(out humanPose.bodyPosition, out humanPose.bodyRotation, humanPose.muscles);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000041B0 File Offset: 0x000023B0
		public void SetInternalHumanPose(ref HumanPose humanPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			this.SetInternalHumanPose(ref humanPose.bodyPosition, ref humanPose.bodyRotation, humanPose.muscles);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00004200 File Offset: 0x00002400
		public void GetInternalAvatarPose(NativeArray<float> avatarPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			this.GetInternalAvatarPose(avatarPose.GetUnsafePtr<float>(), avatarPose.Length);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00004244 File Offset: 0x00002444
		public void SetInternalAvatarPose(NativeArray<float> avatarPose)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			this.SetInternalAvatarPose(avatarPose.GetUnsafeReadOnlyPtr<float>(), avatarPose.Length);
		}

		// Token: 0x04000136 RID: 310
		internal IntPtr m_Ptr;
	}
}

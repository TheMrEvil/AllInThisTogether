using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000114 RID: 276
	[ExecuteInEditMode]
	public class EditorIK : MonoBehaviour
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0005195E File Offset: 0x0004FB5E
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x00051966 File Offset: 0x0004FB66
		public IK ik
		{
			[CompilerGenerated]
			get
			{
				return this.<ik>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ik>k__BackingField = value;
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00051970 File Offset: 0x0004FB70
		private void OnEnable()
		{
			if (Application.isPlaying)
			{
				return;
			}
			if (this.ik == null)
			{
				this.ik = base.GetComponent<IK>();
			}
			if (this.ik == null)
			{
				Debug.LogError("EditorIK needs to have an IK component on the same GameObject.", base.transform);
				return;
			}
			if (this.bones.Length == 0)
			{
				this.bones = this.ik.transform.GetComponentsInChildren<Transform>();
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x000519E0 File Offset: 0x0004FBE0
		private void OnDisable()
		{
			if (Application.isPlaying)
			{
				return;
			}
			if (this.defaultPose != null && this.defaultPose.poseStored)
			{
				this.defaultPose.Restore(this.bones);
			}
			if (this.ik != null)
			{
				this.ik.GetIKSolver().executedInEditor = false;
			}
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00051A44 File Offset: 0x0004FC44
		private void OnDestroy()
		{
			if (Application.isPlaying)
			{
				return;
			}
			if (this.ik == null)
			{
				return;
			}
			if (this.bones.Length == 0)
			{
				this.bones = this.ik.transform.GetComponentsInChildren<Transform>();
			}
			if (this.defaultPose != null && this.defaultPose.poseStored && this.bones.Length != 0)
			{
				this.defaultPose.Restore(this.bones);
			}
			this.ik.GetIKSolver().executedInEditor = false;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00051ACE File Offset: 0x0004FCCE
		public void StoreDefaultPose()
		{
			this.bones = this.ik.transform.GetComponentsInChildren<Transform>();
			this.defaultPose.Store(this.bones);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00051AF8 File Offset: 0x0004FCF8
		public bool Initiate()
		{
			if (this.defaultPose == null)
			{
				return false;
			}
			if (!this.defaultPose.poseStored)
			{
				return false;
			}
			if (this.bones.Length == 0)
			{
				return false;
			}
			if (this.ik == null)
			{
				this.ik = base.GetComponent<IK>();
			}
			if (this.ik == null)
			{
				Debug.LogError("EditorIK can not find an IK component.", base.transform);
				return false;
			}
			this.defaultPose.Restore(this.bones);
			this.ik.GetIKSolver().executedInEditor = false;
			this.ik.GetIKSolver().Initiate(this.ik.transform);
			this.ik.GetIKSolver().executedInEditor = true;
			return true;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00051BBC File Offset: 0x0004FDBC
		public void Update()
		{
			if (Application.isPlaying)
			{
				return;
			}
			if (this.ik == null)
			{
				return;
			}
			if (!this.ik.enabled)
			{
				return;
			}
			if (!this.ik.GetIKSolver().executedInEditor)
			{
				return;
			}
			if (this.bones.Length == 0)
			{
				this.bones = this.ik.transform.GetComponentsInChildren<Transform>();
			}
			if (this.bones.Length == 0)
			{
				return;
			}
			if (!this.defaultPose.Restore(this.bones))
			{
				return;
			}
			this.ik.GetIKSolver().executedInEditor = false;
			if (!this.ik.GetIKSolver().initiated)
			{
				this.ik.GetIKSolver().Initiate(this.ik.transform);
			}
			if (!this.ik.GetIKSolver().initiated)
			{
				return;
			}
			this.ik.GetIKSolver().executedInEditor = true;
			if (this.animator != null && this.animator.runtimeAnimatorController != null)
			{
				this.animator.Update(Time.deltaTime);
			}
			this.ik.GetIKSolver().Update();
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00051CE1 File Offset: 0x0004FEE1
		public EditorIK()
		{
		}

		// Token: 0x04000984 RID: 2436
		[Tooltip("If slot assigned, will update Animator before IK.")]
		public Animator animator;

		// Token: 0x04000985 RID: 2437
		[Tooltip("Create/Final IK/Editor IK Pose")]
		public EditorIKPose defaultPose;

		// Token: 0x04000986 RID: 2438
		[HideInInspector]
		public Transform[] bones = new Transform[0];

		// Token: 0x04000987 RID: 2439
		[CompilerGenerated]
		private IK <ik>k__BackingField;
	}
}

using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000115 RID: 277
	[CreateAssetMenu(fileName = "Editor IK Pose", menuName = "Final IK/Editor IK Pose", order = 1)]
	public class EditorIKPose : ScriptableObject
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x00051CF5 File Offset: 0x0004FEF5
		public bool poseStored
		{
			get
			{
				return this.localPositions.Length != 0;
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00051D04 File Offset: 0x0004FF04
		public void Store(Transform[] T)
		{
			this.localPositions = new Vector3[T.Length];
			this.localRotations = new Quaternion[T.Length];
			for (int i = 1; i < T.Length; i++)
			{
				this.localPositions[i] = T[i].localPosition;
				this.localRotations[i] = T[i].localRotation;
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00051D64 File Offset: 0x0004FF64
		public bool Restore(Transform[] T)
		{
			if (this.localPositions.Length != T.Length)
			{
				Debug.LogError("Can not restore pose (unmatched bone count). Please stop the solver and click on 'Store Default Pose' if you have made changes to character hierarchy.");
				return false;
			}
			for (int i = 1; i < T.Length; i++)
			{
				T[i].localPosition = this.localPositions[i];
				T[i].localRotation = this.localRotations[i];
			}
			return true;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00051DC1 File Offset: 0x0004FFC1
		public EditorIKPose()
		{
		}

		// Token: 0x04000988 RID: 2440
		public Vector3[] localPositions = new Vector3[0];

		// Token: 0x04000989 RID: 2441
		public Quaternion[] localRotations = new Quaternion[0];
	}
}

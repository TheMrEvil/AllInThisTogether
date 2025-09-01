using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000110 RID: 272
	public class AimPoser : MonoBehaviour
	{
		// Token: 0x06000C20 RID: 3104 RVA: 0x00051598 File Offset: 0x0004F798
		public AimPoser.Pose GetPose(Vector3 localDirection)
		{
			if (this.poses.Length == 0)
			{
				return null;
			}
			for (int i = 0; i < this.poses.Length - 1; i++)
			{
				if (this.poses[i].IsInDirection(localDirection))
				{
					return this.poses[i];
				}
			}
			return this.poses[this.poses.Length - 1];
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x000515F0 File Offset: 0x0004F7F0
		public void SetPoseActive(AimPoser.Pose pose)
		{
			for (int i = 0; i < this.poses.Length; i++)
			{
				this.poses[i].SetAngleBuffer((this.poses[i] == pose) ? this.angleBuffer : 0f);
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00051635 File Offset: 0x0004F835
		public AimPoser()
		{
		}

		// Token: 0x04000979 RID: 2425
		public float angleBuffer = 5f;

		// Token: 0x0400097A RID: 2426
		public AimPoser.Pose[] poses = new AimPoser.Pose[0];

		// Token: 0x02000219 RID: 537
		[Serializable]
		public class Pose
		{
			// Token: 0x06001149 RID: 4425 RVA: 0x0006C1DC File Offset: 0x0006A3DC
			public bool IsInDirection(Vector3 d)
			{
				if (this.direction == Vector3.zero)
				{
					return false;
				}
				if (this.yaw <= 0f || this.pitch <= 0f)
				{
					return false;
				}
				if (this.yaw < 180f)
				{
					Vector3 forward = new Vector3(this.direction.x, 0f, this.direction.z);
					if (forward == Vector3.zero)
					{
						forward = Vector3.forward;
					}
					if (Vector3.Angle(new Vector3(d.x, 0f, d.z), forward) > this.yaw + this.angleBuffer)
					{
						return false;
					}
				}
				if (this.pitch >= 180f)
				{
					return true;
				}
				float num = Vector3.Angle(Vector3.up, this.direction);
				return Mathf.Abs(Vector3.Angle(Vector3.up, d) - num) < this.pitch + this.angleBuffer;
			}

			// Token: 0x0600114A RID: 4426 RVA: 0x0006C2C9 File Offset: 0x0006A4C9
			public void SetAngleBuffer(float value)
			{
				this.angleBuffer = value;
			}

			// Token: 0x0600114B RID: 4427 RVA: 0x0006C2D2 File Offset: 0x0006A4D2
			public Pose()
			{
			}

			// Token: 0x04000FF4 RID: 4084
			public bool visualize = true;

			// Token: 0x04000FF5 RID: 4085
			public string name;

			// Token: 0x04000FF6 RID: 4086
			public Vector3 direction;

			// Token: 0x04000FF7 RID: 4087
			public float yaw = 75f;

			// Token: 0x04000FF8 RID: 4088
			public float pitch = 45f;

			// Token: 0x04000FF9 RID: 4089
			private float angleBuffer;
		}
	}
}

using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x0200010D RID: 269
	[HelpURL("http://www.root-motion.com/finalikdox/html/page14.html")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/Rotation Limits/Rotation Limit Polygonal")]
	public class RotationLimitPolygonal : RotationLimit
	{
		// Token: 0x06000C01 RID: 3073 RVA: 0x0005061A File Offset: 0x0004E81A
		[ContextMenu("User Manual")]
		private void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page14.html");
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00050626 File Offset: 0x0004E826
		[ContextMenu("Scrpt Reference")]
		private void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_rotation_limit_polygonal.html");
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00050632 File Offset: 0x0004E832
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0005063E File Offset: 0x0004E83E
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0005064A File Offset: 0x0004E84A
		public void SetLimitPoints(RotationLimitPolygonal.LimitPoint[] points)
		{
			if (points.Length < 3)
			{
				base.LogWarning("The polygon must have at least 3 Limit Points.");
				return;
			}
			this.points = points;
			this.BuildReachCones();
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0005066B File Offset: 0x0004E86B
		protected override Quaternion LimitRotation(Quaternion rotation)
		{
			if (this.reachCones.Length == 0)
			{
				this.Start();
			}
			return RotationLimit.LimitTwist(this.LimitSwing(rotation), this.axis, base.secondaryAxis, this.twistLimit);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0005069C File Offset: 0x0004E89C
		private void Start()
		{
			if (this.points.Length < 3)
			{
				this.ResetToDefault();
			}
			for (int i = 0; i < this.reachCones.Length; i++)
			{
				if (!this.reachCones[i].isValid)
				{
					if (this.smoothIterations <= 0)
					{
						int num;
						if (i < this.reachCones.Length - 1)
						{
							num = i + 1;
						}
						else
						{
							num = 0;
						}
						base.LogWarning(string.Concat(new string[]
						{
							"Reach Cone {point ",
							i.ToString(),
							", point ",
							num.ToString(),
							", Origin} has negative volume. Make sure Axis vector is in the reachable area and the polygon is convex."
						}));
					}
					else
					{
						base.LogWarning("One of the Reach Cones in the polygon has negative volume. Make sure Axis vector is in the reachable area and the polygon is convex.");
					}
				}
			}
			this.axis = this.axis.normalized;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0005075C File Offset: 0x0004E95C
		public void ResetToDefault()
		{
			this.points = new RotationLimitPolygonal.LimitPoint[4];
			for (int i = 0; i < this.points.Length; i++)
			{
				this.points[i] = new RotationLimitPolygonal.LimitPoint();
			}
			Quaternion quaternion = Quaternion.AngleAxis(45f, Vector3.right);
			Quaternion quaternion2 = Quaternion.AngleAxis(45f, Vector3.up);
			this.points[0].point = quaternion * quaternion2 * this.axis;
			this.points[1].point = Quaternion.Inverse(quaternion) * quaternion2 * this.axis;
			this.points[2].point = Quaternion.Inverse(quaternion) * Quaternion.Inverse(quaternion2) * this.axis;
			this.points[3].point = quaternion * Quaternion.Inverse(quaternion2) * this.axis;
			this.BuildReachCones();
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0005084C File Offset: 0x0004EA4C
		public void BuildReachCones()
		{
			this.smoothIterations = Mathf.Clamp(this.smoothIterations, 0, 3);
			this.P = new Vector3[this.points.Length];
			for (int i = 0; i < this.points.Length; i++)
			{
				this.P[i] = this.points[i].point.normalized;
			}
			for (int j = 0; j < this.smoothIterations; j++)
			{
				this.P = this.SmoothPoints();
			}
			this.reachCones = new RotationLimitPolygonal.ReachCone[this.P.Length];
			for (int k = 0; k < this.reachCones.Length - 1; k++)
			{
				this.reachCones[k] = new RotationLimitPolygonal.ReachCone(Vector3.zero, this.axis.normalized, this.P[k], this.P[k + 1]);
			}
			this.reachCones[this.P.Length - 1] = new RotationLimitPolygonal.ReachCone(Vector3.zero, this.axis.normalized, this.P[this.P.Length - 1], this.P[0]);
			for (int l = 0; l < this.reachCones.Length; l++)
			{
				this.reachCones[l].Calculate();
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00050994 File Offset: 0x0004EB94
		private Vector3[] SmoothPoints()
		{
			Vector3[] array = new Vector3[this.P.Length * 2];
			float scalar = this.GetScalar(this.P.Length);
			for (int i = 0; i < array.Length; i += 2)
			{
				array[i] = this.PointToTangentPlane(this.P[i / 2], 1f);
			}
			for (int j = 1; j < array.Length; j += 2)
			{
				Vector3 b = Vector3.zero;
				Vector3 vector = Vector3.zero;
				Vector3 b2 = Vector3.zero;
				if (j > 1 && j < array.Length - 2)
				{
					b = array[j - 2];
					b2 = array[j + 1];
				}
				else if (j == 1)
				{
					b = array[array.Length - 2];
					b2 = array[j + 1];
				}
				else if (j == array.Length - 1)
				{
					b = array[j - 2];
					b2 = array[0];
				}
				if (j < array.Length - 1)
				{
					vector = array[j + 1];
				}
				else
				{
					vector = array[0];
				}
				int num = array.Length / this.points.Length;
				array[j] = 0.5f * (array[j - 1] + vector) + scalar * this.points[j / num].tangentWeight * (vector - b) + scalar * this.points[j / num].tangentWeight * (array[j - 1] - b2);
			}
			for (int k = 0; k < array.Length; k++)
			{
				array[k] = this.TangentPointToSphere(array[k], 1f);
			}
			return array;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00050B41 File Offset: 0x0004ED41
		private float GetScalar(int k)
		{
			if (k <= 3)
			{
				return 0.1667f;
			}
			if (k == 4)
			{
				return 0.1036f;
			}
			if (k == 5)
			{
				return 0.085f;
			}
			if (k == 6)
			{
				return 0.0773f;
			}
			if (k == 7)
			{
				return 0.07f;
			}
			return 0.0625f;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00050B7C File Offset: 0x0004ED7C
		private Vector3 PointToTangentPlane(Vector3 p, float r)
		{
			float num = Vector3.Dot(this.axis, p);
			float num2 = 2f * r * r / (r * r + num);
			return num2 * p + (1f - num2) * -this.axis;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00050BCC File Offset: 0x0004EDCC
		private Vector3 TangentPointToSphere(Vector3 q, float r)
		{
			float num = Vector3.Dot(q - this.axis, q - this.axis);
			float num2 = 4f * r * r / (4f * r * r + num);
			return num2 * q + (1f - num2) * -this.axis;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00050C30 File Offset: 0x0004EE30
		private Quaternion LimitSwing(Quaternion rotation)
		{
			if (rotation == Quaternion.identity)
			{
				return rotation;
			}
			Vector3 vector = rotation * this.axis;
			int reachCone = this.GetReachCone(vector);
			if (reachCone == -1)
			{
				if (!Warning.logged)
				{
					base.LogWarning("RotationLimitPolygonal reach cones are invalid.");
				}
				return rotation;
			}
			if (Vector3.Dot(this.reachCones[reachCone].B, vector) > 0f)
			{
				return rotation;
			}
			Vector3 rhs = Vector3.Cross(this.axis, vector);
			vector = Vector3.Cross(-this.reachCones[reachCone].B, rhs);
			return Quaternion.FromToRotation(rotation * this.axis, vector) * rotation;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00050CD4 File Offset: 0x0004EED4
		private int GetReachCone(Vector3 L)
		{
			float num = Vector3.Dot(this.reachCones[0].S, L);
			for (int i = 0; i < this.reachCones.Length; i++)
			{
				float num2 = num;
				if (i < this.reachCones.Length - 1)
				{
					num = Vector3.Dot(this.reachCones[i + 1].S, L);
				}
				else
				{
					num = Vector3.Dot(this.reachCones[0].S, L);
				}
				if (num2 >= 0f && num < 0f)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00050D54 File Offset: 0x0004EF54
		public RotationLimitPolygonal()
		{
		}

		// Token: 0x04000953 RID: 2387
		[Range(0f, 180f)]
		public float twistLimit = 180f;

		// Token: 0x04000954 RID: 2388
		[Range(0f, 3f)]
		public int smoothIterations;

		// Token: 0x04000955 RID: 2389
		[HideInInspector]
		public RotationLimitPolygonal.LimitPoint[] points;

		// Token: 0x04000956 RID: 2390
		[HideInInspector]
		public Vector3[] P;

		// Token: 0x04000957 RID: 2391
		[HideInInspector]
		public RotationLimitPolygonal.ReachCone[] reachCones = new RotationLimitPolygonal.ReachCone[0];

		// Token: 0x02000216 RID: 534
		[Serializable]
		public class ReachCone
		{
			// Token: 0x17000246 RID: 582
			// (get) Token: 0x0600113B RID: 4411 RVA: 0x0006BFC0 File Offset: 0x0006A1C0
			public Vector3 o
			{
				get
				{
					return this.tetrahedron[0];
				}
			}

			// Token: 0x17000247 RID: 583
			// (get) Token: 0x0600113C RID: 4412 RVA: 0x0006BFCE File Offset: 0x0006A1CE
			public Vector3 a
			{
				get
				{
					return this.tetrahedron[1];
				}
			}

			// Token: 0x17000248 RID: 584
			// (get) Token: 0x0600113D RID: 4413 RVA: 0x0006BFDC File Offset: 0x0006A1DC
			public Vector3 b
			{
				get
				{
					return this.tetrahedron[2];
				}
			}

			// Token: 0x17000249 RID: 585
			// (get) Token: 0x0600113E RID: 4414 RVA: 0x0006BFEA File Offset: 0x0006A1EA
			public Vector3 c
			{
				get
				{
					return this.tetrahedron[3];
				}
			}

			// Token: 0x0600113F RID: 4415 RVA: 0x0006BFF8 File Offset: 0x0006A1F8
			public ReachCone(Vector3 _o, Vector3 _a, Vector3 _b, Vector3 _c)
			{
				this.tetrahedron = new Vector3[4];
				this.tetrahedron[0] = _o;
				this.tetrahedron[1] = _a;
				this.tetrahedron[2] = _b;
				this.tetrahedron[3] = _c;
				this.volume = 0f;
				this.S = Vector3.zero;
				this.B = Vector3.zero;
			}

			// Token: 0x1700024A RID: 586
			// (get) Token: 0x06001140 RID: 4416 RVA: 0x0006C06D File Offset: 0x0006A26D
			public bool isValid
			{
				get
				{
					return this.volume > 0f;
				}
			}

			// Token: 0x06001141 RID: 4417 RVA: 0x0006C07C File Offset: 0x0006A27C
			public void Calculate()
			{
				Vector3 lhs = Vector3.Cross(this.a, this.b);
				this.volume = Vector3.Dot(lhs, this.c) / 6f;
				this.S = Vector3.Cross(this.a, this.b).normalized;
				this.B = Vector3.Cross(this.b, this.c).normalized;
			}

			// Token: 0x04000FEB RID: 4075
			public Vector3[] tetrahedron;

			// Token: 0x04000FEC RID: 4076
			public float volume;

			// Token: 0x04000FED RID: 4077
			public Vector3 S;

			// Token: 0x04000FEE RID: 4078
			public Vector3 B;
		}

		// Token: 0x02000217 RID: 535
		[Serializable]
		public class LimitPoint
		{
			// Token: 0x06001142 RID: 4418 RVA: 0x0006C0F1 File Offset: 0x0006A2F1
			public LimitPoint()
			{
				this.point = Vector3.forward;
				this.tangentWeight = 1f;
			}

			// Token: 0x04000FEF RID: 4079
			public Vector3 point;

			// Token: 0x04000FF0 RID: 4080
			public float tangentWeight;
		}
	}
}

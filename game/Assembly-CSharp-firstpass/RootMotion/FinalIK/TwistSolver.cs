using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000FF RID: 255
	[Serializable]
	public class TwistSolver
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x0004C180 File Offset: 0x0004A380
		public TwistSolver()
		{
			this.weight = 1f;
			this.parentChildCrossfade = 0.5f;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0004C1E4 File Offset: 0x0004A3E4
		public void Initiate()
		{
			if (this.transform == null)
			{
				Debug.LogError("TwistRelaxer solver has unassigned Transform. TwistRelaxer.cs was restructured for FIK v2.0 to support multiple relaxers on the same body part and TwistRelaxer components need to be set up again, sorry for the inconvenience!", this.transform);
				return;
			}
			if (this.parent == null)
			{
				this.parent = this.transform.parent;
			}
			if (this.children.Length == 0)
			{
				if (this.transform.childCount == 0)
				{
					Transform[] componentsInChildren = this.parent.GetComponentsInChildren<Transform>();
					for (int i = 1; i < componentsInChildren.Length; i++)
					{
						if (componentsInChildren[i] != this.transform)
						{
							Transform[] array = new Transform[]
							{
								componentsInChildren[i]
							};
							break;
						}
					}
				}
				else
				{
					this.children = new Transform[]
					{
						this.transform.GetChild(0)
					};
				}
			}
			if (this.children.Length == 0 || this.children[0] == null)
			{
				Debug.LogError("TwistRelaxer has no children assigned.", this.transform);
				return;
			}
			this.twistAxis = this.transform.InverseTransformDirection(this.children[0].position - this.transform.position);
			this.axis = new Vector3(this.twistAxis.y, this.twistAxis.z, this.twistAxis.x);
			Vector3 point = this.transform.rotation * this.axis;
			this.axisRelativeToParentDefault = Quaternion.Inverse(this.parent.rotation) * point;
			this.axisRelativeToChildDefault = Quaternion.Inverse(this.children[0].rotation) * point;
			this.childRotations = new Quaternion[this.children.Length];
			this.inititated = true;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0004C38C File Offset: 0x0004A58C
		public void Relax()
		{
			if (!this.inititated)
			{
				return;
			}
			if (this.weight <= 0f)
			{
				return;
			}
			Quaternion quaternion = this.transform.rotation;
			Quaternion lhs = Quaternion.AngleAxis(this.twistAngleOffset, quaternion * this.twistAxis);
			quaternion = lhs * quaternion;
			Vector3 a = lhs * this.parent.rotation * this.axisRelativeToParentDefault;
			Vector3 b = lhs * this.children[0].rotation * this.axisRelativeToChildDefault;
			Vector3 vector = Vector3.Slerp(a, b, this.parentChildCrossfade);
			vector = Quaternion.Inverse(Quaternion.LookRotation(quaternion * this.axis, quaternion * this.twistAxis)) * vector;
			float num = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
			for (int i = 0; i < this.children.Length; i++)
			{
				this.childRotations[i] = this.children[i].rotation;
			}
			this.transform.rotation = Quaternion.AngleAxis(num * this.weight, quaternion * this.twistAxis) * quaternion;
			for (int j = 0; j < this.children.Length; j++)
			{
				this.children[j].rotation = this.childRotations[j];
			}
		}

		// Token: 0x040008C5 RID: 2245
		[Tooltip("The transform that this solver operates on.")]
		public Transform transform;

		// Token: 0x040008C6 RID: 2246
		[Tooltip("If this is the forearm roll bone, the parent should be the forearm bone. If null, will be found automatically.")]
		public Transform parent;

		// Token: 0x040008C7 RID: 2247
		[Tooltip("If this is the forearm roll bone, the child should be the hand bone. If null, will attempt to find automatically. Assign the hand manually if the hand bone is not a child of the roll bone.")]
		public Transform[] children = new Transform[0];

		// Token: 0x040008C8 RID: 2248
		[Tooltip("The weight of relaxing the twist of this Transform")]
		[Range(0f, 1f)]
		public float weight = 1f;

		// Token: 0x040008C9 RID: 2249
		[Tooltip("If 0.5, this Transform will be twisted half way from parent to child. If 1, the twist angle will be locked to the child and will rotate with along with it.")]
		[Range(0f, 1f)]
		public float parentChildCrossfade = 0.5f;

		// Token: 0x040008CA RID: 2250
		[Tooltip("Rotation offset around the twist axis.")]
		[Range(-180f, 180f)]
		public float twistAngleOffset;

		// Token: 0x040008CB RID: 2251
		private Vector3 twistAxis = Vector3.right;

		// Token: 0x040008CC RID: 2252
		private Vector3 axis = Vector3.forward;

		// Token: 0x040008CD RID: 2253
		private Vector3 axisRelativeToParentDefault;

		// Token: 0x040008CE RID: 2254
		private Vector3 axisRelativeToChildDefault;

		// Token: 0x040008CF RID: 2255
		private Quaternion[] childRotations;

		// Token: 0x040008D0 RID: 2256
		private bool inititated;
	}
}

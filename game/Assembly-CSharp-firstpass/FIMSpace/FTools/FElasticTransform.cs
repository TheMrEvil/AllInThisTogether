using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x02000056 RID: 86
	public class FElasticTransform
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x000168DD File Offset: 0x00014ADD
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x000168E5 File Offset: 0x00014AE5
		public FMuscle_Vector3 PositionMuscle
		{
			[CompilerGenerated]
			get
			{
				return this.<PositionMuscle>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PositionMuscle>k__BackingField = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000168EE File Offset: 0x00014AEE
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x000168F6 File Offset: 0x00014AF6
		public Vector3 ProceduralPosition
		{
			[CompilerGenerated]
			get
			{
				return this.<ProceduralPosition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ProceduralPosition>k__BackingField = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x000168FF File Offset: 0x00014AFF
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00016907 File Offset: 0x00014B07
		public Vector3 sourceAnimationPosition
		{
			[CompilerGenerated]
			get
			{
				return this.<sourceAnimationPosition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<sourceAnimationPosition>k__BackingField = value;
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00016910 File Offset: 0x00014B10
		public void Initialize(Transform transform)
		{
			if (transform == null)
			{
				return;
			}
			this.transform = transform;
			this.ProceduralPosition = transform.position;
			this.proceduralRotation = transform.rotation;
			this.sourceAnimationPosition = transform.position;
			this.PositionMuscle = new FMuscle_Vector3();
			this.PositionMuscle.Initialize(transform.position);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001696E File Offset: 0x00014B6E
		public void OverrideProceduralPosition(Vector3 newPos)
		{
			this.ProceduralPosition = newPos;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00016977 File Offset: 0x00014B77
		public void OverrideProceduralPositionHard(Vector3 newPos)
		{
			this.ProceduralPosition = newPos;
			this.PositionMuscle.OverrideProceduralPosition(newPos);
			this.sourceAnimationPosition = newPos;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00016993 File Offset: 0x00014B93
		public void OverrideProceduralRotation(Quaternion newRot)
		{
			this.proceduralRotation = newRot;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001699C File Offset: 0x00014B9C
		public void CaptureSourceAnimation()
		{
			this.sourceAnimationPosition = this.transform.position;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000169AF File Offset: 0x00014BAF
		public void SetChild(FElasticTransform child)
		{
			this.elChild = child;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000169B8 File Offset: 0x00014BB8
		public FElasticTransform GetElasticChild()
		{
			return this.elChild;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000169C0 File Offset: 0x00014BC0
		public void SetParent(FElasticTransform parent)
		{
			this.elParent = parent;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000169CC File Offset: 0x00014BCC
		public void UpdateElasticPosition(float delta)
		{
			this.delta = delta;
			if (this.elParent != null)
			{
				FElasticTransform felasticTransform = (this.elParent.transform == null) ? this.elParent.elParent : this.elParent;
				Quaternion rotation = felasticTransform.transform.rotation;
				Vector3 desired = felasticTransform.ProceduralPosition + rotation * this.transform.localPosition;
				this.PositionMuscle.Update(delta, desired);
				this.ProceduralPosition = this.PositionMuscle.ProceduralPosition;
				return;
			}
			this.ProceduralPosition = this.transform.position;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00016A67 File Offset: 0x00014C67
		public void UpdateElasticPosition(float delta, Vector3 influenceOffset)
		{
			this.delta = delta;
			if (this.elParent != null)
			{
				this.PositionMuscle.MotionInfluence(influenceOffset);
				this.UpdateElasticPosition(delta);
				return;
			}
			this.ProceduralPosition = this.transform.position;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00016AA0 File Offset: 0x00014CA0
		public void UpdateElasticRotation(float blending)
		{
			if (this.elChild != null)
			{
				Quaternion targetRotation;
				if (blending < 1f)
				{
					targetRotation = this.GetTargetRotation(this.elChild.BlendVector(this.elChild.ProceduralPosition, blending), this.transform.TransformDirection(this.elChild.transform.localPosition), blending);
				}
				else
				{
					targetRotation = this.GetTargetRotation(this.elChild.ProceduralPosition, this.transform.TransformDirection(this.elChild.transform.localPosition), this.ProceduralPosition);
				}
				if (this.RotationRapidness < 1f)
				{
					this.proceduralRotation = Quaternion.Lerp(this.proceduralRotation, targetRotation, Mathf.Min(1f, this.delta * (10f + this.RotationRapidness * 50f)));
					this.transform.rotation = this.proceduralRotation;
					return;
				}
				this.transform.rotation = targetRotation;
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00016B8F File Offset: 0x00014D8F
		public Vector3 BlendVector(Vector3 target, float blend)
		{
			return Vector3.LerpUnclamped(this.sourceAnimationPosition, target, blend);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00016BA0 File Offset: 0x00014DA0
		public Quaternion GetTargetRotation(Vector3 lookPos, Vector3 localOffset, float blending)
		{
			return Quaternion.FromToRotation(localOffset, (lookPos - this.BlendVector(this.ProceduralPosition, blending)).normalized) * this.transform.rotation;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00016BE0 File Offset: 0x00014DE0
		public Quaternion GetTargetRotation(Vector3 lookPos, Vector3 localOffset, Vector3 pos)
		{
			return Quaternion.FromToRotation(localOffset, (lookPos - pos).normalized) * this.transform.rotation;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00016C12 File Offset: 0x00014E12
		public FElasticTransform()
		{
		}

		// Token: 0x040002B0 RID: 688
		public Transform transform;

		// Token: 0x040002B1 RID: 689
		private FElasticTransform elChild;

		// Token: 0x040002B2 RID: 690
		private FElasticTransform elParent;

		// Token: 0x040002B3 RID: 691
		[FPD_Suffix(0f, 1f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		public float RotationRapidness = 0.1f;

		// Token: 0x040002B4 RID: 692
		[CompilerGenerated]
		private FMuscle_Vector3 <PositionMuscle>k__BackingField;

		// Token: 0x040002B5 RID: 693
		[CompilerGenerated]
		private Vector3 <ProceduralPosition>k__BackingField;

		// Token: 0x040002B6 RID: 694
		private Quaternion proceduralRotation;

		// Token: 0x040002B7 RID: 695
		[CompilerGenerated]
		private Vector3 <sourceAnimationPosition>k__BackingField;

		// Token: 0x040002B8 RID: 696
		private float delta = 0.01f;
	}
}

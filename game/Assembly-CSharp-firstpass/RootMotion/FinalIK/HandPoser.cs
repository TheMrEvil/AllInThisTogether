using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000107 RID: 263
	public class HandPoser : Poser
	{
		// Token: 0x06000BC1 RID: 3009 RVA: 0x0004F8D4 File Offset: 0x0004DAD4
		public override void AutoMapping()
		{
			if (this.poseRoot == null)
			{
				this.poseChildren = new Transform[0];
			}
			else
			{
				this.poseChildren = this.poseRoot.GetComponentsInChildren<Transform>();
			}
			this._poseRoot = this.poseRoot;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0004F90F File Offset: 0x0004DB0F
		protected override void InitiatePoser()
		{
			this.children = base.GetComponentsInChildren<Transform>();
			this.StoreDefaultState();
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0004F924 File Offset: 0x0004DB24
		protected override void FixPoserTransforms()
		{
			for (int i = 0; i < this.children.Length; i++)
			{
				this.children[i].localPosition = this.defaultLocalPositions[i];
				this.children[i].localRotation = this.defaultLocalRotations[i];
			}
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0004F978 File Offset: 0x0004DB78
		protected override void UpdatePoser()
		{
			if (this.weight <= 0f)
			{
				return;
			}
			if (this.localPositionWeight <= 0f && this.localRotationWeight <= 0f)
			{
				return;
			}
			if (this._poseRoot != this.poseRoot)
			{
				this.AutoMapping();
			}
			if (this.poseRoot == null)
			{
				return;
			}
			if (this.children.Length != this.poseChildren.Length)
			{
				Warning.Log("Number of children does not match with the pose", base.transform, false);
				return;
			}
			float t = this.localRotationWeight * this.weight;
			float t2 = this.localPositionWeight * this.weight;
			for (int i = 0; i < this.children.Length; i++)
			{
				if (this.children[i] != base.transform)
				{
					this.children[i].localRotation = Quaternion.Lerp(this.children[i].localRotation, this.poseChildren[i].localRotation, t);
					this.children[i].localPosition = Vector3.Lerp(this.children[i].localPosition, this.poseChildren[i].localPosition, t2);
				}
			}
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0004FA98 File Offset: 0x0004DC98
		protected void StoreDefaultState()
		{
			this.defaultLocalPositions = new Vector3[this.children.Length];
			this.defaultLocalRotations = new Quaternion[this.children.Length];
			for (int i = 0; i < this.children.Length; i++)
			{
				this.defaultLocalPositions[i] = this.children[i].localPosition;
				this.defaultLocalRotations[i] = this.children[i].localRotation;
			}
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0004FB10 File Offset: 0x0004DD10
		public HandPoser()
		{
		}

		// Token: 0x0400092C RID: 2348
		protected Transform[] children;

		// Token: 0x0400092D RID: 2349
		private Transform _poseRoot;

		// Token: 0x0400092E RID: 2350
		private Transform[] poseChildren;

		// Token: 0x0400092F RID: 2351
		private Vector3[] defaultLocalPositions;

		// Token: 0x04000930 RID: 2352
		private Quaternion[] defaultLocalRotations;
	}
}

using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000AE RID: 174
	[Serializable]
	public class BakerTransform
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x00035CA4 File Offset: 0x00033EA4
		public BakerTransform(Transform transform, Transform root, bool recordPosition, bool isRootNode)
		{
			this.transform = transform;
			this.recordPosition = (recordPosition || isRootNode);
			this.isRootNode = isRootNode;
			this.relativePath = string.Empty;
			this.Reset();
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00035CD6 File Offset: 0x00033ED6
		public void SetRelativeSpace(Vector3 position, Quaternion rotation)
		{
			this.relativePosition = position;
			this.relativeRotation = rotation;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00035CE8 File Offset: 0x00033EE8
		public void SetCurves(ref AnimationClip clip)
		{
			if (this.recordPosition)
			{
				clip.SetCurve(this.relativePath, typeof(Transform), "localPosition.x", this.posX);
				clip.SetCurve(this.relativePath, typeof(Transform), "localPosition.y", this.posY);
				clip.SetCurve(this.relativePath, typeof(Transform), "localPosition.z", this.posZ);
			}
			clip.SetCurve(this.relativePath, typeof(Transform), "localRotation.x", this.rotX);
			clip.SetCurve(this.relativePath, typeof(Transform), "localRotation.y", this.rotY);
			clip.SetCurve(this.relativePath, typeof(Transform), "localRotation.z", this.rotZ);
			clip.SetCurve(this.relativePath, typeof(Transform), "localRotation.w", this.rotW);
			if (this.isRootNode)
			{
				this.AddRootMotionCurves(ref clip);
			}
			clip.EnsureQuaternionContinuity();
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00035E04 File Offset: 0x00034004
		private void AddRootMotionCurves(ref AnimationClip clip)
		{
			if (this.recordPosition)
			{
				clip.SetCurve("", typeof(Animator), "MotionT.x", this.posX);
				clip.SetCurve("", typeof(Animator), "MotionT.y", this.posY);
				clip.SetCurve("", typeof(Animator), "MotionT.z", this.posZ);
			}
			clip.SetCurve("", typeof(Animator), "MotionQ.x", this.rotX);
			clip.SetCurve("", typeof(Animator), "MotionQ.y", this.rotY);
			clip.SetCurve("", typeof(Animator), "MotionQ.z", this.rotZ);
			clip.SetCurve("", typeof(Animator), "MotionQ.w", this.rotW);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00035F00 File Offset: 0x00034100
		public void Reset()
		{
			this.posX = new AnimationCurve();
			this.posY = new AnimationCurve();
			this.posZ = new AnimationCurve();
			this.rotX = new AnimationCurve();
			this.rotY = new AnimationCurve();
			this.rotZ = new AnimationCurve();
			this.rotW = new AnimationCurve();
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00035F5C File Offset: 0x0003415C
		public void ReduceKeyframes(float maxError)
		{
			BakerUtilities.ReduceKeyframes(this.rotX, maxError);
			BakerUtilities.ReduceKeyframes(this.rotY, maxError);
			BakerUtilities.ReduceKeyframes(this.rotZ, maxError);
			BakerUtilities.ReduceKeyframes(this.rotW, maxError);
			BakerUtilities.ReduceKeyframes(this.posX, maxError);
			BakerUtilities.ReduceKeyframes(this.posY, maxError);
			BakerUtilities.ReduceKeyframes(this.posZ, maxError);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00035FC0 File Offset: 0x000341C0
		public void SetKeyframes(float time)
		{
			if (this.recordPosition)
			{
				Vector3 vector = this.transform.localPosition;
				if (this.isRootNode)
				{
					vector = this.transform.position - this.relativePosition;
				}
				this.posX.AddKey(time, vector.x);
				this.posY.AddKey(time, vector.y);
				this.posZ.AddKey(time, vector.z);
			}
			Quaternion quaternion = this.transform.localRotation;
			if (this.isRootNode)
			{
				quaternion = Quaternion.Inverse(this.relativeRotation) * this.transform.rotation;
			}
			this.rotX.AddKey(time, quaternion.x);
			this.rotY.AddKey(time, quaternion.y);
			this.rotZ.AddKey(time, quaternion.z);
			this.rotW.AddKey(time, quaternion.w);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000360B8 File Offset: 0x000342B8
		public void AddLoopFrame(float time)
		{
			if (this.recordPosition && !this.isRootNode)
			{
				this.posX.AddKey(time, this.posX.keys[0].value);
				this.posY.AddKey(time, this.posY.keys[0].value);
				this.posZ.AddKey(time, this.posZ.keys[0].value);
			}
			this.rotX.AddKey(time, this.rotX.keys[0].value);
			this.rotY.AddKey(time, this.rotY.keys[0].value);
			this.rotZ.AddKey(time, this.rotZ.keys[0].value);
			this.rotW.AddKey(time, this.rotW.keys[0].value);
		}

		// Token: 0x04000632 RID: 1586
		public Transform transform;

		// Token: 0x04000633 RID: 1587
		public AnimationCurve posX;

		// Token: 0x04000634 RID: 1588
		public AnimationCurve posY;

		// Token: 0x04000635 RID: 1589
		public AnimationCurve posZ;

		// Token: 0x04000636 RID: 1590
		public AnimationCurve rotX;

		// Token: 0x04000637 RID: 1591
		public AnimationCurve rotY;

		// Token: 0x04000638 RID: 1592
		public AnimationCurve rotZ;

		// Token: 0x04000639 RID: 1593
		public AnimationCurve rotW;

		// Token: 0x0400063A RID: 1594
		private string relativePath;

		// Token: 0x0400063B RID: 1595
		private bool recordPosition;

		// Token: 0x0400063C RID: 1596
		private Vector3 relativePosition;

		// Token: 0x0400063D RID: 1597
		private bool isRootNode;

		// Token: 0x0400063E RID: 1598
		private Quaternion relativeRotation;
	}
}

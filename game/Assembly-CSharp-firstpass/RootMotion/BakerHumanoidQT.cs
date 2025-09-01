using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000AC RID: 172
	[Serializable]
	public class BakerHumanoidQT
	{
		// Token: 0x060007B5 RID: 1973 RVA: 0x00035248 File Offset: 0x00033448
		public BakerHumanoidQT(string name)
		{
			this.Qx = name + "Q.x";
			this.Qy = name + "Q.y";
			this.Qz = name + "Q.z";
			this.Qw = name + "Q.w";
			this.Tx = name + "T.x";
			this.Ty = name + "T.y";
			this.Tz = name + "T.z";
			this.Reset();
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000352D8 File Offset: 0x000334D8
		public BakerHumanoidQT(Transform transform, AvatarIKGoal goal, string name)
		{
			this.transform = transform;
			this.goal = goal;
			this.Qx = name + "Q.x";
			this.Qy = name + "Q.y";
			this.Qz = name + "Q.z";
			this.Qw = name + "Q.w";
			this.Tx = name + "T.x";
			this.Ty = name + "T.y";
			this.Tz = name + "T.z";
			this.Reset();
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00035378 File Offset: 0x00033578
		public void Reset()
		{
			this.rotX = new AnimationCurve();
			this.rotY = new AnimationCurve();
			this.rotZ = new AnimationCurve();
			this.rotW = new AnimationCurve();
			this.posX = new AnimationCurve();
			this.posY = new AnimationCurve();
			this.posZ = new AnimationCurve();
			this.lastQ = Quaternion.identity;
			this.lastQSet = false;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x000353E4 File Offset: 0x000335E4
		public void SetIKKeyframes(float time, Avatar avatar, Transform root, float humanScale, Vector3 bodyPosition, Quaternion bodyRotation)
		{
			Vector3 vector = this.transform.position;
			Quaternion quaternion = this.transform.rotation;
			if (root.parent != null)
			{
				vector = root.parent.InverseTransformPoint(vector);
				quaternion = Quaternion.Inverse(root.parent.rotation) * quaternion;
			}
			TQ ikgoalTQ = AvatarUtility.GetIKGoalTQ(avatar, humanScale, this.goal, new TQ(bodyPosition, bodyRotation), new TQ(vector, quaternion));
			Quaternion quaternion2 = ikgoalTQ.q;
			if (this.lastQSet)
			{
				quaternion2 = BakerUtilities.EnsureQuaternionContinuity(this.lastQ, ikgoalTQ.q);
			}
			this.lastQ = quaternion2;
			this.lastQSet = true;
			this.rotX.AddKey(time, quaternion2.x);
			this.rotY.AddKey(time, quaternion2.y);
			this.rotZ.AddKey(time, quaternion2.z);
			this.rotW.AddKey(time, quaternion2.w);
			Vector3 t = ikgoalTQ.t;
			this.posX.AddKey(time, t.x);
			this.posY.AddKey(time, t.y);
			this.posZ.AddKey(time, t.z);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0003551C File Offset: 0x0003371C
		public void SetKeyframes(float time, Vector3 pos, Quaternion rot)
		{
			this.rotX.AddKey(time, rot.x);
			this.rotY.AddKey(time, rot.y);
			this.rotZ.AddKey(time, rot.z);
			this.rotW.AddKey(time, rot.w);
			this.posX.AddKey(time, pos.x);
			this.posY.AddKey(time, pos.y);
			this.posZ.AddKey(time, pos.z);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000355B0 File Offset: 0x000337B0
		public void MoveLastKeyframes(float time)
		{
			this.MoveLastKeyframe(time, this.rotX);
			this.MoveLastKeyframe(time, this.rotY);
			this.MoveLastKeyframe(time, this.rotZ);
			this.MoveLastKeyframe(time, this.rotW);
			this.MoveLastKeyframe(time, this.posX);
			this.MoveLastKeyframe(time, this.posY);
			this.MoveLastKeyframe(time, this.posZ);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00035618 File Offset: 0x00033818
		public void SetLoopFrame(float time)
		{
			BakerUtilities.SetLoopFrame(time, this.rotX);
			BakerUtilities.SetLoopFrame(time, this.rotY);
			BakerUtilities.SetLoopFrame(time, this.rotZ);
			BakerUtilities.SetLoopFrame(time, this.rotW);
			BakerUtilities.SetLoopFrame(time, this.posX);
			BakerUtilities.SetLoopFrame(time, this.posY);
			BakerUtilities.SetLoopFrame(time, this.posZ);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0003567C File Offset: 0x0003387C
		private void MoveLastKeyframe(float time, AnimationCurve curve)
		{
			Keyframe[] keys = curve.keys;
			keys[keys.Length - 1].time = time;
			curve.keys = keys;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000356A8 File Offset: 0x000338A8
		public void MultiplyLength(AnimationCurve curve, float mlp)
		{
			Keyframe[] keys = curve.keys;
			for (int i = 0; i < keys.Length; i++)
			{
				Keyframe[] array = keys;
				int num = i;
				array[num].time = array[num].time * mlp;
			}
			curve.keys = keys;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000356E8 File Offset: 0x000338E8
		public void SetCurves(ref AnimationClip clip, float maxError, float lengthMlp)
		{
			this.MultiplyLength(this.rotX, lengthMlp);
			this.MultiplyLength(this.rotY, lengthMlp);
			this.MultiplyLength(this.rotZ, lengthMlp);
			this.MultiplyLength(this.rotW, lengthMlp);
			this.MultiplyLength(this.posX, lengthMlp);
			this.MultiplyLength(this.posY, lengthMlp);
			this.MultiplyLength(this.posZ, lengthMlp);
			BakerUtilities.ReduceKeyframes(this.rotX, maxError);
			BakerUtilities.ReduceKeyframes(this.rotY, maxError);
			BakerUtilities.ReduceKeyframes(this.rotZ, maxError);
			BakerUtilities.ReduceKeyframes(this.rotW, maxError);
			BakerUtilities.ReduceKeyframes(this.posX, maxError);
			BakerUtilities.ReduceKeyframes(this.posY, maxError);
			BakerUtilities.ReduceKeyframes(this.posZ, maxError);
			BakerUtilities.SetTangentMode(this.rotX);
			BakerUtilities.SetTangentMode(this.rotY);
			BakerUtilities.SetTangentMode(this.rotZ);
			BakerUtilities.SetTangentMode(this.rotW);
			clip.SetCurve(string.Empty, typeof(Animator), this.Qx, this.rotX);
			clip.SetCurve(string.Empty, typeof(Animator), this.Qy, this.rotY);
			clip.SetCurve(string.Empty, typeof(Animator), this.Qz, this.rotZ);
			clip.SetCurve(string.Empty, typeof(Animator), this.Qw, this.rotW);
			clip.SetCurve(string.Empty, typeof(Animator), this.Tx, this.posX);
			clip.SetCurve(string.Empty, typeof(Animator), this.Ty, this.posY);
			clip.SetCurve(string.Empty, typeof(Animator), this.Tz, this.posZ);
		}

		// Token: 0x0400061D RID: 1565
		private Transform transform;

		// Token: 0x0400061E RID: 1566
		private string Qx;

		// Token: 0x0400061F RID: 1567
		private string Qy;

		// Token: 0x04000620 RID: 1568
		private string Qz;

		// Token: 0x04000621 RID: 1569
		private string Qw;

		// Token: 0x04000622 RID: 1570
		private string Tx;

		// Token: 0x04000623 RID: 1571
		private string Ty;

		// Token: 0x04000624 RID: 1572
		private string Tz;

		// Token: 0x04000625 RID: 1573
		public AnimationCurve rotX;

		// Token: 0x04000626 RID: 1574
		public AnimationCurve rotY;

		// Token: 0x04000627 RID: 1575
		public AnimationCurve rotZ;

		// Token: 0x04000628 RID: 1576
		public AnimationCurve rotW;

		// Token: 0x04000629 RID: 1577
		public AnimationCurve posX;

		// Token: 0x0400062A RID: 1578
		public AnimationCurve posY;

		// Token: 0x0400062B RID: 1579
		public AnimationCurve posZ;

		// Token: 0x0400062C RID: 1580
		private AvatarIKGoal goal;

		// Token: 0x0400062D RID: 1581
		private Quaternion lastQ;

		// Token: 0x0400062E RID: 1582
		private bool lastQSet;
	}
}

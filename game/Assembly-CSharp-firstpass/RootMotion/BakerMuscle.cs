using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000AD RID: 173
	[Serializable]
	public class BakerMuscle
	{
		// Token: 0x060007BF RID: 1983 RVA: 0x000358BE File Offset: 0x00033ABE
		public BakerMuscle(int muscleIndex)
		{
			this.muscleIndex = muscleIndex;
			this.propertyName = this.MuscleNameToPropertyName(HumanTrait.MuscleName[muscleIndex]);
			this.Reset();
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x000358F0 File Offset: 0x00033AF0
		private string MuscleNameToPropertyName(string n)
		{
			if (n == "Left Index 1 Stretched")
			{
				return "LeftHand.Index.1 Stretched";
			}
			if (n == "Left Index 2 Stretched")
			{
				return "LeftHand.Index.2 Stretched";
			}
			if (n == "Left Index 3 Stretched")
			{
				return "LeftHand.Index.3 Stretched";
			}
			if (n == "Left Middle 1 Stretched")
			{
				return "LeftHand.Middle.1 Stretched";
			}
			if (n == "Left Middle 2 Stretched")
			{
				return "LeftHand.Middle.2 Stretched";
			}
			if (n == "Left Middle 3 Stretched")
			{
				return "LeftHand.Middle.3 Stretched";
			}
			if (n == "Left Ring 1 Stretched")
			{
				return "LeftHand.Ring.1 Stretched";
			}
			if (n == "Left Ring 2 Stretched")
			{
				return "LeftHand.Ring.2 Stretched";
			}
			if (n == "Left Ring 3 Stretched")
			{
				return "LeftHand.Ring.3 Stretched";
			}
			if (n == "Left Little 1 Stretched")
			{
				return "LeftHand.Little.1 Stretched";
			}
			if (n == "Left Little 2 Stretched")
			{
				return "LeftHand.Little.2 Stretched";
			}
			if (n == "Left Little 3 Stretched")
			{
				return "LeftHand.Little.3 Stretched";
			}
			if (n == "Left Thumb 1 Stretched")
			{
				return "LeftHand.Thumb.1 Stretched";
			}
			if (n == "Left Thumb 2 Stretched")
			{
				return "LeftHand.Thumb.2 Stretched";
			}
			if (n == "Left Thumb 3 Stretched")
			{
				return "LeftHand.Thumb.3 Stretched";
			}
			if (n == "Left Index Spread")
			{
				return "LeftHand.Index.Spread";
			}
			if (n == "Left Middle Spread")
			{
				return "LeftHand.Middle.Spread";
			}
			if (n == "Left Ring Spread")
			{
				return "LeftHand.Ring.Spread";
			}
			if (n == "Left Little Spread")
			{
				return "LeftHand.Little.Spread";
			}
			if (n == "Left Thumb Spread")
			{
				return "LeftHand.Thumb.Spread";
			}
			if (n == "Right Index 1 Stretched")
			{
				return "RightHand.Index.1 Stretched";
			}
			if (n == "Right Index 2 Stretched")
			{
				return "RightHand.Index.2 Stretched";
			}
			if (n == "Right Index 3 Stretched")
			{
				return "RightHand.Index.3 Stretched";
			}
			if (n == "Right Middle 1 Stretched")
			{
				return "RightHand.Middle.1 Stretched";
			}
			if (n == "Right Middle 2 Stretched")
			{
				return "RightHand.Middle.2 Stretched";
			}
			if (n == "Right Middle 3 Stretched")
			{
				return "RightHand.Middle.3 Stretched";
			}
			if (n == "Right Ring 1 Stretched")
			{
				return "RightHand.Ring.1 Stretched";
			}
			if (n == "Right Ring 2 Stretched")
			{
				return "RightHand.Ring.2 Stretched";
			}
			if (n == "Right Ring 3 Stretched")
			{
				return "RightHand.Ring.3 Stretched";
			}
			if (n == "Right Little 1 Stretched")
			{
				return "RightHand.Little.1 Stretched";
			}
			if (n == "Right Little 2 Stretched")
			{
				return "RightHand.Little.2 Stretched";
			}
			if (n == "Right Little 3 Stretched")
			{
				return "RightHand.Little.3 Stretched";
			}
			if (n == "Right Thumb 1 Stretched")
			{
				return "RightHand.Thumb.1 Stretched";
			}
			if (n == "Right Thumb 2 Stretched")
			{
				return "RightHand.Thumb.2 Stretched";
			}
			if (n == "Right Thumb 3 Stretched")
			{
				return "RightHand.Thumb.3 Stretched";
			}
			if (n == "Right Index Spread")
			{
				return "RightHand.Index.Spread";
			}
			if (n == "Right Middle Spread")
			{
				return "RightHand.Middle.Spread";
			}
			if (n == "Right Ring Spread")
			{
				return "RightHand.Ring.Spread";
			}
			if (n == "Right Little Spread")
			{
				return "RightHand.Little.Spread";
			}
			if (n == "Right Thumb Spread")
			{
				return "RightHand.Thumb.Spread";
			}
			return n;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00035BF8 File Offset: 0x00033DF8
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

		// Token: 0x060007C2 RID: 1986 RVA: 0x00035C35 File Offset: 0x00033E35
		public void SetCurves(ref AnimationClip clip, float maxError, float lengthMlp)
		{
			this.MultiplyLength(this.curve, lengthMlp);
			BakerUtilities.ReduceKeyframes(this.curve, maxError);
			clip.SetCurve(string.Empty, typeof(Animator), this.propertyName, this.curve);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00035C72 File Offset: 0x00033E72
		public void Reset()
		{
			this.curve = new AnimationCurve();
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00035C7F File Offset: 0x00033E7F
		public void SetKeyframe(float time, float[] muscles)
		{
			this.curve.AddKey(time, muscles[this.muscleIndex]);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00035C96 File Offset: 0x00033E96
		public void SetLoopFrame(float time)
		{
			BakerUtilities.SetLoopFrame(time, this.curve);
		}

		// Token: 0x0400062F RID: 1583
		public AnimationCurve curve;

		// Token: 0x04000630 RID: 1584
		private int muscleIndex = -1;

		// Token: 0x04000631 RID: 1585
		private string propertyName;
	}
}

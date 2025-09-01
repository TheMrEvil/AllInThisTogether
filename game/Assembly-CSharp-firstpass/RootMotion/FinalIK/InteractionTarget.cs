using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000104 RID: 260
	[HelpURL("https://www.youtube.com/watch?v=r5jiZnsDH3M")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/Interaction System/Interaction Target")]
	public class InteractionTarget : MonoBehaviour
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x0004F335 File Offset: 0x0004D535
		[ContextMenu("User Manual")]
		private void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page10.html");
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0004F341 File Offset: 0x0004D541
		[ContextMenu("Scrpt Reference")]
		private void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_interaction_target.html");
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0004F34D File Offset: 0x0004D54D
		[ContextMenu("TUTORIAL VIDEO (PART 1: BASICS)")]
		private void OpenTutorial1()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=r5jiZnsDH3M");
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0004F359 File Offset: 0x0004D559
		[ContextMenu("TUTORIAL VIDEO (PART 2: PICKING UP...)")]
		private void OpenTutorial2()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=eP9-zycoHLk");
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0004F365 File Offset: 0x0004D565
		[ContextMenu("TUTORIAL VIDEO (PART 3: ANIMATION)")]
		private void OpenTutorial3()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=sQfB2RcT1T4&index=14&list=PLVxSIA1OaTOu8Nos3CalXbJ2DrKnntMv6");
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0004F371 File Offset: 0x0004D571
		[ContextMenu("TUTORIAL VIDEO (PART 4: TRIGGERS)")]
		private void OpenTutorial4()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=-TDZpNjt2mk&index=15&list=PLVxSIA1OaTOu8Nos3CalXbJ2DrKnntMv6");
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0004F37D File Offset: 0x0004D57D
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0004F389 File Offset: 0x0004D589
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0004F398 File Offset: 0x0004D598
		public float GetValue(InteractionObject.WeightCurve.Type curveType)
		{
			for (int i = 0; i < this.multipliers.Length; i++)
			{
				if (this.multipliers[i].curve == curveType)
				{
					return this.multipliers[i].multiplier;
				}
			}
			return 1f;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0004F3DB File Offset: 0x0004D5DB
		public void ResetRotation()
		{
			if (this.pivot != null)
			{
				this.pivot.localRotation = this.defaultLocalRotation;
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0004F3FC File Offset: 0x0004D5FC
		public void RotateTo(Transform bone)
		{
			if (this.pivot == null)
			{
				return;
			}
			if (this.pivot != this.lastPivot)
			{
				this.defaultLocalRotation = this.pivot.localRotation;
				this.lastPivot = this.pivot;
			}
			this.pivot.localRotation = this.defaultLocalRotation;
			InteractionTarget.RotationMode rotationMode = this.rotationMode;
			if (rotationMode != InteractionTarget.RotationMode.TwoDOF)
			{
				if (rotationMode != InteractionTarget.RotationMode.ThreeDOF)
				{
					return;
				}
				if (this.threeDOFWeight > 0f)
				{
					Quaternion quaternion = QuaTools.FromToRotation(base.transform.rotation, bone.rotation);
					if (this.threeDOFWeight >= 1f)
					{
						this.pivot.rotation = quaternion * this.pivot.rotation;
						return;
					}
					this.pivot.rotation = Quaternion.Slerp(Quaternion.identity, quaternion, this.threeDOFWeight) * this.pivot.rotation;
				}
			}
			else
			{
				if (this.twistWeight > 0f)
				{
					Vector3 fromDirection = base.transform.position - this.pivot.position;
					Vector3 vector = this.pivot.rotation * this.twistAxis;
					Vector3 vector2 = vector;
					Vector3.OrthoNormalize(ref vector2, ref fromDirection);
					vector2 = vector;
					Vector3 toDirection = bone.position - this.pivot.position;
					Vector3.OrthoNormalize(ref vector2, ref toDirection);
					Quaternion b = QuaTools.FromToAroundAxis(fromDirection, toDirection, vector);
					this.pivot.rotation = Quaternion.Lerp(Quaternion.identity, b, this.twistWeight) * this.pivot.rotation;
				}
				if (this.swingWeight > 0f)
				{
					Quaternion b2 = Quaternion.FromToRotation(base.transform.position - this.pivot.position, bone.position - this.pivot.position);
					this.pivot.rotation = Quaternion.Lerp(Quaternion.identity, b2, this.swingWeight) * this.pivot.rotation;
					return;
				}
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0004F606 File Offset: 0x0004D806
		public InteractionTarget()
		{
		}

		// Token: 0x0400091E RID: 2334
		[Tooltip("The type of the FBBIK effector.")]
		public FullBodyBipedEffector effectorType;

		// Token: 0x0400091F RID: 2335
		[Tooltip("InteractionObject weight curve multipliers for this effector target.")]
		public InteractionTarget.Multiplier[] multipliers;

		// Token: 0x04000920 RID: 2336
		[Tooltip("The interaction speed multiplier for this effector. This can be used to make interactions faster/slower for specific effectors.")]
		public float interactionSpeedMlp = 1f;

		// Token: 0x04000921 RID: 2337
		[Tooltip("The pivot to twist/swing this interaction target about. For symmetric objects that can be interacted with from a certain angular range.")]
		public Transform pivot;

		// Token: 0x04000922 RID: 2338
		[Tooltip("2 or 3 degrees of freedom to match this InteractionTarget's rotation to the effector bone rotation.")]
		public InteractionTarget.RotationMode rotationMode;

		// Token: 0x04000923 RID: 2339
		[Tooltip("The axis of twisting the interaction target (blue line).")]
		public Vector3 twistAxis = Vector3.up;

		// Token: 0x04000924 RID: 2340
		[Tooltip("The weight of twisting the interaction target towards the effector bone in the start of the interaction.")]
		public float twistWeight = 1f;

		// Token: 0x04000925 RID: 2341
		[Tooltip("The weight of swinging the interaction target towards the effector bone in the start of the interaction. Swing is defined as a 3-DOF rotation around any axis, while twist is only around the twist axis.")]
		public float swingWeight;

		// Token: 0x04000926 RID: 2342
		[Tooltip("The weight of rotating this InteractionTarget to the effector bone in the start of the interaction (and during if 'Rotate Once' is disabled")]
		[Range(0f, 1f)]
		public float threeDOFWeight = 1f;

		// Token: 0x04000927 RID: 2343
		[Tooltip("If true, will twist/swing around the pivot only once at the start of the interaction. If false, will continue rotating throuout the whole interaction.")]
		public bool rotateOnce = true;

		// Token: 0x04000928 RID: 2344
		private Quaternion defaultLocalRotation;

		// Token: 0x04000929 RID: 2345
		private Transform lastPivot;

		// Token: 0x0200020D RID: 525
		[Serializable]
		public enum RotationMode
		{
			// Token: 0x04000FC1 RID: 4033
			TwoDOF,
			// Token: 0x04000FC2 RID: 4034
			ThreeDOF
		}

		// Token: 0x0200020E RID: 526
		[Serializable]
		public class Multiplier
		{
			// Token: 0x06001121 RID: 4385 RVA: 0x0006B6BB File Offset: 0x000698BB
			public Multiplier()
			{
			}

			// Token: 0x04000FC3 RID: 4035
			[Tooltip("The curve type (InteractionObject.WeightCurve.Type).")]
			public InteractionObject.WeightCurve.Type curve;

			// Token: 0x04000FC4 RID: 4036
			[Tooltip("Multiplier of the curve's value.")]
			public float multiplier;
		}
	}
}

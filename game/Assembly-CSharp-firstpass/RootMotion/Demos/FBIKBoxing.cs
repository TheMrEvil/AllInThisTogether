using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000137 RID: 311
	public class FBIKBoxing : MonoBehaviour
	{
		// Token: 0x06000CE1 RID: 3297 RVA: 0x0005755E File Offset: 0x0005575E
		private void Start()
		{
			this.animator = base.GetComponent<Animator>();
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0005756C File Offset: 0x0005576C
		private void LateUpdate()
		{
			float @float = this.animator.GetFloat("HitWeight");
			this.ik.solver.GetEffector(this.effector).position = this.target.position;
			this.ik.solver.GetEffector(this.effector).positionWeight = @float * this.weight;
			if (this.aim != null)
			{
				this.aim.solver.transform.LookAt(this.pin.position);
				this.aim.solver.IKPosition = this.target.position;
				this.aim.solver.IKPositionWeight = this.aimWeight.Evaluate(@float) * this.weight;
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0005763F File Offset: 0x0005583F
		public FBIKBoxing()
		{
		}

		// Token: 0x04000A6B RID: 2667
		[Tooltip("The target we want to hit")]
		public Transform target;

		// Token: 0x04000A6C RID: 2668
		[Tooltip("The pin Transform is used to reference the exact hit point in the animation (used by AimIK to aim the upper body to follow the target).In Legacy and Generic modes you can just create and position a reference point in your animating software and include it in the FBX. Then in Unity if you added a GameObject with the exact same name under the character's root, it would be animated to the required position.In Humanoid mode however, Mecanim loses track of any Transform that does not belong to the avatar, so in this case the pin point has to be manually set inside the Unity Editor.")]
		public Transform pin;

		// Token: 0x04000A6D RID: 2669
		[Tooltip("The Full Body Biped IK component")]
		public FullBodyBipedIK ik;

		// Token: 0x04000A6E RID: 2670
		[Tooltip("The Aim IK component. Aim IK is ust used for following the target slightly with the body.")]
		public AimIK aim;

		// Token: 0x04000A6F RID: 2671
		[Tooltip("The master weight")]
		public float weight;

		// Token: 0x04000A70 RID: 2672
		[Tooltip("The effector type of the punching hand")]
		public FullBodyBipedEffector effector;

		// Token: 0x04000A71 RID: 2673
		[Tooltip("Weight of aiming the body to follow the target")]
		public AnimationCurve aimWeight;

		// Token: 0x04000A72 RID: 2674
		private Animator animator;
	}
}

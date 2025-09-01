using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace RootMotion.FinalIK
{
	// Token: 0x02000102 RID: 258
	[HelpURL("https://www.youtube.com/watch?v=r5jiZnsDH3M")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/Interaction System/Interaction Object")]
	public class InteractionObject : MonoBehaviour
	{
		// Token: 0x06000B4F RID: 2895 RVA: 0x0004D741 File Offset: 0x0004B941
		[ContextMenu("User Manual")]
		private void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page10.html");
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0004D74D File Offset: 0x0004B94D
		[ContextMenu("Scrpt Reference")]
		private void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_interaction_object.html");
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0004D759 File Offset: 0x0004B959
		[ContextMenu("TUTORIAL VIDEO (PART 1: BASICS)")]
		private void OpenTutorial1()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=r5jiZnsDH3M");
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0004D765 File Offset: 0x0004B965
		[ContextMenu("TUTORIAL VIDEO (PART 2: PICKING UP...)")]
		private void OpenTutorial2()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=eP9-zycoHLk");
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0004D771 File Offset: 0x0004B971
		[ContextMenu("TUTORIAL VIDEO (PART 3: ANIMATION)")]
		private void OpenTutorial3()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=sQfB2RcT1T4&index=14&list=PLVxSIA1OaTOu8Nos3CalXbJ2DrKnntMv6");
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0004D77D File Offset: 0x0004B97D
		[ContextMenu("TUTORIAL VIDEO (PART 4: TRIGGERS)")]
		private void OpenTutorial4()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=-TDZpNjt2mk&index=15&list=PLVxSIA1OaTOu8Nos3CalXbJ2DrKnntMv6");
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0004D789 File Offset: 0x0004B989
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0004D795 File Offset: 0x0004B995
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0004D7A1 File Offset: 0x0004B9A1
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x0004D7A9 File Offset: 0x0004B9A9
		public float length
		{
			[CompilerGenerated]
			get
			{
				return this.<length>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<length>k__BackingField = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0004D7B2 File Offset: 0x0004B9B2
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x0004D7BA File Offset: 0x0004B9BA
		public InteractionSystem lastUsedInteractionSystem
		{
			[CompilerGenerated]
			get
			{
				return this.<lastUsedInteractionSystem>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<lastUsedInteractionSystem>k__BackingField = value;
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0004D7C4 File Offset: 0x0004B9C4
		public void Initiate()
		{
			for (int i = 0; i < this.weightCurves.Length; i++)
			{
				if (this.weightCurves[i].curve.length > 0)
				{
					float time = this.weightCurves[i].curve.keys[this.weightCurves[i].curve.length - 1].time;
					this.length = Mathf.Clamp(this.length, time, this.length);
				}
			}
			for (int j = 0; j < this.events.Length; j++)
			{
				this.length = Mathf.Clamp(this.length, this.events[j].time, this.length);
			}
			this.targets = this.targetsRoot.GetComponentsInChildren<InteractionTarget>();
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0004D88A File Offset: 0x0004BA8A
		public Transform lookAtTarget
		{
			get
			{
				if (this.otherLookAtTarget != null)
				{
					return this.otherLookAtTarget;
				}
				return base.transform;
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0004D8A8 File Offset: 0x0004BAA8
		public InteractionTarget GetTarget(FullBodyBipedEffector effectorType, InteractionSystem interactionSystem)
		{
			if (interactionSystem.CompareTag(string.Empty) || interactionSystem.CompareTag(""))
			{
				foreach (InteractionTarget interactionTarget in this.targets)
				{
					if (interactionTarget.effectorType == effectorType)
					{
						return interactionTarget;
					}
				}
				return null;
			}
			foreach (InteractionTarget interactionTarget2 in this.targets)
			{
				if (interactionTarget2.effectorType == effectorType && interactionTarget2.CompareTag(interactionSystem.tag))
				{
					return interactionTarget2;
				}
			}
			return null;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0004D928 File Offset: 0x0004BB28
		public bool CurveUsed(InteractionObject.WeightCurve.Type type)
		{
			InteractionObject.WeightCurve[] array = this.weightCurves;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].type == type)
				{
					return true;
				}
			}
			InteractionObject.Multiplier[] array2 = this.multipliers;
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i].result == type)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0004D97A File Offset: 0x0004BB7A
		public InteractionTarget[] GetTargets()
		{
			return this.targets;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0004D984 File Offset: 0x0004BB84
		public Transform GetTarget(FullBodyBipedEffector effectorType, string tag)
		{
			if (tag == string.Empty || tag == "")
			{
				return this.GetTarget(effectorType);
			}
			for (int i = 0; i < this.targets.Length; i++)
			{
				if (this.targets[i].effectorType == effectorType && this.targets[i].CompareTag(tag))
				{
					return this.targets[i].transform;
				}
			}
			return base.transform;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0004D9FA File Offset: 0x0004BBFA
		public void OnStartInteraction(InteractionSystem interactionSystem)
		{
			this.lastUsedInteractionSystem = interactionSystem;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0004DA04 File Offset: 0x0004BC04
		public void Apply(IKSolverFullBodyBiped solver, FullBodyBipedEffector effector, InteractionTarget target, float timer, float weight)
		{
			for (int i = 0; i < this.weightCurves.Length; i++)
			{
				float num = (target == null) ? 1f : target.GetValue(this.weightCurves[i].type);
				this.Apply(solver, effector, this.weightCurves[i].type, this.weightCurves[i].GetValue(timer), weight * num);
			}
			for (int j = 0; j < this.multipliers.Length; j++)
			{
				if (this.multipliers[j].curve == this.multipliers[j].result && !Warning.logged)
				{
					Warning.Log("InteractionObject Multiplier 'Curve' " + this.multipliers[j].curve.ToString() + "and 'Result' are the same.", base.transform, false);
				}
				int weightCurveIndex = this.GetWeightCurveIndex(this.multipliers[j].curve);
				if (weightCurveIndex != -1)
				{
					float num2 = (target == null) ? 1f : target.GetValue(this.multipliers[j].result);
					this.Apply(solver, effector, this.multipliers[j].result, this.multipliers[j].GetValue(this.weightCurves[weightCurveIndex], timer), weight * num2);
				}
				else if (!Warning.logged)
				{
					Warning.Log("InteractionObject Multiplier curve " + this.multipliers[j].curve.ToString() + "does not exist.", base.transform, false);
				}
			}
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0004DB8C File Offset: 0x0004BD8C
		public float GetValue(InteractionObject.WeightCurve.Type weightCurveType, InteractionTarget target, float timer)
		{
			int weightCurveIndex = this.GetWeightCurveIndex(weightCurveType);
			if (weightCurveIndex != -1)
			{
				float num = (target == null) ? 1f : target.GetValue(weightCurveType);
				return this.weightCurves[weightCurveIndex].GetValue(timer) * num;
			}
			for (int i = 0; i < this.multipliers.Length; i++)
			{
				if (this.multipliers[i].result == weightCurveType)
				{
					int weightCurveIndex2 = this.GetWeightCurveIndex(this.multipliers[i].curve);
					if (weightCurveIndex2 != -1)
					{
						float num2 = (target == null) ? 1f : target.GetValue(this.multipliers[i].result);
						return this.multipliers[i].GetValue(this.weightCurves[weightCurveIndex2], timer) * num2;
					}
				}
			}
			return 0f;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0004DC4D File Offset: 0x0004BE4D
		public Transform targetsRoot
		{
			get
			{
				if (this.otherTargetsRoot != null)
				{
					return this.otherTargetsRoot;
				}
				return base.transform;
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0004DC6A File Offset: 0x0004BE6A
		private void Start()
		{
			this.Initiate();
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0004DC74 File Offset: 0x0004BE74
		private void Apply(IKSolverFullBodyBiped solver, FullBodyBipedEffector effector, InteractionObject.WeightCurve.Type type, float value, float weight)
		{
			switch (type)
			{
			case InteractionObject.WeightCurve.Type.PositionWeight:
				solver.GetEffector(effector).positionWeight = Mathf.Lerp(solver.GetEffector(effector).positionWeight, value, weight);
				return;
			case InteractionObject.WeightCurve.Type.RotationWeight:
				solver.GetEffector(effector).rotationWeight = Mathf.Lerp(solver.GetEffector(effector).rotationWeight, value, weight);
				return;
			case InteractionObject.WeightCurve.Type.PositionOffsetX:
				solver.GetEffector(effector).position += ((this.positionOffsetSpace != null) ? this.positionOffsetSpace.rotation : solver.GetRoot().rotation) * Vector3.right * value * weight;
				return;
			case InteractionObject.WeightCurve.Type.PositionOffsetY:
				solver.GetEffector(effector).position += ((this.positionOffsetSpace != null) ? this.positionOffsetSpace.rotation : solver.GetRoot().rotation) * Vector3.up * value * weight;
				return;
			case InteractionObject.WeightCurve.Type.PositionOffsetZ:
				solver.GetEffector(effector).position += ((this.positionOffsetSpace != null) ? this.positionOffsetSpace.rotation : solver.GetRoot().rotation) * Vector3.forward * value * weight;
				return;
			case InteractionObject.WeightCurve.Type.Pull:
				solver.GetChain(effector).pull = Mathf.Lerp(solver.GetChain(effector).pull, value, weight);
				return;
			case InteractionObject.WeightCurve.Type.Reach:
				solver.GetChain(effector).reach = Mathf.Lerp(solver.GetChain(effector).reach, value, weight);
				return;
			case InteractionObject.WeightCurve.Type.RotateBoneWeight:
			case InteractionObject.WeightCurve.Type.PoserWeight:
				return;
			case InteractionObject.WeightCurve.Type.Push:
				solver.GetChain(effector).push = Mathf.Lerp(solver.GetChain(effector).push, value, weight);
				return;
			case InteractionObject.WeightCurve.Type.PushParent:
				solver.GetChain(effector).pushParent = Mathf.Lerp(solver.GetChain(effector).pushParent, value, weight);
				return;
			case InteractionObject.WeightCurve.Type.BendGoalWeight:
				solver.GetChain(effector).bendConstraint.weight = Mathf.Lerp(solver.GetChain(effector).bendConstraint.weight, value, weight);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0004DEB4 File Offset: 0x0004C0B4
		private Transform GetTarget(FullBodyBipedEffector effectorType)
		{
			for (int i = 0; i < this.targets.Length; i++)
			{
				if (this.targets[i].effectorType == effectorType)
				{
					return this.targets[i].transform;
				}
			}
			return base.transform;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0004DEF8 File Offset: 0x0004C0F8
		private int GetWeightCurveIndex(InteractionObject.WeightCurve.Type weightCurveType)
		{
			for (int i = 0; i < this.weightCurves.Length; i++)
			{
				if (this.weightCurves[i].type == weightCurveType)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0004DF2C File Offset: 0x0004C12C
		private int GetMultiplierIndex(InteractionObject.WeightCurve.Type weightCurveType)
		{
			for (int i = 0; i < this.multipliers.Length; i++)
			{
				if (this.multipliers[i].result == weightCurveType)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0004DF5F File Offset: 0x0004C15F
		public InteractionObject()
		{
		}

		// Token: 0x040008FD RID: 2301
		[Tooltip("If the Interaction System has a 'Look At' LookAtIK component assigned, will use it to make the character look at the specified Transform. If unassigned, will look at this GameObject.")]
		public Transform otherLookAtTarget;

		// Token: 0x040008FE RID: 2302
		[Tooltip("The root Transform of the InteractionTargets. If null, will use this GameObject. GetComponentsInChildren<InteractionTarget>() will be used at initiation to find all InteractionTargets associated with this InteractionObject.")]
		public Transform otherTargetsRoot;

		// Token: 0x040008FF RID: 2303
		[Tooltip("If assigned, all PositionOffset channels will be applied in the rotation space of this Transform. If not, they will be in the rotation space of the character.")]
		public Transform positionOffsetSpace;

		// Token: 0x04000900 RID: 2304
		public InteractionObject.WeightCurve[] weightCurves;

		// Token: 0x04000901 RID: 2305
		public InteractionObject.Multiplier[] multipliers;

		// Token: 0x04000902 RID: 2306
		public InteractionObject.InteractionEvent[] events;

		// Token: 0x04000903 RID: 2307
		[CompilerGenerated]
		private float <length>k__BackingField;

		// Token: 0x04000904 RID: 2308
		[CompilerGenerated]
		private InteractionSystem <lastUsedInteractionSystem>k__BackingField;

		// Token: 0x04000905 RID: 2309
		private InteractionTarget[] targets = new InteractionTarget[0];

		// Token: 0x02000206 RID: 518
		[Serializable]
		public class InteractionEvent
		{
			// Token: 0x0600110D RID: 4365 RVA: 0x0006B498 File Offset: 0x00069698
			public void Activate(Transform t)
			{
				this.unityEvent.Invoke();
				InteractionObject.AnimatorEvent[] array = this.animations;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Activate(this.pickUp);
				}
				InteractionObject.Message[] array2 = this.messages;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].Send(t);
				}
			}

			// Token: 0x0600110E RID: 4366 RVA: 0x0006B4F1 File Offset: 0x000696F1
			public InteractionEvent()
			{
			}

			// Token: 0x04000FAB RID: 4011
			[Tooltip("The time of the event since interaction start.")]
			public float time;

			// Token: 0x04000FAC RID: 4012
			[Tooltip("If true, the interaction will be paused on this event. The interaction can be resumed by InteractionSystem.ResumeInteraction() or InteractionSystem.ResumeAll;")]
			public bool pause;

			// Token: 0x04000FAD RID: 4013
			[Tooltip("If true, the object will be parented to the effector bone on this event. Note that picking up like this can be done by only a single effector at a time. If you wish to pick up an object with both hands, see the Interaction PickUp2Handed demo scene.")]
			public bool pickUp;

			// Token: 0x04000FAE RID: 4014
			[Tooltip("The animations called on this event.")]
			public InteractionObject.AnimatorEvent[] animations;

			// Token: 0x04000FAF RID: 4015
			[Tooltip("The messages sent on this event using GameObject.SendMessage().")]
			public InteractionObject.Message[] messages;

			// Token: 0x04000FB0 RID: 4016
			[Tooltip("The UnityEvent to invoke on this event.")]
			public UnityEvent unityEvent;
		}

		// Token: 0x02000207 RID: 519
		[Serializable]
		public class Message
		{
			// Token: 0x0600110F RID: 4367 RVA: 0x0006B4FC File Offset: 0x000696FC
			public void Send(Transform t)
			{
				if (this.recipient == null)
				{
					return;
				}
				if (this.function == string.Empty || this.function == "")
				{
					return;
				}
				this.recipient.SendMessage(this.function, t, SendMessageOptions.RequireReceiver);
			}

			// Token: 0x06001110 RID: 4368 RVA: 0x0006B550 File Offset: 0x00069750
			public Message()
			{
			}

			// Token: 0x04000FB1 RID: 4017
			[Tooltip("The name of the function called.")]
			public string function;

			// Token: 0x04000FB2 RID: 4018
			[Tooltip("The recipient game object.")]
			public GameObject recipient;

			// Token: 0x04000FB3 RID: 4019
			private const string empty = "";
		}

		// Token: 0x02000208 RID: 520
		[Serializable]
		public class AnimatorEvent
		{
			// Token: 0x06001111 RID: 4369 RVA: 0x0006B558 File Offset: 0x00069758
			public void Activate(bool pickUp)
			{
				if (this.animator != null)
				{
					if (pickUp)
					{
						this.animator.applyRootMotion = false;
					}
					this.Activate(this.animator);
				}
				if (this.animation != null)
				{
					this.Activate(this.animation);
				}
			}

			// Token: 0x06001112 RID: 4370 RVA: 0x0006B5A8 File Offset: 0x000697A8
			private void Activate(Animator animator)
			{
				if (this.animationState == "")
				{
					return;
				}
				if (this.resetNormalizedTime)
				{
					animator.CrossFade(this.animationState, this.crossfadeTime, this.layer, 0f);
					return;
				}
				animator.CrossFade(this.animationState, this.crossfadeTime, this.layer);
			}

			// Token: 0x06001113 RID: 4371 RVA: 0x0006B608 File Offset: 0x00069808
			private void Activate(Animation animation)
			{
				if (this.animationState == "")
				{
					return;
				}
				if (this.resetNormalizedTime)
				{
					animation[this.animationState].normalizedTime = 0f;
				}
				animation[this.animationState].layer = this.layer;
				animation.CrossFade(this.animationState, this.crossfadeTime);
			}

			// Token: 0x06001114 RID: 4372 RVA: 0x0006B66F File Offset: 0x0006986F
			public AnimatorEvent()
			{
			}

			// Token: 0x04000FB4 RID: 4020
			[Tooltip("The Animator component that will receive the AnimatorEvents.")]
			public Animator animator;

			// Token: 0x04000FB5 RID: 4021
			[Tooltip("The Animation component that will receive the AnimatorEvents (Legacy).")]
			public Animation animation;

			// Token: 0x04000FB6 RID: 4022
			[Tooltip("The name of the animation state.")]
			public string animationState;

			// Token: 0x04000FB7 RID: 4023
			[Tooltip("The crossfading time.")]
			public float crossfadeTime = 0.3f;

			// Token: 0x04000FB8 RID: 4024
			[Tooltip("The layer of the animation state (if using Legacy, the animation state will be forced to this layer).")]
			public int layer;

			// Token: 0x04000FB9 RID: 4025
			[Tooltip("Should the animation always start from 0 normalized time?")]
			public bool resetNormalizedTime;

			// Token: 0x04000FBA RID: 4026
			private const string empty = "";
		}

		// Token: 0x02000209 RID: 521
		[Serializable]
		public class WeightCurve
		{
			// Token: 0x06001115 RID: 4373 RVA: 0x0006B682 File Offset: 0x00069882
			public float GetValue(float timer)
			{
				return this.curve.Evaluate(timer);
			}

			// Token: 0x06001116 RID: 4374 RVA: 0x0006B690 File Offset: 0x00069890
			public WeightCurve()
			{
			}

			// Token: 0x04000FBB RID: 4027
			[Tooltip("The type of the curve (InteractionObject.WeightCurve.Type).")]
			public InteractionObject.WeightCurve.Type type;

			// Token: 0x04000FBC RID: 4028
			[Tooltip("The weight curve.")]
			public AnimationCurve curve;

			// Token: 0x02000248 RID: 584
			[Serializable]
			public enum Type
			{
				// Token: 0x040010FA RID: 4346
				PositionWeight,
				// Token: 0x040010FB RID: 4347
				RotationWeight,
				// Token: 0x040010FC RID: 4348
				PositionOffsetX,
				// Token: 0x040010FD RID: 4349
				PositionOffsetY,
				// Token: 0x040010FE RID: 4350
				PositionOffsetZ,
				// Token: 0x040010FF RID: 4351
				Pull,
				// Token: 0x04001100 RID: 4352
				Reach,
				// Token: 0x04001101 RID: 4353
				RotateBoneWeight,
				// Token: 0x04001102 RID: 4354
				Push,
				// Token: 0x04001103 RID: 4355
				PushParent,
				// Token: 0x04001104 RID: 4356
				PoserWeight,
				// Token: 0x04001105 RID: 4357
				BendGoalWeight
			}
		}

		// Token: 0x0200020A RID: 522
		[Serializable]
		public class Multiplier
		{
			// Token: 0x06001117 RID: 4375 RVA: 0x0006B698 File Offset: 0x00069898
			public float GetValue(InteractionObject.WeightCurve weightCurve, float timer)
			{
				return weightCurve.GetValue(timer) * this.multiplier;
			}

			// Token: 0x06001118 RID: 4376 RVA: 0x0006B6A8 File Offset: 0x000698A8
			public Multiplier()
			{
			}

			// Token: 0x04000FBD RID: 4029
			[Tooltip("The curve type to multiply.")]
			public InteractionObject.WeightCurve.Type curve;

			// Token: 0x04000FBE RID: 4030
			[Tooltip("The multiplier of the curve's value.")]
			public float multiplier = 1f;

			// Token: 0x04000FBF RID: 4031
			[Tooltip("The resulting value will be applied to this channel.")]
			public InteractionObject.WeightCurve.Type result;
		}
	}
}

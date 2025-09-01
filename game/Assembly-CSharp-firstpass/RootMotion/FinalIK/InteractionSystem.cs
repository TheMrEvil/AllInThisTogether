using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace RootMotion.FinalIK
{
	// Token: 0x02000103 RID: 259
	[HelpURL("https://www.youtube.com/watch?v=r5jiZnsDH3M")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/Interaction System/Interaction System")]
	public class InteractionSystem : MonoBehaviour
	{
		// Token: 0x06000B6B RID: 2923 RVA: 0x0004DF73 File Offset: 0x0004C173
		[ContextMenu("User Manual")]
		private void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page10.html");
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0004DF7F File Offset: 0x0004C17F
		[ContextMenu("Scrpt Reference")]
		private void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_interaction_system.html");
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0004DF8B File Offset: 0x0004C18B
		[ContextMenu("TUTORIAL VIDEO (PART 1: BASICS)")]
		private void OpenTutorial1()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=r5jiZnsDH3M");
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0004DF97 File Offset: 0x0004C197
		[ContextMenu("TUTORIAL VIDEO (PART 2: PICKING UP...)")]
		private void OpenTutorial2()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=eP9-zycoHLk");
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0004DFA3 File Offset: 0x0004C1A3
		[ContextMenu("TUTORIAL VIDEO (PART 3: ANIMATION)")]
		private void OpenTutorial3()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=sQfB2RcT1T4&index=14&list=PLVxSIA1OaTOu8Nos3CalXbJ2DrKnntMv6");
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0004DFAF File Offset: 0x0004C1AF
		[ContextMenu("TUTORIAL VIDEO (PART 4: TRIGGERS)")]
		private void OpenTutorial4()
		{
			Application.OpenURL("https://www.youtube.com/watch?v=-TDZpNjt2mk&index=15&list=PLVxSIA1OaTOu8Nos3CalXbJ2DrKnntMv6");
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0004DFBB File Offset: 0x0004C1BB
		[ContextMenu("Support")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0004DFC7 File Offset: 0x0004C1C7
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0004DFD4 File Offset: 0x0004C1D4
		public bool inInteraction
		{
			get
			{
				if (!this.IsValid(true))
				{
					return false;
				}
				for (int i = 0; i < this.interactionEffectors.Length; i++)
				{
					if (this.interactionEffectors[i].inInteraction && !this.interactionEffectors[i].isPaused)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0004E020 File Offset: 0x0004C220
		public bool IsInInteraction(FullBodyBipedEffector effectorType)
		{
			if (!this.IsValid(true))
			{
				return false;
			}
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				if (this.interactionEffectors[i].effectorType == effectorType)
				{
					return this.interactionEffectors[i].inInteraction && !this.interactionEffectors[i].isPaused;
				}
			}
			return false;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0004E080 File Offset: 0x0004C280
		public bool IsPaused(FullBodyBipedEffector effectorType)
		{
			if (!this.IsValid(true))
			{
				return false;
			}
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				if (this.interactionEffectors[i].effectorType == effectorType)
				{
					return this.interactionEffectors[i].inInteraction && this.interactionEffectors[i].isPaused;
				}
			}
			return false;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0004E0DC File Offset: 0x0004C2DC
		public bool IsPaused()
		{
			if (!this.IsValid(true))
			{
				return false;
			}
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				if (this.interactionEffectors[i].inInteraction && this.interactionEffectors[i].isPaused)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0004E128 File Offset: 0x0004C328
		public bool IsInSync()
		{
			if (!this.IsValid(true))
			{
				return false;
			}
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				if (this.interactionEffectors[i].isPaused)
				{
					for (int j = 0; j < this.interactionEffectors.Length; j++)
					{
						if (j != i && this.interactionEffectors[j].inInteraction && !this.interactionEffectors[j].isPaused)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0004E19C File Offset: 0x0004C39C
		public bool StartInteraction(FullBodyBipedEffector effectorType, InteractionObject interactionObject, bool interrupt)
		{
			if (!this.IsValid(true))
			{
				return false;
			}
			if (interactionObject == null)
			{
				return false;
			}
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				if (this.interactionEffectors[i].effectorType == effectorType)
				{
					return this.interactionEffectors[i].Start(interactionObject, this.targetTag, this.fadeInTime, interrupt);
				}
			}
			return false;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0004E200 File Offset: 0x0004C400
		public bool PauseInteraction(FullBodyBipedEffector effectorType)
		{
			if (!this.IsValid(true))
			{
				return false;
			}
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				if (this.interactionEffectors[i].effectorType == effectorType)
				{
					return this.interactionEffectors[i].Pause();
				}
			}
			return false;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0004E24C File Offset: 0x0004C44C
		public bool ResumeInteraction(FullBodyBipedEffector effectorType)
		{
			if (!this.IsValid(true))
			{
				return false;
			}
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				if (this.interactionEffectors[i].effectorType == effectorType)
				{
					return this.interactionEffectors[i].Resume();
				}
			}
			return false;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0004E298 File Offset: 0x0004C498
		public bool StopInteraction(FullBodyBipedEffector effectorType)
		{
			if (!this.IsValid(true))
			{
				return false;
			}
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				if (this.interactionEffectors[i].effectorType == effectorType)
				{
					return this.interactionEffectors[i].Stop();
				}
			}
			return false;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0004E2E4 File Offset: 0x0004C4E4
		public void PauseAll()
		{
			if (!this.IsValid(true))
			{
				return;
			}
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				this.interactionEffectors[i].Pause();
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0004E31C File Offset: 0x0004C51C
		public void ResumeAll()
		{
			if (!this.IsValid(true))
			{
				return;
			}
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				this.interactionEffectors[i].Resume();
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0004E354 File Offset: 0x0004C554
		public void StopAll()
		{
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				this.interactionEffectors[i].Stop();
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0004E384 File Offset: 0x0004C584
		public InteractionObject GetInteractionObject(FullBodyBipedEffector effectorType)
		{
			if (!this.IsValid(true))
			{
				return null;
			}
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				if (this.interactionEffectors[i].effectorType == effectorType)
				{
					return this.interactionEffectors[i].interactionObject;
				}
			}
			return null;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0004E3D0 File Offset: 0x0004C5D0
		public float GetProgress(FullBodyBipedEffector effectorType)
		{
			if (!this.IsValid(true))
			{
				return 0f;
			}
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				if (this.interactionEffectors[i].effectorType == effectorType)
				{
					return this.interactionEffectors[i].progress;
				}
			}
			return 0f;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0004E424 File Offset: 0x0004C624
		public float GetMinActiveProgress()
		{
			if (!this.IsValid(true))
			{
				return 0f;
			}
			float num = 1f;
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				if (this.interactionEffectors[i].inInteraction)
				{
					float progress = this.interactionEffectors[i].progress;
					if (progress > 0f && progress < num)
					{
						num = progress;
					}
				}
			}
			return num;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0004E488 File Offset: 0x0004C688
		public bool TriggerInteraction(int index, bool interrupt)
		{
			if (!this.IsValid(true))
			{
				return false;
			}
			if (!this.TriggerIndexIsValid(index))
			{
				return false;
			}
			bool result = true;
			InteractionTrigger.Range range = this.triggersInRange[index].ranges[this.bestRangeIndexes[index]];
			for (int i = 0; i < range.interactions.Length; i++)
			{
				for (int j = 0; j < range.interactions[i].effectors.Length; j++)
				{
					if (!this.StartInteraction(range.interactions[i].effectors[j], range.interactions[i].interactionObject, interrupt))
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0004E524 File Offset: 0x0004C724
		public bool TriggerInteraction(int index, bool interrupt, out InteractionObject interactionObject)
		{
			interactionObject = null;
			if (!this.IsValid(true))
			{
				return false;
			}
			if (!this.TriggerIndexIsValid(index))
			{
				return false;
			}
			bool result = true;
			InteractionTrigger.Range range = this.triggersInRange[index].ranges[this.bestRangeIndexes[index]];
			for (int i = 0; i < range.interactions.Length; i++)
			{
				for (int j = 0; j < range.interactions[i].effectors.Length; j++)
				{
					interactionObject = range.interactions[i].interactionObject;
					if (!this.StartInteraction(range.interactions[i].effectors[j], interactionObject, interrupt))
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0004E5C4 File Offset: 0x0004C7C4
		public bool TriggerInteraction(int index, bool interrupt, out InteractionTarget interactionTarget)
		{
			interactionTarget = null;
			if (!this.IsValid(true))
			{
				return false;
			}
			if (!this.TriggerIndexIsValid(index))
			{
				return false;
			}
			bool result = true;
			InteractionTrigger.Range range = this.triggersInRange[index].ranges[this.bestRangeIndexes[index]];
			for (int i = 0; i < range.interactions.Length; i++)
			{
				for (int j = 0; j < range.interactions[i].effectors.Length; j++)
				{
					InteractionObject interactionObject = range.interactions[i].interactionObject;
					Transform target = interactionObject.GetTarget(range.interactions[i].effectors[j], base.tag);
					if (target != null)
					{
						interactionTarget = target.GetComponent<InteractionTarget>();
					}
					if (!this.StartInteraction(range.interactions[i].effectors[j], interactionObject, interrupt))
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0004E698 File Offset: 0x0004C898
		public InteractionTrigger.Range GetClosestInteractionRange()
		{
			if (!this.IsValid(true))
			{
				return null;
			}
			int closestTriggerIndex = this.GetClosestTriggerIndex();
			if (closestTriggerIndex < 0 || closestTriggerIndex >= this.triggersInRange.Count)
			{
				return null;
			}
			return this.triggersInRange[closestTriggerIndex].ranges[this.bestRangeIndexes[closestTriggerIndex]];
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0004E6EC File Offset: 0x0004C8EC
		public InteractionObject GetClosestInteractionObjectInRange()
		{
			InteractionTrigger.Range closestInteractionRange = this.GetClosestInteractionRange();
			if (closestInteractionRange == null)
			{
				return null;
			}
			return closestInteractionRange.interactions[0].interactionObject;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0004E714 File Offset: 0x0004C914
		public InteractionTarget GetClosestInteractionTargetInRange()
		{
			InteractionTrigger.Range closestInteractionRange = this.GetClosestInteractionRange();
			if (closestInteractionRange == null)
			{
				return null;
			}
			return closestInteractionRange.interactions[0].interactionObject.GetTarget(closestInteractionRange.interactions[0].effectors[0], this);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0004E750 File Offset: 0x0004C950
		public InteractionObject[] GetClosestInteractionObjectsInRange()
		{
			InteractionTrigger.Range closestInteractionRange = this.GetClosestInteractionRange();
			if (closestInteractionRange == null)
			{
				return new InteractionObject[0];
			}
			InteractionObject[] array = new InteractionObject[closestInteractionRange.interactions.Length];
			for (int i = 0; i < closestInteractionRange.interactions.Length; i++)
			{
				array[i] = closestInteractionRange.interactions[i].interactionObject;
			}
			return array;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0004E7A0 File Offset: 0x0004C9A0
		public InteractionTarget[] GetClosestInteractionTargetsInRange()
		{
			InteractionTrigger.Range closestInteractionRange = this.GetClosestInteractionRange();
			if (closestInteractionRange == null)
			{
				return new InteractionTarget[0];
			}
			List<InteractionTarget> list = new List<InteractionTarget>();
			foreach (InteractionTrigger.Range.Interaction interaction in closestInteractionRange.interactions)
			{
				foreach (FullBodyBipedEffector effectorType in interaction.effectors)
				{
					list.Add(interaction.interactionObject.GetTarget(effectorType, this));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0004E81C File Offset: 0x0004CA1C
		public bool TriggerEffectorsReady(int index)
		{
			if (!this.IsValid(true))
			{
				return false;
			}
			if (!this.TriggerIndexIsValid(index))
			{
				return false;
			}
			for (int i = 0; i < this.triggersInRange[index].ranges.Length; i++)
			{
				InteractionTrigger.Range range = this.triggersInRange[index].ranges[i];
				for (int j = 0; j < range.interactions.Length; j++)
				{
					for (int k = 0; k < range.interactions[j].effectors.Length; k++)
					{
						if (this.IsInInteraction(range.interactions[j].effectors[k]))
						{
							return false;
						}
					}
				}
				for (int l = 0; l < range.interactions.Length; l++)
				{
					for (int m = 0; m < range.interactions[l].effectors.Length; m++)
					{
						if (this.IsPaused(range.interactions[l].effectors[m]))
						{
							for (int n = 0; n < range.interactions[l].effectors.Length; n++)
							{
								if (n != m && !this.IsPaused(range.interactions[l].effectors[n]))
								{
									return false;
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0004E950 File Offset: 0x0004CB50
		public InteractionTrigger.Range GetTriggerRange(int index)
		{
			if (!this.IsValid(true))
			{
				return null;
			}
			if (index < 0 || index >= this.bestRangeIndexes.Count)
			{
				Warning.Log("Index out of range.", base.transform, false);
				return null;
			}
			return this.triggersInRange[index].ranges[this.bestRangeIndexes[index]];
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0004E9AC File Offset: 0x0004CBAC
		public int GetClosestTriggerIndex()
		{
			if (!this.IsValid(true))
			{
				return -1;
			}
			if (this.triggersInRange.Count == 0)
			{
				return -1;
			}
			if (this.triggersInRange.Count == 1)
			{
				return 0;
			}
			int result = -1;
			float num = float.PositiveInfinity;
			for (int i = 0; i < this.triggersInRange.Count; i++)
			{
				if (this.triggersInRange[i] != null)
				{
					float num2 = Vector3.SqrMagnitude(this.triggersInRange[i].transform.position - base.transform.position);
					if (num2 < num)
					{
						result = i;
						num = num2;
					}
				}
			}
			return result;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0004EA4A File Offset: 0x0004CC4A
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x0004EA52 File Offset: 0x0004CC52
		public FullBodyBipedIK ik
		{
			get
			{
				return this.fullBody;
			}
			set
			{
				this.fullBody = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0004EA5B File Offset: 0x0004CC5B
		// (set) Token: 0x06000B90 RID: 2960 RVA: 0x0004EA63 File Offset: 0x0004CC63
		public List<InteractionTrigger> triggersInRange
		{
			[CompilerGenerated]
			get
			{
				return this.<triggersInRange>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<triggersInRange>k__BackingField = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0004EA6C File Offset: 0x0004CC6C
		// (set) Token: 0x06000B92 RID: 2962 RVA: 0x0004EA74 File Offset: 0x0004CC74
		public bool initiated
		{
			[CompilerGenerated]
			get
			{
				return this.<initiated>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<initiated>k__BackingField = value;
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0004EA80 File Offset: 0x0004CC80
		public void Start()
		{
			if (this.fullBody == null)
			{
				this.fullBody = base.GetComponent<FullBodyBipedIK>();
			}
			if (this.fullBody == null)
			{
				Warning.Log("InteractionSystem can not find a FullBodyBipedIK component", base.transform, false);
				return;
			}
			IKSolverFullBodyBiped solver = this.fullBody.solver;
			solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPreUpdate, new IKSolver.UpdateDelegate(this.OnPreFBBIK));
			IKSolverFullBodyBiped solver2 = this.fullBody.solver;
			solver2.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver2.OnPostUpdate, new IKSolver.UpdateDelegate(this.OnPostFBBIK));
			IKSolverFullBodyBiped solver3 = this.fullBody.solver;
			solver3.OnFixTransforms = (IKSolver.UpdateDelegate)Delegate.Combine(solver3.OnFixTransforms, new IKSolver.UpdateDelegate(this.OnFixTransforms));
			this.OnInteractionStart = (InteractionSystem.InteractionDelegate)Delegate.Combine(this.OnInteractionStart, new InteractionSystem.InteractionDelegate(this.LookAtInteraction));
			this.OnInteractionPause = (InteractionSystem.InteractionDelegate)Delegate.Combine(this.OnInteractionPause, new InteractionSystem.InteractionDelegate(this.InteractionPause));
			this.OnInteractionResume = (InteractionSystem.InteractionDelegate)Delegate.Combine(this.OnInteractionResume, new InteractionSystem.InteractionDelegate(this.InteractionResume));
			this.OnInteractionStop = (InteractionSystem.InteractionDelegate)Delegate.Combine(this.OnInteractionStop, new InteractionSystem.InteractionDelegate(this.InteractionStop));
			InteractionEffector[] array = this.interactionEffectors;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Initiate(this);
			}
			this.triggersInRange = new List<InteractionTrigger>();
			this.c = base.GetComponent<Collider>();
			this.UpdateTriggerEventBroadcasting();
			this.initiated = true;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0004EC15 File Offset: 0x0004CE15
		private void InteractionPause(FullBodyBipedEffector effector, InteractionObject interactionObject)
		{
			this.lookAt.isPaused = true;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0004EC23 File Offset: 0x0004CE23
		private void InteractionResume(FullBodyBipedEffector effector, InteractionObject interactionObject)
		{
			this.lookAt.isPaused = false;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0004EC31 File Offset: 0x0004CE31
		private void InteractionStop(FullBodyBipedEffector effector, InteractionObject interactionObject)
		{
			this.lookAt.isPaused = false;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0004EC3F File Offset: 0x0004CE3F
		private void LookAtInteraction(FullBodyBipedEffector effector, InteractionObject interactionObject)
		{
			this.lookAt.Look(interactionObject.lookAtTarget, Time.time + interactionObject.length * 0.5f);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0004EC64 File Offset: 0x0004CE64
		public void OnTriggerEnter(Collider c)
		{
			if (this.fullBody == null)
			{
				return;
			}
			InteractionTrigger component = c.GetComponent<InteractionTrigger>();
			if (component == null)
			{
				return;
			}
			if (this.inContact.Contains(component))
			{
				return;
			}
			this.inContact.Add(component);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0004ECAC File Offset: 0x0004CEAC
		public void OnTriggerExit(Collider c)
		{
			if (this.fullBody == null)
			{
				return;
			}
			InteractionTrigger component = c.GetComponent<InteractionTrigger>();
			if (component == null)
			{
				return;
			}
			this.inContact.Remove(component);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0004ECE8 File Offset: 0x0004CEE8
		private bool ContactIsInRange(int index, out int bestRangeIndex)
		{
			bestRangeIndex = -1;
			if (!this.IsValid(true))
			{
				return false;
			}
			if (index < 0 || index >= this.inContact.Count)
			{
				Warning.Log("Index out of range.", base.transform, false);
				return false;
			}
			if (this.inContact[index] == null)
			{
				Warning.Log("The InteractionTrigger in the list 'inContact' has been destroyed", base.transform, false);
				return false;
			}
			bestRangeIndex = this.inContact[index].GetBestRangeIndex(base.transform, this.FPSCamera, this.raycastHit);
			return bestRangeIndex != -1;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0004ED7C File Offset: 0x0004CF7C
		private void OnDrawGizmosSelected()
		{
			if (Application.isPlaying)
			{
				return;
			}
			if (this.fullBody == null)
			{
				this.fullBody = base.GetComponent<FullBodyBipedIK>();
			}
			if (this.characterCollider == null)
			{
				this.characterCollider = base.GetComponent<Collider>();
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0004EDBC File Offset: 0x0004CFBC
		public void Update()
		{
			if (this.fullBody == null)
			{
				return;
			}
			this.UpdateTriggerEventBroadcasting();
			this.Raycasting();
			this.triggersInRange.Clear();
			this.bestRangeIndexes.Clear();
			for (int i = 0; i < this.inContact.Count; i++)
			{
				int item = -1;
				if (this.inContact[i] != null && this.inContact[i].gameObject.activeInHierarchy && this.ContactIsInRange(i, out item))
				{
					this.triggersInRange.Add(this.inContact[i]);
					this.bestRangeIndexes.Add(item);
				}
			}
			this.lookAt.Update();
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0004EE78 File Offset: 0x0004D078
		private void Raycasting()
		{
			if (this.camRaycastLayers == -1)
			{
				return;
			}
			if (this.FPSCamera == null)
			{
				return;
			}
			Physics.Raycast(this.FPSCamera.position, this.FPSCamera.forward, out this.raycastHit, this.camRaycastDistance, this.camRaycastLayers);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0004EED8 File Offset: 0x0004D0D8
		private void UpdateTriggerEventBroadcasting()
		{
			if (this.characterCollider == null)
			{
				this.characterCollider = this.c;
			}
			if (this.characterCollider != null && this.characterCollider != this.c)
			{
				if (this.characterCollider.GetComponent<TriggerEventBroadcaster>() == null)
				{
					this.characterCollider.gameObject.AddComponent<TriggerEventBroadcaster>().target = base.gameObject;
				}
				if (this.lastCollider != null && this.lastCollider != this.c && this.lastCollider != this.characterCollider)
				{
					TriggerEventBroadcaster component = this.lastCollider.GetComponent<TriggerEventBroadcaster>();
					if (component != null)
					{
						UnityEngine.Object.Destroy(component);
					}
				}
			}
			this.lastCollider = this.characterCollider;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0004EFAC File Offset: 0x0004D1AC
		private void UpdateEffectors()
		{
			if (this.fullBody == null)
			{
				return;
			}
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				this.interactionEffectors[i].Update(base.transform, this.speed);
			}
			for (int j = 0; j < this.interactionEffectors.Length; j++)
			{
				this.interactionEffectors[j].ResetToDefaults(this.resetToDefaultsSpeed * this.speed);
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0004F022 File Offset: 0x0004D222
		private void OnPreFBBIK()
		{
			if (this.fullBody == null)
			{
				return;
			}
			this.lookAt.SolveSpine();
			this.UpdateEffectors();
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0004F044 File Offset: 0x0004D244
		private void OnPostFBBIK()
		{
			if (this.fullBody == null)
			{
				return;
			}
			for (int i = 0; i < this.interactionEffectors.Length; i++)
			{
				this.interactionEffectors[i].OnPostFBBIK();
			}
			this.lookAt.SolveHead();
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0004F08B File Offset: 0x0004D28B
		private void OnFixTransforms()
		{
			this.lookAt.OnFixTransforms();
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0004F098 File Offset: 0x0004D298
		private void OnDestroy()
		{
			if (this.fullBody == null)
			{
				return;
			}
			IKSolverFullBodyBiped solver = this.fullBody.solver;
			solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPreUpdate, new IKSolver.UpdateDelegate(this.OnPreFBBIK));
			IKSolverFullBodyBiped solver2 = this.fullBody.solver;
			solver2.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver2.OnPostUpdate, new IKSolver.UpdateDelegate(this.OnPostFBBIK));
			IKSolverFullBodyBiped solver3 = this.fullBody.solver;
			solver3.OnFixTransforms = (IKSolver.UpdateDelegate)Delegate.Remove(solver3.OnFixTransforms, new IKSolver.UpdateDelegate(this.OnFixTransforms));
			this.OnInteractionStart = (InteractionSystem.InteractionDelegate)Delegate.Remove(this.OnInteractionStart, new InteractionSystem.InteractionDelegate(this.LookAtInteraction));
			this.OnInteractionPause = (InteractionSystem.InteractionDelegate)Delegate.Remove(this.OnInteractionPause, new InteractionSystem.InteractionDelegate(this.InteractionPause));
			this.OnInteractionResume = (InteractionSystem.InteractionDelegate)Delegate.Remove(this.OnInteractionResume, new InteractionSystem.InteractionDelegate(this.InteractionResume));
			this.OnInteractionStop = (InteractionSystem.InteractionDelegate)Delegate.Remove(this.OnInteractionStop, new InteractionSystem.InteractionDelegate(this.InteractionStop));
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0004F1C0 File Offset: 0x0004D3C0
		private bool IsValid(bool log)
		{
			if (this.fullBody == null)
			{
				if (log)
				{
					Warning.Log("FBBIK is null. Will not update the InteractionSystem", base.transform, false);
				}
				return false;
			}
			if (!this.initiated)
			{
				if (log)
				{
					Warning.Log("The InteractionSystem has not been initiated yet.", base.transform, false);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0004F210 File Offset: 0x0004D410
		private bool TriggerIndexIsValid(int index)
		{
			if (index < 0 || index >= this.triggersInRange.Count)
			{
				Warning.Log("Index out of range.", base.transform, false);
				return false;
			}
			if (this.triggersInRange[index] == null)
			{
				Warning.Log("The InteractionTrigger in the list 'inContact' has been destroyed", base.transform, false);
				return false;
			}
			return true;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0004F26C File Offset: 0x0004D46C
		public InteractionSystem()
		{
		}

		// Token: 0x04000906 RID: 2310
		[Tooltip("If not empty, only the targets with the specified tag will be used by this Interaction System.")]
		public string targetTag = "";

		// Token: 0x04000907 RID: 2311
		[Tooltip("The fade in time of the interaction.")]
		public float fadeInTime = 0.3f;

		// Token: 0x04000908 RID: 2312
		[Tooltip("The master speed for all interactions.")]
		public float speed = 1f;

		// Token: 0x04000909 RID: 2313
		[Tooltip("If > 0, lerps all the FBBIK channels used by the Interaction System back to their default or initial values when not in interaction.")]
		public float resetToDefaultsSpeed = 1f;

		// Token: 0x0400090A RID: 2314
		[Header("Triggering")]
		[Tooltip("The collider that registers OnTriggerEnter and OnTriggerExit events with InteractionTriggers.")]
		[FormerlySerializedAs("collider")]
		public Collider characterCollider;

		// Token: 0x0400090B RID: 2315
		[Tooltip("Will be used by Interaction Triggers that need the camera's position. Assign the first person view character camera.")]
		[FormerlySerializedAs("camera")]
		public Transform FPSCamera;

		// Token: 0x0400090C RID: 2316
		[Tooltip("The layers that will be raycasted from the camera (along camera.forward). All InteractionTrigger look at target colliders should be included.")]
		public LayerMask camRaycastLayers;

		// Token: 0x0400090D RID: 2317
		[Tooltip("Max distance of raycasting from the camera.")]
		public float camRaycastDistance = 1f;

		// Token: 0x0400090E RID: 2318
		[CompilerGenerated]
		private List<InteractionTrigger> <triggersInRange>k__BackingField;

		// Token: 0x0400090F RID: 2319
		private List<InteractionTrigger> inContact = new List<InteractionTrigger>();

		// Token: 0x04000910 RID: 2320
		private List<int> bestRangeIndexes = new List<int>();

		// Token: 0x04000911 RID: 2321
		public InteractionSystem.InteractionDelegate OnInteractionStart;

		// Token: 0x04000912 RID: 2322
		public InteractionSystem.InteractionDelegate OnInteractionPause;

		// Token: 0x04000913 RID: 2323
		public InteractionSystem.InteractionDelegate OnInteractionPickUp;

		// Token: 0x04000914 RID: 2324
		public InteractionSystem.InteractionDelegate OnInteractionResume;

		// Token: 0x04000915 RID: 2325
		public InteractionSystem.InteractionDelegate OnInteractionStop;

		// Token: 0x04000916 RID: 2326
		public InteractionSystem.InteractionEventDelegate OnInteractionEvent;

		// Token: 0x04000917 RID: 2327
		public RaycastHit raycastHit;

		// Token: 0x04000918 RID: 2328
		[Space(10f)]
		[Tooltip("Reference to the FBBIK component.")]
		[SerializeField]
		private FullBodyBipedIK fullBody;

		// Token: 0x04000919 RID: 2329
		[Tooltip("Handles looking at the interactions.")]
		public InteractionLookAt lookAt = new InteractionLookAt();

		// Token: 0x0400091A RID: 2330
		private InteractionEffector[] interactionEffectors = new InteractionEffector[]
		{
			new InteractionEffector(FullBodyBipedEffector.Body),
			new InteractionEffector(FullBodyBipedEffector.LeftFoot),
			new InteractionEffector(FullBodyBipedEffector.LeftHand),
			new InteractionEffector(FullBodyBipedEffector.LeftShoulder),
			new InteractionEffector(FullBodyBipedEffector.LeftThigh),
			new InteractionEffector(FullBodyBipedEffector.RightFoot),
			new InteractionEffector(FullBodyBipedEffector.RightHand),
			new InteractionEffector(FullBodyBipedEffector.RightShoulder),
			new InteractionEffector(FullBodyBipedEffector.RightThigh)
		};

		// Token: 0x0400091B RID: 2331
		[CompilerGenerated]
		private bool <initiated>k__BackingField;

		// Token: 0x0400091C RID: 2332
		private Collider lastCollider;

		// Token: 0x0400091D RID: 2333
		private Collider c;

		// Token: 0x0200020B RID: 523
		// (Invoke) Token: 0x0600111A RID: 4378
		public delegate void InteractionDelegate(FullBodyBipedEffector effectorType, InteractionObject interactionObject);

		// Token: 0x0200020C RID: 524
		// (Invoke) Token: 0x0600111E RID: 4382
		public delegate void InteractionEventDelegate(FullBodyBipedEffector effectorType, InteractionObject interactionObject, InteractionObject.InteractionEvent interactionEvent);
	}
}

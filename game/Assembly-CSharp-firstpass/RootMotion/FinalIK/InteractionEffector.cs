using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000100 RID: 256
	[Serializable]
	public class InteractionEffector
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0004C4F5 File Offset: 0x0004A6F5
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x0004C4FD File Offset: 0x0004A6FD
		public FullBodyBipedEffector effectorType
		{
			[CompilerGenerated]
			get
			{
				return this.<effectorType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<effectorType>k__BackingField = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0004C506 File Offset: 0x0004A706
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x0004C50E File Offset: 0x0004A70E
		public bool isPaused
		{
			[CompilerGenerated]
			get
			{
				return this.<isPaused>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<isPaused>k__BackingField = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0004C517 File Offset: 0x0004A717
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x0004C51F File Offset: 0x0004A71F
		public InteractionObject interactionObject
		{
			[CompilerGenerated]
			get
			{
				return this.<interactionObject>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<interactionObject>k__BackingField = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0004C528 File Offset: 0x0004A728
		public bool inInteraction
		{
			get
			{
				return this.interactionObject != null;
			}
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0004C536 File Offset: 0x0004A736
		public InteractionEffector(FullBodyBipedEffector effectorType)
		{
			this.effectorType = effectorType;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0004C550 File Offset: 0x0004A750
		public void Initiate(InteractionSystem interactionSystem)
		{
			this.interactionSystem = interactionSystem;
			this.effector = interactionSystem.ik.solver.GetEffector(this.effectorType);
			this.poser = this.effector.bone.GetComponent<Poser>();
			this.StoreDefaults();
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0004C59C File Offset: 0x0004A79C
		private void StoreDefaults()
		{
			this.defaultPositionWeight = this.interactionSystem.ik.solver.GetEffector(this.effectorType).positionWeight;
			this.defaultRotationWeight = this.interactionSystem.ik.solver.GetEffector(this.effectorType).rotationWeight;
			this.defaultPull = this.interactionSystem.ik.solver.GetChain(this.effectorType).pull;
			this.defaultReach = this.interactionSystem.ik.solver.GetChain(this.effectorType).reach;
			this.defaultPush = this.interactionSystem.ik.solver.GetChain(this.effectorType).push;
			this.defaultPushParent = this.interactionSystem.ik.solver.GetChain(this.effectorType).pushParent;
			this.defaultBendGoalWeight = this.interactionSystem.ik.solver.GetChain(this.effectorType).bendConstraint.weight;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0004C6B8 File Offset: 0x0004A8B8
		public bool ResetToDefaults(float speed)
		{
			if (this.inInteraction)
			{
				return false;
			}
			if (this.isPaused)
			{
				return false;
			}
			if (this.defaults)
			{
				return false;
			}
			this.resetTimer = Mathf.MoveTowards(this.resetTimer, 0f, Time.deltaTime * speed);
			if (this.effector.isEndEffector)
			{
				if (this.pullUsed)
				{
					this.interactionSystem.ik.solver.GetChain(this.effectorType).pull = Mathf.Lerp(this.defaultPull, this.interactionSystem.ik.solver.GetChain(this.effectorType).pull, this.resetTimer);
				}
				if (this.reachUsed)
				{
					this.interactionSystem.ik.solver.GetChain(this.effectorType).reach = Mathf.Lerp(this.defaultReach, this.interactionSystem.ik.solver.GetChain(this.effectorType).reach, this.resetTimer);
				}
				if (this.pushUsed)
				{
					this.interactionSystem.ik.solver.GetChain(this.effectorType).push = Mathf.Lerp(this.defaultPush, this.interactionSystem.ik.solver.GetChain(this.effectorType).push, this.resetTimer);
				}
				if (this.pushParentUsed)
				{
					this.interactionSystem.ik.solver.GetChain(this.effectorType).pushParent = Mathf.Lerp(this.defaultPushParent, this.interactionSystem.ik.solver.GetChain(this.effectorType).pushParent, this.resetTimer);
				}
				if (this.bendGoalWeightUsed)
				{
					this.interactionSystem.ik.solver.GetChain(this.effectorType).bendConstraint.weight = Mathf.Lerp(this.defaultBendGoalWeight, this.interactionSystem.ik.solver.GetChain(this.effectorType).bendConstraint.weight, this.resetTimer);
				}
			}
			if (this.positionWeightUsed)
			{
				this.effector.positionWeight = Mathf.Lerp(this.defaultPositionWeight, this.effector.positionWeight, this.resetTimer);
			}
			if (this.rotationWeightUsed)
			{
				this.effector.rotationWeight = Mathf.Lerp(this.defaultRotationWeight, this.effector.rotationWeight, this.resetTimer);
			}
			if (this.resetTimer <= 0f)
			{
				this.pullUsed = false;
				this.reachUsed = false;
				this.pushUsed = false;
				this.pushParentUsed = false;
				this.positionWeightUsed = false;
				this.rotationWeightUsed = false;
				this.bendGoalWeightUsed = false;
				this.defaults = true;
			}
			return true;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0004C97C File Offset: 0x0004AB7C
		public bool Pause()
		{
			if (!this.inInteraction)
			{
				return false;
			}
			this.isPaused = true;
			this.pausePositionRelative = this.target.InverseTransformPoint(this.effector.position);
			this.pauseRotationRelative = Quaternion.Inverse(this.target.rotation) * this.effector.rotation;
			if (this.interactionSystem.OnInteractionPause != null)
			{
				this.interactionSystem.OnInteractionPause(this.effectorType, this.interactionObject);
			}
			return true;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0004CA06 File Offset: 0x0004AC06
		public bool Resume()
		{
			if (!this.inInteraction)
			{
				return false;
			}
			this.isPaused = false;
			if (this.interactionSystem.OnInteractionResume != null)
			{
				this.interactionSystem.OnInteractionResume(this.effectorType, this.interactionObject);
			}
			return true;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0004CA44 File Offset: 0x0004AC44
		public bool Start(InteractionObject interactionObject, string tag, float fadeInTime, bool interrupt)
		{
			if (!this.inInteraction)
			{
				this.effector.position = this.effector.bone.position;
				this.effector.rotation = this.effector.bone.rotation;
			}
			else
			{
				if (!interrupt)
				{
					return false;
				}
				this.defaults = false;
			}
			this.target = interactionObject.GetTarget(this.effectorType, tag);
			if (this.target == null)
			{
				return false;
			}
			this.interactionTarget = this.target.GetComponent<InteractionTarget>();
			this.interactionObject = interactionObject;
			if (this.interactionSystem.OnInteractionStart != null)
			{
				this.interactionSystem.OnInteractionStart(this.effectorType, interactionObject);
			}
			interactionObject.OnStartInteraction(this.interactionSystem);
			this.triggered.Clear();
			for (int i = 0; i < interactionObject.events.Length; i++)
			{
				this.triggered.Add(false);
			}
			if (this.poser != null)
			{
				if (this.poser.poseRoot == null)
				{
					this.poser.weight = 0f;
				}
				if (this.interactionTarget != null)
				{
					this.poser.poseRoot = this.target.transform;
				}
				else
				{
					this.poser.poseRoot = null;
				}
				this.poser.AutoMapping();
			}
			this.positionWeightUsed = interactionObject.CurveUsed(InteractionObject.WeightCurve.Type.PositionWeight);
			this.rotationWeightUsed = interactionObject.CurveUsed(InteractionObject.WeightCurve.Type.RotationWeight);
			this.pullUsed = interactionObject.CurveUsed(InteractionObject.WeightCurve.Type.Pull);
			this.reachUsed = interactionObject.CurveUsed(InteractionObject.WeightCurve.Type.Reach);
			this.pushUsed = interactionObject.CurveUsed(InteractionObject.WeightCurve.Type.Push);
			this.pushParentUsed = interactionObject.CurveUsed(InteractionObject.WeightCurve.Type.PushParent);
			this.bendGoalWeightUsed = interactionObject.CurveUsed(InteractionObject.WeightCurve.Type.BendGoalWeight);
			if (this.defaults)
			{
				this.StoreDefaults();
			}
			this.timer = 0f;
			this.weight = 0f;
			this.fadeInSpeed = ((fadeInTime > 0f) ? (1f / fadeInTime) : 1000f);
			this.length = interactionObject.length;
			this.isPaused = false;
			this.pickedUp = false;
			this.pickUpPosition = Vector3.zero;
			this.pickUpRotation = Quaternion.identity;
			if (this.interactionTarget != null)
			{
				this.interactionTarget.RotateTo(this.effector.bone);
			}
			this.started = true;
			return true;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0004CC9C File Offset: 0x0004AE9C
		public void Update(Transform root, float speed)
		{
			if (!this.inInteraction)
			{
				if (this.started)
				{
					this.isPaused = false;
					this.pickedUp = false;
					this.defaults = false;
					this.resetTimer = 1f;
					this.started = false;
				}
				return;
			}
			if (this.interactionTarget != null && !this.interactionTarget.rotateOnce)
			{
				this.interactionTarget.RotateTo(this.effector.bone);
			}
			if (this.isPaused)
			{
				this.effector.position = this.target.TransformPoint(this.pausePositionRelative);
				this.effector.rotation = this.target.rotation * this.pauseRotationRelative;
				this.interactionObject.Apply(this.interactionSystem.ik.solver, this.effectorType, this.interactionTarget, this.timer, this.weight);
				return;
			}
			this.timer += Time.deltaTime * speed * ((this.interactionTarget != null) ? this.interactionTarget.interactionSpeedMlp : 1f);
			this.weight = Mathf.Clamp(this.weight + Time.deltaTime * this.fadeInSpeed * speed, 0f, 1f);
			bool flag = false;
			bool flag2 = false;
			this.TriggerUntriggeredEvents(true, out flag, out flag2);
			Vector3 b = this.pickedUp ? this.interactionSystem.transform.TransformPoint(this.pickUpPosition) : this.target.position;
			Quaternion b2 = this.pickedUp ? (this.interactionSystem.transform.rotation * this.pickUpRotation) : this.target.rotation;
			this.effector.position = Vector3.Lerp(this.effector.bone.position, b, this.weight);
			this.effector.rotation = Quaternion.Lerp(this.effector.bone.rotation, b2, this.weight);
			this.interactionObject.Apply(this.interactionSystem.ik.solver, this.effectorType, this.interactionTarget, this.timer, this.weight);
			if (flag)
			{
				this.PickUp(root);
			}
			if (flag2)
			{
				this.Pause();
			}
			float value = this.interactionObject.GetValue(InteractionObject.WeightCurve.Type.PoserWeight, this.interactionTarget, this.timer);
			if (this.poser != null)
			{
				this.poser.weight = Mathf.Lerp(this.poser.weight, value, this.weight);
			}
			else if (value > 0f)
			{
				Warning.Log(string.Concat(new string[]
				{
					"InteractionObject ",
					this.interactionObject.name,
					" has a curve/multipler for Poser Weight, but the bone of effector ",
					this.effectorType.ToString(),
					" has no HandPoser/GenericPoser attached."
				}), this.effector.bone, false);
			}
			if (this.timer >= this.length)
			{
				this.Stop();
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0004CFB1 File Offset: 0x0004B1B1
		public float progress
		{
			get
			{
				if (!this.inInteraction)
				{
					return 0f;
				}
				if (this.length == 0f)
				{
					return 0f;
				}
				return this.timer / this.length;
			}
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0004CFE4 File Offset: 0x0004B1E4
		private void TriggerUntriggeredEvents(bool checkTime, out bool pickUp, out bool pause)
		{
			pickUp = false;
			pause = false;
			for (int i = 0; i < this.triggered.Count; i++)
			{
				if (!this.triggered[i] && (!checkTime || this.interactionObject.events[i].time < this.timer))
				{
					this.interactionObject.events[i].Activate(this.effector.bone);
					if (this.interactionObject.events[i].pickUp)
					{
						if (this.timer >= this.interactionObject.events[i].time)
						{
							this.timer = this.interactionObject.events[i].time;
						}
						pickUp = true;
					}
					if (this.interactionObject.events[i].pause)
					{
						if (this.timer >= this.interactionObject.events[i].time)
						{
							this.timer = this.interactionObject.events[i].time;
						}
						pause = true;
					}
					if (this.interactionSystem.OnInteractionEvent != null)
					{
						this.interactionSystem.OnInteractionEvent(this.effectorType, this.interactionObject, this.interactionObject.events[i]);
					}
					this.triggered[i] = true;
				}
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0004D138 File Offset: 0x0004B338
		private void PickUp(Transform root)
		{
			this.pickUpPosition = root.InverseTransformPoint(this.effector.position);
			this.pickUpRotation = Quaternion.Inverse(this.interactionSystem.transform.rotation) * this.effector.rotation;
			this.pickUpOnPostFBBIK = true;
			this.pickedUp = true;
			Rigidbody component = this.interactionObject.targetsRoot.GetComponent<Rigidbody>();
			if (component != null)
			{
				if (!component.isKinematic)
				{
					component.isKinematic = true;
				}
				Collider component2 = root.GetComponent<Collider>();
				if (component2 != null)
				{
					foreach (Collider collider in this.interactionObject.targetsRoot.GetComponentsInChildren<Collider>())
					{
						if (!collider.isTrigger && collider.enabled)
						{
							Physics.IgnoreCollision(component2, collider);
						}
					}
				}
			}
			if (this.interactionSystem.OnInteractionPickUp != null)
			{
				this.interactionSystem.OnInteractionPickUp(this.effectorType, this.interactionObject);
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0004D238 File Offset: 0x0004B438
		public bool Stop()
		{
			if (!this.inInteraction)
			{
				return false;
			}
			bool flag = false;
			bool flag2 = false;
			this.TriggerUntriggeredEvents(false, out flag, out flag2);
			if (this.interactionSystem.OnInteractionStop != null)
			{
				this.interactionSystem.OnInteractionStop(this.effectorType, this.interactionObject);
			}
			if (this.interactionTarget != null)
			{
				this.interactionTarget.ResetRotation();
			}
			this.interactionObject = null;
			this.weight = 0f;
			this.timer = 0f;
			this.isPaused = false;
			this.target = null;
			this.defaults = false;
			this.resetTimer = 1f;
			if (this.poser != null && !this.pickedUp)
			{
				this.poser.weight = 0f;
			}
			this.pickedUp = false;
			this.started = false;
			return true;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0004D314 File Offset: 0x0004B514
		public void OnPostFBBIK()
		{
			if (!this.inInteraction)
			{
				return;
			}
			float num = this.interactionObject.GetValue(InteractionObject.WeightCurve.Type.RotateBoneWeight, this.interactionTarget, this.timer) * this.weight;
			if (num > 0f)
			{
				Quaternion b = this.pickedUp ? (this.interactionSystem.transform.rotation * this.pickUpRotation) : this.effector.rotation;
				Quaternion rhs = Quaternion.Slerp(this.effector.bone.rotation, b, num * num);
				this.effector.bone.localRotation = Quaternion.Inverse(this.effector.bone.parent.rotation) * rhs;
			}
			if (this.pickUpOnPostFBBIK)
			{
				Vector3 position = this.effector.bone.position;
				this.effector.bone.position = this.interactionSystem.transform.TransformPoint(this.pickUpPosition);
				this.interactionObject.targetsRoot.parent = this.effector.bone;
				this.effector.bone.position = position;
				this.pickUpOnPostFBBIK = false;
			}
		}

		// Token: 0x040008D1 RID: 2257
		[CompilerGenerated]
		private FullBodyBipedEffector <effectorType>k__BackingField;

		// Token: 0x040008D2 RID: 2258
		[CompilerGenerated]
		private bool <isPaused>k__BackingField;

		// Token: 0x040008D3 RID: 2259
		[CompilerGenerated]
		private InteractionObject <interactionObject>k__BackingField;

		// Token: 0x040008D4 RID: 2260
		private Poser poser;

		// Token: 0x040008D5 RID: 2261
		private IKEffector effector;

		// Token: 0x040008D6 RID: 2262
		private float timer;

		// Token: 0x040008D7 RID: 2263
		private float length;

		// Token: 0x040008D8 RID: 2264
		private float weight;

		// Token: 0x040008D9 RID: 2265
		private float fadeInSpeed;

		// Token: 0x040008DA RID: 2266
		private float defaultPositionWeight;

		// Token: 0x040008DB RID: 2267
		private float defaultRotationWeight;

		// Token: 0x040008DC RID: 2268
		private float defaultPull;

		// Token: 0x040008DD RID: 2269
		private float defaultReach;

		// Token: 0x040008DE RID: 2270
		private float defaultPush;

		// Token: 0x040008DF RID: 2271
		private float defaultPushParent;

		// Token: 0x040008E0 RID: 2272
		private float defaultBendGoalWeight;

		// Token: 0x040008E1 RID: 2273
		private float resetTimer;

		// Token: 0x040008E2 RID: 2274
		private bool positionWeightUsed;

		// Token: 0x040008E3 RID: 2275
		private bool rotationWeightUsed;

		// Token: 0x040008E4 RID: 2276
		private bool pullUsed;

		// Token: 0x040008E5 RID: 2277
		private bool reachUsed;

		// Token: 0x040008E6 RID: 2278
		private bool pushUsed;

		// Token: 0x040008E7 RID: 2279
		private bool pushParentUsed;

		// Token: 0x040008E8 RID: 2280
		private bool bendGoalWeightUsed;

		// Token: 0x040008E9 RID: 2281
		private bool pickedUp;

		// Token: 0x040008EA RID: 2282
		private bool defaults;

		// Token: 0x040008EB RID: 2283
		private bool pickUpOnPostFBBIK;

		// Token: 0x040008EC RID: 2284
		private Vector3 pickUpPosition;

		// Token: 0x040008ED RID: 2285
		private Vector3 pausePositionRelative;

		// Token: 0x040008EE RID: 2286
		private Quaternion pickUpRotation;

		// Token: 0x040008EF RID: 2287
		private Quaternion pauseRotationRelative;

		// Token: 0x040008F0 RID: 2288
		private InteractionTarget interactionTarget;

		// Token: 0x040008F1 RID: 2289
		private Transform target;

		// Token: 0x040008F2 RID: 2290
		private List<bool> triggered = new List<bool>();

		// Token: 0x040008F3 RID: 2291
		private InteractionSystem interactionSystem;

		// Token: 0x040008F4 RID: 2292
		private bool started;
	}
}

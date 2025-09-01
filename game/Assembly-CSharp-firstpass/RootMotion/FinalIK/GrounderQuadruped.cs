using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000D4 RID: 212
	[HelpURL("http://www.root-motion.com/finalikdox/html/page9.html")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/Grounder/Grounder Quadruped")]
	public class GrounderQuadruped : Grounder
	{
		// Token: 0x06000924 RID: 2340 RVA: 0x0003CF13 File Offset: 0x0003B113
		[ContextMenu("User Manual")]
		protected override void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page9.html");
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0003CF1F File Offset: 0x0003B11F
		[ContextMenu("Scrpt Reference")]
		protected override void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_grounder_quadruped.html");
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0003CF2B File Offset: 0x0003B12B
		public override void ResetPosition()
		{
			this.solver.Reset();
			this.forelegSolver.Reset();
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0003CF44 File Offset: 0x0003B144
		private bool IsReadyToInitiate()
		{
			return !(this.pelvis == null) && !(this.lastSpineBone == null) && this.legs.Length != 0 && this.forelegs.Length != 0 && !(this.characterRoot == null) && this.IsReadyToInitiateLegs(this.legs) && this.IsReadyToInitiateLegs(this.forelegs);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0003CFB8 File Offset: 0x0003B1B8
		private bool IsReadyToInitiateLegs(IK[] ikComponents)
		{
			foreach (IK ik in ikComponents)
			{
				if (ik == null)
				{
					return false;
				}
				if (ik is FullBodyBipedIK)
				{
					base.LogWarning("GrounderIK does not support FullBodyBipedIK, use CCDIK, FABRIK, LimbIK or TrigonometricIK instead. If you want to use FullBodyBipedIK, use the GrounderFBBIK component.");
					return false;
				}
				if (ik is FABRIKRoot)
				{
					base.LogWarning("GrounderIK does not support FABRIKRoot, use CCDIK, FABRIK, LimbIK or TrigonometricIK instead.");
					return false;
				}
				if (ik is AimIK)
				{
					base.LogWarning("GrounderIK does not support AimIK, use CCDIK, FABRIK, LimbIK or TrigonometricIK instead.");
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0003D024 File Offset: 0x0003B224
		private void OnDisable()
		{
			if (!base.initiated)
			{
				return;
			}
			for (int i = 0; i < this.feet.Length; i++)
			{
				if (this.feet[i].solver != null)
				{
					this.feet[i].solver.IKPositionWeight = 0f;
				}
			}
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0003D07C File Offset: 0x0003B27C
		private void Update()
		{
			this.weight = Mathf.Clamp(this.weight, 0f, 1f);
			if (this.weight <= 0f)
			{
				return;
			}
			this.solved = false;
			if (base.initiated)
			{
				return;
			}
			if (!this.IsReadyToInitiate())
			{
				return;
			}
			this.Initiate();
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0003D0D4 File Offset: 0x0003B2D4
		private void Initiate()
		{
			this.feet = new GrounderQuadruped.Foot[this.legs.Length + this.forelegs.Length];
			Transform[] array = this.InitiateFeet(this.legs, ref this.feet, 0);
			Transform[] array2 = this.InitiateFeet(this.forelegs, ref this.feet, this.legs.Length);
			this.animatedPelvisLocalPosition = this.pelvis.localPosition;
			this.animatedPelvisLocalRotation = this.pelvis.localRotation;
			if (this.head != null)
			{
				this.animatedHeadLocalRotation = this.head.localRotation;
			}
			this.forefeetRoot = new GameObject().transform;
			this.forefeetRoot.parent = base.transform;
			this.forefeetRoot.name = "Forefeet Root";
			this.solver.Initiate(base.transform, array);
			this.forelegSolver.Initiate(this.forefeetRoot, array2);
			for (int i = 0; i < array.Length; i++)
			{
				this.feet[i].leg = this.solver.legs[i];
			}
			for (int j = 0; j < array2.Length; j++)
			{
				this.feet[j + this.legs.Length].leg = this.forelegSolver.legs[j];
			}
			this.characterRootRigidbody = this.characterRoot.GetComponent<Rigidbody>();
			base.initiated = true;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0003D23C File Offset: 0x0003B43C
		private Transform[] InitiateFeet(IK[] ikComponents, ref GrounderQuadruped.Foot[] f, int indexOffset)
		{
			Transform[] array = new Transform[ikComponents.Length];
			for (int i = 0; i < ikComponents.Length; i++)
			{
				IKSolver.Point[] points = ikComponents[i].GetIKSolver().GetPoints();
				f[i + indexOffset] = new GrounderQuadruped.Foot(ikComponents[i].GetIKSolver(), points[points.Length - 1].transform);
				array[i] = f[i + indexOffset].transform;
				IKSolver solver = f[i + indexOffset].solver;
				solver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPreUpdate, new IKSolver.UpdateDelegate(this.OnSolverUpdate));
				IKSolver solver2 = f[i + indexOffset].solver;
				solver2.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver2.OnPostUpdate, new IKSolver.UpdateDelegate(this.OnPostSolverUpdate));
			}
			return array;
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0003D30C File Offset: 0x0003B50C
		private void LateUpdate()
		{
			if (this.weight <= 0f)
			{
				return;
			}
			this.rootRotationWeight = Mathf.Clamp(this.rootRotationWeight, 0f, 1f);
			this.minRootRotation = Mathf.Clamp(this.minRootRotation, -90f, this.maxRootRotation);
			this.maxRootRotation = Mathf.Clamp(this.maxRootRotation, this.minRootRotation, 90f);
			this.rootRotationSpeed = Mathf.Clamp(this.rootRotationSpeed, 0f, this.rootRotationSpeed);
			this.maxLegOffset = Mathf.Clamp(this.maxLegOffset, 0f, this.maxLegOffset);
			this.maxForeLegOffset = Mathf.Clamp(this.maxForeLegOffset, 0f, this.maxForeLegOffset);
			this.maintainHeadRotationWeight = Mathf.Clamp(this.maintainHeadRotationWeight, 0f, 1f);
			this.RootRotation();
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0003D3F0 File Offset: 0x0003B5F0
		private void RootRotation()
		{
			if (this.rootRotationWeight <= 0f)
			{
				return;
			}
			if (this.rootRotationSpeed <= 0f)
			{
				return;
			}
			this.solver.rotateSolver = true;
			this.forelegSolver.rotateSolver = true;
			Vector3 forward = this.characterRoot.forward;
			Vector3 vector = -this.gravity;
			Vector3.OrthoNormalize(ref vector, ref forward);
			Quaternion quaternion = Quaternion.LookRotation(forward, -this.gravity);
			Vector3 point = this.forelegSolver.rootHit.point - this.solver.rootHit.point;
			Vector3 vector2 = Quaternion.Inverse(quaternion) * point;
			float num = Mathf.Atan2(vector2.y, vector2.z) * 57.29578f;
			num = Mathf.Clamp(num * this.rootRotationWeight, this.minRootRotation, this.maxRootRotation);
			this.angle = Mathf.Lerp(this.angle, num, Time.deltaTime * this.rootRotationSpeed);
			if (this.characterRootRigidbody == null)
			{
				this.characterRoot.rotation = Quaternion.Slerp(this.characterRoot.rotation, Quaternion.AngleAxis(-this.angle, this.characterRoot.right) * quaternion, this.weight);
				return;
			}
			this.characterRootRigidbody.MoveRotation(Quaternion.Slerp(this.characterRoot.rotation, Quaternion.AngleAxis(-this.angle, this.characterRoot.right) * quaternion, this.weight));
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0003D584 File Offset: 0x0003B784
		private void OnSolverUpdate()
		{
			if (!base.enabled)
			{
				return;
			}
			if (this.weight <= 0f)
			{
				if (this.lastWeight <= 0f)
				{
					return;
				}
				this.OnDisable();
			}
			this.lastWeight = this.weight;
			if (this.solved)
			{
				return;
			}
			if (this.OnPreGrounder != null)
			{
				this.OnPreGrounder();
			}
			if (this.pelvis.localPosition != this.solvedPelvisLocalPosition)
			{
				this.animatedPelvisLocalPosition = this.pelvis.localPosition;
			}
			else
			{
				this.pelvis.localPosition = this.animatedPelvisLocalPosition;
			}
			if (this.pelvis.localRotation != this.solvedPelvisLocalRotation)
			{
				this.animatedPelvisLocalRotation = this.pelvis.localRotation;
			}
			else
			{
				this.pelvis.localRotation = this.animatedPelvisLocalRotation;
			}
			if (this.head != null)
			{
				if (this.head.localRotation != this.solvedHeadLocalRotation)
				{
					this.animatedHeadLocalRotation = this.head.localRotation;
				}
				else
				{
					this.head.localRotation = this.animatedHeadLocalRotation;
				}
			}
			for (int i = 0; i < this.feet.Length; i++)
			{
				this.feet[i].rotation = this.feet[i].transform.rotation;
			}
			if (this.head != null)
			{
				this.headRotation = this.head.rotation;
			}
			this.UpdateForefeetRoot();
			this.solver.Update();
			this.forelegSolver.Update();
			this.pelvis.position += this.solver.pelvis.IKOffset * this.weight;
			Vector3 fromDirection = this.lastSpineBone.position - this.pelvis.position;
			Vector3 toDirection = this.lastSpineBone.position + this.forelegSolver.root.up * Mathf.Clamp(this.forelegSolver.pelvis.heightOffset, float.NegativeInfinity, 0f) - this.solver.root.up * this.solver.pelvis.heightOffset - this.pelvis.position;
			Quaternion b = Quaternion.FromToRotation(fromDirection, toDirection);
			this.pelvis.rotation = Quaternion.Slerp(Quaternion.identity, b, this.weight) * this.pelvis.rotation;
			for (int j = 0; j < this.feet.Length; j++)
			{
				this.SetFootIK(this.feet[j], (j < 2) ? this.maxLegOffset : this.maxForeLegOffset);
			}
			this.solved = true;
			this.solvedFeet = 0;
			if (this.OnPostGrounder != null)
			{
				this.OnPostGrounder();
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0003D870 File Offset: 0x0003BA70
		private void UpdateForefeetRoot()
		{
			Vector3 a = Vector3.zero;
			for (int i = 0; i < this.forelegSolver.legs.Length; i++)
			{
				a += this.forelegSolver.legs[i].transform.position;
			}
			a /= (float)this.forelegs.Length;
			Vector3 vector = a - base.transform.position;
			Vector3 up = base.transform.up;
			Vector3 vector2 = vector;
			Vector3.OrthoNormalize(ref up, ref vector2);
			this.forefeetRoot.position = base.transform.position + vector2.normalized * vector.magnitude;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0003D928 File Offset: 0x0003BB28
		private void SetFootIK(GrounderQuadruped.Foot foot, float maxOffset)
		{
			Vector3 vector = foot.leg.IKPosition - foot.transform.position;
			foot.solver.IKPosition = foot.transform.position + Vector3.ClampMagnitude(vector, maxOffset);
			foot.solver.IKPositionWeight = this.weight;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0003D984 File Offset: 0x0003BB84
		private void OnPostSolverUpdate()
		{
			if (this.weight <= 0f)
			{
				return;
			}
			if (!base.enabled)
			{
				return;
			}
			this.solvedFeet++;
			if (this.solvedFeet < this.feet.Length)
			{
				return;
			}
			for (int i = 0; i < this.feet.Length; i++)
			{
				this.feet[i].transform.rotation = Quaternion.Slerp(Quaternion.identity, this.feet[i].leg.rotationOffset, this.weight) * this.feet[i].rotation;
			}
			if (this.head != null)
			{
				this.head.rotation = Quaternion.Lerp(this.head.rotation, this.headRotation, this.maintainHeadRotationWeight * this.weight);
			}
			this.solvedPelvisLocalPosition = this.pelvis.localPosition;
			this.solvedPelvisLocalRotation = this.pelvis.localRotation;
			if (this.head != null)
			{
				this.solvedHeadLocalRotation = this.head.localRotation;
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0003DAA9 File Offset: 0x0003BCA9
		private void OnDestroy()
		{
			if (base.initiated)
			{
				this.DestroyLegs(this.legs);
				this.DestroyLegs(this.forelegs);
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0003DACC File Offset: 0x0003BCCC
		private void DestroyLegs(IK[] ikComponents)
		{
			foreach (IK ik in ikComponents)
			{
				if (ik != null)
				{
					IKSolver iksolver = ik.GetIKSolver();
					iksolver.OnPreUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(iksolver.OnPreUpdate, new IKSolver.UpdateDelegate(this.OnSolverUpdate));
					IKSolver iksolver2 = ik.GetIKSolver();
					iksolver2.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(iksolver2.OnPostUpdate, new IKSolver.UpdateDelegate(this.OnPostSolverUpdate));
				}
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0003DB44 File Offset: 0x0003BD44
		public GrounderQuadruped()
		{
		}

		// Token: 0x0400072B RID: 1835
		[Tooltip("The Grounding solver for the forelegs.")]
		public Grounding forelegSolver = new Grounding();

		// Token: 0x0400072C RID: 1836
		[Tooltip("The weight of rotating the character root to the ground angle (range: 0 - 1).")]
		[Range(0f, 1f)]
		public float rootRotationWeight = 0.5f;

		// Token: 0x0400072D RID: 1837
		[Tooltip("The maximum angle of rotating the quadruped downwards (going downhill, range: -90 - 0).")]
		[Range(-90f, 0f)]
		public float minRootRotation = -25f;

		// Token: 0x0400072E RID: 1838
		[Tooltip("The maximum angle of rotating the quadruped upwards (going uphill, range: 0 - 90).")]
		[Range(0f, 90f)]
		public float maxRootRotation = 45f;

		// Token: 0x0400072F RID: 1839
		[Tooltip("The speed of interpolating the character root rotation (range: 0 - inf).")]
		public float rootRotationSpeed = 5f;

		// Token: 0x04000730 RID: 1840
		[Tooltip("The maximum IK offset for the legs (range: 0 - inf).")]
		public float maxLegOffset = 0.5f;

		// Token: 0x04000731 RID: 1841
		[Tooltip("The maximum IK offset for the forelegs (range: 0 - inf).")]
		public float maxForeLegOffset = 0.5f;

		// Token: 0x04000732 RID: 1842
		[Tooltip("The weight of maintaining the head's rotation as it was before solving the Grounding (range: 0 - 1).")]
		[Range(0f, 1f)]
		public float maintainHeadRotationWeight = 0.5f;

		// Token: 0x04000733 RID: 1843
		[Tooltip("The root Transform of the character, with the rigidbody and the collider.")]
		public Transform characterRoot;

		// Token: 0x04000734 RID: 1844
		[Tooltip("The pelvis transform. Common ancestor of both legs and the spine.")]
		public Transform pelvis;

		// Token: 0x04000735 RID: 1845
		[Tooltip("The last bone in the spine that is the common parent for both forelegs.")]
		public Transform lastSpineBone;

		// Token: 0x04000736 RID: 1846
		[Tooltip("The head (optional, if you intend to maintain it's rotation).")]
		public Transform head;

		// Token: 0x04000737 RID: 1847
		public IK[] legs;

		// Token: 0x04000738 RID: 1848
		public IK[] forelegs;

		// Token: 0x04000739 RID: 1849
		[HideInInspector]
		public Vector3 gravity = Vector3.down;

		// Token: 0x0400073A RID: 1850
		private GrounderQuadruped.Foot[] feet = new GrounderQuadruped.Foot[0];

		// Token: 0x0400073B RID: 1851
		private Vector3 animatedPelvisLocalPosition;

		// Token: 0x0400073C RID: 1852
		private Quaternion animatedPelvisLocalRotation;

		// Token: 0x0400073D RID: 1853
		private Quaternion animatedHeadLocalRotation;

		// Token: 0x0400073E RID: 1854
		private Vector3 solvedPelvisLocalPosition;

		// Token: 0x0400073F RID: 1855
		private Quaternion solvedPelvisLocalRotation;

		// Token: 0x04000740 RID: 1856
		private Quaternion solvedHeadLocalRotation;

		// Token: 0x04000741 RID: 1857
		private int solvedFeet;

		// Token: 0x04000742 RID: 1858
		private bool solved;

		// Token: 0x04000743 RID: 1859
		private float angle;

		// Token: 0x04000744 RID: 1860
		private Transform forefeetRoot;

		// Token: 0x04000745 RID: 1861
		private Quaternion headRotation;

		// Token: 0x04000746 RID: 1862
		private float lastWeight;

		// Token: 0x04000747 RID: 1863
		private Rigidbody characterRootRigidbody;

		// Token: 0x020001EA RID: 490
		public struct Foot
		{
			// Token: 0x06001013 RID: 4115 RVA: 0x000648FB File Offset: 0x00062AFB
			public Foot(IKSolver solver, Transform transform)
			{
				this.solver = solver;
				this.transform = transform;
				this.leg = null;
				this.rotation = transform.rotation;
			}

			// Token: 0x04000E72 RID: 3698
			public IKSolver solver;

			// Token: 0x04000E73 RID: 3699
			public Transform transform;

			// Token: 0x04000E74 RID: 3700
			public Quaternion rotation;

			// Token: 0x04000E75 RID: 3701
			public Grounding.Leg leg;
		}
	}
}

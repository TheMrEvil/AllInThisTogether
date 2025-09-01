using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000D6 RID: 214
	[Serializable]
	public class Grounding
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0003E027 File Offset: 0x0003C227
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x0003E02F File Offset: 0x0003C22F
		public Grounding.Leg[] legs
		{
			[CompilerGenerated]
			get
			{
				return this.<legs>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<legs>k__BackingField = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0003E038 File Offset: 0x0003C238
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x0003E040 File Offset: 0x0003C240
		public Grounding.Pelvis pelvis
		{
			[CompilerGenerated]
			get
			{
				return this.<pelvis>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<pelvis>k__BackingField = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0003E049 File Offset: 0x0003C249
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x0003E051 File Offset: 0x0003C251
		public bool isGrounded
		{
			[CompilerGenerated]
			get
			{
				return this.<isGrounded>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<isGrounded>k__BackingField = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0003E05A File Offset: 0x0003C25A
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x0003E062 File Offset: 0x0003C262
		public Transform root
		{
			[CompilerGenerated]
			get
			{
				return this.<root>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<root>k__BackingField = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0003E06B File Offset: 0x0003C26B
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x0003E073 File Offset: 0x0003C273
		public RaycastHit rootHit
		{
			[CompilerGenerated]
			get
			{
				return this.<rootHit>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<rootHit>k__BackingField = value;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x0003E07C File Offset: 0x0003C27C
		public bool rootGrounded
		{
			get
			{
				return this.rootHit.distance < this.maxStep * 2f;
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0003E0A8 File Offset: 0x0003C2A8
		public RaycastHit GetRootHit(float maxDistanceMlp = 10f)
		{
			RaycastHit result = default(RaycastHit);
			Vector3 up = this.up;
			Vector3 a = Vector3.zero;
			foreach (Grounding.Leg leg in this.legs)
			{
				a += leg.transform.position;
			}
			a /= (float)this.legs.Length;
			result.point = a - up * this.maxStep * 10f;
			float num = maxDistanceMlp + 1f;
			result.distance = this.maxStep * num;
			if (this.maxStep <= 0f)
			{
				return result;
			}
			if (this.quality != Grounding.Quality.Best)
			{
				Physics.Raycast(a + up * this.maxStep, -up, out result, this.maxStep * num, this.layers, QueryTriggerInteraction.Ignore);
			}
			else
			{
				Physics.SphereCast(a + up * this.maxStep, this.rootSphereCastRadius, -this.up, out result, this.maxStep * num, this.layers, QueryTriggerInteraction.Ignore);
			}
			return result;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0003E1D8 File Offset: 0x0003C3D8
		public bool IsValid(ref string errorMessage)
		{
			if (this.root == null)
			{
				errorMessage = "Root transform is null. Can't initiate Grounding.";
				return false;
			}
			if (this.legs == null)
			{
				errorMessage = "Grounding legs is null. Can't initiate Grounding.";
				return false;
			}
			if (this.pelvis == null)
			{
				errorMessage = "Grounding pelvis is null. Can't initiate Grounding.";
				return false;
			}
			if (this.legs.Length == 0)
			{
				errorMessage = "Grounding has 0 legs. Can't initiate Grounding.";
				return false;
			}
			return true;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0003E234 File Offset: 0x0003C434
		public void Initiate(Transform root, Transform[] feet)
		{
			this.root = root;
			this.initiated = false;
			this.rootHit = default(RaycastHit);
			if (this.legs == null)
			{
				this.legs = new Grounding.Leg[feet.Length];
			}
			if (this.legs.Length != feet.Length)
			{
				this.legs = new Grounding.Leg[feet.Length];
			}
			for (int i = 0; i < feet.Length; i++)
			{
				if (this.legs[i] == null)
				{
					this.legs[i] = new Grounding.Leg();
				}
			}
			if (this.pelvis == null)
			{
				this.pelvis = new Grounding.Pelvis();
			}
			string empty = string.Empty;
			if (!this.IsValid(ref empty))
			{
				Warning.Log(empty, root, false);
				return;
			}
			if (Application.isPlaying)
			{
				for (int j = 0; j < feet.Length; j++)
				{
					this.legs[j].Initiate(this, feet[j]);
				}
				this.pelvis.Initiate(this);
				this.initiated = true;
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0003E31C File Offset: 0x0003C51C
		public void Update()
		{
			if (!this.initiated)
			{
				return;
			}
			if (this.layers == 0)
			{
				this.LogWarning("Grounding layers are set to nothing. Please add a ground layer.");
			}
			this.maxStep = Mathf.Clamp(this.maxStep, 0f, this.maxStep);
			this.footRadius = Mathf.Clamp(this.footRadius, 0.0001f, this.maxStep);
			this.pelvisDamper = Mathf.Clamp(this.pelvisDamper, 0f, 1f);
			this.rootSphereCastRadius = Mathf.Clamp(this.rootSphereCastRadius, 0.0001f, this.rootSphereCastRadius);
			this.maxFootRotationAngle = Mathf.Clamp(this.maxFootRotationAngle, 0f, 90f);
			this.prediction = Mathf.Clamp(this.prediction, 0f, this.prediction);
			this.footSpeed = Mathf.Clamp(this.footSpeed, 0f, this.footSpeed);
			this.rootHit = this.GetRootHit(10f);
			float num = float.NegativeInfinity;
			float num2 = float.PositiveInfinity;
			this.isGrounded = false;
			foreach (Grounding.Leg leg in this.legs)
			{
				leg.Process();
				if (leg.IKOffset > num)
				{
					num = leg.IKOffset;
				}
				if (leg.IKOffset < num2)
				{
					num2 = leg.IKOffset;
				}
				if (leg.isGrounded)
				{
					this.isGrounded = true;
				}
			}
			num = Mathf.Max(num, 0f);
			num2 = Mathf.Min(num2, 0f);
			this.pelvis.Process(-num * this.lowerPelvisWeight, -num2 * this.liftPelvisWeight, this.isGrounded);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0003E4C0 File Offset: 0x0003C6C0
		public Vector3 GetLegsPlaneNormal()
		{
			if (!this.initiated)
			{
				return Vector3.up;
			}
			Vector3 up = this.up;
			Vector3 vector = up;
			for (int i = 0; i < this.legs.Length; i++)
			{
				Vector3 vector2 = this.legs[i].IKPosition - this.root.position;
				Vector3 vector3 = up;
				Vector3 fromDirection = vector2;
				Vector3.OrthoNormalize(ref vector3, ref fromDirection);
				vector = Quaternion.FromToRotation(fromDirection, vector2) * vector;
			}
			return vector;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0003E534 File Offset: 0x0003C734
		public void Reset()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.pelvis.Reset();
			Grounding.Leg[] legs = this.legs;
			for (int i = 0; i < legs.Length; i++)
			{
				legs[i].Reset();
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0003E571 File Offset: 0x0003C771
		public void LogWarning(string message)
		{
			Warning.Log(message, this.root, false);
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0003E580 File Offset: 0x0003C780
		public Vector3 up
		{
			get
			{
				if (!this.useRootRotation)
				{
					return Vector3.up;
				}
				return this.root.up;
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0003E59B File Offset: 0x0003C79B
		public float GetVerticalOffset(Vector3 p1, Vector3 p2)
		{
			if (this.useRootRotation)
			{
				return (Quaternion.Inverse(this.root.rotation) * (p1 - p2)).y;
			}
			return p1.y - p2.y;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0003E5D4 File Offset: 0x0003C7D4
		public Vector3 Flatten(Vector3 v)
		{
			if (this.useRootRotation)
			{
				Vector3 onNormal = v;
				Vector3 up = this.root.up;
				Vector3.OrthoNormalize(ref up, ref onNormal);
				return Vector3.Project(v, onNormal);
			}
			v.y = 0f;
			return v;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0003E615 File Offset: 0x0003C815
		private bool useRootRotation
		{
			get
			{
				return this.rotateSolver && !(this.root.up == Vector3.up);
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0003E63B File Offset: 0x0003C83B
		public Vector3 GetFootCenterOffset()
		{
			return this.root.forward * this.footRadius + this.root.forward * this.footCenterOffset;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0003E670 File Offset: 0x0003C870
		public Grounding()
		{
		}

		// Token: 0x0400074A RID: 1866
		[Tooltip("Layers to ground the character to. Make sure to exclude the layer of the character controller.")]
		public LayerMask layers;

		// Token: 0x0400074B RID: 1867
		[Tooltip("Max step height. Maximum vertical distance of Grounding from the root of the character.")]
		public float maxStep = 0.5f;

		// Token: 0x0400074C RID: 1868
		[Tooltip("The height offset of the root.")]
		public float heightOffset;

		// Token: 0x0400074D RID: 1869
		[Tooltip("The speed of moving the feet up/down.")]
		public float footSpeed = 2.5f;

		// Token: 0x0400074E RID: 1870
		[Tooltip("CapsuleCast radius. Should match approximately with the size of the feet.")]
		public float footRadius = 0.15f;

		// Token: 0x0400074F RID: 1871
		[Tooltip("Offset of the foot center along character forward axis.")]
		[HideInInspector]
		public float footCenterOffset;

		// Token: 0x04000750 RID: 1872
		[Tooltip("Amount of velocity based prediction of the foot positions.")]
		public float prediction = 0.05f;

		// Token: 0x04000751 RID: 1873
		[Tooltip("Weight of rotating the feet to the ground normal offset.")]
		[Range(0f, 1f)]
		public float footRotationWeight = 1f;

		// Token: 0x04000752 RID: 1874
		[Tooltip("Speed of slerping the feet to their grounded rotations.")]
		public float footRotationSpeed = 7f;

		// Token: 0x04000753 RID: 1875
		[Tooltip("Max Foot Rotation Angle. Max angular offset from the foot's rotation.")]
		[Range(0f, 90f)]
		public float maxFootRotationAngle = 45f;

		// Token: 0x04000754 RID: 1876
		[Tooltip("If true, solver will rotate with the character root so the character can be grounded for example to spherical planets. For performance reasons leave this off unless needed.")]
		public bool rotateSolver;

		// Token: 0x04000755 RID: 1877
		[Tooltip("The speed of moving the character up/down.")]
		public float pelvisSpeed = 5f;

		// Token: 0x04000756 RID: 1878
		[Tooltip("Used for smoothing out vertical pelvis movement (range 0 - 1).")]
		[Range(0f, 1f)]
		public float pelvisDamper;

		// Token: 0x04000757 RID: 1879
		[Tooltip("The weight of lowering the pelvis to the lowest foot.")]
		public float lowerPelvisWeight = 1f;

		// Token: 0x04000758 RID: 1880
		[Tooltip("The weight of lifting the pelvis to the highest foot. This is useful when you don't want the feet to go too high relative to the body when crouching.")]
		public float liftPelvisWeight;

		// Token: 0x04000759 RID: 1881
		[Tooltip("The radius of the spherecast from the root that determines whether the character root is grounded.")]
		public float rootSphereCastRadius = 0.1f;

		// Token: 0x0400075A RID: 1882
		[Tooltip("If false, keeps the foot that is over a ledge at the root level. If true, lowers the overstepping foot and body by the 'Max Step' value.")]
		public bool overstepFallsDown = true;

		// Token: 0x0400075B RID: 1883
		[Tooltip("The raycasting quality. Fastest is a single raycast per foot, Simple is three raycasts, Best is one raycast and a capsule cast per foot.")]
		public Grounding.Quality quality = Grounding.Quality.Best;

		// Token: 0x0400075C RID: 1884
		[CompilerGenerated]
		private Grounding.Leg[] <legs>k__BackingField;

		// Token: 0x0400075D RID: 1885
		[CompilerGenerated]
		private Grounding.Pelvis <pelvis>k__BackingField;

		// Token: 0x0400075E RID: 1886
		[CompilerGenerated]
		private bool <isGrounded>k__BackingField;

		// Token: 0x0400075F RID: 1887
		[CompilerGenerated]
		private Transform <root>k__BackingField;

		// Token: 0x04000760 RID: 1888
		[CompilerGenerated]
		private RaycastHit <rootHit>k__BackingField;

		// Token: 0x04000761 RID: 1889
		private bool initiated;

		// Token: 0x020001EB RID: 491
		[Serializable]
		public enum Quality
		{
			// Token: 0x04000E77 RID: 3703
			Fastest,
			// Token: 0x04000E78 RID: 3704
			Simple,
			// Token: 0x04000E79 RID: 3705
			Best
		}

		// Token: 0x020001EC RID: 492
		public class Leg
		{
			// Token: 0x1700020C RID: 524
			// (get) Token: 0x06001014 RID: 4116 RVA: 0x0006491E File Offset: 0x00062B1E
			// (set) Token: 0x06001015 RID: 4117 RVA: 0x00064926 File Offset: 0x00062B26
			public bool isGrounded
			{
				[CompilerGenerated]
				get
				{
					return this.<isGrounded>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<isGrounded>k__BackingField = value;
				}
			}

			// Token: 0x1700020D RID: 525
			// (get) Token: 0x06001016 RID: 4118 RVA: 0x0006492F File Offset: 0x00062B2F
			// (set) Token: 0x06001017 RID: 4119 RVA: 0x00064937 File Offset: 0x00062B37
			public Vector3 IKPosition
			{
				[CompilerGenerated]
				get
				{
					return this.<IKPosition>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<IKPosition>k__BackingField = value;
				}
			}

			// Token: 0x1700020E RID: 526
			// (get) Token: 0x06001018 RID: 4120 RVA: 0x00064940 File Offset: 0x00062B40
			// (set) Token: 0x06001019 RID: 4121 RVA: 0x00064948 File Offset: 0x00062B48
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

			// Token: 0x1700020F RID: 527
			// (get) Token: 0x0600101A RID: 4122 RVA: 0x00064951 File Offset: 0x00062B51
			// (set) Token: 0x0600101B RID: 4123 RVA: 0x00064959 File Offset: 0x00062B59
			public float heightFromGround
			{
				[CompilerGenerated]
				get
				{
					return this.<heightFromGround>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<heightFromGround>k__BackingField = value;
				}
			}

			// Token: 0x17000210 RID: 528
			// (get) Token: 0x0600101C RID: 4124 RVA: 0x00064962 File Offset: 0x00062B62
			// (set) Token: 0x0600101D RID: 4125 RVA: 0x0006496A File Offset: 0x00062B6A
			public Vector3 velocity
			{
				[CompilerGenerated]
				get
				{
					return this.<velocity>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<velocity>k__BackingField = value;
				}
			}

			// Token: 0x17000211 RID: 529
			// (get) Token: 0x0600101E RID: 4126 RVA: 0x00064973 File Offset: 0x00062B73
			// (set) Token: 0x0600101F RID: 4127 RVA: 0x0006497B File Offset: 0x00062B7B
			public Transform transform
			{
				[CompilerGenerated]
				get
				{
					return this.<transform>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<transform>k__BackingField = value;
				}
			}

			// Token: 0x17000212 RID: 530
			// (get) Token: 0x06001020 RID: 4128 RVA: 0x00064984 File Offset: 0x00062B84
			// (set) Token: 0x06001021 RID: 4129 RVA: 0x0006498C File Offset: 0x00062B8C
			public float IKOffset
			{
				[CompilerGenerated]
				get
				{
					return this.<IKOffset>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<IKOffset>k__BackingField = value;
				}
			}

			// Token: 0x17000213 RID: 531
			// (get) Token: 0x06001022 RID: 4130 RVA: 0x00064995 File Offset: 0x00062B95
			// (set) Token: 0x06001023 RID: 4131 RVA: 0x0006499D File Offset: 0x00062B9D
			public RaycastHit heelHit
			{
				[CompilerGenerated]
				get
				{
					return this.<heelHit>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<heelHit>k__BackingField = value;
				}
			}

			// Token: 0x17000214 RID: 532
			// (get) Token: 0x06001024 RID: 4132 RVA: 0x000649A6 File Offset: 0x00062BA6
			// (set) Token: 0x06001025 RID: 4133 RVA: 0x000649AE File Offset: 0x00062BAE
			public RaycastHit capsuleHit
			{
				[CompilerGenerated]
				get
				{
					return this.<capsuleHit>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<capsuleHit>k__BackingField = value;
				}
			}

			// Token: 0x17000215 RID: 533
			// (get) Token: 0x06001026 RID: 4134 RVA: 0x000649B7 File Offset: 0x00062BB7
			public RaycastHit GetHitPoint
			{
				get
				{
					if (this.grounding.quality == Grounding.Quality.Best)
					{
						return this.capsuleHit;
					}
					return this.heelHit;
				}
			}

			// Token: 0x06001027 RID: 4135 RVA: 0x000649D4 File Offset: 0x00062BD4
			public void SetFootPosition(Vector3 position)
			{
				this.doOverrideFootPosition = true;
				this.overrideFootPosition = position;
			}

			// Token: 0x06001028 RID: 4136 RVA: 0x000649E4 File Offset: 0x00062BE4
			public void Initiate(Grounding grounding, Transform transform)
			{
				this.initiated = false;
				this.grounding = grounding;
				this.transform = transform;
				this.up = Vector3.up;
				this.IKPosition = transform.position;
				this.rotationOffset = Quaternion.identity;
				this.initiated = true;
				this.OnEnable();
			}

			// Token: 0x06001029 RID: 4137 RVA: 0x00064A35 File Offset: 0x00062C35
			public void OnEnable()
			{
				if (!this.initiated)
				{
					return;
				}
				this.lastPosition = this.transform.position;
				this.lastTime = Time.deltaTime;
			}

			// Token: 0x0600102A RID: 4138 RVA: 0x00064A5C File Offset: 0x00062C5C
			public void Reset()
			{
				this.lastPosition = this.transform.position;
				this.lastTime = Time.deltaTime;
				this.IKOffset = 0f;
				this.IKPosition = this.transform.position;
				this.rotationOffset = Quaternion.identity;
			}

			// Token: 0x0600102B RID: 4139 RVA: 0x00064AAC File Offset: 0x00062CAC
			public void Process()
			{
				if (!this.initiated)
				{
					return;
				}
				if (this.grounding.maxStep <= 0f)
				{
					return;
				}
				this.transformPosition = (this.doOverrideFootPosition ? this.overrideFootPosition : this.transform.position);
				this.doOverrideFootPosition = false;
				this.deltaTime = Time.time - this.lastTime;
				this.lastTime = Time.time;
				if (this.deltaTime == 0f)
				{
					return;
				}
				this.up = this.grounding.up;
				this.heightFromGround = float.PositiveInfinity;
				this.velocity = (this.transformPosition - this.lastPosition) / this.deltaTime;
				this.lastPosition = this.transformPosition;
				Vector3 vector = this.velocity * this.grounding.prediction;
				if (this.grounding.footRadius <= 0f)
				{
					this.grounding.quality = Grounding.Quality.Fastest;
				}
				this.isGrounded = false;
				switch (this.grounding.quality)
				{
				case Grounding.Quality.Fastest:
				{
					RaycastHit raycastHit = this.GetRaycastHit(vector);
					this.SetFootToPoint(raycastHit.normal, raycastHit.point);
					if (raycastHit.collider != null)
					{
						this.isGrounded = true;
					}
					break;
				}
				case Grounding.Quality.Simple:
				{
					this.heelHit = this.GetRaycastHit(Vector3.zero);
					Vector3 a = this.grounding.GetFootCenterOffset();
					if (this.invertFootCenter)
					{
						a = -a;
					}
					RaycastHit raycastHit2 = this.GetRaycastHit(a + vector);
					RaycastHit raycastHit3 = this.GetRaycastHit(this.grounding.root.right * this.grounding.footRadius * 0.5f);
					if (this.heelHit.collider != null || raycastHit2.collider != null || raycastHit3.collider != null)
					{
						this.isGrounded = true;
					}
					Vector3 vector2 = Vector3.Cross(raycastHit2.point - this.heelHit.point, raycastHit3.point - this.heelHit.point).normalized;
					if (Vector3.Dot(vector2, this.up) < 0f)
					{
						vector2 = -vector2;
					}
					this.SetFootToPlane(vector2, this.heelHit.point, this.heelHit.point);
					break;
				}
				case Grounding.Quality.Best:
					this.heelHit = this.GetRaycastHit(this.invertFootCenter ? (-this.grounding.GetFootCenterOffset()) : Vector3.zero);
					this.capsuleHit = this.GetCapsuleHit(vector);
					if (this.heelHit.collider != null || this.capsuleHit.collider != null)
					{
						this.isGrounded = true;
					}
					this.SetFootToPlane(this.capsuleHit.normal, this.capsuleHit.point, this.heelHit.point);
					break;
				}
				float num = this.stepHeightFromGround;
				if (!this.grounding.rootGrounded)
				{
					num = 0f;
				}
				this.IKOffset = Interp.LerpValue(this.IKOffset, num, this.grounding.footSpeed, this.grounding.footSpeed);
				this.IKOffset = Mathf.Lerp(this.IKOffset, num, this.deltaTime * this.grounding.footSpeed);
				float verticalOffset = this.grounding.GetVerticalOffset(this.transformPosition, this.grounding.root.position);
				float num2 = Mathf.Clamp(this.grounding.maxStep - verticalOffset, 0f, this.grounding.maxStep);
				this.IKOffset = Mathf.Clamp(this.IKOffset, -num2, this.IKOffset);
				this.RotateFoot();
				this.IKPosition = this.transformPosition - this.up * this.IKOffset;
				float footRotationWeight = this.grounding.footRotationWeight;
				this.rotationOffset = ((footRotationWeight >= 1f) ? this.r : Quaternion.Slerp(Quaternion.identity, this.r, footRotationWeight));
			}

			// Token: 0x17000216 RID: 534
			// (get) Token: 0x0600102C RID: 4140 RVA: 0x00064F0B File Offset: 0x0006310B
			public float stepHeightFromGround
			{
				get
				{
					return Mathf.Clamp(this.heightFromGround, -this.grounding.maxStep, this.grounding.maxStep);
				}
			}

			// Token: 0x0600102D RID: 4141 RVA: 0x00064F30 File Offset: 0x00063130
			private RaycastHit GetCapsuleHit(Vector3 offsetFromHeel)
			{
				RaycastHit result = default(RaycastHit);
				Vector3 vector = this.grounding.GetFootCenterOffset();
				if (this.invertFootCenter)
				{
					vector = -vector;
				}
				Vector3 vector2 = this.transformPosition + vector;
				if (this.grounding.overstepFallsDown)
				{
					result.point = vector2 - this.up * this.grounding.maxStep;
				}
				else
				{
					result.point = new Vector3(vector2.x, this.grounding.root.position.y, vector2.z);
				}
				result.normal = this.up;
				Vector3 vector3 = vector2 + this.grounding.maxStep * this.up;
				Vector3 point = vector3 + offsetFromHeel;
				if (Physics.CapsuleCast(vector3, point, this.grounding.footRadius, -this.up, out result, this.grounding.maxStep * 2f, this.grounding.layers, QueryTriggerInteraction.Ignore) && float.IsNaN(result.point.x))
				{
					result.point = vector2 - this.up * this.grounding.maxStep * 2f;
					result.normal = this.up;
				}
				if (result.point == Vector3.zero && result.normal == Vector3.zero)
				{
					if (this.grounding.overstepFallsDown)
					{
						result.point = vector2 - this.up * this.grounding.maxStep;
					}
					else
					{
						result.point = new Vector3(vector2.x, this.grounding.root.position.y, vector2.z);
					}
				}
				return result;
			}

			// Token: 0x0600102E RID: 4142 RVA: 0x00065114 File Offset: 0x00063314
			private RaycastHit GetRaycastHit(Vector3 offsetFromHeel)
			{
				RaycastHit result = default(RaycastHit);
				Vector3 vector = this.transformPosition + offsetFromHeel;
				if (this.grounding.overstepFallsDown)
				{
					result.point = vector - this.up * this.grounding.maxStep;
				}
				else
				{
					result.point = new Vector3(vector.x, this.grounding.root.position.y, vector.z);
				}
				result.normal = this.up;
				if (this.grounding.maxStep <= 0f)
				{
					return result;
				}
				Physics.Raycast(vector + this.grounding.maxStep * this.up, -this.up, out result, this.grounding.maxStep * 2f, this.grounding.layers, QueryTriggerInteraction.Ignore);
				if (result.point == Vector3.zero && result.normal == Vector3.zero)
				{
					if (this.grounding.overstepFallsDown)
					{
						result.point = vector - this.up * this.grounding.maxStep;
					}
					else
					{
						result.point = new Vector3(vector.x, this.grounding.root.position.y, vector.z);
					}
				}
				return result;
			}

			// Token: 0x0600102F RID: 4143 RVA: 0x0006528D File Offset: 0x0006348D
			private Vector3 RotateNormal(Vector3 normal)
			{
				if (this.grounding.quality == Grounding.Quality.Best)
				{
					return normal;
				}
				return Vector3.RotateTowards(this.up, normal, this.grounding.maxFootRotationAngle * 0.017453292f, this.deltaTime);
			}

			// Token: 0x06001030 RID: 4144 RVA: 0x000652C2 File Offset: 0x000634C2
			private void SetFootToPoint(Vector3 normal, Vector3 point)
			{
				this.toHitNormal = Quaternion.FromToRotation(this.up, this.RotateNormal(normal));
				this.heightFromGround = this.GetHeightFromGround(point);
			}

			// Token: 0x06001031 RID: 4145 RVA: 0x000652EC File Offset: 0x000634EC
			private void SetFootToPlane(Vector3 planeNormal, Vector3 planePoint, Vector3 heelHitPoint)
			{
				planeNormal = this.RotateNormal(planeNormal);
				this.toHitNormal = Quaternion.FromToRotation(this.up, planeNormal);
				Vector3 hitPoint = V3Tools.LineToPlane(this.transformPosition + this.up * this.grounding.maxStep, -this.up, planeNormal, planePoint);
				this.heightFromGround = this.GetHeightFromGround(hitPoint);
				float heightFromGround = this.GetHeightFromGround(heelHitPoint);
				this.heightFromGround = Mathf.Clamp(this.heightFromGround, float.NegativeInfinity, heightFromGround);
			}

			// Token: 0x06001032 RID: 4146 RVA: 0x00065374 File Offset: 0x00063574
			private float GetHeightFromGround(Vector3 hitPoint)
			{
				return this.grounding.GetVerticalOffset(this.transformPosition, hitPoint) - this.rootYOffset;
			}

			// Token: 0x06001033 RID: 4147 RVA: 0x00065390 File Offset: 0x00063590
			private void RotateFoot()
			{
				Quaternion rotationOffsetTarget = this.GetRotationOffsetTarget();
				this.r = Quaternion.Slerp(this.r, rotationOffsetTarget, this.deltaTime * this.grounding.footRotationSpeed);
			}

			// Token: 0x06001034 RID: 4148 RVA: 0x000653C8 File Offset: 0x000635C8
			private Quaternion GetRotationOffsetTarget()
			{
				if (this.grounding.maxFootRotationAngle <= 0f)
				{
					return Quaternion.identity;
				}
				if (this.grounding.maxFootRotationAngle >= 180f)
				{
					return this.toHitNormal;
				}
				return Quaternion.RotateTowards(Quaternion.identity, this.toHitNormal, this.grounding.maxFootRotationAngle);
			}

			// Token: 0x17000217 RID: 535
			// (get) Token: 0x06001035 RID: 4149 RVA: 0x00065421 File Offset: 0x00063621
			private float rootYOffset
			{
				get
				{
					return this.grounding.GetVerticalOffset(this.transformPosition, this.grounding.root.position - this.up * this.grounding.heightOffset);
				}
			}

			// Token: 0x06001036 RID: 4150 RVA: 0x0006545F File Offset: 0x0006365F
			public Leg()
			{
			}

			// Token: 0x04000E7A RID: 3706
			[CompilerGenerated]
			private bool <isGrounded>k__BackingField;

			// Token: 0x04000E7B RID: 3707
			[CompilerGenerated]
			private Vector3 <IKPosition>k__BackingField;

			// Token: 0x04000E7C RID: 3708
			public Quaternion rotationOffset = Quaternion.identity;

			// Token: 0x04000E7D RID: 3709
			[CompilerGenerated]
			private bool <initiated>k__BackingField;

			// Token: 0x04000E7E RID: 3710
			[CompilerGenerated]
			private float <heightFromGround>k__BackingField;

			// Token: 0x04000E7F RID: 3711
			[CompilerGenerated]
			private Vector3 <velocity>k__BackingField;

			// Token: 0x04000E80 RID: 3712
			[CompilerGenerated]
			private Transform <transform>k__BackingField;

			// Token: 0x04000E81 RID: 3713
			[CompilerGenerated]
			private float <IKOffset>k__BackingField;

			// Token: 0x04000E82 RID: 3714
			public bool invertFootCenter;

			// Token: 0x04000E83 RID: 3715
			[CompilerGenerated]
			private RaycastHit <heelHit>k__BackingField;

			// Token: 0x04000E84 RID: 3716
			[CompilerGenerated]
			private RaycastHit <capsuleHit>k__BackingField;

			// Token: 0x04000E85 RID: 3717
			private Grounding grounding;

			// Token: 0x04000E86 RID: 3718
			private float lastTime;

			// Token: 0x04000E87 RID: 3719
			private float deltaTime;

			// Token: 0x04000E88 RID: 3720
			private Vector3 lastPosition;

			// Token: 0x04000E89 RID: 3721
			private Quaternion toHitNormal;

			// Token: 0x04000E8A RID: 3722
			private Quaternion r;

			// Token: 0x04000E8B RID: 3723
			private Vector3 up = Vector3.up;

			// Token: 0x04000E8C RID: 3724
			private bool doOverrideFootPosition;

			// Token: 0x04000E8D RID: 3725
			private Vector3 overrideFootPosition;

			// Token: 0x04000E8E RID: 3726
			private Vector3 transformPosition;
		}

		// Token: 0x020001ED RID: 493
		public class Pelvis
		{
			// Token: 0x17000218 RID: 536
			// (get) Token: 0x06001037 RID: 4151 RVA: 0x0006547D File Offset: 0x0006367D
			// (set) Token: 0x06001038 RID: 4152 RVA: 0x00065485 File Offset: 0x00063685
			public Vector3 IKOffset
			{
				[CompilerGenerated]
				get
				{
					return this.<IKOffset>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<IKOffset>k__BackingField = value;
				}
			}

			// Token: 0x17000219 RID: 537
			// (get) Token: 0x06001039 RID: 4153 RVA: 0x0006548E File Offset: 0x0006368E
			// (set) Token: 0x0600103A RID: 4154 RVA: 0x00065496 File Offset: 0x00063696
			public float heightOffset
			{
				[CompilerGenerated]
				get
				{
					return this.<heightOffset>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<heightOffset>k__BackingField = value;
				}
			}

			// Token: 0x0600103B RID: 4155 RVA: 0x0006549F File Offset: 0x0006369F
			public void Initiate(Grounding grounding)
			{
				this.grounding = grounding;
				this.initiated = true;
				this.OnEnable();
			}

			// Token: 0x0600103C RID: 4156 RVA: 0x000654B5 File Offset: 0x000636B5
			public void Reset()
			{
				this.lastRootPosition = this.grounding.root.transform.position;
				this.lastTime = Time.deltaTime;
				this.IKOffset = Vector3.zero;
				this.heightOffset = 0f;
			}

			// Token: 0x0600103D RID: 4157 RVA: 0x000654F3 File Offset: 0x000636F3
			public void OnEnable()
			{
				if (!this.initiated)
				{
					return;
				}
				this.lastRootPosition = this.grounding.root.transform.position;
				this.lastTime = Time.time;
			}

			// Token: 0x0600103E RID: 4158 RVA: 0x00065524 File Offset: 0x00063724
			public void Process(float lowestOffset, float highestOffset, bool isGrounded)
			{
				if (!this.initiated)
				{
					return;
				}
				float num = Time.time - this.lastTime;
				this.lastTime = Time.time;
				if (num <= 0f)
				{
					return;
				}
				float b = lowestOffset + highestOffset;
				if (!this.grounding.rootGrounded)
				{
					b = 0f;
				}
				this.heightOffset = Mathf.Lerp(this.heightOffset, b, num * this.grounding.pelvisSpeed);
				Vector3 p = this.grounding.root.position - this.lastRootPosition;
				this.lastRootPosition = this.grounding.root.position;
				this.damperF = Interp.LerpValue(this.damperF, isGrounded ? 1f : 0f, 1f, 10f);
				this.heightOffset -= this.grounding.GetVerticalOffset(p, Vector3.zero) * this.grounding.pelvisDamper * this.damperF;
				this.IKOffset = this.grounding.up * this.heightOffset;
			}

			// Token: 0x0600103F RID: 4159 RVA: 0x0006563A File Offset: 0x0006383A
			public Pelvis()
			{
			}

			// Token: 0x04000E8F RID: 3727
			[CompilerGenerated]
			private Vector3 <IKOffset>k__BackingField;

			// Token: 0x04000E90 RID: 3728
			[CompilerGenerated]
			private float <heightOffset>k__BackingField;

			// Token: 0x04000E91 RID: 3729
			private Grounding grounding;

			// Token: 0x04000E92 RID: 3730
			private Vector3 lastRootPosition;

			// Token: 0x04000E93 RID: 3731
			private float damperF;

			// Token: 0x04000E94 RID: 3732
			private bool initiated;

			// Token: 0x04000E95 RID: 3733
			private float lastTime;
		}
	}
}

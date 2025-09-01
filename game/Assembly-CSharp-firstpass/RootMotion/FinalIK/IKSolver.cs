using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000EE RID: 238
	[Serializable]
	public abstract class IKSolver
	{
		// Token: 0x06000A22 RID: 2594 RVA: 0x00043718 File Offset: 0x00041918
		public bool IsValid()
		{
			string empty = string.Empty;
			return this.IsValid(ref empty);
		}

		// Token: 0x06000A23 RID: 2595
		public abstract bool IsValid(ref string message);

		// Token: 0x06000A24 RID: 2596 RVA: 0x00043734 File Offset: 0x00041934
		public void Initiate(Transform root)
		{
			if (this.executedInEditor)
			{
				return;
			}
			if (this.OnPreInitiate != null)
			{
				this.OnPreInitiate();
			}
			if (root == null)
			{
				Debug.LogError("Initiating IKSolver with null root Transform.");
			}
			this.root = root;
			this.initiated = false;
			string empty = string.Empty;
			if (!this.IsValid(ref empty))
			{
				Warning.Log(empty, root, false);
				return;
			}
			this.OnInitiate();
			this.StoreDefaultLocalState();
			this.initiated = true;
			this.firstInitiation = false;
			if (this.OnPostInitiate != null)
			{
				this.OnPostInitiate();
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x000437C4 File Offset: 0x000419C4
		public void Update()
		{
			if (this.OnPreUpdate != null)
			{
				this.OnPreUpdate();
			}
			if (this.firstInitiation)
			{
				this.Initiate(this.root);
			}
			if (!this.initiated)
			{
				return;
			}
			this.OnUpdate();
			if (this.OnPostUpdate != null)
			{
				this.OnPostUpdate();
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0004381A File Offset: 0x00041A1A
		public virtual Vector3 GetIKPosition()
		{
			return this.IKPosition;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00043822 File Offset: 0x00041A22
		public void SetIKPosition(Vector3 position)
		{
			this.IKPosition = position;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0004382B File Offset: 0x00041A2B
		public float GetIKPositionWeight()
		{
			return this.IKPositionWeight;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00043833 File Offset: 0x00041A33
		public void SetIKPositionWeight(float weight)
		{
			this.IKPositionWeight = Mathf.Clamp(weight, 0f, 1f);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0004384B File Offset: 0x00041A4B
		public Transform GetRoot()
		{
			return this.root;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x00043853 File Offset: 0x00041A53
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x0004385B File Offset: 0x00041A5B
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

		// Token: 0x06000A2D RID: 2605
		public abstract IKSolver.Point[] GetPoints();

		// Token: 0x06000A2E RID: 2606
		public abstract IKSolver.Point GetPoint(Transform transform);

		// Token: 0x06000A2F RID: 2607
		public abstract void FixTransforms();

		// Token: 0x06000A30 RID: 2608
		public abstract void StoreDefaultLocalState();

		// Token: 0x06000A31 RID: 2609
		protected abstract void OnInitiate();

		// Token: 0x06000A32 RID: 2610
		protected abstract void OnUpdate();

		// Token: 0x06000A33 RID: 2611 RVA: 0x00043864 File Offset: 0x00041A64
		protected void LogWarning(string message)
		{
			Warning.Log(message, this.root, true);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00043874 File Offset: 0x00041A74
		public static Transform ContainsDuplicateBone(IKSolver.Bone[] bones)
		{
			for (int i = 0; i < bones.Length; i++)
			{
				for (int j = 0; j < bones.Length; j++)
				{
					if (i != j && bones[i].transform == bones[j].transform)
					{
						return bones[i].transform;
					}
				}
			}
			return null;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000438C4 File Offset: 0x00041AC4
		public static bool HierarchyIsValid(IKSolver.Bone[] bones)
		{
			for (int i = 1; i < bones.Length; i++)
			{
				if (!Hierarchy.IsAncestor(bones[i].transform, bones[i - 1].transform))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x000438FC File Offset: 0x00041AFC
		protected static float PreSolveBones(ref IKSolver.Bone[] bones)
		{
			float num = 0f;
			for (int i = 0; i < bones.Length; i++)
			{
				bones[i].solverPosition = bones[i].transform.position;
				bones[i].solverRotation = bones[i].transform.rotation;
			}
			for (int j = 0; j < bones.Length; j++)
			{
				if (j < bones.Length - 1)
				{
					bones[j].sqrMag = (bones[j + 1].solverPosition - bones[j].solverPosition).sqrMagnitude;
					bones[j].length = Mathf.Sqrt(bones[j].sqrMag);
					num += bones[j].length;
					bones[j].axis = Quaternion.Inverse(bones[j].solverRotation) * (bones[j + 1].solverPosition - bones[j].solverPosition);
				}
				else
				{
					bones[j].sqrMag = 0f;
					bones[j].length = 0f;
				}
			}
			return num;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00043A0E File Offset: 0x00041C0E
		protected IKSolver()
		{
		}

		// Token: 0x04000811 RID: 2065
		[HideInInspector]
		public bool executedInEditor;

		// Token: 0x04000812 RID: 2066
		[HideInInspector]
		public Vector3 IKPosition;

		// Token: 0x04000813 RID: 2067
		[Tooltip("The positional or the master weight of the solver.")]
		[Range(0f, 1f)]
		public float IKPositionWeight = 1f;

		// Token: 0x04000814 RID: 2068
		[CompilerGenerated]
		private bool <initiated>k__BackingField;

		// Token: 0x04000815 RID: 2069
		public IKSolver.UpdateDelegate OnPreInitiate;

		// Token: 0x04000816 RID: 2070
		public IKSolver.UpdateDelegate OnPostInitiate;

		// Token: 0x04000817 RID: 2071
		public IKSolver.UpdateDelegate OnPreUpdate;

		// Token: 0x04000818 RID: 2072
		public IKSolver.UpdateDelegate OnPostUpdate;

		// Token: 0x04000819 RID: 2073
		protected bool firstInitiation = true;

		// Token: 0x0400081A RID: 2074
		[SerializeField]
		[HideInInspector]
		protected Transform root;

		// Token: 0x020001F4 RID: 500
		[Serializable]
		public class Point
		{
			// Token: 0x0600106B RID: 4203 RVA: 0x00066443 File Offset: 0x00064643
			public void StoreDefaultLocalState()
			{
				this.defaultLocalPosition = this.transform.localPosition;
				this.defaultLocalRotation = this.transform.localRotation;
			}

			// Token: 0x0600106C RID: 4204 RVA: 0x00066468 File Offset: 0x00064668
			public void FixTransform()
			{
				if (this.transform.localPosition != this.defaultLocalPosition)
				{
					this.transform.localPosition = this.defaultLocalPosition;
				}
				if (this.transform.localRotation != this.defaultLocalRotation)
				{
					this.transform.localRotation = this.defaultLocalRotation;
				}
			}

			// Token: 0x0600106D RID: 4205 RVA: 0x000664C7 File Offset: 0x000646C7
			public void UpdateSolverPosition()
			{
				this.solverPosition = this.transform.position;
			}

			// Token: 0x0600106E RID: 4206 RVA: 0x000664DA File Offset: 0x000646DA
			public void UpdateSolverLocalPosition()
			{
				this.solverPosition = this.transform.localPosition;
			}

			// Token: 0x0600106F RID: 4207 RVA: 0x000664ED File Offset: 0x000646ED
			public void UpdateSolverState()
			{
				this.solverPosition = this.transform.position;
				this.solverRotation = this.transform.rotation;
			}

			// Token: 0x06001070 RID: 4208 RVA: 0x00066511 File Offset: 0x00064711
			public void UpdateSolverLocalState()
			{
				this.solverPosition = this.transform.localPosition;
				this.solverRotation = this.transform.localRotation;
			}

			// Token: 0x06001071 RID: 4209 RVA: 0x00066535 File Offset: 0x00064735
			public Point()
			{
			}

			// Token: 0x04000ED8 RID: 3800
			public Transform transform;

			// Token: 0x04000ED9 RID: 3801
			[Range(0f, 1f)]
			public float weight = 1f;

			// Token: 0x04000EDA RID: 3802
			public Vector3 solverPosition;

			// Token: 0x04000EDB RID: 3803
			public Quaternion solverRotation = Quaternion.identity;

			// Token: 0x04000EDC RID: 3804
			public Vector3 defaultLocalPosition;

			// Token: 0x04000EDD RID: 3805
			public Quaternion defaultLocalRotation;
		}

		// Token: 0x020001F5 RID: 501
		[Serializable]
		public class Bone : IKSolver.Point
		{
			// Token: 0x17000221 RID: 545
			// (get) Token: 0x06001072 RID: 4210 RVA: 0x00066554 File Offset: 0x00064754
			// (set) Token: 0x06001073 RID: 4211 RVA: 0x000665A2 File Offset: 0x000647A2
			public RotationLimit rotationLimit
			{
				get
				{
					if (!this.isLimited)
					{
						return null;
					}
					if (this._rotationLimit == null)
					{
						this._rotationLimit = this.transform.GetComponent<RotationLimit>();
					}
					this.isLimited = (this._rotationLimit != null);
					return this._rotationLimit;
				}
				set
				{
					this._rotationLimit = value;
					this.isLimited = (value != null);
				}
			}

			// Token: 0x06001074 RID: 4212 RVA: 0x000665B8 File Offset: 0x000647B8
			public void Swing(Vector3 swingTarget, float weight = 1f)
			{
				if (weight <= 0f)
				{
					return;
				}
				Quaternion quaternion = Quaternion.FromToRotation(this.transform.rotation * this.axis, swingTarget - this.transform.position);
				if (weight >= 1f)
				{
					this.transform.rotation = quaternion * this.transform.rotation;
					return;
				}
				this.transform.rotation = Quaternion.Lerp(Quaternion.identity, quaternion, weight) * this.transform.rotation;
			}

			// Token: 0x06001075 RID: 4213 RVA: 0x00066648 File Offset: 0x00064848
			public static void SolverSwing(IKSolver.Bone[] bones, int index, Vector3 swingTarget, float weight = 1f)
			{
				if (weight <= 0f)
				{
					return;
				}
				Quaternion quaternion = Quaternion.FromToRotation(bones[index].solverRotation * bones[index].axis, swingTarget - bones[index].solverPosition);
				if (weight >= 1f)
				{
					for (int i = index; i < bones.Length; i++)
					{
						bones[i].solverRotation = quaternion * bones[i].solverRotation;
					}
					return;
				}
				for (int j = index; j < bones.Length; j++)
				{
					bones[j].solverRotation = Quaternion.Lerp(Quaternion.identity, quaternion, weight) * bones[j].solverRotation;
				}
			}

			// Token: 0x06001076 RID: 4214 RVA: 0x000666E4 File Offset: 0x000648E4
			public void Swing2D(Vector3 swingTarget, float weight = 1f)
			{
				if (weight <= 0f)
				{
					return;
				}
				Vector3 vector = this.transform.rotation * this.axis;
				Vector3 vector2 = swingTarget - this.transform.position;
				float current = Mathf.Atan2(vector.x, vector.y) * 57.29578f;
				float target = Mathf.Atan2(vector2.x, vector2.y) * 57.29578f;
				this.transform.rotation = Quaternion.AngleAxis(Mathf.DeltaAngle(current, target) * weight, Vector3.back) * this.transform.rotation;
			}

			// Token: 0x06001077 RID: 4215 RVA: 0x00066781 File Offset: 0x00064981
			public void SetToSolverPosition()
			{
				this.transform.position = this.solverPosition;
			}

			// Token: 0x06001078 RID: 4216 RVA: 0x00066794 File Offset: 0x00064994
			public Bone()
			{
			}

			// Token: 0x06001079 RID: 4217 RVA: 0x000667B3 File Offset: 0x000649B3
			public Bone(Transform transform)
			{
				this.transform = transform;
			}

			// Token: 0x0600107A RID: 4218 RVA: 0x000667D9 File Offset: 0x000649D9
			public Bone(Transform transform, float weight)
			{
				this.transform = transform;
				this.weight = weight;
			}

			// Token: 0x04000EDE RID: 3806
			public float length;

			// Token: 0x04000EDF RID: 3807
			public float sqrMag;

			// Token: 0x04000EE0 RID: 3808
			public Vector3 axis = -Vector3.right;

			// Token: 0x04000EE1 RID: 3809
			private RotationLimit _rotationLimit;

			// Token: 0x04000EE2 RID: 3810
			private bool isLimited = true;
		}

		// Token: 0x020001F6 RID: 502
		[Serializable]
		public class Node : IKSolver.Point
		{
			// Token: 0x0600107B RID: 4219 RVA: 0x00066806 File Offset: 0x00064A06
			public Node()
			{
			}

			// Token: 0x0600107C RID: 4220 RVA: 0x0006680E File Offset: 0x00064A0E
			public Node(Transform transform)
			{
				this.transform = transform;
			}

			// Token: 0x0600107D RID: 4221 RVA: 0x0006681D File Offset: 0x00064A1D
			public Node(Transform transform, float weight)
			{
				this.transform = transform;
				this.weight = weight;
			}

			// Token: 0x04000EE3 RID: 3811
			public float length;

			// Token: 0x04000EE4 RID: 3812
			public float effectorPositionWeight;

			// Token: 0x04000EE5 RID: 3813
			public float effectorRotationWeight;

			// Token: 0x04000EE6 RID: 3814
			public Vector3 offset;
		}

		// Token: 0x020001F7 RID: 503
		// (Invoke) Token: 0x0600107F RID: 4223
		public delegate void UpdateDelegate();

		// Token: 0x020001F8 RID: 504
		// (Invoke) Token: 0x06001083 RID: 4227
		public delegate void IterationDelegate(int i);
	}
}

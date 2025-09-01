using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000CF RID: 207
	public class FingerRig : SolverManager
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0003B792 File Offset: 0x00039992
		// (set) Token: 0x060008E7 RID: 2279 RVA: 0x0003B79A File Offset: 0x0003999A
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

		// Token: 0x060008E8 RID: 2280 RVA: 0x0003B7A4 File Offset: 0x000399A4
		public bool IsValid(ref string errorMessage)
		{
			Finger[] array = this.fingers;
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].IsValid(ref errorMessage))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0003B7D4 File Offset: 0x000399D4
		[ContextMenu("Auto-detect")]
		public void AutoDetect()
		{
			this.fingers = new Finger[0];
			for (int i = 0; i < base.transform.childCount; i++)
			{
				Transform[] array = new Transform[0];
				this.AddChildrenRecursive(base.transform.GetChild(i), ref array);
				if (array.Length == 3 || array.Length == 4)
				{
					Finger finger = new Finger();
					finger.bone1 = array[0];
					finger.bone2 = array[1];
					if (array.Length == 3)
					{
						finger.tip = array[2];
					}
					else
					{
						finger.bone3 = array[2];
						finger.tip = array[3];
					}
					finger.weight = 1f;
					Array.Resize<Finger>(ref this.fingers, this.fingers.Length + 1);
					this.fingers[this.fingers.Length - 1] = finger;
				}
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0003B8A0 File Offset: 0x00039AA0
		public void AddFinger(Transform bone1, Transform bone2, Transform bone3, Transform tip, Transform target = null)
		{
			Finger finger = new Finger();
			finger.bone1 = bone1;
			finger.bone2 = bone2;
			finger.bone3 = bone3;
			finger.tip = tip;
			finger.target = target;
			Array.Resize<Finger>(ref this.fingers, this.fingers.Length + 1);
			this.fingers[this.fingers.Length - 1] = finger;
			this.initiated = false;
			finger.Initiate(base.transform, this.fingers.Length - 1);
			if (this.fingers[this.fingers.Length - 1].initiated)
			{
				this.initiated = true;
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0003B93C File Offset: 0x00039B3C
		public void RemoveFinger(int index)
		{
			if ((float)index < 0f || index >= this.fingers.Length)
			{
				Warning.Log("RemoveFinger index out of bounds.", base.transform, false);
				return;
			}
			if (this.fingers.Length == 1)
			{
				this.fingers = new Finger[0];
				return;
			}
			Finger[] array = new Finger[this.fingers.Length - 1];
			int num = 0;
			for (int i = 0; i < this.fingers.Length; i++)
			{
				if (i != index)
				{
					array[num] = this.fingers[i];
					num++;
				}
			}
			this.fingers = array;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0003B9C6 File Offset: 0x00039BC6
		private void AddChildrenRecursive(Transform parent, ref Transform[] array)
		{
			Array.Resize<Transform>(ref array, array.Length + 1);
			array[array.Length - 1] = parent;
			if (parent.childCount != 1)
			{
				return;
			}
			this.AddChildrenRecursive(parent.GetChild(0), ref array);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0003B9F8 File Offset: 0x00039BF8
		protected override void InitiateSolver()
		{
			this.initiated = true;
			for (int i = 0; i < this.fingers.Length; i++)
			{
				this.fingers[i].Initiate(base.transform, i);
				if (!this.fingers[i].initiated)
				{
					this.initiated = false;
				}
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0003BA4C File Offset: 0x00039C4C
		public void UpdateFingerSolvers()
		{
			Finger[] array = this.fingers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Update(this.weight);
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0003BA7C File Offset: 0x00039C7C
		public void FixFingerTransforms()
		{
			if (this.weight <= 0f)
			{
				return;
			}
			Finger[] array = this.fingers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].FixTransforms();
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0003BAB4 File Offset: 0x00039CB4
		public void StoreDefaultLocalState()
		{
			Finger[] array = this.fingers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].StoreDefaultLocalState();
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0003BADE File Offset: 0x00039CDE
		protected override void UpdateSolver()
		{
			this.UpdateFingerSolvers();
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0003BAE6 File Offset: 0x00039CE6
		protected override void FixTransforms()
		{
			if (this.weight <= 0f)
			{
				return;
			}
			this.FixFingerTransforms();
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0003BAFC File Offset: 0x00039CFC
		public FingerRig()
		{
		}

		// Token: 0x04000705 RID: 1797
		[Tooltip("The master weight for all fingers.")]
		[Range(0f, 1f)]
		public float weight = 1f;

		// Token: 0x04000706 RID: 1798
		public Finger[] fingers = new Finger[0];

		// Token: 0x04000707 RID: 1799
		[CompilerGenerated]
		private bool <initiated>k__BackingField;
	}
}

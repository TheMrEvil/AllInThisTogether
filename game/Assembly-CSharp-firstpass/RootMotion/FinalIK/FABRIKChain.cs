using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000E4 RID: 228
	[Serializable]
	public class FABRIKChain
	{
		// Token: 0x060009B6 RID: 2486 RVA: 0x0003EEF4 File Offset: 0x0003D0F4
		public bool IsValid(ref string message)
		{
			if (this.ik == null)
			{
				message = "IK unassigned in FABRIKChain.";
				return false;
			}
			return this.ik.solver.IsValid(ref message);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0003EF23 File Offset: 0x0003D123
		public void Initiate()
		{
			this.ik.enabled = false;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0003EF34 File Offset: 0x0003D134
		public void Stage1(FABRIKChain[] chain)
		{
			for (int i = 0; i < this.children.Length; i++)
			{
				chain[this.children[i]].Stage1(chain);
			}
			if (this.children.Length == 0)
			{
				this.ik.solver.SolveForward(this.ik.solver.GetIKPosition());
				return;
			}
			this.ik.solver.SolveForward(this.GetCentroid(chain));
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0003EFA8 File Offset: 0x0003D1A8
		public void Stage2(Vector3 rootPosition, FABRIKChain[] chain)
		{
			this.ik.solver.SolveBackward(rootPosition);
			for (int i = 0; i < this.children.Length; i++)
			{
				chain[this.children[i]].Stage2(this.ik.solver.bones[this.ik.solver.bones.Length - 1].transform.position, chain);
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0003F018 File Offset: 0x0003D218
		private Vector3 GetCentroid(FABRIKChain[] chain)
		{
			Vector3 ikposition = this.ik.solver.GetIKPosition();
			if (this.pin >= 1f)
			{
				return ikposition;
			}
			float num = 0f;
			for (int i = 0; i < this.children.Length; i++)
			{
				num += chain[this.children[i]].pull;
			}
			if (num <= 0f)
			{
				return ikposition;
			}
			if (num < 1f)
			{
				num = 1f;
			}
			Vector3 vector = ikposition;
			for (int j = 0; j < this.children.Length; j++)
			{
				Vector3 a = chain[this.children[j]].ik.solver.bones[0].solverPosition - ikposition;
				float d = chain[this.children[j]].pull / num;
				vector += a * d;
			}
			if (this.pin <= 0f)
			{
				return vector;
			}
			return vector + (ikposition - vector) * this.pin;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0003F115 File Offset: 0x0003D315
		public FABRIKChain()
		{
		}

		// Token: 0x04000772 RID: 1906
		public FABRIK ik;

		// Token: 0x04000773 RID: 1907
		[Range(0f, 1f)]
		public float pull = 1f;

		// Token: 0x04000774 RID: 1908
		[Range(0f, 1f)]
		public float pin = 1f;

		// Token: 0x04000775 RID: 1909
		public int[] children = new int[0];
	}
}

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000D0 RID: 208
	public abstract class Grounder : MonoBehaviour
	{
		// Token: 0x060008F4 RID: 2292
		public abstract void ResetPosition();

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0003BB1B File Offset: 0x00039D1B
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x0003BB23 File Offset: 0x00039D23
		public bool initiated
		{
			[CompilerGenerated]
			get
			{
				return this.<initiated>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<initiated>k__BackingField = value;
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0003BB2C File Offset: 0x00039D2C
		protected Vector3 GetSpineOffsetTarget()
		{
			Vector3 vector = Vector3.zero;
			for (int i = 0; i < this.solver.legs.Length; i++)
			{
				vector += this.GetLegSpineBendVector(this.solver.legs[i]);
			}
			return vector;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0003BB72 File Offset: 0x00039D72
		protected void LogWarning(string message)
		{
			Warning.Log(message, base.transform, false);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0003BB84 File Offset: 0x00039D84
		private Vector3 GetLegSpineBendVector(Grounding.Leg leg)
		{
			Vector3 legSpineTangent = this.GetLegSpineTangent(leg);
			float d = (Vector3.Dot(this.solver.root.forward, legSpineTangent.normalized) + 1f) * 0.5f;
			float magnitude = (leg.IKPosition - leg.transform.position).magnitude;
			return legSpineTangent * magnitude * d;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0003BBF0 File Offset: 0x00039DF0
		private Vector3 GetLegSpineTangent(Grounding.Leg leg)
		{
			Vector3 vector = leg.transform.position - this.solver.root.position;
			if (!this.solver.rotateSolver || this.solver.root.up == Vector3.up)
			{
				return new Vector3(vector.x, 0f, vector.z);
			}
			Vector3 up = this.solver.root.up;
			Vector3.OrthoNormalize(ref up, ref vector);
			return vector;
		}

		// Token: 0x060008FB RID: 2299
		protected abstract void OpenUserManual();

		// Token: 0x060008FC RID: 2300
		protected abstract void OpenScriptReference();

		// Token: 0x060008FD RID: 2301 RVA: 0x0003BC79 File Offset: 0x00039E79
		protected Grounder()
		{
		}

		// Token: 0x04000708 RID: 1800
		[Tooltip("The master weight. Use this to fade in/out the grounding effect.")]
		[Range(0f, 1f)]
		public float weight = 1f;

		// Token: 0x04000709 RID: 1801
		[Tooltip("The Grounding solver. Not to confuse with IK solvers.")]
		public Grounding solver = new Grounding();

		// Token: 0x0400070A RID: 1802
		public Grounder.GrounderDelegate OnPreGrounder;

		// Token: 0x0400070B RID: 1803
		public Grounder.GrounderDelegate OnPostGrounder;

		// Token: 0x0400070C RID: 1804
		[CompilerGenerated]
		private bool <initiated>k__BackingField;

		// Token: 0x020001E8 RID: 488
		// (Invoke) Token: 0x0600100E RID: 4110
		public delegate void GrounderDelegate();
	}
}

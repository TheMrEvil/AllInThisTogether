using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000149 RID: 329
	public class RecoilTest : MonoBehaviour
	{
		// Token: 0x06000D28 RID: 3368 RVA: 0x000595FC File Offset: 0x000577FC
		private void Start()
		{
			this.recoil = base.GetComponent<Recoil>();
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0005960A File Offset: 0x0005780A
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(0))
			{
				this.recoil.Fire(this.magnitude);
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0005962E File Offset: 0x0005782E
		private void OnGUI()
		{
			GUILayout.Label("Press R or LMB for procedural recoil.", Array.Empty<GUILayoutOption>());
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0005963F File Offset: 0x0005783F
		public RecoilTest()
		{
		}

		// Token: 0x04000AD7 RID: 2775
		public float magnitude = 1f;

		// Token: 0x04000AD8 RID: 2776
		private Recoil recoil;
	}
}

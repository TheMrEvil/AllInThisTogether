using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200013A RID: 314
	public class FPSCharacter : MonoBehaviour
	{
		// Token: 0x06000CF0 RID: 3312 RVA: 0x00058012 File Offset: 0x00056212
		private void Start()
		{
			this.animator = base.GetComponent<Animator>();
			this.FPSAiming = base.GetComponent<FPSAiming>();
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0005802C File Offset: 0x0005622C
		private void Update()
		{
			this.FPSAiming.sightWeight = Mathf.SmoothDamp(this.FPSAiming.sightWeight, Input.GetMouseButton(1) ? 1f : 0f, ref this.sVel, 0.1f);
			if (this.FPSAiming.sightWeight < 0.001f)
			{
				this.FPSAiming.sightWeight = 0f;
			}
			if (this.FPSAiming.sightWeight > 0.999f)
			{
				this.FPSAiming.sightWeight = 1f;
			}
			this.animator.SetFloat("Speed", this.walkSpeed);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x000580CD File Offset: 0x000562CD
		private void OnGUI()
		{
			GUI.Label(new Rect((float)(Screen.width - 210), 10f, 200f, 25f), "Hold RMB to aim down the sight");
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x000580F9 File Offset: 0x000562F9
		public FPSCharacter()
		{
		}

		// Token: 0x04000A87 RID: 2695
		[Range(0f, 1f)]
		public float walkSpeed = 0.5f;

		// Token: 0x04000A88 RID: 2696
		private float sVel;

		// Token: 0x04000A89 RID: 2697
		private Animator animator;

		// Token: 0x04000A8A RID: 2698
		private FPSAiming FPSAiming;
	}
}

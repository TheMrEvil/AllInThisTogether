using System;
using UnityEngine;

namespace Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.AimAssists
{
	// Token: 0x02000010 RID: 16
	public class AimEaseIn : MonoBehaviour
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000026E4 File Offset: 0x000008E4
		public Vector2 AssistAim(Vector2 lookInputDelta)
		{
			if (!this.aimAssistEnabled)
			{
				return lookInputDelta;
			}
			float num = Mathf.Abs(lookInputDelta.x);
			float num2 = Mathf.Abs(lookInputDelta.y);
			float x = (num < num2) ? (lookInputDelta.x * this.smoothnessMultiplier) : lookInputDelta.x;
			float y = (num2 < num) ? (lookInputDelta.y * this.smoothnessMultiplier) : lookInputDelta.y;
			return new Vector2(x, y);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000274C File Offset: 0x0000094C
		public AimEaseIn()
		{
		}

		// Token: 0x04000020 RID: 32
		[Header("Master switch")]
		[Tooltip("Enables or disables the aim assist.")]
		public bool aimAssistEnabled = true;

		// Token: 0x04000021 RID: 33
		[Tooltip("Determines the multiplier for the less dominant axis for the input. The lower the value, the less the less dominant axis will be taken into account.")]
		[Range(0.1f, 1f)]
		public float smoothnessMultiplier = 0.6f;
	}
}

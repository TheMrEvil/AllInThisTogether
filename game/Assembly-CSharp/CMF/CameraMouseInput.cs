using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003B2 RID: 946
	public class CameraMouseInput : CameraInput
	{
		// Token: 0x06001F4C RID: 8012 RVA: 0x000BB3A8 File Offset: 0x000B95A8
		public override float GetHorizontalCameraInput()
		{
			float num = Input.GetAxisRaw(this.mouseHorizontalAxis);
			if (Time.timeScale > 0f && Time.deltaTime > 0f)
			{
				num /= Time.deltaTime;
				num *= Time.timeScale;
			}
			else
			{
				num = 0f;
			}
			num *= this.mouseInputMultiplier;
			if (this.invertHorizontalInput)
			{
				num *= -1f;
			}
			return num;
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x000BB40C File Offset: 0x000B960C
		public override float GetVerticalCameraInput()
		{
			float num = -Input.GetAxisRaw(this.mouseVerticalAxis);
			if (Time.timeScale > 0f && Time.deltaTime > 0f)
			{
				num /= Time.deltaTime;
				num *= Time.timeScale;
			}
			else
			{
				num = 0f;
			}
			num *= this.mouseInputMultiplier;
			if (this.invertVerticalInput)
			{
				num *= -1f;
			}
			return num;
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x000BB470 File Offset: 0x000B9670
		public CameraMouseInput()
		{
		}

		// Token: 0x04001F91 RID: 8081
		public string mouseHorizontalAxis = "Mouse X";

		// Token: 0x04001F92 RID: 8082
		public string mouseVerticalAxis = "Mouse Y";

		// Token: 0x04001F93 RID: 8083
		public bool invertHorizontalInput;

		// Token: 0x04001F94 RID: 8084
		public bool invertVerticalInput;

		// Token: 0x04001F95 RID: 8085
		public float mouseInputMultiplier = 0.01f;
	}
}

using System;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.AimAssists;
using UnityEngine;

namespace Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Helper.Chaining
{
	// Token: 0x0200000C RID: 12
	public sealed class LookInputBasedAimAssistChainer
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000023B1 File Offset: 0x000005B1
		public LookInputBasedAimAssistChainer WithLookInputDelta(Vector2 lookInputDelta)
		{
			this.lookInputDelta = lookInputDelta;
			return this;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023BB File Offset: 0x000005BB
		public LookInputBasedAimAssistChainer UsingPrecisionAim(PrecisionAim precisionAim)
		{
			this.precisionAim = precisionAim;
			return this;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023C5 File Offset: 0x000005C5
		public LookInputBasedAimAssistChainer UsingAimEaseIn(AimEaseIn aimEaseIn)
		{
			this.aimEaseIn = aimEaseIn;
			return this;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023CF File Offset: 0x000005CF
		public LookInputBasedAimAssistChainer UsingAutoAim(AutoAim autoAim)
		{
			this.autoAim = autoAim;
			return this;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023DC File Offset: 0x000005DC
		public Vector2 GetModifiedLookInputDelta()
		{
			if (this.autoAim != null)
			{
				this.lookInputDelta = this.autoAim.AssistAim(this.lookInputDelta);
			}
			if (this.aimEaseIn != null)
			{
				this.lookInputDelta = this.aimEaseIn.AssistAim(this.lookInputDelta);
			}
			if (this.precisionAim != null)
			{
				this.lookInputDelta = this.precisionAim.AssistAim(this.lookInputDelta);
			}
			return this.lookInputDelta;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000245E File Offset: 0x0000065E
		public LookInputBasedAimAssistChainer()
		{
		}

		// Token: 0x04000017 RID: 23
		private Vector2 lookInputDelta;

		// Token: 0x04000018 RID: 24
		private PrecisionAim precisionAim;

		// Token: 0x04000019 RID: 25
		private AimEaseIn aimEaseIn;

		// Token: 0x0400001A RID: 26
		private AutoAim autoAim;
	}
}

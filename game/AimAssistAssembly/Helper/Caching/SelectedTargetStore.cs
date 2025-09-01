using System;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Target;

namespace Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Helper.Caching
{
	// Token: 0x0200000E RID: 14
	public class SelectedTargetStore
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000025B8 File Offset: 0x000007B8
		public void ProcessTarget(AimAssistTarget target)
		{
			if (target)
			{
				this.OnTargetFound(target);
				return;
			}
			this.NotifyAndEraseTargetIfExists();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000025D0 File Offset: 0x000007D0
		private void NotifyAndStoreTarget(AimAssistTarget target)
		{
			this.selectedTarget = target;
			this.selectedTarget.TargetSelected.Invoke();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025E9 File Offset: 0x000007E9
		private void NotifyAndEraseTargetIfExists()
		{
			if (!this.selectedTarget)
			{
				return;
			}
			this.selectedTarget.TargetLost.Invoke();
			this.selectedTarget = null;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002610 File Offset: 0x00000810
		private void OnTargetFound(AimAssistTarget target)
		{
			if (target == this.selectedTarget)
			{
				return;
			}
			this.NotifyAndEraseTargetIfExists();
			this.NotifyAndStoreTarget(target);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000262E File Offset: 0x0000082E
		public SelectedTargetStore()
		{
		}

		// Token: 0x0400001D RID: 29
		private AimAssistTarget selectedTarget;
	}
}

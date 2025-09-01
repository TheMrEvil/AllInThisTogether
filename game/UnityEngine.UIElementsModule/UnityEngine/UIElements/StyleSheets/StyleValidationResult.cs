using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200037A RID: 890
	internal struct StyleValidationResult
	{
		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x00087D98 File Offset: 0x00085F98
		public bool success
		{
			get
			{
				return this.status == StyleValidationStatus.Ok;
			}
		}

		// Token: 0x04000E51 RID: 3665
		public StyleValidationStatus status;

		// Token: 0x04000E52 RID: 3666
		public string message;

		// Token: 0x04000E53 RID: 3667
		public string errorValue;

		// Token: 0x04000E54 RID: 3668
		public string hint;
	}
}

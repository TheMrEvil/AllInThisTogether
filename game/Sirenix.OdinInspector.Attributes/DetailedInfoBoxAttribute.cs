using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[DontApplyToListElements]
	[Conditional("UNITY_EDITOR")]
	public class DetailedInfoBoxAttribute : Attribute
	{
		// Token: 0x06000043 RID: 67 RVA: 0x0000250A File Offset: 0x0000070A
		public DetailedInfoBoxAttribute(string message, string details, InfoMessageType infoMessageType = InfoMessageType.Info, string visibleIf = null)
		{
			this.Message = message;
			this.Details = details;
			this.InfoMessageType = infoMessageType;
			this.VisibleIf = visibleIf;
		}

		// Token: 0x04000037 RID: 55
		public string Message;

		// Token: 0x04000038 RID: 56
		public string Details;

		// Token: 0x04000039 RID: 57
		public InfoMessageType InfoMessageType;

		// Token: 0x0400003A RID: 58
		public string VisibleIf;
	}
}

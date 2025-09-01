using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000058 RID: 88
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class RequiredAttribute : Attribute
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00003516 File Offset: 0x00001716
		public RequiredAttribute()
		{
			this.MessageType = InfoMessageType.Error;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003525 File Offset: 0x00001725
		public RequiredAttribute(string errorMessage, InfoMessageType messageType)
		{
			this.ErrorMessage = errorMessage;
			this.MessageType = messageType;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000353B File Offset: 0x0000173B
		public RequiredAttribute(string errorMessage)
		{
			this.ErrorMessage = errorMessage;
			this.MessageType = InfoMessageType.Error;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003551 File Offset: 0x00001751
		public RequiredAttribute(InfoMessageType messageType)
		{
			this.MessageType = messageType;
		}

		// Token: 0x040000F4 RID: 244
		public string ErrorMessage;

		// Token: 0x040000F5 RID: 245
		public InfoMessageType MessageType;
	}
}

using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200007B RID: 123
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class ValidateInputAttribute : Attribute
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00003FE2 File Offset: 0x000021E2
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00003FEA File Offset: 0x000021EA
		[Obsolete("Use the Condition member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string MemberName
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.Condition = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00003FF3 File Offset: 0x000021F3
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00003FFB File Offset: 0x000021FB
		[Obsolete("Use the ContinuousValidationCheck member instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ContiniousValidationCheck
		{
			get
			{
				return this.ContinuousValidationCheck;
			}
			set
			{
				this.ContinuousValidationCheck = value;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00004004 File Offset: 0x00002204
		public ValidateInputAttribute(string condition, string defaultMessage = null, InfoMessageType messageType = InfoMessageType.Error)
		{
			this.Condition = condition;
			this.DefaultMessage = defaultMessage;
			this.MessageType = messageType;
			this.IncludeChildren = true;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00004004 File Offset: 0x00002204
		[Obsolete("Rejecting invalid input is no longer supported. Use the other constructor instead.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ValidateInputAttribute(string condition, string message, InfoMessageType messageType, bool rejectedInvalidInput)
		{
			this.Condition = condition;
			this.DefaultMessage = message;
			this.MessageType = messageType;
			this.IncludeChildren = true;
		}

		// Token: 0x04000246 RID: 582
		public string DefaultMessage;

		// Token: 0x04000247 RID: 583
		public string Condition;

		// Token: 0x04000248 RID: 584
		public InfoMessageType MessageType;

		// Token: 0x04000249 RID: 585
		public bool IncludeChildren;

		// Token: 0x0400024A RID: 586
		public bool ContinuousValidationCheck;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020009D2 RID: 2514
	[Serializable]
	internal sealed class ContractException : Exception
	{
		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06005A32 RID: 23090 RVA: 0x00134167 File Offset: 0x00132367
		public ContractFailureKind Kind
		{
			get
			{
				return this._Kind;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06005A33 RID: 23091 RVA: 0x0013416F File Offset: 0x0013236F
		public string Failure
		{
			get
			{
				return this.Message;
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06005A34 RID: 23092 RVA: 0x00134177 File Offset: 0x00132377
		public string UserMessage
		{
			get
			{
				return this._UserMessage;
			}
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06005A35 RID: 23093 RVA: 0x0013417F File Offset: 0x0013237F
		public string Condition
		{
			get
			{
				return this._Condition;
			}
		}

		// Token: 0x06005A36 RID: 23094 RVA: 0x00134187 File Offset: 0x00132387
		private ContractException()
		{
			base.HResult = -2146233022;
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x0013419A File Offset: 0x0013239A
		public ContractException(ContractFailureKind kind, string failure, string userMessage, string condition, Exception innerException) : base(failure, innerException)
		{
			base.HResult = -2146233022;
			this._Kind = kind;
			this._UserMessage = userMessage;
			this._Condition = condition;
		}

		// Token: 0x06005A38 RID: 23096 RVA: 0x001341C6 File Offset: 0x001323C6
		private ContractException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._Kind = (ContractFailureKind)info.GetInt32("Kind");
			this._UserMessage = info.GetString("UserMessage");
			this._Condition = info.GetString("Condition");
		}

		// Token: 0x06005A39 RID: 23097 RVA: 0x00134204 File Offset: 0x00132404
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Kind", this._Kind);
			info.AddValue("UserMessage", this._UserMessage);
			info.AddValue("Condition", this._Condition);
		}

		// Token: 0x040037BA RID: 14266
		private readonly ContractFailureKind _Kind;

		// Token: 0x040037BB RID: 14267
		private readonly string _UserMessage;

		// Token: 0x040037BC RID: 14268
		private readonly string _Condition;
	}
}

using System;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000024 RID: 36
	public class OperationResponse
	{
		// Token: 0x17000086 RID: 134
		public object this[byte parameterCode]
		{
			get
			{
				object result;
				this.Parameters.TryGetValue(parameterCode, out result);
				return result;
			}
			set
			{
				this.Parameters.Add(parameterCode, value);
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
		public override string ToString()
		{
			return string.Format("OperationResponse {0}: ReturnCode: {1}.", this.OperationCode, this.ReturnCode);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000D0D4 File Offset: 0x0000B2D4
		public string ToStringFull()
		{
			return string.Format("OperationResponse {0}: ReturnCode: {1} ({3}). Parameters: {2}", new object[]
			{
				this.OperationCode,
				this.ReturnCode,
				SupportClass.DictionaryToString(this.Parameters, true),
				this.DebugMessage
			});
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000D12F File Offset: 0x0000B32F
		public OperationResponse()
		{
		}

		// Token: 0x04000165 RID: 357
		public byte OperationCode;

		// Token: 0x04000166 RID: 358
		public short ReturnCode;

		// Token: 0x04000167 RID: 359
		public string DebugMessage;

		// Token: 0x04000168 RID: 360
		public ParameterDictionary Parameters;
	}
}

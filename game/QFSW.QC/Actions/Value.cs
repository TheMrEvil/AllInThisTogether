using System;

namespace QFSW.QC.Actions
{
	// Token: 0x02000078 RID: 120
	public class Value : ICommandAction
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000A8F9 File Offset: 0x00008AF9
		public bool IsFinished
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000A8FC File Offset: 0x00008AFC
		public bool StartsIdle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A8FF File Offset: 0x00008AFF
		public Value(object value, bool newline = true)
		{
			this._value = value;
			this._newline = newline;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A915 File Offset: 0x00008B15
		public void Start(ActionContext context)
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A918 File Offset: 0x00008B18
		public void Finalize(ActionContext context)
		{
			QuantumConsole console = context.Console;
			string logText = (this._value as string) ?? console.Serialize(this._value);
			console.LogToConsole(logText, this._newline);
		}

		// Token: 0x04000160 RID: 352
		private readonly object _value;

		// Token: 0x04000161 RID: 353
		private readonly bool _newline;
	}
}

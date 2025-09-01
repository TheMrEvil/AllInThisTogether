using System;

namespace QFSW.QC.Actions
{
	// Token: 0x02000074 RID: 116
	public class ReadLine : ICommandAction
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000A7AC File Offset: 0x000089AC
		public bool IsFinished
		{
			get
			{
				return this._response != null;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000A7B7 File Offset: 0x000089B7
		public bool StartsIdle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A7BA File Offset: 0x000089BA
		public ReadLine(Action<string> getInput, ResponseConfig config)
		{
			if (getInput == null)
			{
				throw new ArgumentNullException("getInput");
			}
			this._getInput = getInput;
			this._config = config;
			this._console = null;
			this._response = null;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A7EC File Offset: 0x000089EC
		public ReadLine(Action<string> getInput) : this(getInput, ResponseConfig.Default)
		{
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000A7FA File Offset: 0x000089FA
		public void Finalize(ActionContext context)
		{
			this._getInput(this._response);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A80D File Offset: 0x00008A0D
		public void Start(ActionContext context)
		{
			this._response = null;
			this._console = context.Console;
			this._console.BeginResponse(new Action<string>(this.OnResponseSubmittedHandler), this._config);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A83F File Offset: 0x00008A3F
		private void OnResponseSubmittedHandler(string response)
		{
			this._response = response;
		}

		// Token: 0x04000159 RID: 345
		private readonly Action<string> _getInput;

		// Token: 0x0400015A RID: 346
		private readonly ResponseConfig _config;

		// Token: 0x0400015B RID: 347
		private QuantumConsole _console;

		// Token: 0x0400015C RID: 348
		private string _response;
	}
}

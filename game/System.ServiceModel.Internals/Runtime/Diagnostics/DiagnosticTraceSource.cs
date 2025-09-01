using System;
using System.Diagnostics;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000041 RID: 65
	internal class DiagnosticTraceSource : TraceSource
	{
		// Token: 0x06000262 RID: 610 RVA: 0x0000A22D File Offset: 0x0000842D
		internal DiagnosticTraceSource(string name) : base(name)
		{
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A236 File Offset: 0x00008436
		protected override string[] GetSupportedAttributes()
		{
			return new string[]
			{
				"propagateActivity"
			};
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000A248 File Offset: 0x00008448
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000A27D File Offset: 0x0000847D
		internal bool PropagateActivity
		{
			get
			{
				bool result = false;
				string value = base.Attributes["propagateActivity"];
				if (!string.IsNullOrEmpty(value) && !bool.TryParse(value, out result))
				{
					result = false;
				}
				return result;
			}
			set
			{
				base.Attributes["propagateActivity"] = value.ToString();
			}
		}

		// Token: 0x04000156 RID: 342
		private const string PropagateActivityValue = "propagateActivity";
	}
}

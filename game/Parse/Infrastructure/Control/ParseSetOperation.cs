using System;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Control;
using Parse.Infrastructure.Data;

namespace Parse.Infrastructure.Control
{
	// Token: 0x02000071 RID: 113
	public class ParseSetOperation : IParseFieldOperation
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x00011C43 File Offset: 0x0000FE43
		public ParseSetOperation(object value)
		{
			this.Value = value;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00011C52 File Offset: 0x0000FE52
		public object Encode(IServiceHub serviceHub)
		{
			return PointerOrLocalIdEncoder.Instance.Encode(this.Value, serviceHub);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00011C65 File Offset: 0x0000FE65
		public IParseFieldOperation MergeWithPrevious(IParseFieldOperation previous)
		{
			return this;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00011C68 File Offset: 0x0000FE68
		public object Apply(object oldValue, string key)
		{
			return this.Value;
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00011C70 File Offset: 0x0000FE70
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00011C78 File Offset: 0x0000FE78
		public object Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Value>k__BackingField = value;
			}
		}

		// Token: 0x040000FE RID: 254
		[CompilerGenerated]
		private object <Value>k__BackingField;
	}
}

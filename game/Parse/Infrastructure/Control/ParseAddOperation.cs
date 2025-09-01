using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Control;
using Parse.Infrastructure.Data;
using Parse.Infrastructure.Utilities;

namespace Parse.Infrastructure.Control
{
	// Token: 0x02000069 RID: 105
	public class ParseAddOperation : IParseFieldOperation
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0001046B File Offset: 0x0000E66B
		private ReadOnlyCollection<object> Data
		{
			[CompilerGenerated]
			get
			{
				return this.<Data>k__BackingField;
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00010473 File Offset: 0x0000E673
		public ParseAddOperation(IEnumerable<object> objects)
		{
			this.Data = new ReadOnlyCollection<object>(objects.ToList<object>());
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001048C File Offset: 0x0000E68C
		public object Encode(IServiceHub serviceHub)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["__op"] = "Add";
			dictionary["objects"] = PointerOrLocalIdEncoder.Instance.Encode(this.Data, serviceHub);
			return dictionary;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000104C0 File Offset: 0x0000E6C0
		public IParseFieldOperation MergeWithPrevious(IParseFieldOperation previous)
		{
			IParseFieldOperation result;
			if (previous != null)
			{
				if (!(previous is ParseDeleteOperation))
				{
					ParseSetOperation parseSetOperation = previous as ParseSetOperation;
					if (parseSetOperation == null)
					{
						ParseAddOperation parseAddOperation = previous as ParseAddOperation;
						if (parseAddOperation == null)
						{
							throw new InvalidOperationException("Operation is invalid after previous operation.");
						}
						result = new ParseAddOperation(parseAddOperation.Objects.Concat(this.Data));
					}
					else
					{
						result = new ParseSetOperation(Conversion.To<IList<object>>(parseSetOperation.Value).Concat(this.Data).ToList<object>());
					}
				}
				else
				{
					result = new ParseSetOperation(this.Data.ToList<object>());
				}
			}
			else
			{
				result = this;
			}
			return result;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001054D File Offset: 0x0000E74D
		public object Apply(object oldValue, string key)
		{
			if (oldValue == null)
			{
				return this.Data.ToList<object>();
			}
			return Conversion.To<IList<object>>(oldValue).Concat(this.Data).ToList<object>();
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00010574 File Offset: 0x0000E774
		public IEnumerable<object> Objects
		{
			get
			{
				return this.Data;
			}
		}

		// Token: 0x040000F2 RID: 242
		[CompilerGenerated]
		private readonly ReadOnlyCollection<object> <Data>k__BackingField;
	}
}

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
	// Token: 0x02000070 RID: 112
	public class ParseRemoveOperation : IParseFieldOperation
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00011B44 File Offset: 0x0000FD44
		private ReadOnlyCollection<object> Data
		{
			[CompilerGenerated]
			get
			{
				return this.<Data>k__BackingField;
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00011B4C File Offset: 0x0000FD4C
		public ParseRemoveOperation(IEnumerable<object> objects)
		{
			this.Data = new ReadOnlyCollection<object>(objects.Distinct<object>().ToList<object>());
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00011B6A File Offset: 0x0000FD6A
		public object Encode(IServiceHub serviceHub)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["__op"] = "Remove";
			dictionary["objects"] = PointerOrLocalIdEncoder.Instance.Encode(this.Data, serviceHub);
			return dictionary;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00011BA0 File Offset: 0x0000FDA0
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
						ParseRemoveOperation parseRemoveOperation = previous as ParseRemoveOperation;
						if (parseRemoveOperation == null)
						{
							throw new InvalidOperationException("Operation is invalid after previous operation.");
						}
						result = new ParseRemoveOperation(parseRemoveOperation.Objects.Concat(this.Data));
					}
					else
					{
						result = new ParseSetOperation(this.Apply(Conversion.As<IList<object>>(parseSetOperation.Value), null));
					}
				}
				else
				{
					result = previous;
				}
			}
			else
			{
				result = this;
			}
			return result;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00011C15 File Offset: 0x0000FE15
		public object Apply(object oldValue, string key)
		{
			if (oldValue == null)
			{
				return new List<object>();
			}
			return Conversion.As<IList<object>>(oldValue).Except(this.Data, ParseFieldOperations.ParseObjectComparer).ToList<object>();
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00011C3B File Offset: 0x0000FE3B
		public IEnumerable<object> Objects
		{
			get
			{
				return this.Data;
			}
		}

		// Token: 0x040000FD RID: 253
		[CompilerGenerated]
		private readonly ReadOnlyCollection<object> <Data>k__BackingField;
	}
}

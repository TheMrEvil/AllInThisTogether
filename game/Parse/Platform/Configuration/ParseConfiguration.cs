using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Data;
using Parse.Infrastructure.Data;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Configuration
{
	// Token: 0x02000038 RID: 56
	public class ParseConfiguration : IJsonConvertible
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000AA79 File Offset: 0x00008C79
		private IDictionary<string, object> Properties
		{
			[CompilerGenerated]
			get
			{
				return this.<Properties>k__BackingField;
			}
		} = new Dictionary<string, object>();

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000AA81 File Offset: 0x00008C81
		private IServiceHub Services
		{
			[CompilerGenerated]
			get
			{
				return this.<Services>k__BackingField;
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000AA89 File Offset: 0x00008C89
		internal ParseConfiguration(IServiceHub serviceHub)
		{
			this.Services = serviceHub;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000AAA3 File Offset: 0x00008CA3
		private ParseConfiguration(IDictionary<string, object> properties, IServiceHub serviceHub) : this(serviceHub)
		{
			this.Properties = properties;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000AAB3 File Offset: 0x00008CB3
		internal static ParseConfiguration Create(IDictionary<string, object> configurationData, IParseDataDecoder decoder, IServiceHub serviceHub)
		{
			return new ParseConfiguration(decoder.Decode(configurationData["params"], serviceHub) as IDictionary<string, object>, serviceHub);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000AAD2 File Offset: 0x00008CD2
		public T Get<T>(string key)
		{
			return Conversion.To<T>(this.Properties[key]);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		public bool TryGetValue<T>(string key, out T result)
		{
			if (this.Properties.ContainsKey(key))
			{
				try
				{
					T t = Conversion.To<T>(this.Properties[key]);
					result = t;
					return true;
				}
				catch
				{
				}
			}
			result = default(T);
			return false;
		}

		// Token: 0x170000AD RID: 173
		public virtual object this[string key]
		{
			get
			{
				return this.Properties[key];
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000AB4E File Offset: 0x00008D4E
		IDictionary<string, object> IJsonConvertible.ConvertToJSON()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["params"] = NoObjectsEncoder.Instance.Encode(this.Properties, this.Services);
			return dictionary;
		}

		// Token: 0x0400007C RID: 124
		[CompilerGenerated]
		private readonly IDictionary<string, object> <Properties>k__BackingField;

		// Token: 0x0400007D RID: 125
		[CompilerGenerated]
		private readonly IServiceHub <Services>k__BackingField;
	}
}

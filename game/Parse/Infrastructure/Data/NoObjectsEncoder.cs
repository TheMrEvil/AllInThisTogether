using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Parse.Infrastructure.Data
{
	// Token: 0x02000064 RID: 100
	public class NoObjectsEncoder : ParseDataEncoder
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000FB40 File Offset: 0x0000DD40
		public static NoObjectsEncoder Instance
		{
			[CompilerGenerated]
			get
			{
				return NoObjectsEncoder.<Instance>k__BackingField;
			}
		} = new NoObjectsEncoder();

		// Token: 0x06000483 RID: 1155 RVA: 0x0000FB47 File Offset: 0x0000DD47
		protected override IDictionary<string, object> EncodeObject(ParseObject value)
		{
			throw new ArgumentException("ParseObjects not allowed here.");
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000FB53 File Offset: 0x0000DD53
		public NoObjectsEncoder()
		{
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000FB5B File Offset: 0x0000DD5B
		// Note: this type is marked as 'beforefieldinit'.
		static NoObjectsEncoder()
		{
		}

		// Token: 0x040000ED RID: 237
		[CompilerGenerated]
		private static readonly NoObjectsEncoder <Instance>k__BackingField;
	}
}

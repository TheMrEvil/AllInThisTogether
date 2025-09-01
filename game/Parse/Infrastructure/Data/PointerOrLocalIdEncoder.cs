using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Parse.Infrastructure.Data
{
	// Token: 0x02000068 RID: 104
	public class PointerOrLocalIdEncoder : ParseDataEncoder
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x000103F9 File Offset: 0x0000E5F9
		public static PointerOrLocalIdEncoder Instance
		{
			[CompilerGenerated]
			get
			{
				return PointerOrLocalIdEncoder.<Instance>k__BackingField;
			}
		} = new PointerOrLocalIdEncoder();

		// Token: 0x06000499 RID: 1177 RVA: 0x00010400 File Offset: 0x0000E600
		protected override IDictionary<string, object> EncodeObject(ParseObject value)
		{
			if (value.ObjectId == null)
			{
				throw new InvalidOperationException("Cannot create a pointer to an object without an objectId.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["__type"] = "Pointer";
			dictionary["className"] = value.ClassName;
			dictionary["objectId"] = value.ObjectId;
			return dictionary;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00010457 File Offset: 0x0000E657
		public PointerOrLocalIdEncoder()
		{
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001045F File Offset: 0x0000E65F
		// Note: this type is marked as 'beforefieldinit'.
		static PointerOrLocalIdEncoder()
		{
		}

		// Token: 0x040000F1 RID: 241
		[CompilerGenerated]
		private static readonly PointerOrLocalIdEncoder <Instance>k__BackingField;
	}
}

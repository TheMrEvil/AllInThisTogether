using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x0200024B RID: 587
	internal sealed class AlphabeticalEnumConverter : EnumConverter
	{
		// Token: 0x06001213 RID: 4627 RVA: 0x0004E288 File Offset: 0x0004C488
		public AlphabeticalEnumConverter(Type type) : base(type)
		{
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0004E291 File Offset: 0x0004C491
		[MonoTODO("Create sorted standart values")]
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return base.Values;
		}
	}
}

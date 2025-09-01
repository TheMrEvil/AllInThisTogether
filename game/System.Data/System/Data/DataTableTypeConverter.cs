using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000D4 RID: 212
	internal sealed class DataTableTypeConverter : ReferenceConverter
	{
		// Token: 0x06000CDB RID: 3291 RVA: 0x000350A7 File Offset: 0x000332A7
		public DataTableTypeConverter() : base(typeof(DataTable))
		{
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}

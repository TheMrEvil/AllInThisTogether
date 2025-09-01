using System;
using QFSW.QC.Utilities;

namespace QFSW.QC.Serializers
{
	// Token: 0x02000009 RID: 9
	public class TypeSerialiazer : PolymorphicQcSerializer<Type>
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000022FE File Offset: 0x000004FE
		public override string SerializeFormatted(Type value, QuantumTheme theme)
		{
			return value.GetDisplayName(false);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002307 File Offset: 0x00000507
		public TypeSerialiazer()
		{
		}
	}
}

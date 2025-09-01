using System;

namespace QFSW.QC.Parsers
{
	// Token: 0x0200000A RID: 10
	public class NullableParser : GenericQcParser
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000026D5 File Offset: 0x000008D5
		protected override Type GenericType
		{
			get
			{
				return typeof(Nullable<>);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026E4 File Offset: 0x000008E4
		public override object Parse(string value, Type type)
		{
			if (value == "null")
			{
				return null;
			}
			Type type2 = type.GetGenericArguments()[0];
			return base.ParseRecursive(value, type2);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002711 File Offset: 0x00000911
		public NullableParser()
		{
		}
	}
}

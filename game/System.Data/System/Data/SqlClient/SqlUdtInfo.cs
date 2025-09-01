using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000232 RID: 562
	internal class SqlUdtInfo
	{
		// Token: 0x06001B0A RID: 6922 RVA: 0x0007C558 File Offset: 0x0007A758
		private SqlUdtInfo(SqlUserDefinedTypeAttribute attr)
		{
			this.SerializationFormat = attr.Format;
			this.IsByteOrdered = attr.IsByteOrdered;
			this.IsFixedLength = attr.IsFixedLength;
			this.MaxByteSize = attr.MaxByteSize;
			this.Name = attr.Name;
			this.ValidationMethodName = attr.ValidationMethodName;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0007C5B3 File Offset: 0x0007A7B3
		internal static SqlUdtInfo GetFromType(Type target)
		{
			SqlUdtInfo sqlUdtInfo = SqlUdtInfo.TryGetFromType(target);
			if (sqlUdtInfo == null)
			{
				throw InvalidUdtException.Create(target, "no UDT attribute");
			}
			return sqlUdtInfo;
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0007C5CC File Offset: 0x0007A7CC
		internal static SqlUdtInfo TryGetFromType(Type target)
		{
			if (SqlUdtInfo.s_types2UdtInfo == null)
			{
				SqlUdtInfo.s_types2UdtInfo = new Dictionary<Type, SqlUdtInfo>();
			}
			SqlUdtInfo sqlUdtInfo = null;
			if (!SqlUdtInfo.s_types2UdtInfo.TryGetValue(target, out sqlUdtInfo))
			{
				object[] customAttributes = target.GetCustomAttributes(typeof(SqlUserDefinedTypeAttribute), false);
				if (customAttributes != null && customAttributes.Length == 1)
				{
					sqlUdtInfo = new SqlUdtInfo((SqlUserDefinedTypeAttribute)customAttributes[0]);
				}
				SqlUdtInfo.s_types2UdtInfo.Add(target, sqlUdtInfo);
			}
			return sqlUdtInfo;
		}

		// Token: 0x04001147 RID: 4423
		internal readonly Format SerializationFormat;

		// Token: 0x04001148 RID: 4424
		internal readonly bool IsByteOrdered;

		// Token: 0x04001149 RID: 4425
		internal readonly bool IsFixedLength;

		// Token: 0x0400114A RID: 4426
		internal readonly int MaxByteSize;

		// Token: 0x0400114B RID: 4427
		internal readonly string Name;

		// Token: 0x0400114C RID: 4428
		internal readonly string ValidationMethodName;

		// Token: 0x0400114D RID: 4429
		[ThreadStatic]
		private static Dictionary<Type, SqlUdtInfo> s_types2UdtInfo;
	}
}

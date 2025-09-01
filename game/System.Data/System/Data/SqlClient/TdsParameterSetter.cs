using System;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000246 RID: 582
	internal class TdsParameterSetter : SmiTypedGetterSetter
	{
		// Token: 0x06001BD1 RID: 7121 RVA: 0x0007DD90 File Offset: 0x0007BF90
		internal TdsParameterSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._target = new TdsRecordBufferSetter(stateObj, md);
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x00006D64 File Offset: 0x00004F64
		internal override bool CanGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x00006D61 File Offset: 0x00004F61
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x0007DDA5 File Offset: 0x0007BFA5
		internal override SmiTypedGetterSetter GetTypedGetterSetter(SmiEventSink sink, int ordinal)
		{
			return this._target;
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x0007DDAD File Offset: 0x0007BFAD
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._target.EndElements(sink);
		}

		// Token: 0x04001312 RID: 4882
		private TdsRecordBufferSetter _target;
	}
}

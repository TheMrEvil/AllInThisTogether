using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001FC RID: 508
	internal class SqlNotification : MarshalByRefObject
	{
		// Token: 0x060018AF RID: 6319 RVA: 0x00072373 File Offset: 0x00070573
		internal SqlNotification(SqlNotificationInfo info, SqlNotificationSource source, SqlNotificationType type, string key)
		{
			this._info = info;
			this._source = source;
			this._type = type;
			this._key = key;
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x00072398 File Offset: 0x00070598
		internal SqlNotificationInfo Info
		{
			get
			{
				return this._info;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x000723A0 File Offset: 0x000705A0
		internal string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x000723A8 File Offset: 0x000705A8
		internal SqlNotificationSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x000723B0 File Offset: 0x000705B0
		internal SqlNotificationType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000FC9 RID: 4041
		private readonly SqlNotificationInfo _info;

		// Token: 0x04000FCA RID: 4042
		private readonly SqlNotificationSource _source;

		// Token: 0x04000FCB RID: 4043
		private readonly SqlNotificationType _type;

		// Token: 0x04000FCC RID: 4044
		private readonly string _key;
	}
}

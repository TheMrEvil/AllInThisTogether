using System;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;

namespace System.Data.SqlClient
{
	// Token: 0x0200021D RID: 541
	internal sealed class SqlReferenceCollection : DbReferenceCollection
	{
		// Token: 0x06001A68 RID: 6760 RVA: 0x0007A25A File Offset: 0x0007845A
		public override void Add(object value, int tag)
		{
			base.AddItem(value, tag);
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0007A264 File Offset: 0x00078464
		internal void Deactivate()
		{
			base.Notify(0);
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x0007A270 File Offset: 0x00078470
		internal SqlDataReader FindLiveReader(SqlCommand command)
		{
			if (command == null)
			{
				return base.FindItem<SqlDataReader>(1, (SqlDataReader dataReader) => !dataReader.IsClosed);
			}
			return base.FindItem<SqlDataReader>(1, (SqlDataReader dataReader) => !dataReader.IsClosed && command == dataReader.Command);
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x0007A2CC File Offset: 0x000784CC
		internal SqlCommand FindLiveCommand(TdsParserStateObject stateObj)
		{
			return base.FindItem<SqlCommand>(2, (SqlCommand command) => command.StateObject == stateObj);
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x0007A2FC File Offset: 0x000784FC
		protected override void NotifyItem(int message, int tag, object value)
		{
			if (tag == 1)
			{
				SqlDataReader sqlDataReader = (SqlDataReader)value;
				if (!sqlDataReader.IsClosed)
				{
					sqlDataReader.CloseReaderFromConnection();
					return;
				}
			}
			else
			{
				if (tag == 2)
				{
					((SqlCommand)value).OnConnectionClosed();
					return;
				}
				if (tag == 3)
				{
					((SqlBulkCopy)value).OnConnectionClosed();
				}
			}
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x0007A342 File Offset: 0x00078542
		public override void Remove(object value)
		{
			base.RemoveItem(value);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x0007A34B File Offset: 0x0007854B
		public SqlReferenceCollection()
		{
		}

		// Token: 0x040010F2 RID: 4338
		internal const int DataReaderTag = 1;

		// Token: 0x040010F3 RID: 4339
		internal const int CommandTag = 2;

		// Token: 0x040010F4 RID: 4340
		internal const int BulkCopyTag = 3;

		// Token: 0x0200021E RID: 542
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x06001A6F RID: 6767 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x06001A70 RID: 6768 RVA: 0x0007A353 File Offset: 0x00078553
			internal bool <FindLiveReader>b__1(SqlDataReader dataReader)
			{
				return !dataReader.IsClosed && this.command == dataReader.Command;
			}

			// Token: 0x040010F5 RID: 4341
			public SqlCommand command;
		}

		// Token: 0x0200021F RID: 543
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001A71 RID: 6769 RVA: 0x0007A36D File Offset: 0x0007856D
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001A72 RID: 6770 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c()
			{
			}

			// Token: 0x06001A73 RID: 6771 RVA: 0x0007A379 File Offset: 0x00078579
			internal bool <FindLiveReader>b__5_0(SqlDataReader dataReader)
			{
				return !dataReader.IsClosed;
			}

			// Token: 0x040010F6 RID: 4342
			public static readonly SqlReferenceCollection.<>c <>9 = new SqlReferenceCollection.<>c();

			// Token: 0x040010F7 RID: 4343
			public static Func<SqlDataReader, bool> <>9__5_0;
		}

		// Token: 0x02000220 RID: 544
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06001A74 RID: 6772 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06001A75 RID: 6773 RVA: 0x0007A384 File Offset: 0x00078584
			internal bool <FindLiveCommand>b__0(SqlCommand command)
			{
				return command.StateObject == this.stateObj;
			}

			// Token: 0x040010F8 RID: 4344
			public TdsParserStateObject stateObj;
		}
	}
}

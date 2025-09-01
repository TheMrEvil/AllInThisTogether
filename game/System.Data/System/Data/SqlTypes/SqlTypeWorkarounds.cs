using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;

namespace System.Data.SqlTypes
{
	// Token: 0x02000322 RID: 802
	internal static class SqlTypeWorkarounds
	{
		// Token: 0x06002621 RID: 9761 RVA: 0x000A9FBC File Offset: 0x000A81BC
		internal static XmlReader SqlXmlCreateSqlXmlReader(Stream stream, bool closeInput = false, bool async = false)
		{
			XmlReaderSettings settings = closeInput ? (async ? SqlTypeWorkarounds.s_defaultXmlReaderSettingsAsyncCloseInput : SqlTypeWorkarounds.s_defaultXmlReaderSettingsCloseInput) : SqlTypeWorkarounds.s_defaultXmlReaderSettings;
			return XmlReader.Create(stream, settings);
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x000A9FEC File Offset: 0x000A81EC
		internal static DateTime SqlDateTimeToDateTime(int daypart, int timepart)
		{
			if (daypart < -53690 || daypart > 2958463 || timepart < 0 || timepart > 25919999)
			{
				throw new OverflowException(SQLResource.DateTimeOverflowMessage);
			}
			long ticks = new DateTime(1900, 1, 1).Ticks;
			long num = (long)daypart * 864000000000L;
			long num2 = (long)((double)timepart / 0.3 + 0.5) * 10000L;
			return new DateTime(ticks + num + num2);
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000AA06C File Offset: 0x000A826C
		internal static SqlMoney SqlMoneyCtor(long value, int ignored)
		{
			SqlTypeWorkarounds.SqlMoneyCaster sqlMoneyCaster = default(SqlTypeWorkarounds.SqlMoneyCaster);
			sqlMoneyCaster.Fake._fNotNull = true;
			sqlMoneyCaster.Fake._value = value;
			return sqlMoneyCaster.Real;
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x000AA0A4 File Offset: 0x000A82A4
		internal static long SqlMoneyToSqlInternalRepresentation(SqlMoney money)
		{
			SqlTypeWorkarounds.SqlMoneyCaster sqlMoneyCaster = default(SqlTypeWorkarounds.SqlMoneyCaster);
			sqlMoneyCaster.Real = money;
			if (money.IsNull)
			{
				throw new SqlNullValueException();
			}
			return sqlMoneyCaster.Fake._value;
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000AA0DC File Offset: 0x000A82DC
		internal static void SqlDecimalExtractData(SqlDecimal d, out uint data1, out uint data2, out uint data3, out uint data4)
		{
			SqlTypeWorkarounds.SqlDecimalCaster sqlDecimalCaster = new SqlTypeWorkarounds.SqlDecimalCaster
			{
				Real = d
			};
			data1 = sqlDecimalCaster.Fake._data1;
			data2 = sqlDecimalCaster.Fake._data2;
			data3 = sqlDecimalCaster.Fake._data3;
			data4 = sqlDecimalCaster.Fake._data4;
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x000AA130 File Offset: 0x000A8330
		internal static SqlBinary SqlBinaryCtor(byte[] value, bool ignored)
		{
			SqlTypeWorkarounds.SqlBinaryCaster sqlBinaryCaster = default(SqlTypeWorkarounds.SqlBinaryCaster);
			sqlBinaryCaster.Fake._value = value;
			return sqlBinaryCaster.Real;
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x000AA158 File Offset: 0x000A8358
		internal static SqlGuid SqlGuidCtor(byte[] value, bool ignored)
		{
			SqlTypeWorkarounds.SqlGuidCaster sqlGuidCaster = default(SqlTypeWorkarounds.SqlGuidCaster);
			sqlGuidCaster.Fake._value = value;
			return sqlGuidCaster.Real;
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x000AA180 File Offset: 0x000A8380
		// Note: this type is marked as 'beforefieldinit'.
		static SqlTypeWorkarounds()
		{
		}

		// Token: 0x0400190A RID: 6410
		private static readonly XmlReaderSettings s_defaultXmlReaderSettings = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment
		};

		// Token: 0x0400190B RID: 6411
		private static readonly XmlReaderSettings s_defaultXmlReaderSettingsCloseInput = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment,
			CloseInput = true
		};

		// Token: 0x0400190C RID: 6412
		private static readonly XmlReaderSettings s_defaultXmlReaderSettingsAsyncCloseInput = new XmlReaderSettings
		{
			Async = true,
			ConformanceLevel = ConformanceLevel.Fragment,
			CloseInput = true
		};

		// Token: 0x0400190D RID: 6413
		internal const SqlCompareOptions SqlStringValidSqlCompareOptionMask = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth | SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2;

		// Token: 0x02000323 RID: 803
		private struct SqlMoneyLookalike
		{
			// Token: 0x0400190E RID: 6414
			internal bool _fNotNull;

			// Token: 0x0400190F RID: 6415
			internal long _value;
		}

		// Token: 0x02000324 RID: 804
		[StructLayout(LayoutKind.Explicit)]
		private struct SqlMoneyCaster
		{
			// Token: 0x04001910 RID: 6416
			[FieldOffset(0)]
			internal SqlMoney Real;

			// Token: 0x04001911 RID: 6417
			[FieldOffset(0)]
			internal SqlTypeWorkarounds.SqlMoneyLookalike Fake;
		}

		// Token: 0x02000325 RID: 805
		private struct SqlDecimalLookalike
		{
			// Token: 0x04001912 RID: 6418
			internal byte _bStatus;

			// Token: 0x04001913 RID: 6419
			internal byte _bLen;

			// Token: 0x04001914 RID: 6420
			internal byte _bPrec;

			// Token: 0x04001915 RID: 6421
			internal byte _bScale;

			// Token: 0x04001916 RID: 6422
			internal uint _data1;

			// Token: 0x04001917 RID: 6423
			internal uint _data2;

			// Token: 0x04001918 RID: 6424
			internal uint _data3;

			// Token: 0x04001919 RID: 6425
			internal uint _data4;
		}

		// Token: 0x02000326 RID: 806
		[StructLayout(LayoutKind.Explicit)]
		private struct SqlDecimalCaster
		{
			// Token: 0x0400191A RID: 6426
			[FieldOffset(0)]
			internal SqlDecimal Real;

			// Token: 0x0400191B RID: 6427
			[FieldOffset(0)]
			internal SqlTypeWorkarounds.SqlDecimalLookalike Fake;
		}

		// Token: 0x02000327 RID: 807
		private struct SqlBinaryLookalike
		{
			// Token: 0x0400191C RID: 6428
			internal byte[] _value;
		}

		// Token: 0x02000328 RID: 808
		[StructLayout(LayoutKind.Explicit)]
		private struct SqlBinaryCaster
		{
			// Token: 0x0400191D RID: 6429
			[FieldOffset(0)]
			internal SqlBinary Real;

			// Token: 0x0400191E RID: 6430
			[FieldOffset(0)]
			internal SqlTypeWorkarounds.SqlBinaryLookalike Fake;
		}

		// Token: 0x02000329 RID: 809
		private struct SqlGuidLookalike
		{
			// Token: 0x0400191F RID: 6431
			internal byte[] _value;
		}

		// Token: 0x0200032A RID: 810
		[StructLayout(LayoutKind.Explicit)]
		private struct SqlGuidCaster
		{
			// Token: 0x04001920 RID: 6432
			[FieldOffset(0)]
			internal SqlGuid Real;

			// Token: 0x04001921 RID: 6433
			[FieldOffset(0)]
			internal SqlTypeWorkarounds.SqlGuidLookalike Fake;
		}
	}
}

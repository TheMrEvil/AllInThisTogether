using System;
using System.ComponentModel;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data
{
	// Token: 0x02000159 RID: 345
	public static class DataReaderExtensions
	{
		// Token: 0x06001276 RID: 4726 RVA: 0x0005A876 File Offset: 0x00058A76
		public static bool GetBoolean(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetBoolean(reader.GetOrdinal(name));
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0005A88B File Offset: 0x00058A8B
		public static byte GetByte(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetByte(reader.GetOrdinal(name));
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0005A8A0 File Offset: 0x00058AA0
		public static long GetBytes(this DbDataReader reader, string name, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetBytes(reader.GetOrdinal(name), dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0005A8BB File Offset: 0x00058ABB
		public static char GetChar(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetChar(reader.GetOrdinal(name));
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0005A8D0 File Offset: 0x00058AD0
		public static long GetChars(this DbDataReader reader, string name, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetChars(reader.GetOrdinal(name), dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0005A8EB File Offset: 0x00058AEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static DbDataReader GetData(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetData(reader.GetOrdinal(name));
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0005A900 File Offset: 0x00058B00
		public static string GetDataTypeName(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetDataTypeName(reader.GetOrdinal(name));
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0005A915 File Offset: 0x00058B15
		public static DateTime GetDateTime(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetDateTime(reader.GetOrdinal(name));
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0005A92A File Offset: 0x00058B2A
		public static decimal GetDecimal(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetDecimal(reader.GetOrdinal(name));
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0005A93F File Offset: 0x00058B3F
		public static double GetDouble(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetDouble(reader.GetOrdinal(name));
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0005A954 File Offset: 0x00058B54
		public static Type GetFieldType(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetFieldType(reader.GetOrdinal(name));
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0005A969 File Offset: 0x00058B69
		public static T GetFieldValue<T>(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetFieldValue<T>(reader.GetOrdinal(name));
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0005A97E File Offset: 0x00058B7E
		public static Task<T> GetFieldValueAsync<T>(this DbDataReader reader, string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetFieldValueAsync<T>(reader.GetOrdinal(name), cancellationToken);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0005A994 File Offset: 0x00058B94
		public static float GetFloat(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetFloat(reader.GetOrdinal(name));
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0005A9A9 File Offset: 0x00058BA9
		public static Guid GetGuid(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetGuid(reader.GetOrdinal(name));
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0005A9BE File Offset: 0x00058BBE
		public static short GetInt16(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetInt16(reader.GetOrdinal(name));
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0005A9D3 File Offset: 0x00058BD3
		public static int GetInt32(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetInt32(reader.GetOrdinal(name));
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0005A9E8 File Offset: 0x00058BE8
		public static long GetInt64(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetInt64(reader.GetOrdinal(name));
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0005A9FD File Offset: 0x00058BFD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static Type GetProviderSpecificFieldType(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetProviderSpecificFieldType(reader.GetOrdinal(name));
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0005AA12 File Offset: 0x00058C12
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static object GetProviderSpecificValue(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetProviderSpecificValue(reader.GetOrdinal(name));
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0005AA27 File Offset: 0x00058C27
		public static Stream GetStream(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetStream(reader.GetOrdinal(name));
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0005AA3C File Offset: 0x00058C3C
		public static string GetString(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetString(reader.GetOrdinal(name));
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0005AA51 File Offset: 0x00058C51
		public static TextReader GetTextReader(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetTextReader(reader.GetOrdinal(name));
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0005AA66 File Offset: 0x00058C66
		public static object GetValue(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.GetValue(reader.GetOrdinal(name));
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0005AA7B File Offset: 0x00058C7B
		public static bool IsDBNull(this DbDataReader reader, string name)
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.IsDBNull(reader.GetOrdinal(name));
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0005AA90 File Offset: 0x00058C90
		public static Task<bool> IsDBNullAsync(this DbDataReader reader, string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			DataReaderExtensions.AssertNotNull(reader);
			return reader.IsDBNullAsync(reader.GetOrdinal(name), cancellationToken);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0005AAA6 File Offset: 0x00058CA6
		private static void AssertNotNull(DbDataReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
		}
	}
}

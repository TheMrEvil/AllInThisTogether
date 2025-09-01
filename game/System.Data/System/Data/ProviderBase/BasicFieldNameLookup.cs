using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.ProviderBase
{
	// Token: 0x02000343 RID: 835
	internal class BasicFieldNameLookup
	{
		// Token: 0x06002647 RID: 9799 RVA: 0x000AA551 File Offset: 0x000A8751
		public BasicFieldNameLookup(string[] fieldNames)
		{
			if (fieldNames == null)
			{
				throw ADP.ArgumentNull("fieldNames");
			}
			this._fieldNames = fieldNames;
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000AA570 File Offset: 0x000A8770
		public BasicFieldNameLookup(ReadOnlyCollection<string> columnNames)
		{
			int count = columnNames.Count;
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = columnNames[i];
			}
			this._fieldNames = array;
			this.GenerateLookup();
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000AA5B4 File Offset: 0x000A87B4
		public BasicFieldNameLookup(IDataReader reader)
		{
			int fieldCount = reader.FieldCount;
			string[] array = new string[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = reader.GetName(i);
			}
			this._fieldNames = array;
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x000AA5F4 File Offset: 0x000A87F4
		public int GetOrdinal(string fieldName)
		{
			if (fieldName == null)
			{
				throw ADP.ArgumentNull("fieldName");
			}
			int num = this.IndexOf(fieldName);
			if (-1 == num)
			{
				throw ADP.IndexOutOfRange(fieldName);
			}
			return num;
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000AA624 File Offset: 0x000A8824
		public int IndexOfName(string fieldName)
		{
			if (this._fieldNameLookup == null)
			{
				this.GenerateLookup();
			}
			int result;
			if (!this._fieldNameLookup.TryGetValue(fieldName, out result))
			{
				return -1;
			}
			return result;
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x000AA654 File Offset: 0x000A8854
		public int IndexOf(string fieldName)
		{
			if (this._fieldNameLookup == null)
			{
				this.GenerateLookup();
			}
			int num;
			if (!this._fieldNameLookup.TryGetValue(fieldName, out num))
			{
				num = this.LinearIndexOf(fieldName, CompareOptions.IgnoreCase);
				if (-1 == num)
				{
					num = this.LinearIndexOf(fieldName, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
				}
			}
			return num;
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000AA697 File Offset: 0x000A8897
		protected virtual CompareInfo GetCompareInfo()
		{
			return CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000AA6A4 File Offset: 0x000A88A4
		private int LinearIndexOf(string fieldName, CompareOptions compareOptions)
		{
			if (this._compareInfo == null)
			{
				this._compareInfo = this.GetCompareInfo();
			}
			int num = this._fieldNames.Length;
			for (int i = 0; i < num; i++)
			{
				if (this._compareInfo.Compare(fieldName, this._fieldNames[i], compareOptions) == 0)
				{
					this._fieldNameLookup[fieldName] = i;
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x000AA704 File Offset: 0x000A8904
		private void GenerateLookup()
		{
			int num = this._fieldNames.Length;
			Dictionary<string, int> dictionary = new Dictionary<string, int>(num);
			int num2 = num - 1;
			while (0 <= num2)
			{
				string key = this._fieldNames[num2];
				dictionary[key] = num2;
				num2--;
			}
			this._fieldNameLookup = dictionary;
		}

		// Token: 0x04001929 RID: 6441
		private Dictionary<string, int> _fieldNameLookup;

		// Token: 0x0400192A RID: 6442
		private readonly string[] _fieldNames;

		// Token: 0x0400192B RID: 6443
		private CompareInfo _compareInfo;
	}
}

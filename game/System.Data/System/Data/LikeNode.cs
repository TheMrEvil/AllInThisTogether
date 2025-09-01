using System;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x020000E7 RID: 231
	internal sealed class LikeNode : BinaryNode
	{
		// Token: 0x06000E04 RID: 3588 RVA: 0x0003A6C7 File Offset: 0x000388C7
		internal LikeNode(DataTable table, int op, ExpressionNode left, ExpressionNode right) : base(table, op, left, right)
		{
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0003A6D4 File Offset: 0x000388D4
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			object obj = this._left.Eval(row, version);
			if (obj == DBNull.Value || (this._left.IsSqlColumn && DataStorage.IsObjectSqlNull(obj)))
			{
				return DBNull.Value;
			}
			string text;
			if (this._pattern == null)
			{
				object obj2 = this._right.Eval(row, version);
				if (!(obj2 is string) && !(obj2 is SqlString))
				{
					base.SetTypeMismatchError(this._op, obj.GetType(), obj2.GetType());
				}
				if (obj2 == DBNull.Value || DataStorage.IsObjectSqlNull(obj2))
				{
					return DBNull.Value;
				}
				string pat = (string)SqlConvert.ChangeType2(obj2, StorageType.String, typeof(string), base.FormatProvider);
				text = this.AnalyzePattern(pat);
				if (this._right.IsConstant())
				{
					this._pattern = text;
				}
			}
			else
			{
				text = this._pattern;
			}
			if (!(obj is string) && !(obj is SqlString))
			{
				base.SetTypeMismatchError(this._op, obj.GetType(), typeof(string));
			}
			char[] trimChars = new char[]
			{
				' ',
				'\u3000'
			};
			string text2;
			if (obj is SqlString)
			{
				text2 = ((SqlString)obj).Value;
			}
			else
			{
				text2 = (string)obj;
			}
			string s = text2.TrimEnd(trimChars);
			switch (this._kind)
			{
			case 1:
				return base.table.IndexOf(s, text) == 0;
			case 2:
			{
				string s2 = text.TrimEnd(trimChars);
				return base.table.IsSuffix(s, s2);
			}
			case 3:
				return 0 <= base.table.IndexOf(s, text);
			case 4:
				return base.table.Compare(s, text) == 0;
			case 5:
				return true;
			default:
				return DBNull.Value;
			}
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0003A8B4 File Offset: 0x00038AB4
		internal string AnalyzePattern(string pat)
		{
			int length = pat.Length;
			char[] array = new char[length + 1];
			pat.CopyTo(0, array, 0, length);
			array[length] = '\0';
			char[] array2 = new char[length + 1];
			int num = 0;
			int num2 = 0;
			int i = 0;
			while (i < length)
			{
				if (array[i] != '*')
				{
					if (array[i] != '%')
					{
						if (array[i] != '[')
						{
							array2[num++] = array[i];
							i++;
							continue;
						}
						i++;
						if (i >= length)
						{
							throw ExprException.InvalidPattern(pat);
						}
						array2[num++] = array[i++];
						if (i >= length)
						{
							throw ExprException.InvalidPattern(pat);
						}
						if (array[i] != ']')
						{
							throw ExprException.InvalidPattern(pat);
						}
						i++;
						continue;
					}
				}
				while ((array[i] == '*' || array[i] == '%') && i < length)
				{
					i++;
				}
				if ((i < length && num > 0) || num2 >= 2)
				{
					throw ExprException.InvalidPattern(pat);
				}
				num2++;
			}
			string result = new string(array2, 0, num);
			if (num2 == 0)
			{
				this._kind = 4;
				return result;
			}
			if (num <= 0)
			{
				this._kind = 5;
				return result;
			}
			if (array[0] != '*' && array[0] != '%')
			{
				this._kind = 1;
				return result;
			}
			if (array[length - 1] == '*' || array[length - 1] == '%')
			{
				this._kind = 3;
				return result;
			}
			this._kind = 2;
			return result;
		}

		// Token: 0x040008B8 RID: 2232
		internal const int match_left = 1;

		// Token: 0x040008B9 RID: 2233
		internal const int match_right = 2;

		// Token: 0x040008BA RID: 2234
		internal const int match_middle = 3;

		// Token: 0x040008BB RID: 2235
		internal const int match_exact = 4;

		// Token: 0x040008BC RID: 2236
		internal const int match_all = 5;

		// Token: 0x040008BD RID: 2237
		private int _kind;

		// Token: 0x040008BE RID: 2238
		private string _pattern;
	}
}

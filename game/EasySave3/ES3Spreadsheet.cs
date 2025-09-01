using System;
using System.Collections.Generic;
using System.IO;
using ES3Internal;

// Token: 0x0200000D RID: 13
public class ES3Spreadsheet
{
	// Token: 0x17000005 RID: 5
	// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004BD8 File Offset: 0x00002DD8
	public int ColumnCount
	{
		get
		{
			return this.cols;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x060000EA RID: 234 RVA: 0x00004BE0 File Offset: 0x00002DE0
	public int RowCount
	{
		get
		{
			return this.rows;
		}
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00004BE8 File Offset: 0x00002DE8
	public int GetColumnLength(int col)
	{
		if (col >= this.cols)
		{
			return 0;
		}
		int num = -1;
		foreach (ES3Spreadsheet.Index index in this.cells.Keys)
		{
			if (index.col == col && index.row > num)
			{
				num = index.row;
			}
		}
		return num + 1;
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00004C64 File Offset: 0x00002E64
	public int GetRowLength(int row)
	{
		if (row >= this.rows)
		{
			return 0;
		}
		int num = -1;
		foreach (ES3Spreadsheet.Index index in this.cells.Keys)
		{
			if (index.row == row && index.col > num)
			{
				num = index.col;
			}
		}
		return num + 1;
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00004CE0 File Offset: 0x00002EE0
	public void SetCell(int col, int row, object value)
	{
		Type type = value.GetType();
		if (type == typeof(string))
		{
			this.SetCellString(col, row, (string)value);
			return;
		}
		ES3Settings es3Settings = new ES3Settings(null, null);
		if (ES3Reflection.IsPrimitive(type))
		{
			this.SetCellString(col, row, value.ToString());
		}
		else
		{
			this.SetCellString(col, row, es3Settings.encoding.GetString(ES3.Serialize(value, ES3TypeMgr.GetOrCreateES3Type(type, true), null)));
		}
		if (col >= this.cols)
		{
			this.cols = col + 1;
		}
		if (row >= this.rows)
		{
			this.rows = row + 1;
		}
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00004D7A File Offset: 0x00002F7A
	private void SetCellString(int col, int row, string value)
	{
		this.cells[new ES3Spreadsheet.Index(col, row)] = value;
		if (col >= this.cols)
		{
			this.cols = col + 1;
		}
		if (row >= this.rows)
		{
			this.rows = row + 1;
		}
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00004DB4 File Offset: 0x00002FB4
	public T GetCell<T>(int col, int row)
	{
		object cell = this.GetCell(typeof(T), col, row);
		if (cell == null)
		{
			return default(T);
		}
		return (T)((object)cell);
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x00004DE8 File Offset: 0x00002FE8
	public object GetCell(Type type, int col, int row)
	{
		if (col >= this.cols || row >= this.rows)
		{
			throw new IndexOutOfRangeException(string.Concat(new string[]
			{
				"Cell (",
				col.ToString(),
				", ",
				row.ToString(),
				") is out of bounds of spreadsheet (",
				this.cols.ToString(),
				", ",
				this.rows.ToString(),
				")."
			}));
		}
		string text;
		if (!this.cells.TryGetValue(new ES3Spreadsheet.Index(col, row), out text) || text == null)
		{
			return null;
		}
		if (type == typeof(string))
		{
			return text;
		}
		ES3Settings es3Settings = new ES3Settings(null, null);
		return ES3.Deserialize(ES3TypeMgr.GetOrCreateES3Type(type, true), es3Settings.encoding.GetBytes(text), es3Settings);
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00004EC1 File Offset: 0x000030C1
	public void Load(string filePath)
	{
		this.Load(new ES3Settings(filePath, null));
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00004ED0 File Offset: 0x000030D0
	public void Load(string filePath, ES3Settings settings)
	{
		this.Load(new ES3Settings(filePath, settings));
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00004EDF File Offset: 0x000030DF
	public void Load(ES3Settings settings)
	{
		this.Load(ES3Stream.CreateStream(settings, ES3FileMode.Read), settings);
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00004EEF File Offset: 0x000030EF
	public void LoadRaw(string str)
	{
		this.Load(new MemoryStream(new ES3Settings(null, null).encoding.GetBytes(str)), new ES3Settings(null, null));
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x00004F15 File Offset: 0x00003115
	public void LoadRaw(string str, ES3Settings settings)
	{
		this.Load(new MemoryStream(settings.encoding.GetBytes(str)), settings);
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00004F30 File Offset: 0x00003130
	private void Load(Stream stream, ES3Settings settings)
	{
		using (StreamReader streamReader = new StreamReader(stream))
		{
			string text = "";
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				int num3 = streamReader.Read();
				char c = (char)num3;
				if (c == '"')
				{
					for (;;)
					{
						c = (char)streamReader.Read();
						if (c == '"')
						{
							if ((ushort)streamReader.Peek() != 34)
							{
								break;
							}
							c = (char)streamReader.Read();
						}
						text += c.ToString();
					}
				}
				else if (c == ',' || c == '\n' || num3 == -1)
				{
					this.SetCell(num, num2, text);
					text = "";
					if (c == ',')
					{
						num++;
					}
					else
					{
						if (c != '\n')
						{
							break;
						}
						num = 0;
						num2++;
					}
				}
				else
				{
					text += c.ToString();
				}
			}
		}
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00004FFC File Offset: 0x000031FC
	public void Save(string filePath)
	{
		this.Save(new ES3Settings(filePath, null), false);
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0000500C File Offset: 0x0000320C
	public void Save(string filePath, ES3Settings settings)
	{
		this.Save(new ES3Settings(filePath, settings), false);
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x0000501C File Offset: 0x0000321C
	public void Save(ES3Settings settings)
	{
		this.Save(settings, false);
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00005026 File Offset: 0x00003226
	public void Save(string filePath, bool append)
	{
		this.Save(new ES3Settings(filePath, null), append);
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00005036 File Offset: 0x00003236
	public void Save(string filePath, ES3Settings settings, bool append)
	{
		this.Save(new ES3Settings(filePath, settings), append);
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00005048 File Offset: 0x00003248
	public void Save(ES3Settings settings, bool append)
	{
		using (StreamWriter streamWriter = new StreamWriter(ES3Stream.CreateStream(settings, append ? ES3FileMode.Append : ES3FileMode.Write)))
		{
			if (append && ES3.FileExists(settings))
			{
				streamWriter.Write('\n');
			}
			string[,] array = this.ToArray();
			for (int i = 0; i < this.rows; i++)
			{
				if (i != 0)
				{
					streamWriter.Write('\n');
				}
				for (int j = 0; j < this.cols; j++)
				{
					if (j != 0)
					{
						streamWriter.Write(',');
					}
					streamWriter.Write(ES3Spreadsheet.Escape(array[j, i], false));
				}
			}
		}
		if (!append)
		{
			ES3IO.CommitBackup(settings);
		}
	}

	// Token: 0x060000FD RID: 253 RVA: 0x000050F4 File Offset: 0x000032F4
	private static string Escape(string str, bool isAlreadyWrappedInQuotes = false)
	{
		if (str == "")
		{
			return "\"\"";
		}
		if (str == null)
		{
			return null;
		}
		if (str.Contains("\""))
		{
			str = str.Replace("\"", "\"\"");
		}
		if (str.IndexOfAny(ES3Spreadsheet.CHARS_TO_ESCAPE) > -1 || ES3Spreadsheet.StartsOrEndsWithWhitespace(str))
		{
			str = "\"" + str + "\"";
		}
		return str;
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00005164 File Offset: 0x00003364
	private static string Unescape(string str)
	{
		if (str.StartsWith("\"") && str.EndsWith("\""))
		{
			str = str.Substring(1, str.Length - 2);
			if (str.Contains("\"\""))
			{
				str = str.Replace("\"\"", "\"");
			}
		}
		return str;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x000051BC File Offset: 0x000033BC
	private static bool StartsOrEndsWithWhitespace(string str)
	{
		return !string.IsNullOrEmpty(str) && (char.IsWhiteSpace(str[0]) || char.IsWhiteSpace(str[str.Length - 1]));
	}

	// Token: 0x06000100 RID: 256 RVA: 0x000051F0 File Offset: 0x000033F0
	private string[,] ToArray()
	{
		string[,] array = new string[this.cols, this.rows];
		foreach (KeyValuePair<ES3Spreadsheet.Index, string> keyValuePair in this.cells)
		{
			array[keyValuePair.Key.col, keyValuePair.Key.row] = keyValuePair.Value;
		}
		return array;
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00005274 File Offset: 0x00003474
	public ES3Spreadsheet()
	{
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00005287 File Offset: 0x00003487
	// Note: this type is marked as 'beforefieldinit'.
	static ES3Spreadsheet()
	{
	}

	// Token: 0x0400001D RID: 29
	private int cols;

	// Token: 0x0400001E RID: 30
	private int rows;

	// Token: 0x0400001F RID: 31
	private Dictionary<ES3Spreadsheet.Index, string> cells = new Dictionary<ES3Spreadsheet.Index, string>();

	// Token: 0x04000020 RID: 32
	private const string QUOTE = "\"";

	// Token: 0x04000021 RID: 33
	private const char QUOTE_CHAR = '"';

	// Token: 0x04000022 RID: 34
	private const char COMMA_CHAR = ',';

	// Token: 0x04000023 RID: 35
	private const char NEWLINE_CHAR = '\n';

	// Token: 0x04000024 RID: 36
	private const string ESCAPED_QUOTE = "\"\"";

	// Token: 0x04000025 RID: 37
	private static char[] CHARS_TO_ESCAPE = new char[]
	{
		',',
		'"',
		'\n'
	};

	// Token: 0x020000F8 RID: 248
	protected struct Index
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x0001F593 File Offset: 0x0001D793
		public Index(int col, int row)
		{
			this.col = col;
			this.row = row;
		}

		// Token: 0x040001AF RID: 431
		public int col;

		// Token: 0x040001B0 RID: 432
		public int row;
	}
}

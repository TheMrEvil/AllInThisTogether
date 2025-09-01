using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InControl
{
	// Token: 0x02000015 RID: 21
	public struct KeyCombo
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002AEC File Offset: 0x00000CEC
		public KeyCombo(params Key[] keys)
		{
			this.includeData = 0UL;
			this.includeSize = 0;
			this.excludeData = 0UL;
			this.excludeSize = 0;
			for (int i = 0; i < keys.Length; i++)
			{
				this.AddInclude(keys[i]);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002B2E File Offset: 0x00000D2E
		private void AddIncludeInt(int key)
		{
			if (this.includeSize == 8)
			{
				return;
			}
			this.includeData |= (ulong)((ulong)((long)key & 255L) << this.includeSize * 8);
			this.includeSize++;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002B6A File Offset: 0x00000D6A
		private int GetIncludeInt(int index)
		{
			return (int)(this.includeData >> index * 8 & 255UL);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002B81 File Offset: 0x00000D81
		[Obsolete("Use KeyCombo.AddInclude instead.")]
		public void Add(Key key)
		{
			this.AddInclude(key);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002B8A File Offset: 0x00000D8A
		[Obsolete("Use KeyCombo.GetInclude instead.")]
		public Key Get(int index)
		{
			return this.GetInclude(index);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002B93 File Offset: 0x00000D93
		public void AddInclude(Key key)
		{
			this.AddIncludeInt((int)key);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002B9C File Offset: 0x00000D9C
		public Key GetInclude(int index)
		{
			if (index < 0 || index >= this.includeSize)
			{
				throw new IndexOutOfRangeException("Index " + index.ToString() + " is out of the range 0.." + this.includeSize.ToString());
			}
			return (Key)this.GetIncludeInt(index);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002BD9 File Offset: 0x00000DD9
		private void AddExcludeInt(int key)
		{
			if (this.excludeSize == 8)
			{
				return;
			}
			this.excludeData |= (ulong)((ulong)((long)key & 255L) << this.excludeSize * 8);
			this.excludeSize++;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002C15 File Offset: 0x00000E15
		private int GetExcludeInt(int index)
		{
			return (int)(this.excludeData >> index * 8 & 255UL);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002C2C File Offset: 0x00000E2C
		public void AddExclude(Key key)
		{
			this.AddExcludeInt((int)key);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002C35 File Offset: 0x00000E35
		public Key GetExclude(int index)
		{
			if (index < 0 || index >= this.excludeSize)
			{
				throw new IndexOutOfRangeException("Index " + index.ToString() + " is out of the range 0.." + this.excludeSize.ToString());
			}
			return (Key)this.GetExcludeInt(index);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002C72 File Offset: 0x00000E72
		public static KeyCombo With(params Key[] keys)
		{
			return new KeyCombo(keys);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002C7C File Offset: 0x00000E7C
		public KeyCombo AndNot(params Key[] keys)
		{
			for (int i = 0; i < keys.Length; i++)
			{
				this.AddExclude(keys[i]);
			}
			return this;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002CA6 File Offset: 0x00000EA6
		public void Clear()
		{
			this.includeData = 0UL;
			this.includeSize = 0;
			this.excludeData = 0UL;
			this.excludeSize = 0;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002CC6 File Offset: 0x00000EC6
		[Obsolete("Use KeyCombo.IncludeCount instead.")]
		public int Count
		{
			get
			{
				return this.includeSize;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002CCE File Offset: 0x00000ECE
		public int IncludeCount
		{
			get
			{
				return this.includeSize;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002CD6 File Offset: 0x00000ED6
		public int ExcludeCount
		{
			get
			{
				return this.excludeSize;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public bool IsPressed
		{
			get
			{
				if (this.includeSize == 0)
				{
					return false;
				}
				IKeyboardProvider keyboardProvider = InputManager.KeyboardProvider;
				bool flag = true;
				for (int i = 0; i < this.includeSize; i++)
				{
					Key include = this.GetInclude(i);
					flag = (flag && keyboardProvider.GetKeyIsPressed(include));
				}
				for (int j = 0; j < this.excludeSize; j++)
				{
					Key exclude = this.GetExclude(j);
					if (keyboardProvider.GetKeyIsPressed(exclude))
					{
						return false;
					}
				}
				return flag;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002D54 File Offset: 0x00000F54
		public static KeyCombo Detect(bool modifiersAsKeys)
		{
			KeyCombo empty = KeyCombo.Empty;
			IKeyboardProvider keyboardProvider = InputManager.KeyboardProvider;
			if (keyboardProvider == null)
			{
				return empty;
			}
			if (modifiersAsKeys)
			{
				for (Key key = Key.LeftShift; key <= Key.RightControl; key++)
				{
					if (keyboardProvider.GetKeyIsPressed(key))
					{
						empty.AddInclude(key);
						if (key == Key.LeftControl && keyboardProvider.GetKeyIsPressed(Key.RightAlt))
						{
							empty.AddInclude(Key.RightAlt);
						}
						return empty;
					}
				}
			}
			else
			{
				for (Key key2 = Key.Shift; key2 <= Key.Control; key2++)
				{
					if (keyboardProvider.GetKeyIsPressed(key2))
					{
						empty.AddInclude(key2);
					}
				}
			}
			for (Key key3 = Key.Escape; key3 <= Key.QuestionMark; key3++)
			{
				if (keyboardProvider.GetKeyIsPressed(key3))
				{
					empty.AddInclude(key3);
					return empty;
				}
			}
			empty.Clear();
			return empty;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002E00 File Offset: 0x00001000
		public override string ToString()
		{
			string text;
			if (!KeyCombo.cachedStrings.TryGetValue(this.includeData, out text))
			{
				KeyCombo.cachedStringBuilder.Clear();
				for (int i = 0; i < this.includeSize; i++)
				{
					if (i != 0)
					{
						KeyCombo.cachedStringBuilder.Append(" ");
					}
					Key include = this.GetInclude(i);
					KeyCombo.cachedStringBuilder.Append(InputManager.KeyboardProvider.GetNameForKey(include));
				}
				text = KeyCombo.cachedStringBuilder.ToString();
				KeyCombo.cachedStrings[this.includeData] = text;
			}
			return text;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002E8B File Offset: 0x0000108B
		public static bool operator ==(KeyCombo a, KeyCombo b)
		{
			return a.includeData == b.includeData && a.excludeData == b.excludeData;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002EAB File Offset: 0x000010AB
		public static bool operator !=(KeyCombo a, KeyCombo b)
		{
			return a.includeData != b.includeData || a.excludeData != b.excludeData;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002ED0 File Offset: 0x000010D0
		public override bool Equals(object other)
		{
			if (other is KeyCombo)
			{
				KeyCombo keyCombo = (KeyCombo)other;
				return this.includeData == keyCombo.includeData && this.excludeData == keyCombo.excludeData;
			}
			return false;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002F0C File Offset: 0x0000110C
		public override int GetHashCode()
		{
			return (17 * 31 + this.includeData.GetHashCode()) * 31 + this.excludeData.GetHashCode();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002F30 File Offset: 0x00001130
		internal void Load(BinaryReader reader, ushort dataFormatVersion)
		{
			if (dataFormatVersion == 1)
			{
				this.includeSize = reader.ReadInt32();
				this.includeData = reader.ReadUInt64();
				return;
			}
			if (dataFormatVersion != 2)
			{
				throw new InControlException("Unknown data format version: " + dataFormatVersion.ToString());
			}
			this.includeSize = reader.ReadInt32();
			this.includeData = reader.ReadUInt64();
			this.excludeSize = reader.ReadInt32();
			this.excludeData = reader.ReadUInt64();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002FA7 File Offset: 0x000011A7
		internal void Save(BinaryWriter writer)
		{
			writer.Write(this.includeSize);
			writer.Write(this.includeData);
			writer.Write(this.excludeSize);
			writer.Write(this.excludeData);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002FD9 File Offset: 0x000011D9
		// Note: this type is marked as 'beforefieldinit'.
		static KeyCombo()
		{
		}

		// Token: 0x040000C8 RID: 200
		public static readonly KeyCombo Empty = default(KeyCombo);

		// Token: 0x040000C9 RID: 201
		private int includeSize;

		// Token: 0x040000CA RID: 202
		private ulong includeData;

		// Token: 0x040000CB RID: 203
		private int excludeSize;

		// Token: 0x040000CC RID: 204
		private ulong excludeData;

		// Token: 0x040000CD RID: 205
		private static readonly Dictionary<ulong, string> cachedStrings = new Dictionary<ulong, string>();

		// Token: 0x040000CE RID: 206
		private static readonly StringBuilder cachedStringBuilder = new StringBuilder(256);
	}
}

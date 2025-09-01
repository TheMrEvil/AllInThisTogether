using System;
using System.Collections;
using System.Security.Util;
using System.Text;

namespace System.Security
{
	// Token: 0x020003DB RID: 987
	[Serializable]
	internal sealed class SecurityDocument
	{
		// Token: 0x06002870 RID: 10352 RVA: 0x00092A89 File Offset: 0x00090C89
		public SecurityDocument(int numData)
		{
			this.m_data = new byte[numData];
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x00092A9D File Offset: 0x00090C9D
		public SecurityDocument(byte[] data)
		{
			this.m_data = data;
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x00092AAC File Offset: 0x00090CAC
		public SecurityDocument(SecurityElement elRoot)
		{
			this.m_data = new byte[32];
			int num = 0;
			this.ConvertElement(elRoot, ref num);
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x00092AD8 File Offset: 0x00090CD8
		public void GuaranteeSize(int size)
		{
			if (this.m_data.Length < size)
			{
				byte[] array = new byte[(size / 32 + 1) * 32];
				Array.Copy(this.m_data, 0, array, 0, this.m_data.Length);
				this.m_data = array;
			}
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x00092B1C File Offset: 0x00090D1C
		public void AddString(string str, ref int position)
		{
			this.GuaranteeSize(position + str.Length * 2 + 2);
			for (int i = 0; i < str.Length; i++)
			{
				this.m_data[position + 2 * i] = (byte)(str[i] >> 8);
				this.m_data[position + 2 * i + 1] = (byte)(str[i] & 'ÿ');
			}
			this.m_data[position + str.Length * 2] = 0;
			this.m_data[position + str.Length * 2 + 1] = 0;
			position += str.Length * 2 + 2;
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x00092BB8 File Offset: 0x00090DB8
		public void AppendString(string str, ref int position)
		{
			if (position <= 1 || this.m_data[position - 1] != 0 || this.m_data[position - 2] != 0)
			{
				throw new XmlSyntaxException();
			}
			position -= 2;
			this.AddString(str, ref position);
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x00092BED File Offset: 0x00090DED
		public static int EncodedStringSize(string str)
		{
			return str.Length * 2 + 2;
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x00092BF9 File Offset: 0x00090DF9
		public string GetString(ref int position)
		{
			return this.GetString(ref position, true);
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x00092C04 File Offset: 0x00090E04
		public string GetString(ref int position, bool bCreate)
		{
			int num = position;
			while (num < this.m_data.Length - 1 && (this.m_data[num] != 0 || this.m_data[num + 1] != 0))
			{
				num += 2;
			}
			Tokenizer.StringMaker sharedStringMaker = SharedStatics.GetSharedStringMaker();
			string result;
			try
			{
				if (bCreate)
				{
					sharedStringMaker._outStringBuilder = null;
					sharedStringMaker._outIndex = 0;
					for (int i = position; i < num; i += 2)
					{
						char c = (char)((int)this.m_data[i] << 8 | (int)this.m_data[i + 1]);
						if (sharedStringMaker._outIndex < 512)
						{
							char[] outChars = sharedStringMaker._outChars;
							Tokenizer.StringMaker stringMaker = sharedStringMaker;
							int outIndex = stringMaker._outIndex;
							stringMaker._outIndex = outIndex + 1;
							outChars[outIndex] = c;
						}
						else
						{
							if (sharedStringMaker._outStringBuilder == null)
							{
								sharedStringMaker._outStringBuilder = new StringBuilder();
							}
							sharedStringMaker._outStringBuilder.Append(sharedStringMaker._outChars, 0, 512);
							sharedStringMaker._outChars[0] = c;
							sharedStringMaker._outIndex = 1;
						}
					}
				}
				position = num + 2;
				if (bCreate)
				{
					result = sharedStringMaker.MakeString();
				}
				else
				{
					result = null;
				}
			}
			finally
			{
				SharedStatics.ReleaseSharedStringMaker(ref sharedStringMaker);
			}
			return result;
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x00092D18 File Offset: 0x00090F18
		public void AddToken(byte b, ref int position)
		{
			this.GuaranteeSize(position + 1);
			byte[] data = this.m_data;
			int num = position;
			position = num + 1;
			data[num] = b;
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x00092D40 File Offset: 0x00090F40
		public void ConvertElement(SecurityElement elCurrent, ref int position)
		{
			this.AddToken(1, ref position);
			this.AddString(elCurrent.m_strTag, ref position);
			if (elCurrent.m_lAttributes != null)
			{
				for (int i = 0; i < elCurrent.m_lAttributes.Count; i += 2)
				{
					this.AddToken(2, ref position);
					this.AddString((string)elCurrent.m_lAttributes[i], ref position);
					this.AddString((string)elCurrent.m_lAttributes[i + 1], ref position);
				}
			}
			if (elCurrent.m_strText != null)
			{
				this.AddToken(3, ref position);
				this.AddString(elCurrent.m_strText, ref position);
			}
			if (elCurrent.InternalChildren != null)
			{
				for (int j = 0; j < elCurrent.InternalChildren.Count; j++)
				{
					this.ConvertElement((SecurityElement)elCurrent.Children[j], ref position);
				}
			}
			this.AddToken(4, ref position);
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x00092E15 File Offset: 0x00091015
		public SecurityElement GetRootElement()
		{
			return this.GetElement(0, true);
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x00092E1F File Offset: 0x0009101F
		public SecurityElement GetElement(int position, bool bCreate)
		{
			return this.InternalGetElement(ref position, bCreate);
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x00092E2C File Offset: 0x0009102C
		internal SecurityElement InternalGetElement(ref int position, bool bCreate)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			byte[] data = this.m_data;
			int num = position;
			position = num + 1;
			if (data[num] != 1)
			{
				throw new XmlSyntaxException();
			}
			SecurityElement securityElement = null;
			string @string = this.GetString(ref position, bCreate);
			if (bCreate)
			{
				securityElement = new SecurityElement(@string);
			}
			while (this.m_data[position] == 2)
			{
				position++;
				string string2 = this.GetString(ref position, bCreate);
				string string3 = this.GetString(ref position, bCreate);
				if (bCreate)
				{
					securityElement.AddAttribute(string2, string3);
				}
			}
			if (this.m_data[position] == 3)
			{
				position++;
				string string4 = this.GetString(ref position, bCreate);
				if (bCreate)
				{
					securityElement.m_strText = string4;
				}
			}
			while (this.m_data[position] != 4)
			{
				SecurityElement child = this.InternalGetElement(ref position, bCreate);
				if (bCreate)
				{
					securityElement.AddChild(child);
				}
			}
			position++;
			return securityElement;
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x00092EFD File Offset: 0x000910FD
		public string GetTagForElement(int position)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			if (this.m_data[position++] != 1)
			{
				throw new XmlSyntaxException();
			}
			return this.GetString(ref position);
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x00092F30 File Offset: 0x00091130
		public ArrayList GetChildrenPositionForElement(int position)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			if (this.m_data[position++] != 1)
			{
				throw new XmlSyntaxException();
			}
			ArrayList arrayList = new ArrayList();
			this.GetString(ref position);
			while (this.m_data[position] == 2)
			{
				position++;
				this.GetString(ref position, false);
				this.GetString(ref position, false);
			}
			if (this.m_data[position] == 3)
			{
				position++;
				this.GetString(ref position, false);
			}
			while (this.m_data[position] != 4)
			{
				arrayList.Add(position);
				this.InternalGetElement(ref position, false);
			}
			position++;
			return arrayList;
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x00092FE0 File Offset: 0x000911E0
		public string GetAttributeForElement(int position, string attributeName)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			if (this.m_data[position++] != 1)
			{
				throw new XmlSyntaxException();
			}
			string result = null;
			this.GetString(ref position, false);
			while (this.m_data[position] == 2)
			{
				position++;
				string @string = this.GetString(ref position);
				string string2 = this.GetString(ref position);
				if (string.Equals(@string, attributeName))
				{
					result = string2;
					break;
				}
			}
			return result;
		}

		// Token: 0x04001EAF RID: 7855
		internal byte[] m_data;

		// Token: 0x04001EB0 RID: 7856
		internal const byte c_element = 1;

		// Token: 0x04001EB1 RID: 7857
		internal const byte c_attribute = 2;

		// Token: 0x04001EB2 RID: 7858
		internal const byte c_text = 3;

		// Token: 0x04001EB3 RID: 7859
		internal const byte c_children = 4;

		// Token: 0x04001EB4 RID: 7860
		internal const int c_growthSize = 32;
	}
}

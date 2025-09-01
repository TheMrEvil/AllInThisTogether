using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Mono.Xml
{
	// Token: 0x02000061 RID: 97
	internal class SmallXmlParser
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00004CB0 File Offset: 0x00002EB0
		public SmallXmlParser()
		{
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004D08 File Offset: 0x00002F08
		private Exception Error(string msg)
		{
			return new SmallXmlParserException(msg, this.line, this.column);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004D1C File Offset: 0x00002F1C
		private Exception UnexpectedEndError()
		{
			string[] array = new string[this.elementNames.Count];
			this.elementNames.CopyTo(array, 0);
			return this.Error(string.Format("Unexpected end of stream. Element stack content is {0}", string.Join(",", array)));
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004D64 File Offset: 0x00002F64
		private bool IsNameChar(char c, bool start)
		{
			if (c <= '.')
			{
				if (c == '-' || c == '.')
				{
					return !start;
				}
			}
			else if (c == ':' || c == '_')
			{
				return true;
			}
			if (c > 'Ā')
			{
				if (c == 'ՙ' || c == 'ۥ' || c == 'ۦ')
				{
					return true;
				}
				if ('ʻ' <= c && c <= 'ˁ')
				{
					return true;
				}
			}
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.LetterNumber:
				return true;
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.EnclosingMark:
			case UnicodeCategory.DecimalDigitNumber:
				return !start;
			default:
				return false;
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004E06 File Offset: 0x00003006
		private bool IsWhitespace(int c)
		{
			return c - 9 <= 1 || c == 13 || c == 32;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004E1C File Offset: 0x0000301C
		public void SkipWhitespaces()
		{
			this.SkipWhitespaces(false);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004E25 File Offset: 0x00003025
		private void HandleWhitespaces()
		{
			while (this.IsWhitespace(this.Peek()))
			{
				this.buffer.Append((char)this.Read());
			}
			if (this.Peek() != 60 && this.Peek() >= 0)
			{
				this.isWhitespace = false;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004E64 File Offset: 0x00003064
		public void SkipWhitespaces(bool expected)
		{
			for (;;)
			{
				int num = this.Peek();
				if (num - 9 > 1 && num != 13 && num != 32)
				{
					break;
				}
				this.Read();
				if (expected)
				{
					expected = false;
				}
			}
			if (expected)
			{
				throw this.Error("Whitespace is expected.");
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004EA7 File Offset: 0x000030A7
		private int Peek()
		{
			return this.reader.Peek();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004EB4 File Offset: 0x000030B4
		private int Read()
		{
			int num = this.reader.Read();
			if (num == 10)
			{
				this.resetColumn = true;
			}
			if (this.resetColumn)
			{
				this.line++;
				this.resetColumn = false;
				this.column = 1;
				return num;
			}
			this.column++;
			return num;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004F0C File Offset: 0x0000310C
		public void Expect(int c)
		{
			int num = this.Read();
			if (num < 0)
			{
				throw this.UnexpectedEndError();
			}
			if (num != c)
			{
				throw this.Error(string.Format("Expected '{0}' but got {1}", (char)c, (char)num));
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004F50 File Offset: 0x00003150
		private string ReadUntil(char until, bool handleReferences)
		{
			while (this.Peek() >= 0)
			{
				char c = (char)this.Read();
				if (c == until)
				{
					string result = this.buffer.ToString();
					this.buffer.Length = 0;
					return result;
				}
				if (handleReferences && c == '&')
				{
					this.ReadReference();
				}
				else
				{
					this.buffer.Append(c);
				}
			}
			throw this.UnexpectedEndError();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004FB0 File Offset: 0x000031B0
		public string ReadName()
		{
			int num = 0;
			if (this.Peek() < 0 || !this.IsNameChar((char)this.Peek(), true))
			{
				throw this.Error("XML name start character is expected.");
			}
			for (int i = this.Peek(); i >= 0; i = this.Peek())
			{
				char c = (char)i;
				if (!this.IsNameChar(c, false))
				{
					break;
				}
				if (num == this.nameBuffer.Length)
				{
					char[] destinationArray = new char[num * 2];
					Array.Copy(this.nameBuffer, destinationArray, num);
					this.nameBuffer = destinationArray;
				}
				this.nameBuffer[num++] = c;
				this.Read();
			}
			if (num == 0)
			{
				throw this.Error("Valid XML name is expected.");
			}
			return new string(this.nameBuffer, 0, num);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005060 File Offset: 0x00003260
		public void Parse(TextReader input, SmallXmlParser.IContentHandler handler)
		{
			this.reader = input;
			this.handler = handler;
			handler.OnStartParsing(this);
			while (this.Peek() >= 0)
			{
				this.ReadContent();
			}
			this.HandleBufferedContent();
			if (this.elementNames.Count > 0)
			{
				throw this.Error(string.Format("Insufficient close tag: {0}", this.elementNames.Peek()));
			}
			handler.OnEndParsing(this);
			this.Cleanup();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000050D0 File Offset: 0x000032D0
		private void Cleanup()
		{
			this.line = 1;
			this.column = 0;
			this.handler = null;
			this.reader = null;
			this.elementNames.Clear();
			this.xmlSpaces.Clear();
			this.attributes.Clear();
			this.buffer.Length = 0;
			this.xmlSpace = null;
			this.isWhitespace = false;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005134 File Offset: 0x00003334
		public void ReadContent()
		{
			if (this.IsWhitespace(this.Peek()))
			{
				if (this.buffer.Length == 0)
				{
					this.isWhitespace = true;
				}
				this.HandleWhitespaces();
			}
			if (this.Peek() != 60)
			{
				this.ReadCharacters();
				return;
			}
			this.Read();
			int num = this.Peek();
			if (num != 33)
			{
				if (num != 47)
				{
					string text;
					if (num != 63)
					{
						this.HandleBufferedContent();
						text = this.ReadName();
						while (this.Peek() != 62 && this.Peek() != 47)
						{
							this.ReadAttribute(this.attributes);
						}
						this.handler.OnStartElement(text, this.attributes);
						this.attributes.Clear();
						this.SkipWhitespaces();
						if (this.Peek() == 47)
						{
							this.Read();
							this.handler.OnEndElement(text);
						}
						else
						{
							this.elementNames.Push(text);
							this.xmlSpaces.Push(this.xmlSpace);
						}
						this.Expect(62);
						return;
					}
					this.HandleBufferedContent();
					this.Read();
					text = this.ReadName();
					this.SkipWhitespaces();
					string text2 = string.Empty;
					if (this.Peek() != 63)
					{
						for (;;)
						{
							text2 += this.ReadUntil('?', false);
							if (this.Peek() == 62)
							{
								break;
							}
							text2 += "?";
						}
					}
					this.handler.OnProcessingInstruction(text, text2);
					this.Expect(62);
					return;
				}
				else
				{
					this.HandleBufferedContent();
					if (this.elementNames.Count == 0)
					{
						throw this.UnexpectedEndError();
					}
					this.Read();
					string text = this.ReadName();
					this.SkipWhitespaces();
					string text3 = (string)this.elementNames.Pop();
					this.xmlSpaces.Pop();
					if (this.xmlSpaces.Count > 0)
					{
						this.xmlSpace = (string)this.xmlSpaces.Peek();
					}
					else
					{
						this.xmlSpace = null;
					}
					if (text != text3)
					{
						throw this.Error(string.Format("End tag mismatch: expected {0} but found {1}", text3, text));
					}
					this.handler.OnEndElement(text);
					this.Expect(62);
					return;
				}
			}
			else
			{
				this.Read();
				if (this.Peek() == 91)
				{
					this.Read();
					if (this.ReadName() != "CDATA")
					{
						throw this.Error("Invalid declaration markup");
					}
					this.Expect(91);
					this.ReadCDATASection();
					return;
				}
				else
				{
					if (this.Peek() == 45)
					{
						this.ReadComment();
						return;
					}
					if (this.ReadName() != "DOCTYPE")
					{
						throw this.Error("Invalid declaration markup.");
					}
					throw this.Error("This parser does not support document type.");
				}
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000053CC File Offset: 0x000035CC
		private void HandleBufferedContent()
		{
			if (this.buffer.Length == 0)
			{
				return;
			}
			if (this.isWhitespace)
			{
				this.handler.OnIgnorableWhitespace(this.buffer.ToString());
			}
			else
			{
				this.handler.OnChars(this.buffer.ToString());
			}
			this.buffer.Length = 0;
			this.isWhitespace = false;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005430 File Offset: 0x00003630
		private void ReadCharacters()
		{
			this.isWhitespace = false;
			for (;;)
			{
				int num = this.Peek();
				if (num == -1)
				{
					break;
				}
				if (num != 38)
				{
					if (num == 60)
					{
						return;
					}
					this.buffer.Append((char)this.Read());
				}
				else
				{
					this.Read();
					this.ReadReference();
				}
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005480 File Offset: 0x00003680
		private void ReadReference()
		{
			if (this.Peek() == 35)
			{
				this.Read();
				this.ReadCharacterReference();
				return;
			}
			string a = this.ReadName();
			this.Expect(59);
			if (a == "amp")
			{
				this.buffer.Append('&');
				return;
			}
			if (a == "quot")
			{
				this.buffer.Append('"');
				return;
			}
			if (a == "apos")
			{
				this.buffer.Append('\'');
				return;
			}
			if (a == "lt")
			{
				this.buffer.Append('<');
				return;
			}
			if (!(a == "gt"))
			{
				throw this.Error("General non-predefined entity reference is not supported in this parser.");
			}
			this.buffer.Append('>');
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005550 File Offset: 0x00003750
		private int ReadCharacterReference()
		{
			int num = 0;
			if (this.Peek() == 120)
			{
				this.Read();
				for (int i = this.Peek(); i >= 0; i = this.Peek())
				{
					if (48 <= i && i <= 57)
					{
						num <<= 4 + i - 48;
					}
					else if (65 <= i && i <= 70)
					{
						num <<= 4 + i - 65 + 10;
					}
					else
					{
						if (97 > i || i > 102)
						{
							break;
						}
						num <<= 4 + i - 97 + 10;
					}
					this.Read();
				}
			}
			else
			{
				int num2 = this.Peek();
				while (num2 >= 0 && 48 <= num2 && num2 <= 57)
				{
					num <<= 4 + num2 - 48;
					this.Read();
					num2 = this.Peek();
				}
			}
			return num;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000560C File Offset: 0x0000380C
		private void ReadAttribute(SmallXmlParser.AttrListImpl a)
		{
			this.SkipWhitespaces(true);
			if (this.Peek() == 47 || this.Peek() == 62)
			{
				return;
			}
			string text = this.ReadName();
			this.SkipWhitespaces();
			this.Expect(61);
			this.SkipWhitespaces();
			int num = this.Read();
			string value;
			if (num != 34)
			{
				if (num != 39)
				{
					throw this.Error("Invalid attribute value markup.");
				}
				value = this.ReadUntil('\'', true);
			}
			else
			{
				value = this.ReadUntil('"', true);
			}
			if (text == "xml:space")
			{
				this.xmlSpace = value;
			}
			a.Add(text, value);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000056A4 File Offset: 0x000038A4
		private void ReadCDATASection()
		{
			int num = 0;
			while (this.Peek() >= 0)
			{
				char c = (char)this.Read();
				if (c == ']')
				{
					num++;
				}
				else
				{
					if (c == '>' && num > 1)
					{
						for (int i = num; i > 2; i--)
						{
							this.buffer.Append(']');
						}
						return;
					}
					for (int j = 0; j < num; j++)
					{
						this.buffer.Append(']');
					}
					num = 0;
					this.buffer.Append(c);
				}
			}
			throw this.UnexpectedEndError();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005724 File Offset: 0x00003924
		private void ReadComment()
		{
			this.Expect(45);
			this.Expect(45);
			while (this.Read() != 45 || this.Read() != 45)
			{
			}
			if (this.Read() != 62)
			{
				throw this.Error("'--' is not allowed inside comment markup.");
			}
		}

		// Token: 0x04000E15 RID: 3605
		private SmallXmlParser.IContentHandler handler;

		// Token: 0x04000E16 RID: 3606
		private TextReader reader;

		// Token: 0x04000E17 RID: 3607
		private Stack elementNames = new Stack();

		// Token: 0x04000E18 RID: 3608
		private Stack xmlSpaces = new Stack();

		// Token: 0x04000E19 RID: 3609
		private string xmlSpace;

		// Token: 0x04000E1A RID: 3610
		private StringBuilder buffer = new StringBuilder(200);

		// Token: 0x04000E1B RID: 3611
		private char[] nameBuffer = new char[30];

		// Token: 0x04000E1C RID: 3612
		private bool isWhitespace;

		// Token: 0x04000E1D RID: 3613
		private SmallXmlParser.AttrListImpl attributes = new SmallXmlParser.AttrListImpl();

		// Token: 0x04000E1E RID: 3614
		private int line = 1;

		// Token: 0x04000E1F RID: 3615
		private int column;

		// Token: 0x04000E20 RID: 3616
		private bool resetColumn;

		// Token: 0x02000062 RID: 98
		public interface IContentHandler
		{
			// Token: 0x0600015A RID: 346
			void OnStartParsing(SmallXmlParser parser);

			// Token: 0x0600015B RID: 347
			void OnEndParsing(SmallXmlParser parser);

			// Token: 0x0600015C RID: 348
			void OnStartElement(string name, SmallXmlParser.IAttrList attrs);

			// Token: 0x0600015D RID: 349
			void OnEndElement(string name);

			// Token: 0x0600015E RID: 350
			void OnProcessingInstruction(string name, string text);

			// Token: 0x0600015F RID: 351
			void OnChars(string text);

			// Token: 0x06000160 RID: 352
			void OnIgnorableWhitespace(string text);
		}

		// Token: 0x02000063 RID: 99
		public interface IAttrList
		{
			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000161 RID: 353
			int Length { get; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000162 RID: 354
			bool IsEmpty { get; }

			// Token: 0x06000163 RID: 355
			string GetName(int i);

			// Token: 0x06000164 RID: 356
			string GetValue(int i);

			// Token: 0x06000165 RID: 357
			string GetValue(string name);

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000166 RID: 358
			string[] Names { get; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000167 RID: 359
			string[] Values { get; }
		}

		// Token: 0x02000064 RID: 100
		private class AttrListImpl : SmallXmlParser.IAttrList
		{
			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000168 RID: 360 RVA: 0x00005760 File Offset: 0x00003960
			public int Length
			{
				get
				{
					return this.attrNames.Count;
				}
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000169 RID: 361 RVA: 0x0000576D File Offset: 0x0000396D
			public bool IsEmpty
			{
				get
				{
					return this.attrNames.Count == 0;
				}
			}

			// Token: 0x0600016A RID: 362 RVA: 0x0000577D File Offset: 0x0000397D
			public string GetName(int i)
			{
				return this.attrNames[i];
			}

			// Token: 0x0600016B RID: 363 RVA: 0x0000578B File Offset: 0x0000398B
			public string GetValue(int i)
			{
				return this.attrValues[i];
			}

			// Token: 0x0600016C RID: 364 RVA: 0x0000579C File Offset: 0x0000399C
			public string GetValue(string name)
			{
				for (int i = 0; i < this.attrNames.Count; i++)
				{
					if (this.attrNames[i] == name)
					{
						return this.attrValues[i];
					}
				}
				return null;
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x0600016D RID: 365 RVA: 0x000057E1 File Offset: 0x000039E1
			public string[] Names
			{
				get
				{
					return this.attrNames.ToArray();
				}
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x0600016E RID: 366 RVA: 0x000057EE File Offset: 0x000039EE
			public string[] Values
			{
				get
				{
					return this.attrValues.ToArray();
				}
			}

			// Token: 0x0600016F RID: 367 RVA: 0x000057FB File Offset: 0x000039FB
			internal void Clear()
			{
				this.attrNames.Clear();
				this.attrValues.Clear();
			}

			// Token: 0x06000170 RID: 368 RVA: 0x00005813 File Offset: 0x00003A13
			internal void Add(string name, string value)
			{
				this.attrNames.Add(name);
				this.attrValues.Add(value);
			}

			// Token: 0x06000171 RID: 369 RVA: 0x0000582D File Offset: 0x00003A2D
			public AttrListImpl()
			{
			}

			// Token: 0x04000E21 RID: 3617
			private List<string> attrNames = new List<string>();

			// Token: 0x04000E22 RID: 3618
			private List<string> attrValues = new List<string>();
		}
	}
}

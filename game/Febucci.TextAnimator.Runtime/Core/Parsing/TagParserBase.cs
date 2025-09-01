using System;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x02000050 RID: 80
	public abstract class TagParserBase
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00007AE1 File Offset: 0x00005CE1
		public virtual bool shouldPasteTag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007AE4 File Offset: 0x00005CE4
		public TagParserBase()
		{
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007AEC File Offset: 0x00005CEC
		public TagParserBase(char startSymbol, char closingSymbol, char endSymbol)
		{
			this.startSymbol = startSymbol;
			this.closingSymbol = closingSymbol;
			this.endSymbol = endSymbol;
		}

		// Token: 0x06000196 RID: 406
		public abstract bool TryProcessingTag(string textInsideBrackets, int tagLength, int realTextIndex, int internalOrder);

		// Token: 0x06000197 RID: 407 RVA: 0x00007B09 File Offset: 0x00005D09
		public void Initialize()
		{
			this.OnInitialize();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007B11 File Offset: 0x00005D11
		protected virtual void OnInitialize()
		{
		}

		// Token: 0x04000117 RID: 279
		public char startSymbol;

		// Token: 0x04000118 RID: 280
		public char endSymbol;

		// Token: 0x04000119 RID: 281
		public char closingSymbol;
	}
}

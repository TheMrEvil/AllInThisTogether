using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200029B RID: 667
	public sealed class KeywordRecognizer : PhraseRecognizer
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x0002DCAE File Offset: 0x0002BEAE
		// (set) Token: 0x06001C8B RID: 7307 RVA: 0x0002DCB6 File Offset: 0x0002BEB6
		public IEnumerable<string> Keywords
		{
			[CompilerGenerated]
			get
			{
				return this.<Keywords>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Keywords>k__BackingField = value;
			}
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0002DCBF File Offset: 0x0002BEBF
		public KeywordRecognizer(string[] keywords) : this(keywords, ConfidenceLevel.Medium)
		{
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0002DCCC File Offset: 0x0002BECC
		public KeywordRecognizer(string[] keywords, ConfidenceLevel minimumConfidence)
		{
			bool flag = keywords == null;
			if (flag)
			{
				throw new ArgumentNullException("keywords");
			}
			bool flag2 = keywords.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("At least one keyword must be specified.", "keywords");
			}
			int num = keywords.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag3 = keywords[i] == null;
				if (flag3)
				{
					throw new ArgumentNullException(string.Format("Keyword at index {0} is null.", i));
				}
			}
			this.Keywords = keywords;
			this.m_Recognizer = PhraseRecognizer.CreateFromKeywords(this, keywords, minimumConfidence);
		}

		// Token: 0x0400095A RID: 2394
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private IEnumerable<string> <Keywords>k__BackingField;
	}
}

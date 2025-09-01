using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200029C RID: 668
	public sealed class GrammarRecognizer : PhraseRecognizer
	{
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x0002DD5D File Offset: 0x0002BF5D
		// (set) Token: 0x06001C8F RID: 7311 RVA: 0x0002DD65 File Offset: 0x0002BF65
		public string GrammarFilePath
		{
			[CompilerGenerated]
			get
			{
				return this.<GrammarFilePath>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GrammarFilePath>k__BackingField = value;
			}
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0002DD6E File Offset: 0x0002BF6E
		public GrammarRecognizer(string grammarFilePath) : this(grammarFilePath, ConfidenceLevel.Medium)
		{
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0002DD7C File Offset: 0x0002BF7C
		public GrammarRecognizer(string grammarFilePath, ConfidenceLevel minimumConfidence)
		{
			bool flag = grammarFilePath == null;
			if (flag)
			{
				throw new ArgumentNullException("grammarFilePath");
			}
			bool flag2 = grammarFilePath.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Grammar file path cannot be empty.");
			}
			this.GrammarFilePath = grammarFilePath;
			this.m_Recognizer = PhraseRecognizer.CreateFromGrammarFile(this, grammarFilePath, minimumConfidence);
		}

		// Token: 0x0400095B RID: 2395
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <GrammarFilePath>k__BackingField;
	}
}

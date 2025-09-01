using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200029A RID: 666
	public struct PhraseRecognizedEventArgs
	{
		// Token: 0x06001C89 RID: 7305 RVA: 0x0002DC86 File Offset: 0x0002BE86
		internal PhraseRecognizedEventArgs(string text, ConfidenceLevel confidence, SemanticMeaning[] semanticMeanings, DateTime phraseStartTime, TimeSpan phraseDuration)
		{
			this.text = text;
			this.confidence = confidence;
			this.semanticMeanings = semanticMeanings;
			this.phraseStartTime = phraseStartTime;
			this.phraseDuration = phraseDuration;
		}

		// Token: 0x04000955 RID: 2389
		public readonly ConfidenceLevel confidence;

		// Token: 0x04000956 RID: 2390
		public readonly SemanticMeaning[] semanticMeanings;

		// Token: 0x04000957 RID: 2391
		public readonly string text;

		// Token: 0x04000958 RID: 2392
		public readonly DateTime phraseStartTime;

		// Token: 0x04000959 RID: 2393
		public readonly TimeSpan phraseDuration;
	}
}

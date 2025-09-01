using System;

namespace MagicaCloth2
{
	// Token: 0x020000F4 RID: 244
	[Serializable]
	public class MagicaClothProcessingException : Exception
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x000227AE File Offset: 0x000209AE
		public MagicaClothProcessingException()
		{
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000227B6 File Offset: 0x000209B6
		public MagicaClothProcessingException(string message) : base(message)
		{
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000227BF File Offset: 0x000209BF
		public MagicaClothProcessingException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}

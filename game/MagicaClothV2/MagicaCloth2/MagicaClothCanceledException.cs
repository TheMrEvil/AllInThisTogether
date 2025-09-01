using System;

namespace MagicaCloth2
{
	// Token: 0x020000F5 RID: 245
	[Serializable]
	public class MagicaClothCanceledException : Exception
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x000227AE File Offset: 0x000209AE
		public MagicaClothCanceledException()
		{
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000227B6 File Offset: 0x000209B6
		public MagicaClothCanceledException(string message) : base(message)
		{
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000227BF File Offset: 0x000209BF
		public MagicaClothCanceledException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}

using System;

namespace System
{
	/// <summary>Specifies how mathematical rounding methods should process a number that is midway between two numbers.</summary>
	// Token: 0x0200015B RID: 347
	public enum MidpointRounding
	{
		/// <summary>When a number is halfway between two others, it is rounded toward the nearest even number.</summary>
		// Token: 0x04001282 RID: 4738
		ToEven,
		/// <summary>When a number is halfway between two others, it is rounded toward the nearest number that is away from zero.</summary>
		// Token: 0x04001283 RID: 4739
		AwayFromZero
	}
}

using System;

namespace System.ComponentModel
{
	/// <summary>Specifies values that succinctly describe the results of a masked text parsing operation.</summary>
	// Token: 0x020003D9 RID: 985
	public enum MaskedTextResultHint
	{
		/// <summary>Unknown. The result of the operation could not be determined.</summary>
		// Token: 0x04000FA5 RID: 4005
		Unknown,
		/// <summary>Success. The operation succeeded because a literal, prompt or space character was an escaped character. For more information about escaped characters, see the <see cref="M:System.ComponentModel.MaskedTextProvider.VerifyEscapeChar(System.Char,System.Int32)" /> method.</summary>
		// Token: 0x04000FA6 RID: 4006
		CharacterEscaped,
		/// <summary>Success. The primary operation was not performed because it was not needed; therefore, no side effect was produced.</summary>
		// Token: 0x04000FA7 RID: 4007
		NoEffect,
		/// <summary>Success. The primary operation was not performed because it was not needed, but the method produced a side effect. For example, the <see cref="Overload:System.ComponentModel.MaskedTextProvider.RemoveAt" /> method can delete an unassigned edit position, which causes left-shifting of subsequent characters in the formatted string.</summary>
		// Token: 0x04000FA8 RID: 4008
		SideEffect,
		/// <summary>Success. The primary operation succeeded.</summary>
		// Token: 0x04000FA9 RID: 4009
		Success,
		/// <summary>Operation did not succeed.An input character was encountered that was not a member of the ASCII character set.</summary>
		// Token: 0x04000FAA RID: 4010
		AsciiCharacterExpected = -1,
		/// <summary>Operation did not succeed.An input character was encountered that was not alphanumeric. .</summary>
		// Token: 0x04000FAB RID: 4011
		AlphanumericCharacterExpected = -2,
		/// <summary>Operation did not succeed. An input character was encountered that was not a digit.</summary>
		// Token: 0x04000FAC RID: 4012
		DigitExpected = -3,
		/// <summary>Operation did not succeed. An input character was encountered that was not a letter.</summary>
		// Token: 0x04000FAD RID: 4013
		LetterExpected = -4,
		/// <summary>Operation did not succeed. An input character was encountered that was not a signed digit.</summary>
		// Token: 0x04000FAE RID: 4014
		SignedDigitExpected = -5,
		/// <summary>Operation did not succeed. The program encountered an  input character that was not valid. For more information about characters that are not valid, see the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidInputChar(System.Char)" /> method.</summary>
		// Token: 0x04000FAF RID: 4015
		InvalidInput = -51,
		/// <summary>Operation did not succeed. The prompt character is not valid at input, perhaps because the <see cref="P:System.ComponentModel.MaskedTextProvider.AllowPromptAsInput" /> property is set to <see langword="false" />.</summary>
		// Token: 0x04000FB0 RID: 4016
		PromptCharNotAllowed = -52,
		/// <summary>Operation did not succeed. There were not enough edit positions available to fulfill the request.</summary>
		// Token: 0x04000FB1 RID: 4017
		UnavailableEditPosition = -53,
		/// <summary>Operation did not succeed. The current position in the formatted string is a literal character.</summary>
		// Token: 0x04000FB2 RID: 4018
		NonEditPosition = -54,
		/// <summary>Operation did not succeed. The specified position is not in the range of the target string; typically it is either less than zero or greater then the length of the target string.</summary>
		// Token: 0x04000FB3 RID: 4019
		PositionOutOfRange = -55
	}
}

using System;

namespace System.Net.Mime
{
	/// <summary>Supplies the strings used to specify the disposition type for an email attachment.</summary>
	// Token: 0x020007FB RID: 2043
	public static class DispositionTypeNames
	{
		/// <summary>Specifies that the attachment is to be displayed as part of the email message body.</summary>
		// Token: 0x04002796 RID: 10134
		public const string Inline = "inline";

		/// <summary>Specifies that the attachment is to be displayed as a file attached to the email message.</summary>
		// Token: 0x04002797 RID: 10135
		public const string Attachment = "attachment";
	}
}

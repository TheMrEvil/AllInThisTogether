using System;

namespace System.Net.Mime
{
	/// <summary>Specifies the media type information for an email message attachment.</summary>
	// Token: 0x02000800 RID: 2048
	public static class MediaTypeNames
	{
		/// <summary>Specifies the type of text data in an email message attachment.</summary>
		// Token: 0x02000801 RID: 2049
		public static class Text
		{
			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in plain text format.</summary>
			// Token: 0x0400279D RID: 10141
			public const string Plain = "text/plain";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in HTML format.</summary>
			// Token: 0x0400279E RID: 10142
			public const string Html = "text/html";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in XML format.</summary>
			// Token: 0x0400279F RID: 10143
			public const string Xml = "text/xml";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in Rich Text Format (RTF).</summary>
			// Token: 0x040027A0 RID: 10144
			public const string RichText = "text/richtext";
		}

		/// <summary>Specifies the kind of application data in an email message attachment.</summary>
		// Token: 0x02000802 RID: 2050
		public static class Application
		{
			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is a SOAP document.</summary>
			// Token: 0x040027A1 RID: 10145
			public const string Soap = "application/soap+xml";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is not interpreted.</summary>
			// Token: 0x040027A2 RID: 10146
			public const string Octet = "application/octet-stream";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is in Rich Text Format (RTF).</summary>
			// Token: 0x040027A3 RID: 10147
			public const string Rtf = "application/rtf";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is in Portable Document Format (PDF).</summary>
			// Token: 0x040027A4 RID: 10148
			public const string Pdf = "application/pdf";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is compressed.</summary>
			// Token: 0x040027A5 RID: 10149
			public const string Zip = "application/zip";

			// Token: 0x040027A6 RID: 10150
			public const string Json = "application/json";

			// Token: 0x040027A7 RID: 10151
			public const string Xml = "application/xml";
		}

		/// <summary>Specifies the type of image data in an email message attachment.</summary>
		// Token: 0x02000803 RID: 2051
		public static class Image
		{
			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Graphics Interchange Format (GIF).</summary>
			// Token: 0x040027A8 RID: 10152
			public const string Gif = "image/gif";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Tagged Image File Format (TIFF).</summary>
			// Token: 0x040027A9 RID: 10153
			public const string Tiff = "image/tiff";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Joint Photographic Experts Group (JPEG) format.</summary>
			// Token: 0x040027AA RID: 10154
			public const string Jpeg = "image/jpeg";
		}
	}
}

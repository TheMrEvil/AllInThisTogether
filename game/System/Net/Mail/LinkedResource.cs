using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents an embedded external resource in an email attachment, such as an image in an HTML attachment.</summary>
	// Token: 0x02000829 RID: 2089
	public class LinkedResource : AttachmentBase
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> using the specified file name.</summary>
		/// <param name="fileName">The file name holding the content for this embedded resource.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		// Token: 0x06004271 RID: 17009 RVA: 0x000E7843 File Offset: 0x000E5A43
		public LinkedResource(string fileName) : base(fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the specified file name and content type.</summary>
		/// <param name="fileName">The file name that holds the content for this embedded resource.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		// Token: 0x06004272 RID: 17010 RVA: 0x000E7855 File Offset: 0x000E5A55
		public LinkedResource(string fileName, ContentType contentType) : base(fileName, contentType)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the specified file name and media type.</summary>
		/// <param name="fileName">The file name that holds the content for this embedded resource.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		// Token: 0x06004273 RID: 17011 RVA: 0x000E7868 File Offset: 0x000E5A68
		public LinkedResource(string fileName, string mediaType) : base(fileName, mediaType)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> using the supplied <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="contentStream">A stream that contains the content for this embedded resource.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		// Token: 0x06004274 RID: 17012 RVA: 0x000E787B File Offset: 0x000E5A7B
		public LinkedResource(Stream contentStream) : base(contentStream)
		{
			if (contentStream == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the values supplied by <see cref="T:System.IO.Stream" /> and <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <param name="contentStream">A stream that contains the content for this embedded resource.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		// Token: 0x06004275 RID: 17013 RVA: 0x000E788D File Offset: 0x000E5A8D
		public LinkedResource(Stream contentStream, ContentType contentType) : base(contentStream, contentType)
		{
			if (contentStream == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the specified <see cref="T:System.IO.Stream" /> and media type.</summary>
		/// <param name="contentStream">A stream that contains the content for this embedded resource.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		// Token: 0x06004276 RID: 17014 RVA: 0x000E78A0 File Offset: 0x000E5AA0
		public LinkedResource(Stream contentStream, string mediaType) : base(contentStream, mediaType)
		{
			if (contentStream == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Gets or sets a URI that the resource must match.</summary>
		/// <returns>If <see cref="P:System.Net.Mail.LinkedResource.ContentLink" /> is a relative URI, the recipient of the message must resolve it.</returns>
		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06004277 RID: 17015 RVA: 0x000E78B3 File Offset: 0x000E5AB3
		// (set) Token: 0x06004278 RID: 17016 RVA: 0x000E78BB File Offset: 0x000E5ABB
		public Uri ContentLink
		{
			get
			{
				return this.contentLink;
			}
			set
			{
				this.contentLink = value;
			}
		}

		/// <summary>Creates a <see cref="T:System.Net.Mail.LinkedResource" /> object from a string to be included in an email attachment as an embedded resource. The default media type is plain text, and the default content type is ASCII.</summary>
		/// <param name="content">A string that contains the embedded resource to be included in the email attachment.</param>
		/// <returns>A <see cref="T:System.Net.Mail.LinkedResource" /> object that contains the embedded resource to be included in the email attachment.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified content string is null.</exception>
		// Token: 0x06004279 RID: 17017 RVA: 0x000E78C4 File Offset: 0x000E5AC4
		public static LinkedResource CreateLinkedResourceFromString(string content)
		{
			if (content == null)
			{
				throw new ArgumentNullException();
			}
			return new LinkedResource(new MemoryStream(Encoding.Default.GetBytes(content)))
			{
				TransferEncoding = TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Creates a <see cref="T:System.Net.Mail.LinkedResource" /> object from a string to be included in an email attachment as an embedded resource, with the specified content type, and media type as plain text.</summary>
		/// <param name="content">A string that contains the embedded resource to be included in the email attachment.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <returns>A <see cref="T:System.Net.Mail.LinkedResource" /> object that contains the embedded resource to be included in the email attachment.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified content string is null.</exception>
		// Token: 0x0600427A RID: 17018 RVA: 0x000E78EB File Offset: 0x000E5AEB
		public static LinkedResource CreateLinkedResourceFromString(string content, ContentType contentType)
		{
			if (content == null)
			{
				throw new ArgumentNullException();
			}
			return new LinkedResource(new MemoryStream(Encoding.Default.GetBytes(content)), contentType)
			{
				TransferEncoding = TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Creates a <see cref="T:System.Net.Mail.LinkedResource" /> object from a string to be included in an email attachment as an embedded resource, with the specified content type, and media type.</summary>
		/// <param name="content">A string that contains the embedded resource to be included in the email attachment.</param>
		/// <param name="contentEncoding">The type of the content.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <returns>A <see cref="T:System.Net.Mail.LinkedResource" /> object that contains the embedded resource to be included in the email attachment.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified content string is null.</exception>
		// Token: 0x0600427B RID: 17019 RVA: 0x000E7913 File Offset: 0x000E5B13
		public static LinkedResource CreateLinkedResourceFromString(string content, Encoding contentEncoding, string mediaType)
		{
			if (content == null)
			{
				throw new ArgumentNullException();
			}
			return new LinkedResource(new MemoryStream(contentEncoding.GetBytes(content)), mediaType)
			{
				TransferEncoding = TransferEncoding.QuotedPrintable
			};
		}

		// Token: 0x04002851 RID: 10321
		private Uri contentLink;
	}
}

using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents the format to view an email message.</summary>
	// Token: 0x02000822 RID: 2082
	public class AlternateView : AttachmentBase
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified file name.</summary>
		/// <param name="fileName">The name of the file that contains the content for this alternate view.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred, such as a disk error.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified file handle, such as when access is Write or ReadWrite and the file handle is set for read-only access.</exception>
		// Token: 0x06004237 RID: 16951 RVA: 0x000E500D File Offset: 0x000E320D
		public AlternateView(string fileName) : base(fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified file name and content type.</summary>
		/// <param name="fileName">The name of the file that contains the content for this alternate view.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred, such as a disk error.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified file handle, such as when access is Write or ReadWrite and the file handle is set for read-only access.</exception>
		// Token: 0x06004238 RID: 16952 RVA: 0x000E502A File Offset: 0x000E322A
		public AlternateView(string fileName, ContentType contentType) : base(fileName, contentType)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified file name and media type.</summary>
		/// <param name="fileName">The name of the file that contains the content for this alternate view.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred, such as a disk error.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified file handle, such as when access is Write or ReadWrite and the file handle is set for read-only access.</exception>
		// Token: 0x06004239 RID: 16953 RVA: 0x000E5048 File Offset: 0x000E3248
		public AlternateView(string fileName, string mediaType) : base(fileName, mediaType)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="contentStream">A stream that contains the content for this view.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		// Token: 0x0600423A RID: 16954 RVA: 0x000E5066 File Offset: 0x000E3266
		public AlternateView(Stream contentStream) : base(contentStream)
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified <see cref="T:System.IO.Stream" /> and media type.</summary>
		/// <param name="contentStream">A stream that contains the content for this attachment.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		// Token: 0x0600423B RID: 16955 RVA: 0x000E507A File Offset: 0x000E327A
		public AlternateView(Stream contentStream, string mediaType) : base(contentStream, mediaType)
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified <see cref="T:System.IO.Stream" /> and <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <param name="contentStream">A stream that contains the content for this attachment.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		// Token: 0x0600423C RID: 16956 RVA: 0x000E508F File Offset: 0x000E328F
		public AlternateView(Stream contentStream, ContentType contentType) : base(contentStream, contentType)
		{
		}

		/// <summary>Gets or sets the base URI to use for resolving relative URIs in the <see cref="T:System.Net.Mail.AlternateView" />.</summary>
		/// <returns>The base URI to use for resolving relative URIs in the <see cref="T:System.Net.Mail.AlternateView" />.</returns>
		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x0600423D RID: 16957 RVA: 0x000E50A4 File Offset: 0x000E32A4
		// (set) Token: 0x0600423E RID: 16958 RVA: 0x000E50AC File Offset: 0x000E32AC
		public Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
			set
			{
				this.baseUri = value;
			}
		}

		/// <summary>Gets the set of embedded resources referred to by this attachment.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.LinkedResourceCollection" /> object that stores the collection of linked resources to be sent as part of an email message.</returns>
		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x0600423F RID: 16959 RVA: 0x000E50B5 File Offset: 0x000E32B5
		public LinkedResourceCollection LinkedResources
		{
			get
			{
				return this.linkedResources;
			}
		}

		/// <summary>Creates a <see cref="T:System.Net.Mail.AlternateView" /> of an email message using the content specified in a <see cref="T:System.String" />.</summary>
		/// <param name="content">The <see cref="T:System.String" /> that contains the content of the email message.</param>
		/// <returns>An <see cref="T:System.Net.Mail.AlternateView" /> object that represents an alternate view of an email message.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="content" /> is null.</exception>
		// Token: 0x06004240 RID: 16960 RVA: 0x000E50BD File Offset: 0x000E32BD
		public static AlternateView CreateAlternateViewFromString(string content)
		{
			if (content == null)
			{
				throw new ArgumentNullException();
			}
			return new AlternateView(new MemoryStream(Encoding.UTF8.GetBytes(content)))
			{
				TransferEncoding = TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Creates an <see cref="T:System.Net.Mail.AlternateView" /> of an email message using the content specified in a <see cref="T:System.String" /> and the specified MIME media type of the content.</summary>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> that describes the data in <paramref name="content" />.</param>
		/// <returns>An <see cref="T:System.Net.Mail.AlternateView" /> object that represents an alternate view of an email message.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="content" /> is null.</exception>
		// Token: 0x06004241 RID: 16961 RVA: 0x000E50E4 File Offset: 0x000E32E4
		public static AlternateView CreateAlternateViewFromString(string content, ContentType contentType)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			return new AlternateView(new MemoryStream(((contentType.CharSet != null) ? Encoding.GetEncoding(contentType.CharSet) : Encoding.UTF8).GetBytes(content)), contentType)
			{
				TransferEncoding = TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Creates an <see cref="T:System.Net.Mail.AlternateView" /> of an email message using the content specified in a <see cref="T:System.String" />, the specified text encoding, and MIME media type of the content.</summary>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="contentEncoding">An <see cref="T:System.Text.Encoding" />. This value can be <see langword="null." /></param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <returns>An <see cref="T:System.Net.Mail.AlternateView" /> object that represents an alternate view of an email message.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="content" /> is null.</exception>
		// Token: 0x06004242 RID: 16962 RVA: 0x000E5134 File Offset: 0x000E3334
		public static AlternateView CreateAlternateViewFromString(string content, Encoding contentEncoding, string mediaType)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (contentEncoding == null)
			{
				contentEncoding = Encoding.UTF8;
			}
			return new AlternateView(new MemoryStream(contentEncoding.GetBytes(content)), new ContentType
			{
				MediaType = mediaType,
				CharSet = contentEncoding.HeaderName
			})
			{
				TransferEncoding = TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Mail.AlternateView" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06004243 RID: 16963 RVA: 0x000E518C File Offset: 0x000E338C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (LinkedResource linkedResource in this.linkedResources)
				{
					linkedResource.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04002842 RID: 10306
		private Uri baseUri;

		// Token: 0x04002843 RID: 10307
		private LinkedResourceCollection linkedResources = new LinkedResourceCollection();
	}
}

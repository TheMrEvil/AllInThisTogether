using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents an attachment to an email.</summary>
	// Token: 0x02000824 RID: 2084
	public class Attachment : AttachmentBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified content string.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains a file path to use to create this attachment.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileName" /> is empty.</exception>
		// Token: 0x0600424A RID: 16970 RVA: 0x000E520D File Offset: 0x000E340D
		public Attachment(string fileName) : base(fileName)
		{
			this.InitName(fileName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified content string and MIME type information.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="mediaType">A <see cref="T:System.String" /> that contains the MIME Content-Header information for this attachment. This value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not in the correct format.</exception>
		// Token: 0x0600424B RID: 16971 RVA: 0x000E5228 File Offset: 0x000E3428
		public Attachment(string fileName, string mediaType) : base(fileName, mediaType)
		{
			this.InitName(fileName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified content string and <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains a file path to use to create this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> that describes the data in <paramref name="fileName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mediaType" /> is not in the correct format.</exception>
		// Token: 0x0600424C RID: 16972 RVA: 0x000E5244 File Offset: 0x000E3444
		public Attachment(string fileName, ContentType contentType) : base(fileName, contentType)
		{
			this.InitName(fileName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified stream and content type.</summary>
		/// <param name="contentStream">A readable <see cref="T:System.IO.Stream" /> that contains the content for this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> that describes the data in <paramref name="contentStream" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentType" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="contentStream" /> is <see langword="null" />.</exception>
		// Token: 0x0600424D RID: 16973 RVA: 0x000E5260 File Offset: 0x000E3460
		public Attachment(Stream contentStream, ContentType contentType) : base(contentStream, contentType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified stream and name.</summary>
		/// <param name="contentStream">A readable <see cref="T:System.IO.Stream" /> that contains the content for this attachment.</param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the value for the <see cref="P:System.Net.Mime.ContentType.Name" /> property of the <see cref="T:System.Net.Mime.ContentType" /> associated with this attachment. This value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is <see langword="null" />.</exception>
		// Token: 0x0600424E RID: 16974 RVA: 0x000E5275 File Offset: 0x000E3475
		public Attachment(Stream contentStream, string name) : base(contentStream)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified stream, name, and MIME type information.</summary>
		/// <param name="contentStream">A readable <see cref="T:System.IO.Stream" /> that contains the content for this attachment.</param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the value for the <see cref="P:System.Net.Mime.ContentType.Name" /> property of the <see cref="T:System.Net.Mime.ContentType" /> associated with this attachment. This value can be <see langword="null" />.</param>
		/// <param name="mediaType">A <see cref="T:System.String" /> that contains the MIME Content-Header information for this attachment. This value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not in the correct format.</exception>
		// Token: 0x0600424F RID: 16975 RVA: 0x000E5290 File Offset: 0x000E3490
		public Attachment(Stream contentStream, string name, string mediaType) : base(contentStream, mediaType)
		{
			this.Name = name;
		}

		/// <summary>Gets the MIME content disposition for this attachment.</summary>
		/// <returns>A <see cref="T:System.Net.Mime.ContentDisposition" /> that provides the presentation information for this attachment.</returns>
		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06004250 RID: 16976 RVA: 0x000E52AC File Offset: 0x000E34AC
		public ContentDisposition ContentDisposition
		{
			get
			{
				return this.contentDisposition;
			}
		}

		/// <summary>Gets or sets the MIME content type name value in the content type associated with this attachment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value for the content type <paramref name="name" /> represented by the <see cref="P:System.Net.Mime.ContentType.Name" /> property.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is <see cref="F:System.String.Empty" /> ("").</exception>
		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06004251 RID: 16977 RVA: 0x000E52B4 File Offset: 0x000E34B4
		// (set) Token: 0x06004252 RID: 16978 RVA: 0x000E52C1 File Offset: 0x000E34C1
		public string Name
		{
			get
			{
				return base.ContentType.Name;
			}
			set
			{
				base.ContentType.Name = value;
			}
		}

		/// <summary>Specifies the encoding for the <see cref="T:System.Net.Mail.Attachment" /><see cref="P:System.Net.Mail.Attachment.Name" />.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> value that specifies the type of name encoding. The default value is determined from the name of the attachment.</returns>
		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06004253 RID: 16979 RVA: 0x000E52CF File Offset: 0x000E34CF
		// (set) Token: 0x06004254 RID: 16980 RVA: 0x000E52D7 File Offset: 0x000E34D7
		public Encoding NameEncoding
		{
			get
			{
				return this.nameEncoding;
			}
			set
			{
				this.nameEncoding = value;
			}
		}

		/// <summary>Creates a mail attachment using the content from the specified string, and the specified <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> object that represents the Multipurpose Internet Mail Exchange (MIME) protocol Content-Type header to be used.</param>
		/// <returns>An object of type <see cref="T:System.Net.Mail.Attachment" />.</returns>
		// Token: 0x06004255 RID: 16981 RVA: 0x000E52E0 File Offset: 0x000E34E0
		public static Attachment CreateAttachmentFromString(string content, ContentType contentType)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return new Attachment(memoryStream, contentType)
			{
				TransferEncoding = TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Creates a mail attachment using the content from the specified string, and the specified MIME content type name.</summary>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="name">The MIME content type name value in the content type associated with this attachment.</param>
		/// <returns>An object of type <see cref="T:System.Net.Mail.Attachment" />.</returns>
		// Token: 0x06004256 RID: 16982 RVA: 0x000E531C File Offset: 0x000E351C
		public static Attachment CreateAttachmentFromString(string content, string name)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return new Attachment(memoryStream, new ContentType("text/plain"))
			{
				TransferEncoding = TransferEncoding.QuotedPrintable,
				Name = name
			};
		}

		/// <summary>Creates a mail attachment using the content from the specified string, the specified MIME content type name, character encoding, and MIME header information for the attachment.</summary>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="name">The MIME content type name value in the content type associated with this attachment.</param>
		/// <param name="contentEncoding">An <see cref="T:System.Text.Encoding" />. This value can be <see langword="null" />.</param>
		/// <param name="mediaType">A <see cref="T:System.String" /> that contains the MIME Content-Header information for this attachment. This value can be <see langword="null" />.</param>
		/// <returns>An object of type <see cref="T:System.Net.Mail.Attachment" />.</returns>
		// Token: 0x06004257 RID: 16983 RVA: 0x000E5374 File Offset: 0x000E3574
		public static Attachment CreateAttachmentFromString(string content, string name, Encoding contentEncoding, string mediaType)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream, contentEncoding);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return new Attachment(memoryStream, name, mediaType)
			{
				TransferEncoding = MailMessage.GuessTransferEncoding(contentEncoding),
				ContentType = 
				{
					CharSet = streamWriter.Encoding.BodyName
				}
			};
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x000E53DA File Offset: 0x000E35DA
		private void InitName(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.Name = Path.GetFileName(fileName);
		}

		// Token: 0x04002844 RID: 10308
		private ContentDisposition contentDisposition = new ContentDisposition();

		// Token: 0x04002845 RID: 10309
		private Encoding nameEncoding;
	}
}

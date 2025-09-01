using System;
using System.Net.Http.Headers;

namespace System.Net.Http
{
	/// <summary>Provides a container for content encoded using multipart/form-data MIME type.</summary>
	// Token: 0x02000030 RID: 48
	public class MultipartFormDataContent : MultipartContent
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.MultipartFormDataContent" /> class.</summary>
		// Token: 0x06000186 RID: 390 RVA: 0x00006846 File Offset: 0x00004A46
		public MultipartFormDataContent() : base("form-data")
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.MultipartFormDataContent" /> class.</summary>
		/// <param name="boundary">The boundary string for the multipart form data content.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="boundary" /> was <see langword="null" /> or contains only white space characters.  
		///  -or-  
		///  The <paramref name="boundary" /> ends with a space character.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the <paramref name="boundary" /> was greater than 70.</exception>
		// Token: 0x06000187 RID: 391 RVA: 0x00006853 File Offset: 0x00004A53
		public MultipartFormDataContent(string boundary) : base("form-data", boundary)
		{
		}

		/// <summary>Add HTTP content to a collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized to multipart/form-data MIME type.</summary>
		/// <param name="content">The HTTP content to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> was <see langword="null" />.</exception>
		// Token: 0x06000188 RID: 392 RVA: 0x00006861 File Offset: 0x00004A61
		public override void Add(HttpContent content)
		{
			base.Add(content);
			this.AddContentDisposition(content, null, null);
		}

		/// <summary>Add HTTP content to a collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized to multipart/form-data MIME type.</summary>
		/// <param name="content">The HTTP content to add to the collection.</param>
		/// <param name="name">The name for the HTTP content to add.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> was <see langword="null" /> or contains only white space characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> was <see langword="null" />.</exception>
		// Token: 0x06000189 RID: 393 RVA: 0x00006873 File Offset: 0x00004A73
		public void Add(HttpContent content, string name)
		{
			base.Add(content);
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("name");
			}
			this.AddContentDisposition(content, name, null);
		}

		/// <summary>Add HTTP content to a collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized to multipart/form-data MIME type.</summary>
		/// <param name="content">The HTTP content to add to the collection.</param>
		/// <param name="name">The name for the HTTP content to add.</param>
		/// <param name="fileName">The file name for the HTTP content to add to the collection.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> was <see langword="null" /> or contains only white space characters.  
		///  -or-  
		///  The <paramref name="fileName" /> was <see langword="null" /> or contains only white space characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> was <see langword="null" />.</exception>
		// Token: 0x0600018A RID: 394 RVA: 0x00006898 File Offset: 0x00004A98
		public void Add(HttpContent content, string name, string fileName)
		{
			base.Add(content);
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("name");
			}
			if (string.IsNullOrWhiteSpace(fileName))
			{
				throw new ArgumentException("fileName");
			}
			this.AddContentDisposition(content, name, fileName);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000068D0 File Offset: 0x00004AD0
		private void AddContentDisposition(HttpContent content, string name, string fileName)
		{
			HttpContentHeaders headers = content.Headers;
			if (headers.ContentDisposition != null)
			{
				return;
			}
			headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
			{
				Name = name,
				FileName = fileName,
				FileNameStar = fileName
			};
		}
	}
}

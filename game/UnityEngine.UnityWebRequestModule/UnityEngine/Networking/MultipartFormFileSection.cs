using System;
using System.Text;

namespace UnityEngine.Networking
{
	// Token: 0x02000007 RID: 7
	public class MultipartFormFileSection : IMultipartFormSection
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00003428 File Offset: 0x00001628
		private void Init(string name, byte[] data, string fileName, string contentType)
		{
			this.name = name;
			this.data = data;
			this.file = fileName;
			this.content = contentType;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003448 File Offset: 0x00001648
		public MultipartFormFileSection(string name, byte[] data, string fileName, string contentType)
		{
			bool flag = data == null || data.Length < 1;
			if (flag)
			{
				throw new ArgumentException("Cannot create a multipart form file section without body data");
			}
			bool flag2 = string.IsNullOrEmpty(fileName);
			if (flag2)
			{
				fileName = "file.dat";
			}
			bool flag3 = string.IsNullOrEmpty(contentType);
			if (flag3)
			{
				contentType = "application/octet-stream";
			}
			this.Init(name, data, fileName, contentType);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000034AC File Offset: 0x000016AC
		public MultipartFormFileSection(byte[] data) : this(null, data, null, null)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000034BA File Offset: 0x000016BA
		public MultipartFormFileSection(string fileName, byte[] data) : this(null, data, fileName, null)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000034C8 File Offset: 0x000016C8
		public MultipartFormFileSection(string name, string data, Encoding dataEncoding, string fileName)
		{
			bool flag = string.IsNullOrEmpty(data);
			if (flag)
			{
				throw new ArgumentException("Cannot create a multipart form file section without body data");
			}
			bool flag2 = dataEncoding == null;
			if (flag2)
			{
				dataEncoding = Encoding.UTF8;
			}
			byte[] bytes = dataEncoding.GetBytes(data);
			bool flag3 = string.IsNullOrEmpty(fileName);
			if (flag3)
			{
				fileName = "file.txt";
			}
			bool flag4 = string.IsNullOrEmpty(this.content);
			if (flag4)
			{
				this.content = "text/plain; charset=" + dataEncoding.WebName;
			}
			this.Init(name, bytes, fileName, this.content);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000355A File Offset: 0x0000175A
		public MultipartFormFileSection(string data, Encoding dataEncoding, string fileName) : this(null, data, dataEncoding, fileName)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003568 File Offset: 0x00001768
		public MultipartFormFileSection(string data, string fileName) : this(data, null, fileName)
		{
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003578 File Offset: 0x00001778
		public string sectionName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003590 File Offset: 0x00001790
		public byte[] sectionData
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000035A8 File Offset: 0x000017A8
		public string fileName
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000035C0 File Offset: 0x000017C0
		public string contentType
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x0400001C RID: 28
		private string name;

		// Token: 0x0400001D RID: 29
		private byte[] data;

		// Token: 0x0400001E RID: 30
		private string file;

		// Token: 0x0400001F RID: 31
		private string content;
	}
}

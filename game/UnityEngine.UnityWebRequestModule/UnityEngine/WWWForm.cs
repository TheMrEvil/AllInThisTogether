using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	public class WWWForm
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002420 File Offset: 0x00000620
		internal static Encoding DefaultEncoding
		{
			get
			{
				return Encoding.ASCII;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002438 File Offset: 0x00000638
		public WWWForm()
		{
			this.formData = new List<byte[]>();
			this.fieldNames = new List<string>();
			this.fileNames = new List<string>();
			this.types = new List<string>();
			this.boundary = new byte[40];
			for (int i = 0; i < 40; i++)
			{
				int num = Random.Range(48, 110);
				bool flag = num > 57;
				if (flag)
				{
					num += 7;
				}
				bool flag2 = num > 90;
				if (flag2)
				{
					num += 6;
				}
				this.boundary[i] = (byte)num;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000024D0 File Offset: 0x000006D0
		public void AddField(string fieldName, string value)
		{
			this.AddField(fieldName, value, Encoding.UTF8);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000024E4 File Offset: 0x000006E4
		public void AddField(string fieldName, string value, Encoding e)
		{
			this.fieldNames.Add(fieldName);
			this.fileNames.Add(null);
			this.formData.Add(e.GetBytes(value));
			this.types.Add("text/plain; charset=\"" + e.WebName + "\"");
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002540 File Offset: 0x00000740
		public void AddField(string fieldName, int i)
		{
			this.AddField(fieldName, i.ToString());
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002552 File Offset: 0x00000752
		[ExcludeFromDocs]
		public void AddBinaryData(string fieldName, byte[] contents)
		{
			this.AddBinaryData(fieldName, contents, null, null);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002560 File Offset: 0x00000760
		[ExcludeFromDocs]
		public void AddBinaryData(string fieldName, byte[] contents, string fileName)
		{
			this.AddBinaryData(fieldName, contents, fileName, null);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002570 File Offset: 0x00000770
		public void AddBinaryData(string fieldName, byte[] contents, [DefaultValue("null")] string fileName, [DefaultValue("null")] string mimeType)
		{
			this.containsFiles = true;
			bool flag = contents.Length > 8 && contents[0] == 137 && contents[1] == 80 && contents[2] == 78 && contents[3] == 71 && contents[4] == 13 && contents[5] == 10 && contents[6] == 26 && contents[7] == 10;
			bool flag2 = fileName == null;
			if (flag2)
			{
				fileName = fieldName + (flag ? ".png" : ".dat");
			}
			bool flag3 = mimeType == null;
			if (flag3)
			{
				bool flag4 = flag;
				if (flag4)
				{
					mimeType = "image/png";
				}
				else
				{
					mimeType = "application/octet-stream";
				}
			}
			this.fieldNames.Add(fieldName);
			this.fileNames.Add(fileName);
			this.formData.Add(contents);
			this.types.Add(mimeType);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002640 File Offset: 0x00000840
		public Dictionary<string, string> headers
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				bool flag = this.containsFiles;
				if (flag)
				{
					dictionary["Content-Type"] = "multipart/form-data; boundary=\"" + Encoding.UTF8.GetString(this.boundary, 0, this.boundary.Length) + "\"";
				}
				else
				{
					dictionary["Content-Type"] = "application/x-www-form-urlencoded";
				}
				return dictionary;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000026AC File Offset: 0x000008AC
		public byte[] data
		{
			get
			{
				byte[] result;
				using (MemoryStream memoryStream = new MemoryStream(1024))
				{
					bool flag = this.containsFiles;
					if (flag)
					{
						for (int i = 0; i < this.formData.Count; i++)
						{
							memoryStream.Write(WWWForm.crlf, 0, WWWForm.crlf.Length);
							memoryStream.Write(WWWForm.dDash, 0, WWWForm.dDash.Length);
							memoryStream.Write(this.boundary, 0, this.boundary.Length);
							memoryStream.Write(WWWForm.crlf, 0, WWWForm.crlf.Length);
							memoryStream.Write(WWWForm.contentTypeHeader, 0, WWWForm.contentTypeHeader.Length);
							byte[] bytes = Encoding.UTF8.GetBytes(this.types[i]);
							memoryStream.Write(bytes, 0, bytes.Length);
							memoryStream.Write(WWWForm.crlf, 0, WWWForm.crlf.Length);
							memoryStream.Write(WWWForm.dispositionHeader, 0, WWWForm.dispositionHeader.Length);
							string headerName = Encoding.UTF8.HeaderName;
							string text = this.fieldNames[i];
							bool flag2 = !WWWTranscoder.SevenBitClean(text, Encoding.UTF8) || text.IndexOf("=?") > -1;
							if (flag2)
							{
								text = string.Concat(new string[]
								{
									"=?",
									headerName,
									"?Q?",
									WWWTranscoder.QPEncode(text, Encoding.UTF8),
									"?="
								});
							}
							byte[] bytes2 = Encoding.UTF8.GetBytes(text);
							memoryStream.Write(bytes2, 0, bytes2.Length);
							memoryStream.Write(WWWForm.endQuote, 0, WWWForm.endQuote.Length);
							bool flag3 = this.fileNames[i] != null;
							if (flag3)
							{
								string text2 = this.fileNames[i];
								bool flag4 = !WWWTranscoder.SevenBitClean(text2, Encoding.UTF8) || text2.IndexOf("=?") > -1;
								if (flag4)
								{
									text2 = string.Concat(new string[]
									{
										"=?",
										headerName,
										"?Q?",
										WWWTranscoder.QPEncode(text2, Encoding.UTF8),
										"?="
									});
								}
								byte[] bytes3 = Encoding.UTF8.GetBytes(text2);
								memoryStream.Write(WWWForm.fileNameField, 0, WWWForm.fileNameField.Length);
								memoryStream.Write(bytes3, 0, bytes3.Length);
								memoryStream.Write(WWWForm.endQuote, 0, WWWForm.endQuote.Length);
							}
							memoryStream.Write(WWWForm.crlf, 0, WWWForm.crlf.Length);
							memoryStream.Write(WWWForm.crlf, 0, WWWForm.crlf.Length);
							byte[] array = this.formData[i];
							memoryStream.Write(array, 0, array.Length);
						}
						memoryStream.Write(WWWForm.crlf, 0, WWWForm.crlf.Length);
						memoryStream.Write(WWWForm.dDash, 0, WWWForm.dDash.Length);
						memoryStream.Write(this.boundary, 0, this.boundary.Length);
						memoryStream.Write(WWWForm.dDash, 0, WWWForm.dDash.Length);
						memoryStream.Write(WWWForm.crlf, 0, WWWForm.crlf.Length);
					}
					else
					{
						for (int j = 0; j < this.formData.Count; j++)
						{
							byte[] array2 = WWWTranscoder.DataEncode(Encoding.UTF8.GetBytes(this.fieldNames[j]));
							byte[] toEncode = this.formData[j];
							byte[] array3 = WWWTranscoder.DataEncode(toEncode);
							bool flag5 = j > 0;
							if (flag5)
							{
								memoryStream.Write(WWWForm.ampersand, 0, WWWForm.ampersand.Length);
							}
							memoryStream.Write(array2, 0, array2.Length);
							memoryStream.Write(WWWForm.equal, 0, WWWForm.equal.Length);
							memoryStream.Write(array3, 0, array3.Length);
						}
					}
					result = memoryStream.ToArray();
				}
				return result;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002AB4 File Offset: 0x00000CB4
		// Note: this type is marked as 'beforefieldinit'.
		static WWWForm()
		{
		}

		// Token: 0x04000002 RID: 2
		private List<byte[]> formData;

		// Token: 0x04000003 RID: 3
		private List<string> fieldNames;

		// Token: 0x04000004 RID: 4
		private List<string> fileNames;

		// Token: 0x04000005 RID: 5
		private List<string> types;

		// Token: 0x04000006 RID: 6
		private byte[] boundary;

		// Token: 0x04000007 RID: 7
		private bool containsFiles = false;

		// Token: 0x04000008 RID: 8
		private static byte[] dDash = WWWForm.DefaultEncoding.GetBytes("--");

		// Token: 0x04000009 RID: 9
		private static byte[] crlf = WWWForm.DefaultEncoding.GetBytes("\r\n");

		// Token: 0x0400000A RID: 10
		private static byte[] contentTypeHeader = WWWForm.DefaultEncoding.GetBytes("Content-Type: ");

		// Token: 0x0400000B RID: 11
		private static byte[] dispositionHeader = WWWForm.DefaultEncoding.GetBytes("Content-disposition: form-data; name=\"");

		// Token: 0x0400000C RID: 12
		private static byte[] endQuote = WWWForm.DefaultEncoding.GetBytes("\"");

		// Token: 0x0400000D RID: 13
		private static byte[] fileNameField = WWWForm.DefaultEncoding.GetBytes("; filename=\"");

		// Token: 0x0400000E RID: 14
		private static byte[] ampersand = WWWForm.DefaultEncoding.GetBytes("&");

		// Token: 0x0400000F RID: 15
		private static byte[] equal = WWWForm.DefaultEncoding.GetBytes("=");
	}
}

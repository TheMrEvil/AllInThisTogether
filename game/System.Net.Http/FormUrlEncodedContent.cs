using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace System.Net.Http
{
	/// <summary>A container for name/value tuples encoded using application/x-www-form-urlencoded MIME type.</summary>
	// Token: 0x02000015 RID: 21
	public class FormUrlEncodedContent : ByteArrayContent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.FormUrlEncodedContent" /> class with a specific collection of name/value pairs.</summary>
		/// <param name="nameValueCollection">A collection of name/value pairs.</param>
		// Token: 0x060000CC RID: 204 RVA: 0x00003C58 File Offset: 0x00001E58
		public FormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection) : base(FormUrlEncodedContent.EncodeContent(nameValueCollection))
		{
			base.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003C7C File Offset: 0x00001E7C
		private static byte[] EncodeContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
		{
			if (nameValueCollection == null)
			{
				throw new ArgumentNullException("nameValueCollection");
			}
			List<byte> list = new List<byte>();
			foreach (KeyValuePair<string, string> keyValuePair in nameValueCollection)
			{
				if (list.Count != 0)
				{
					list.Add(38);
				}
				byte[] array = FormUrlEncodedContent.SerializeValue(keyValuePair.Key);
				if (array != null)
				{
					list.AddRange(array);
				}
				list.Add(61);
				array = FormUrlEncodedContent.SerializeValue(keyValuePair.Value);
				if (array != null)
				{
					list.AddRange(array);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003D20 File Offset: 0x00001F20
		private static byte[] SerializeValue(string value)
		{
			if (value == null)
			{
				return null;
			}
			value = Uri.EscapeDataString(value).Replace("%20", "+");
			return Encoding.ASCII.GetBytes(value);
		}
	}
}

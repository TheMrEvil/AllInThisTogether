using System;

namespace WebSocketSharp.Net
{
	// Token: 0x0200002E RID: 46
	internal class HttpHeaderInfo
	{
		// Token: 0x0600037E RID: 894 RVA: 0x00016529 File Offset: 0x00014729
		internal HttpHeaderInfo(string headerName, HttpHeaderType headerType)
		{
			this._headerName = headerName;
			this._headerType = headerType;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00016544 File Offset: 0x00014744
		internal bool IsMultiValueInRequest
		{
			get
			{
				HttpHeaderType httpHeaderType = this._headerType & HttpHeaderType.MultiValueInRequest;
				return httpHeaderType == HttpHeaderType.MultiValueInRequest;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00016568 File Offset: 0x00014768
		internal bool IsMultiValueInResponse
		{
			get
			{
				HttpHeaderType httpHeaderType = this._headerType & HttpHeaderType.MultiValueInResponse;
				return httpHeaderType == HttpHeaderType.MultiValueInResponse;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0001658C File Offset: 0x0001478C
		public string HeaderName
		{
			get
			{
				return this._headerName;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000382 RID: 898 RVA: 0x000165A4 File Offset: 0x000147A4
		public HttpHeaderType HeaderType
		{
			get
			{
				return this._headerType;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000383 RID: 899 RVA: 0x000165BC File Offset: 0x000147BC
		public bool IsRequest
		{
			get
			{
				HttpHeaderType httpHeaderType = this._headerType & HttpHeaderType.Request;
				return httpHeaderType == HttpHeaderType.Request;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000384 RID: 900 RVA: 0x000165DC File Offset: 0x000147DC
		public bool IsResponse
		{
			get
			{
				HttpHeaderType httpHeaderType = this._headerType & HttpHeaderType.Response;
				return httpHeaderType == HttpHeaderType.Response;
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000165FC File Offset: 0x000147FC
		public bool IsMultiValue(bool response)
		{
			HttpHeaderType httpHeaderType = this._headerType & HttpHeaderType.MultiValue;
			bool flag = httpHeaderType != HttpHeaderType.MultiValue;
			bool result;
			if (flag)
			{
				result = (response ? this.IsMultiValueInResponse : this.IsMultiValueInRequest);
			}
			else
			{
				result = (response ? this.IsResponse : this.IsRequest);
			}
			return result;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00016648 File Offset: 0x00014848
		public bool IsRestricted(bool response)
		{
			HttpHeaderType httpHeaderType = this._headerType & HttpHeaderType.Restricted;
			bool flag = httpHeaderType != HttpHeaderType.Restricted;
			return !flag && (response ? this.IsResponse : this.IsRequest);
		}

		// Token: 0x04000175 RID: 373
		private string _headerName;

		// Token: 0x04000176 RID: 374
		private HttpHeaderType _headerType;
	}
}

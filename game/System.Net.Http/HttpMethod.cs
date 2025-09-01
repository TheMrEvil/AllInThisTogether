using System;
using System.Net.Http.Headers;

namespace System.Net.Http
{
	/// <summary>A helper class for retrieving and comparing standard HTTP methods and for creating new HTTP methods.</summary>
	// Token: 0x02000028 RID: 40
	public class HttpMethod : IEquatable<HttpMethod>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.HttpMethod" /> class with a specific HTTP method.</summary>
		/// <param name="method">The HTTP method.</param>
		// Token: 0x06000139 RID: 313 RVA: 0x0000585E File Offset: 0x00003A5E
		public HttpMethod(string method)
		{
			if (string.IsNullOrEmpty(method))
			{
				throw new ArgumentException("method");
			}
			Parser.Token.Check(method);
			this.method = method;
		}

		/// <summary>Represents an HTTP DELETE protocol method.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00005886 File Offset: 0x00003A86
		public static HttpMethod Delete
		{
			get
			{
				return HttpMethod.delete_method;
			}
		}

		/// <summary>Represents an HTTP GET protocol method.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000588D File Offset: 0x00003A8D
		public static HttpMethod Get
		{
			get
			{
				return HttpMethod.get_method;
			}
		}

		/// <summary>Represents an HTTP HEAD protocol method. The HEAD method is identical to GET except that the server only returns message-headers in the response, without a message-body.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00005894 File Offset: 0x00003A94
		public static HttpMethod Head
		{
			get
			{
				return HttpMethod.head_method;
			}
		}

		/// <summary>An HTTP method.</summary>
		/// <returns>An HTTP method represented as a <see cref="T:System.String" />.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000589B File Offset: 0x00003A9B
		public string Method
		{
			get
			{
				return this.method;
			}
		}

		/// <summary>Represents an HTTP OPTIONS protocol method.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000058A3 File Offset: 0x00003AA3
		public static HttpMethod Options
		{
			get
			{
				return HttpMethod.options_method;
			}
		}

		/// <summary>Represents an HTTP POST protocol method that is used to post a new entity as an addition to a URI.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000058AA File Offset: 0x00003AAA
		public static HttpMethod Post
		{
			get
			{
				return HttpMethod.post_method;
			}
		}

		/// <summary>Represents an HTTP PUT protocol method that is used to replace an entity identified by a URI.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000058B1 File Offset: 0x00003AB1
		public static HttpMethod Put
		{
			get
			{
				return HttpMethod.put_method;
			}
		}

		/// <summary>Represents an HTTP TRACE protocol method.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.HttpMethod" />.</returns>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000058B8 File Offset: 0x00003AB8
		public static HttpMethod Trace
		{
			get
			{
				return HttpMethod.trace_method;
			}
		}

		/// <summary>The equality operator for comparing two <see cref="T:System.Net.Http.HttpMethod" /> objects.</summary>
		/// <param name="left">The left <see cref="T:System.Net.Http.HttpMethod" /> to an equality operator.</param>
		/// <param name="right">The right  <see cref="T:System.Net.Http.HttpMethod" /> to an equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <paramref name="left" /> and <paramref name="right" /> parameters are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000142 RID: 322 RVA: 0x000058BF File Offset: 0x00003ABF
		public static bool operator ==(HttpMethod left, HttpMethod right)
		{
			if (left == null || right == null)
			{
				return left == right;
			}
			return left.Equals(right);
		}

		/// <summary>The inequality operator for comparing two <see cref="T:System.Net.Http.HttpMethod" /> objects.</summary>
		/// <param name="left">The left <see cref="T:System.Net.Http.HttpMethod" /> to an inequality operator.</param>
		/// <param name="right">The right  <see cref="T:System.Net.Http.HttpMethod" /> to an inequality operator.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <paramref name="left" /> and <paramref name="right" /> parameters are inequal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000143 RID: 323 RVA: 0x000058D3 File Offset: 0x00003AD3
		public static bool operator !=(HttpMethod left, HttpMethod right)
		{
			return !(left == right);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Net.Http.HttpMethod" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <param name="other">The HTTP method to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000144 RID: 324 RVA: 0x000058DF File Offset: 0x00003ADF
		public bool Equals(HttpMethod other)
		{
			return string.Equals(this.method, other.method, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000145 RID: 325 RVA: 0x000058F4 File Offset: 0x00003AF4
		public override bool Equals(object obj)
		{
			HttpMethod httpMethod = obj as HttpMethod;
			return httpMethod != null && this.Equals(httpMethod);
		}

		/// <summary>Serves as a hash function for this type.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06000146 RID: 326 RVA: 0x00005914 File Offset: 0x00003B14
		public override int GetHashCode()
		{
			return this.method.GetHashCode();
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string representing the current object.</returns>
		// Token: 0x06000147 RID: 327 RVA: 0x0000589B File Offset: 0x00003A9B
		public override string ToString()
		{
			return this.method;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00002874 File Offset: 0x00000A74
		public static HttpMethod Patch
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005924 File Offset: 0x00003B24
		// Note: this type is marked as 'beforefieldinit'.
		static HttpMethod()
		{
		}

		// Token: 0x040000AB RID: 171
		private static readonly HttpMethod delete_method = new HttpMethod("DELETE");

		// Token: 0x040000AC RID: 172
		private static readonly HttpMethod get_method = new HttpMethod("GET");

		// Token: 0x040000AD RID: 173
		private static readonly HttpMethod head_method = new HttpMethod("HEAD");

		// Token: 0x040000AE RID: 174
		private static readonly HttpMethod options_method = new HttpMethod("OPTIONS");

		// Token: 0x040000AF RID: 175
		private static readonly HttpMethod post_method = new HttpMethod("POST");

		// Token: 0x040000B0 RID: 176
		private static readonly HttpMethod put_method = new HttpMethod("PUT");

		// Token: 0x040000B1 RID: 177
		private static readonly HttpMethod trace_method = new HttpMethod("TRACE");

		// Token: 0x040000B2 RID: 178
		private readonly string method;
	}
}

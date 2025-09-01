using System;

namespace Steamworks
{
	// Token: 0x02000160 RID: 352
	public enum EHTTPStatusCode
	{
		// Token: 0x040008D4 RID: 2260
		k_EHTTPStatusCodeInvalid,
		// Token: 0x040008D5 RID: 2261
		k_EHTTPStatusCode100Continue = 100,
		// Token: 0x040008D6 RID: 2262
		k_EHTTPStatusCode101SwitchingProtocols,
		// Token: 0x040008D7 RID: 2263
		k_EHTTPStatusCode200OK = 200,
		// Token: 0x040008D8 RID: 2264
		k_EHTTPStatusCode201Created,
		// Token: 0x040008D9 RID: 2265
		k_EHTTPStatusCode202Accepted,
		// Token: 0x040008DA RID: 2266
		k_EHTTPStatusCode203NonAuthoritative,
		// Token: 0x040008DB RID: 2267
		k_EHTTPStatusCode204NoContent,
		// Token: 0x040008DC RID: 2268
		k_EHTTPStatusCode205ResetContent,
		// Token: 0x040008DD RID: 2269
		k_EHTTPStatusCode206PartialContent,
		// Token: 0x040008DE RID: 2270
		k_EHTTPStatusCode300MultipleChoices = 300,
		// Token: 0x040008DF RID: 2271
		k_EHTTPStatusCode301MovedPermanently,
		// Token: 0x040008E0 RID: 2272
		k_EHTTPStatusCode302Found,
		// Token: 0x040008E1 RID: 2273
		k_EHTTPStatusCode303SeeOther,
		// Token: 0x040008E2 RID: 2274
		k_EHTTPStatusCode304NotModified,
		// Token: 0x040008E3 RID: 2275
		k_EHTTPStatusCode305UseProxy,
		// Token: 0x040008E4 RID: 2276
		k_EHTTPStatusCode307TemporaryRedirect = 307,
		// Token: 0x040008E5 RID: 2277
		k_EHTTPStatusCode308PermanentRedirect,
		// Token: 0x040008E6 RID: 2278
		k_EHTTPStatusCode400BadRequest = 400,
		// Token: 0x040008E7 RID: 2279
		k_EHTTPStatusCode401Unauthorized,
		// Token: 0x040008E8 RID: 2280
		k_EHTTPStatusCode402PaymentRequired,
		// Token: 0x040008E9 RID: 2281
		k_EHTTPStatusCode403Forbidden,
		// Token: 0x040008EA RID: 2282
		k_EHTTPStatusCode404NotFound,
		// Token: 0x040008EB RID: 2283
		k_EHTTPStatusCode405MethodNotAllowed,
		// Token: 0x040008EC RID: 2284
		k_EHTTPStatusCode406NotAcceptable,
		// Token: 0x040008ED RID: 2285
		k_EHTTPStatusCode407ProxyAuthRequired,
		// Token: 0x040008EE RID: 2286
		k_EHTTPStatusCode408RequestTimeout,
		// Token: 0x040008EF RID: 2287
		k_EHTTPStatusCode409Conflict,
		// Token: 0x040008F0 RID: 2288
		k_EHTTPStatusCode410Gone,
		// Token: 0x040008F1 RID: 2289
		k_EHTTPStatusCode411LengthRequired,
		// Token: 0x040008F2 RID: 2290
		k_EHTTPStatusCode412PreconditionFailed,
		// Token: 0x040008F3 RID: 2291
		k_EHTTPStatusCode413RequestEntityTooLarge,
		// Token: 0x040008F4 RID: 2292
		k_EHTTPStatusCode414RequestURITooLong,
		// Token: 0x040008F5 RID: 2293
		k_EHTTPStatusCode415UnsupportedMediaType,
		// Token: 0x040008F6 RID: 2294
		k_EHTTPStatusCode416RequestedRangeNotSatisfiable,
		// Token: 0x040008F7 RID: 2295
		k_EHTTPStatusCode417ExpectationFailed,
		// Token: 0x040008F8 RID: 2296
		k_EHTTPStatusCode4xxUnknown,
		// Token: 0x040008F9 RID: 2297
		k_EHTTPStatusCode429TooManyRequests = 429,
		// Token: 0x040008FA RID: 2298
		k_EHTTPStatusCode444ConnectionClosed = 444,
		// Token: 0x040008FB RID: 2299
		k_EHTTPStatusCode500InternalServerError = 500,
		// Token: 0x040008FC RID: 2300
		k_EHTTPStatusCode501NotImplemented,
		// Token: 0x040008FD RID: 2301
		k_EHTTPStatusCode502BadGateway,
		// Token: 0x040008FE RID: 2302
		k_EHTTPStatusCode503ServiceUnavailable,
		// Token: 0x040008FF RID: 2303
		k_EHTTPStatusCode504GatewayTimeout,
		// Token: 0x04000900 RID: 2304
		k_EHTTPStatusCode505HTTPVersionNotSupported,
		// Token: 0x04000901 RID: 2305
		k_EHTTPStatusCode5xxUnknown = 599
	}
}

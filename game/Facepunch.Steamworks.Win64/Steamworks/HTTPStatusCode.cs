using System;

namespace Steamworks
{
	// Token: 0x0200006F RID: 111
	internal enum HTTPStatusCode
	{
		// Token: 0x04000367 RID: 871
		Invalid,
		// Token: 0x04000368 RID: 872
		Code100Continue = 100,
		// Token: 0x04000369 RID: 873
		Code101SwitchingProtocols,
		// Token: 0x0400036A RID: 874
		Code200OK = 200,
		// Token: 0x0400036B RID: 875
		Code201Created,
		// Token: 0x0400036C RID: 876
		Code202Accepted,
		// Token: 0x0400036D RID: 877
		Code203NonAuthoritative,
		// Token: 0x0400036E RID: 878
		Code204NoContent,
		// Token: 0x0400036F RID: 879
		Code205ResetContent,
		// Token: 0x04000370 RID: 880
		Code206PartialContent,
		// Token: 0x04000371 RID: 881
		Code300MultipleChoices = 300,
		// Token: 0x04000372 RID: 882
		Code301MovedPermanently,
		// Token: 0x04000373 RID: 883
		Code302Found,
		// Token: 0x04000374 RID: 884
		Code303SeeOther,
		// Token: 0x04000375 RID: 885
		Code304NotModified,
		// Token: 0x04000376 RID: 886
		Code305UseProxy,
		// Token: 0x04000377 RID: 887
		Code307TemporaryRedirect = 307,
		// Token: 0x04000378 RID: 888
		Code400BadRequest = 400,
		// Token: 0x04000379 RID: 889
		Code401Unauthorized,
		// Token: 0x0400037A RID: 890
		Code402PaymentRequired,
		// Token: 0x0400037B RID: 891
		Code403Forbidden,
		// Token: 0x0400037C RID: 892
		Code404NotFound,
		// Token: 0x0400037D RID: 893
		Code405MethodNotAllowed,
		// Token: 0x0400037E RID: 894
		Code406NotAcceptable,
		// Token: 0x0400037F RID: 895
		Code407ProxyAuthRequired,
		// Token: 0x04000380 RID: 896
		Code408RequestTimeout,
		// Token: 0x04000381 RID: 897
		Code409Conflict,
		// Token: 0x04000382 RID: 898
		Code410Gone,
		// Token: 0x04000383 RID: 899
		Code411LengthRequired,
		// Token: 0x04000384 RID: 900
		Code412PreconditionFailed,
		// Token: 0x04000385 RID: 901
		Code413RequestEntityTooLarge,
		// Token: 0x04000386 RID: 902
		Code414RequestURITooLong,
		// Token: 0x04000387 RID: 903
		Code415UnsupportedMediaType,
		// Token: 0x04000388 RID: 904
		Code416RequestedRangeNotSatisfiable,
		// Token: 0x04000389 RID: 905
		Code417ExpectationFailed,
		// Token: 0x0400038A RID: 906
		Code4xxUnknown,
		// Token: 0x0400038B RID: 907
		Code429TooManyRequests = 429,
		// Token: 0x0400038C RID: 908
		Code500InternalServerError = 500,
		// Token: 0x0400038D RID: 909
		Code501NotImplemented,
		// Token: 0x0400038E RID: 910
		Code502BadGateway,
		// Token: 0x0400038F RID: 911
		Code503ServiceUnavailable,
		// Token: 0x04000390 RID: 912
		Code504GatewayTimeout,
		// Token: 0x04000391 RID: 913
		Code505HTTPVersionNotSupported,
		// Token: 0x04000392 RID: 914
		Code5xxUnknown = 599
	}
}

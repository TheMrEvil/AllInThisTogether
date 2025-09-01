using System;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000047 RID: 71
	internal enum EventLogEventId : uint
	{
		// Token: 0x04000181 RID: 385
		FailedToSetupTracing = 3221291108U,
		// Token: 0x04000182 RID: 386
		FailedToInitializeTraceSource,
		// Token: 0x04000183 RID: 387
		FailFast,
		// Token: 0x04000184 RID: 388
		FailFastException,
		// Token: 0x04000185 RID: 389
		FailedToTraceEvent,
		// Token: 0x04000186 RID: 390
		FailedToTraceEventWithException,
		// Token: 0x04000187 RID: 391
		InvariantAssertionFailed,
		// Token: 0x04000188 RID: 392
		PiiLoggingOn,
		// Token: 0x04000189 RID: 393
		PiiLoggingNotAllowed,
		// Token: 0x0400018A RID: 394
		WebHostUnhandledException = 3221356545U,
		// Token: 0x0400018B RID: 395
		WebHostHttpError,
		// Token: 0x0400018C RID: 396
		WebHostFailedToProcessRequest,
		// Token: 0x0400018D RID: 397
		WebHostFailedToListen,
		// Token: 0x0400018E RID: 398
		FailedToLogMessage,
		// Token: 0x0400018F RID: 399
		RemovedBadFilter,
		// Token: 0x04000190 RID: 400
		FailedToCreateMessageLoggingTraceSource,
		// Token: 0x04000191 RID: 401
		MessageLoggingOn,
		// Token: 0x04000192 RID: 402
		MessageLoggingOff,
		// Token: 0x04000193 RID: 403
		FailedToLoadPerformanceCounter,
		// Token: 0x04000194 RID: 404
		FailedToRemovePerformanceCounter,
		// Token: 0x04000195 RID: 405
		WmiGetObjectFailed,
		// Token: 0x04000196 RID: 406
		WmiPutInstanceFailed,
		// Token: 0x04000197 RID: 407
		WmiDeleteInstanceFailed,
		// Token: 0x04000198 RID: 408
		WmiCreateInstanceFailed,
		// Token: 0x04000199 RID: 409
		WmiExecQueryFailed,
		// Token: 0x0400019A RID: 410
		WmiExecMethodFailed,
		// Token: 0x0400019B RID: 411
		WmiRegistrationFailed,
		// Token: 0x0400019C RID: 412
		WmiUnregistrationFailed,
		// Token: 0x0400019D RID: 413
		WmiAdminTypeMismatch,
		// Token: 0x0400019E RID: 414
		WmiPropertyMissing,
		// Token: 0x0400019F RID: 415
		ComPlusServiceHostStartingServiceError,
		// Token: 0x040001A0 RID: 416
		ComPlusDllHostInitializerStartingError,
		// Token: 0x040001A1 RID: 417
		ComPlusTLBImportError,
		// Token: 0x040001A2 RID: 418
		ComPlusInvokingMethodFailed,
		// Token: 0x040001A3 RID: 419
		ComPlusInstanceCreationError,
		// Token: 0x040001A4 RID: 420
		ComPlusInvokingMethodFailedMismatchedTransactions,
		// Token: 0x040001A5 RID: 421
		WebHostNotLoggingInsufficientMemoryExceptionsOnActivationForNextTimeInterval = 2147614748U,
		// Token: 0x040001A6 RID: 422
		UnhandledStateMachineExceptionRecordDescription = 3221422081U,
		// Token: 0x040001A7 RID: 423
		FatalUnexpectedStateMachineEvent,
		// Token: 0x040001A8 RID: 424
		ParticipantRecoveryLogEntryCorrupt,
		// Token: 0x040001A9 RID: 425
		CoordinatorRecoveryLogEntryCorrupt,
		// Token: 0x040001AA RID: 426
		CoordinatorRecoveryLogEntryCreationFailure,
		// Token: 0x040001AB RID: 427
		ParticipantRecoveryLogEntryCreationFailure,
		// Token: 0x040001AC RID: 428
		ProtocolInitializationFailure,
		// Token: 0x040001AD RID: 429
		ProtocolStartFailure,
		// Token: 0x040001AE RID: 430
		ProtocolRecoveryBeginningFailure,
		// Token: 0x040001AF RID: 431
		ProtocolRecoveryCompleteFailure,
		// Token: 0x040001B0 RID: 432
		TransactionBridgeRecoveryFailure,
		// Token: 0x040001B1 RID: 433
		ProtocolStopFailure,
		// Token: 0x040001B2 RID: 434
		NonFatalUnexpectedStateMachineEvent,
		// Token: 0x040001B3 RID: 435
		PerformanceCounterInitializationFailure,
		// Token: 0x040001B4 RID: 436
		ProtocolRecoveryComplete,
		// Token: 0x040001B5 RID: 437
		ProtocolStopped,
		// Token: 0x040001B6 RID: 438
		ThumbPrintNotFound,
		// Token: 0x040001B7 RID: 439
		ThumbPrintNotValidated,
		// Token: 0x040001B8 RID: 440
		SslNoPrivateKey,
		// Token: 0x040001B9 RID: 441
		SslNoAccessiblePrivateKey,
		// Token: 0x040001BA RID: 442
		MissingNecessaryKeyUsage,
		// Token: 0x040001BB RID: 443
		MissingNecessaryEnhancedKeyUsage,
		// Token: 0x040001BC RID: 444
		StartErrorPublish = 3221487617U,
		// Token: 0x040001BD RID: 445
		BindingError,
		// Token: 0x040001BE RID: 446
		LAFailedToListenForApp,
		// Token: 0x040001BF RID: 447
		UnknownListenerAdapterError,
		// Token: 0x040001C0 RID: 448
		WasDisconnected,
		// Token: 0x040001C1 RID: 449
		WasConnectionTimedout,
		// Token: 0x040001C2 RID: 450
		ServiceStartFailed,
		// Token: 0x040001C3 RID: 451
		MessageQueueDuplicatedSocketLeak,
		// Token: 0x040001C4 RID: 452
		MessageQueueDuplicatedPipeLeak,
		// Token: 0x040001C5 RID: 453
		SharingUnhandledException,
		// Token: 0x040001C6 RID: 454
		ServiceAuthorizationSuccess = 1074135041U,
		// Token: 0x040001C7 RID: 455
		ServiceAuthorizationFailure = 3221618690U,
		// Token: 0x040001C8 RID: 456
		MessageAuthenticationSuccess = 1074135043U,
		// Token: 0x040001C9 RID: 457
		MessageAuthenticationFailure = 3221618692U,
		// Token: 0x040001CA RID: 458
		SecurityNegotiationSuccess = 1074135045U,
		// Token: 0x040001CB RID: 459
		SecurityNegotiationFailure = 3221618694U,
		// Token: 0x040001CC RID: 460
		TransportAuthenticationSuccess = 1074135047U,
		// Token: 0x040001CD RID: 461
		TransportAuthenticationFailure = 3221618696U,
		// Token: 0x040001CE RID: 462
		ImpersonationSuccess = 1074135049U,
		// Token: 0x040001CF RID: 463
		ImpersonationFailure = 3221618698U
	}
}

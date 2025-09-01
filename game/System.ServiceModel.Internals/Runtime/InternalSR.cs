using System;

namespace System.Runtime
{
	// Token: 0x0200003B RID: 59
	internal static class InternalSR
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x00008993 File Offset: 0x00006B93
		public static string ArgumentNullOrEmpty(string paramName)
		{
			return string.Format("{0} is null or empty", Array.Empty<object>());
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000089A4 File Offset: 0x00006BA4
		public static string AsyncEventArgsCompletedTwice(Type t)
		{
			return string.Format("AsyncEventArgs completed twice for {0}", t);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000089B1 File Offset: 0x00006BB1
		public static string AsyncEventArgsCompletionPending(Type t)
		{
			return string.Format("AsyncEventArgs completion pending for {0}", t);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000089BE File Offset: 0x00006BBE
		public static string BufferAllocationFailed(int size)
		{
			return string.Format("Buffer allocation of size {0} failed", size);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000089D0 File Offset: 0x00006BD0
		public static string BufferedOutputStreamQuotaExceeded(int maxSizeQuota)
		{
			return string.Format("Buffered output stream quota exceeded (maxSizeQuota={0})", maxSizeQuota);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000089E2 File Offset: 0x00006BE2
		public static string CannotConvertObject(object source, Type t)
		{
			return string.Format("Cannot convert object {0} to {1}", source, t);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000089F0 File Offset: 0x00006BF0
		public static string EtwAPIMaxStringCountExceeded(object max)
		{
			return string.Format("ETW API max string count exceeded {0}", max);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000089FD File Offset: 0x00006BFD
		public static string EtwMaxNumberArgumentsExceeded(object max)
		{
			return string.Format("ETW max number arguments exceeded {0}", max);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008A0A File Offset: 0x00006C0A
		public static string EtwRegistrationFailed(object arg)
		{
			return string.Format("ETW registration failed {0}", arg);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008A17 File Offset: 0x00006C17
		public static string FailFastMessage(string description)
		{
			return string.Format("Fail fast: {0}", description);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008A24 File Offset: 0x00006C24
		public static string InvalidAsyncResultImplementation(Type t)
		{
			return string.Format("Invalid AsyncResult implementation: {0}", t);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008A31 File Offset: 0x00006C31
		public static string LockTimeoutExceptionMessage(object timeout)
		{
			return string.Format("Lock timeout {0}", timeout);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008A3E File Offset: 0x00006C3E
		public static string ShipAssertExceptionMessage(object description)
		{
			return string.Format("Ship assert exception {0}", description);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008A4B File Offset: 0x00006C4B
		public static string TaskTimedOutError(object timeout)
		{
			return string.Format("Task timed out error {0}", timeout);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008A58 File Offset: 0x00006C58
		public static string TimeoutInputQueueDequeue(object timeout)
		{
			return string.Format("Timeout input queue dequeue {0}", timeout);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008A65 File Offset: 0x00006C65
		public static string TimeoutMustBeNonNegative(object argumentName, object timeout)
		{
			return string.Format("Timeout must be non-negative {0} and {1}", argumentName, timeout);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008A73 File Offset: 0x00006C73
		public static string TimeoutMustBePositive(string argumentName, object timeout)
		{
			return string.Format("Timeout must be positive {0} {1}", argumentName, timeout);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008A81 File Offset: 0x00006C81
		public static string TimeoutOnOperation(object timeout)
		{
			return string.Format("Timeout on operation {0}", timeout);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008A8E File Offset: 0x00006C8E
		public static string AsyncResultCompletedTwice(Type t)
		{
			return string.Format("AsyncResult Completed Twice for {0}", t);
		}

		// Token: 0x04000119 RID: 281
		public const string ActionItemIsAlreadyScheduled = "Action Item Is Already Scheduled";

		// Token: 0x0400011A RID: 282
		public const string AsyncCallbackThrewException = "Async Callback Threw Exception";

		// Token: 0x0400011B RID: 283
		public const string AsyncResultAlreadyEnded = "Async Result Already Ended";

		// Token: 0x0400011C RID: 284
		public const string BadCopyToArray = "Bad Copy To Array";

		// Token: 0x0400011D RID: 285
		public const string BufferIsNotRightSizeForBufferManager = "Buffer Is Not Right Size For Buffer Manager";

		// Token: 0x0400011E RID: 286
		public const string DictionaryIsReadOnly = "Dictionary Is Read Only";

		// Token: 0x0400011F RID: 287
		public const string InvalidAsyncResult = "Invalid Async Result";

		// Token: 0x04000120 RID: 288
		public const string InvalidAsyncResultImplementationGeneric = "Invalid Async Result Implementation Generic";

		// Token: 0x04000121 RID: 289
		public const string InvalidNullAsyncResult = "Invalid Null Async Result";

		// Token: 0x04000122 RID: 290
		public const string InvalidSemaphoreExit = "Invalid Semaphore Exit";

		// Token: 0x04000123 RID: 291
		public const string KeyCollectionUpdatesNotAllowed = "Key Collection Updates Not Allowed";

		// Token: 0x04000124 RID: 292
		public const string KeyNotFoundInDictionary = "Key Not Found In Dictionary";

		// Token: 0x04000125 RID: 293
		public const string MustCancelOldTimer = "Must Cancel Old Timer";

		// Token: 0x04000126 RID: 294
		public const string NullKeyAlreadyPresent = "Null Key Already Present";

		// Token: 0x04000127 RID: 295
		public const string ReadNotSupported = "Read Not Supported";

		// Token: 0x04000128 RID: 296
		public const string SFxTaskNotStarted = "SFx Task Not Started";

		// Token: 0x04000129 RID: 297
		public const string SeekNotSupported = "Seek Not Supported";

		// Token: 0x0400012A RID: 298
		public const string ThreadNeutralSemaphoreAborted = "Thread Neutral Semaphore Aborted";

		// Token: 0x0400012B RID: 299
		public const string ValueCollectionUpdatesNotAllowed = "Value Collection Updates Not Allowed";

		// Token: 0x0400012C RID: 300
		public const string ValueMustBeNonNegative = "Value Must Be Non Negative";
	}
}

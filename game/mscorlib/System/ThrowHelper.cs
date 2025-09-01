using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x020001C6 RID: 454
	[StackTraceHidden]
	internal static class ThrowHelper
	{
		// Token: 0x06001350 RID: 4944 RVA: 0x0004DBB4 File Offset: 0x0004BDB4
		internal static void ThrowArgumentNullException(ExceptionArgument argument)
		{
			throw ThrowHelper.CreateArgumentNullException(argument);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0004DBBC File Offset: 0x0004BDBC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentNullException(ExceptionArgument argument)
		{
			return new ArgumentNullException(argument.ToString());
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0004DBD0 File Offset: 0x0004BDD0
		internal static void ThrowArrayTypeMismatchException()
		{
			throw ThrowHelper.CreateArrayTypeMismatchException();
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0004DBD7 File Offset: 0x0004BDD7
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArrayTypeMismatchException()
		{
			return new ArrayTypeMismatchException();
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0004DBDE File Offset: 0x0004BDDE
		internal static void ThrowArgumentException_InvalidTypeWithPointersNotSupported(Type type)
		{
			throw ThrowHelper.CreateArgumentException_InvalidTypeWithPointersNotSupported(type);
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0004DBE6 File Offset: 0x0004BDE6
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentException_InvalidTypeWithPointersNotSupported(Type type)
		{
			return new ArgumentException(SR.Format("Cannot use type '{0}'. Only value types without pointers or references are supported.", type));
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0004DBF8 File Offset: 0x0004BDF8
		internal static void ThrowArgumentException_DestinationTooShort()
		{
			throw ThrowHelper.CreateArgumentException_DestinationTooShort();
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0004DBFF File Offset: 0x0004BDFF
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentException_DestinationTooShort()
		{
			return new ArgumentException("Destination is too short.");
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0004DC0B File Offset: 0x0004BE0B
		internal static void ThrowIndexOutOfRangeException()
		{
			throw ThrowHelper.CreateIndexOutOfRangeException();
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0004DC12 File Offset: 0x0004BE12
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateIndexOutOfRangeException()
		{
			return new IndexOutOfRangeException();
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0004DC19 File Offset: 0x0004BE19
		internal static void ThrowArgumentOutOfRangeException()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException();
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0004DC20 File Offset: 0x0004BE20
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException()
		{
			return new ArgumentOutOfRangeException();
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0004DC27 File Offset: 0x0004BE27
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException(argument);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0004DC2F File Offset: 0x0004BE2F
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException(ExceptionArgument argument)
		{
			return new ArgumentOutOfRangeException(argument.ToString());
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0004DC43 File Offset: 0x0004BE43
		internal static void ThrowArgumentOutOfRangeException_PrecisionTooLarge()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_PrecisionTooLarge();
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0004DC4A File Offset: 0x0004BE4A
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_PrecisionTooLarge()
		{
			return new ArgumentOutOfRangeException("precision", SR.Format("Precision cannot be larger than {0}.", 99));
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0004DC67 File Offset: 0x0004BE67
		internal static void ThrowArgumentOutOfRangeException_SymbolDoesNotFit()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_SymbolDoesNotFit();
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0004DC6E File Offset: 0x0004BE6E
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_SymbolDoesNotFit()
		{
			return new ArgumentOutOfRangeException("symbol", "Format specifier was invalid.");
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0004DC7F File Offset: 0x0004BE7F
		internal static void ThrowInvalidOperationException()
		{
			throw ThrowHelper.CreateInvalidOperationException();
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0004DC86 File Offset: 0x0004BE86
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException()
		{
			return new InvalidOperationException();
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0004DC8D File Offset: 0x0004BE8D
		internal static void ThrowInvalidOperationException_OutstandingReferences()
		{
			throw ThrowHelper.CreateInvalidOperationException_OutstandingReferences();
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0004DC94 File Offset: 0x0004BE94
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException_OutstandingReferences()
		{
			return new InvalidOperationException("Release all references before disposing this instance.");
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0004DCA0 File Offset: 0x0004BEA0
		internal static void ThrowInvalidOperationException_UnexpectedSegmentType()
		{
			throw ThrowHelper.CreateInvalidOperationException_UnexpectedSegmentType();
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0004DCA7 File Offset: 0x0004BEA7
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException_UnexpectedSegmentType()
		{
			return new InvalidOperationException("Unexpected segment type.");
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0004DCB3 File Offset: 0x0004BEB3
		internal static void ThrowInvalidOperationException_EndPositionNotReached()
		{
			throw ThrowHelper.CreateInvalidOperationException_EndPositionNotReached();
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0004DCBA File Offset: 0x0004BEBA
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateInvalidOperationException_EndPositionNotReached()
		{
			return new InvalidOperationException("End position was not reached during enumeration.");
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0004DCC6 File Offset: 0x0004BEC6
		internal static void ThrowArgumentOutOfRangeException_PositionOutOfRange()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_PositionOutOfRange();
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0004DCCD File Offset: 0x0004BECD
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_PositionOutOfRange()
		{
			return new ArgumentOutOfRangeException("position");
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0004DCD9 File Offset: 0x0004BED9
		internal static void ThrowArgumentOutOfRangeException_OffsetOutOfRange()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_OffsetOutOfRange();
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0004DCE0 File Offset: 0x0004BEE0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException_OffsetOutOfRange()
		{
			return new ArgumentOutOfRangeException("offset");
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0004DCEC File Offset: 0x0004BEEC
		internal static void ThrowObjectDisposedException_ArrayMemoryPoolBuffer()
		{
			throw ThrowHelper.CreateObjectDisposedException_ArrayMemoryPoolBuffer();
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0004DCF3 File Offset: 0x0004BEF3
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateObjectDisposedException_ArrayMemoryPoolBuffer()
		{
			return new ObjectDisposedException("ArrayMemoryPoolBuffer");
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0004DCFF File Offset: 0x0004BEFF
		internal static void ThrowFormatException_BadFormatSpecifier()
		{
			throw ThrowHelper.CreateFormatException_BadFormatSpecifier();
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0004DD06 File Offset: 0x0004BF06
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateFormatException_BadFormatSpecifier()
		{
			return new FormatException("Format specifier was invalid.");
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0004DD12 File Offset: 0x0004BF12
		internal static void ThrowArgumentException_OverlapAlignmentMismatch()
		{
			throw ThrowHelper.CreateArgumentException_OverlapAlignmentMismatch();
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0004DD19 File Offset: 0x0004BF19
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentException_OverlapAlignmentMismatch()
		{
			return new ArgumentException("Overlapping spans have mismatching alignment.");
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0004DD25 File Offset: 0x0004BF25
		internal static void ThrowNotSupportedException()
		{
			throw ThrowHelper.CreateThrowNotSupportedException();
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0004DD2C File Offset: 0x0004BF2C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateThrowNotSupportedException()
		{
			return new NotSupportedException();
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0004DD33 File Offset: 0x0004BF33
		public static bool TryFormatThrowFormatException(out int bytesWritten)
		{
			bytesWritten = 0;
			ThrowHelper.ThrowFormatException_BadFormatSpecifier();
			return false;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0004DD3E File Offset: 0x0004BF3E
		public static bool TryParseThrowFormatException<T>(out T value, out int bytesConsumed)
		{
			value = default(T);
			bytesConsumed = 0;
			ThrowHelper.ThrowFormatException_BadFormatSpecifier();
			return false;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0004DD50 File Offset: 0x0004BF50
		public static void ThrowArgumentValidationException<T>(ReadOnlySequenceSegment<T> startSegment, int startIndex, ReadOnlySequenceSegment<T> endSegment)
		{
			throw ThrowHelper.CreateArgumentValidationException<T>(startSegment, startIndex, endSegment);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0004DD5C File Offset: 0x0004BF5C
		private static Exception CreateArgumentValidationException<T>(ReadOnlySequenceSegment<T> startSegment, int startIndex, ReadOnlySequenceSegment<T> endSegment)
		{
			if (startSegment == null)
			{
				return ThrowHelper.CreateArgumentNullException(ExceptionArgument.startSegment);
			}
			if (endSegment == null)
			{
				return ThrowHelper.CreateArgumentNullException(ExceptionArgument.endSegment);
			}
			if (startSegment != endSegment && startSegment.RunningIndex > endSegment.RunningIndex)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.endSegment);
			}
			if (startSegment.Memory.Length < startIndex)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.startIndex);
			}
			return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.endIndex);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0004DDB9 File Offset: 0x0004BFB9
		public static void ThrowArgumentValidationException(Array array, int start)
		{
			throw ThrowHelper.CreateArgumentValidationException(array, start);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0004DDC2 File Offset: 0x0004BFC2
		private static Exception CreateArgumentValidationException(Array array, int start)
		{
			if (array == null)
			{
				return ThrowHelper.CreateArgumentNullException(ExceptionArgument.array);
			}
			if (start > array.Length)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.length);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0004DDE6 File Offset: 0x0004BFE6
		public static void ThrowStartOrEndArgumentValidationException(long start)
		{
			throw ThrowHelper.CreateStartOrEndArgumentValidationException(start);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0004DDEE File Offset: 0x0004BFEE
		private static Exception CreateStartOrEndArgumentValidationException(long start)
		{
			if (start < 0L)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.length);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0004DE04 File Offset: 0x0004C004
		internal static void ThrowWrongKeyTypeArgumentException(object key, Type targetType)
		{
			throw new ArgumentException(Environment.GetResourceString("The value \"{0}\" is not of type \"{1}\" and cannot be used in this generic collection.", new object[]
			{
				key,
				targetType
			}), "key");
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0004DE28 File Offset: 0x0004C028
		internal static void ThrowWrongValueTypeArgumentException(object value, Type targetType)
		{
			throw new ArgumentException(Environment.GetResourceString("The value \"{0}\" is not of type \"{1}\" and cannot be used in this generic collection.", new object[]
			{
				value,
				targetType
			}), "value");
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0004DE4C File Offset: 0x0004C04C
		internal static void ThrowKeyNotFoundException()
		{
			throw new KeyNotFoundException();
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0004DE53 File Offset: 0x0004C053
		internal static void ThrowArgumentException(ExceptionResource resource)
		{
			throw new ArgumentException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0004DE65 File Offset: 0x0004C065
		internal static void ThrowArgumentException(ExceptionResource resource, ExceptionArgument argument)
		{
			throw new ArgumentException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)), ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0004DE7D File Offset: 0x0004C07D
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
		{
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), string.Empty);
			}
			throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0004DEAD File Offset: 0x0004C0AD
		internal static void ThrowInvalidOperationException(ExceptionResource resource)
		{
			throw new InvalidOperationException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0004DEBF File Offset: 0x0004C0BF
		internal static void ThrowSerializationException(ExceptionResource resource)
		{
			throw new SerializationException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0004DED1 File Offset: 0x0004C0D1
		internal static void ThrowSecurityException(ExceptionResource resource)
		{
			throw new SecurityException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0004DEE3 File Offset: 0x0004C0E3
		internal static void ThrowNotSupportedException(ExceptionResource resource)
		{
			throw new NotSupportedException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0004DEF5 File Offset: 0x0004C0F5
		internal static void ThrowUnauthorizedAccessException(ExceptionResource resource)
		{
			throw new UnauthorizedAccessException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0004DF07 File Offset: 0x0004C107
		internal static void ThrowObjectDisposedException(string objectName, ExceptionResource resource)
		{
			throw new ObjectDisposedException(objectName, Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0004DF1A File Offset: 0x0004C11A
		internal static void ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion()
		{
			throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0004DF26 File Offset: 0x0004C126
		internal static void ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen()
		{
			throw new InvalidOperationException("Enumeration has either not started or has already finished.");
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0004DF32 File Offset: 0x0004C132
		internal static void ThrowInvalidOperationException_InvalidOperation_EnumNotStarted()
		{
			throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0004DF3E File Offset: 0x0004C13E
		internal static void ThrowInvalidOperationException_InvalidOperation_EnumEnded()
		{
			throw new InvalidOperationException("Enumeration already finished.");
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0004DF4A File Offset: 0x0004C14A
		internal static void ThrowInvalidOperationException_InvalidOperation_NoValue()
		{
			throw new InvalidOperationException("Nullable object must have a value.");
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0004DF56 File Offset: 0x0004C156
		private static ArgumentOutOfRangeException GetArgumentOutOfRangeException(ExceptionArgument argument, string resource)
		{
			return new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), resource);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0004DF64 File Offset: 0x0004C164
		internal static void ThrowArgumentOutOfRange_IndexException()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.index, "Index was out of range. Must be non-negative and less than the size of the collection.");
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0004DF72 File Offset: 0x0004C172
		internal static void ThrowIndexArgumentOutOfRange_NeedNonNegNumException()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.index, "Non-negative number required.");
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0004DF80 File Offset: 0x0004C180
		internal static void ThrowArgumentException_Argument_InvalidArrayType()
		{
			throw new ArgumentException("Target array type is not compatible with the type of items in the collection.");
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0004DF8C File Offset: 0x0004C18C
		private static ArgumentException GetAddingDuplicateWithKeyArgumentException(object key)
		{
			return new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", key));
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0004DF9E File Offset: 0x0004C19E
		internal static void ThrowAddingDuplicateWithKeyArgumentException(object key)
		{
			throw ThrowHelper.GetAddingDuplicateWithKeyArgumentException(key);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0004DFA6 File Offset: 0x0004C1A6
		private static KeyNotFoundException GetKeyNotFoundException(object key)
		{
			throw new KeyNotFoundException(SR.Format("The given key '{0}' was not present in the dictionary.", key.ToString()));
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0004DFBD File Offset: 0x0004C1BD
		internal static void ThrowKeyNotFoundException(object key)
		{
			throw ThrowHelper.GetKeyNotFoundException(key);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0004DFC5 File Offset: 0x0004C1C5
		internal static void ThrowInvalidTypeWithPointersNotSupported(Type targetType)
		{
			throw new ArgumentException(SR.Format("Cannot use type '{0}'. Only value types without pointers or references are supported.", targetType));
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0004DFD7 File Offset: 0x0004C1D7
		internal static void ThrowInvalidOperationException_ConcurrentOperationsNotSupported()
		{
			throw ThrowHelper.GetInvalidOperationException("Operations that change non-concurrent collections must have exclusive access. A concurrent update was performed on this collection and corrupted its state. The collection's state is no longer correct.");
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0004DFE3 File Offset: 0x0004C1E3
		internal static InvalidOperationException GetInvalidOperationException(string str)
		{
			return new InvalidOperationException(str);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0004DFEB File Offset: 0x0004C1EB
		internal static void ThrowArraySegmentCtorValidationFailedExceptions(Array array, int offset, int count)
		{
			throw ThrowHelper.GetArraySegmentCtorValidationFailedException(array, offset, count);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0004DFF5 File Offset: 0x0004C1F5
		private static Exception GetArraySegmentCtorValidationFailedException(Array array, int offset, int count)
		{
			if (array == null)
			{
				return ThrowHelper.GetArgumentNullException(ExceptionArgument.array);
			}
			if (offset < 0)
			{
				return ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.offset, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				return ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			return ThrowHelper.GetArgumentException(ExceptionResource.Argument_InvalidOffLen);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0004E022 File Offset: 0x0004C222
		private static ArgumentException GetArgumentException(ExceptionResource resource)
		{
			return new ArgumentException(resource.ToString());
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0004E036 File Offset: 0x0004C236
		private static ArgumentNullException GetArgumentNullException(ExceptionArgument argument)
		{
			return new ArgumentNullException(ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0004E044 File Offset: 0x0004C244
		internal static void IfNullAndNullsAreIllegalThenThrow<T>(object value, ExceptionArgument argName)
		{
			if (value == null && default(T) != null)
			{
				ThrowHelper.ThrowArgumentNullException(argName);
			}
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0004E06C File Offset: 0x0004C26C
		internal static string GetArgumentName(ExceptionArgument argument)
		{
			string result;
			switch (argument)
			{
			case ExceptionArgument.obj:
				result = "obj";
				break;
			case ExceptionArgument.dictionary:
				result = "dictionary";
				break;
			case ExceptionArgument.dictionaryCreationThreshold:
				result = "dictionaryCreationThreshold";
				break;
			case ExceptionArgument.array:
				result = "array";
				break;
			case ExceptionArgument.info:
				result = "info";
				break;
			case ExceptionArgument.key:
				result = "key";
				break;
			case ExceptionArgument.collection:
				result = "collection";
				break;
			case ExceptionArgument.list:
				result = "list";
				break;
			case ExceptionArgument.match:
				result = "match";
				break;
			case ExceptionArgument.converter:
				result = "converter";
				break;
			case ExceptionArgument.queue:
				result = "queue";
				break;
			case ExceptionArgument.stack:
				result = "stack";
				break;
			case ExceptionArgument.capacity:
				result = "capacity";
				break;
			case ExceptionArgument.index:
				result = "index";
				break;
			case ExceptionArgument.startIndex:
				result = "startIndex";
				break;
			case ExceptionArgument.value:
				result = "value";
				break;
			case ExceptionArgument.count:
				result = "count";
				break;
			case ExceptionArgument.arrayIndex:
				result = "arrayIndex";
				break;
			case ExceptionArgument.name:
				result = "name";
				break;
			case ExceptionArgument.mode:
				result = "mode";
				break;
			case ExceptionArgument.item:
				result = "item";
				break;
			case ExceptionArgument.options:
				result = "options";
				break;
			case ExceptionArgument.view:
				result = "view";
				break;
			case ExceptionArgument.sourceBytesToCopy:
				result = "sourceBytesToCopy";
				break;
			default:
				return string.Empty;
			}
			return result;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0004E1C5 File Offset: 0x0004C3C5
		private static ArgumentOutOfRangeException GetArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
		{
			return new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), resource.ToString());
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0004E1DF File Offset: 0x0004C3DF
		internal static void ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0004E1EA File Offset: 0x0004C3EA
		internal static void ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0004E1F8 File Offset: 0x0004C3F8
		internal static string GetResourceName(ExceptionResource resource)
		{
			string result;
			switch (resource)
			{
			case ExceptionResource.Argument_ImplementIComparable:
				result = "At least one object must implement IComparable.";
				break;
			case ExceptionResource.Argument_InvalidType:
				result = "The type of arguments passed into generic comparer methods is invalid.";
				break;
			case ExceptionResource.Argument_InvalidArgumentForComparison:
				result = "Type of argument is not compatible with the generic comparer.";
				break;
			case ExceptionResource.Argument_InvalidRegistryKeyPermissionCheck:
				result = "The specified RegistryKeyPermissionCheck value is invalid.";
				break;
			case ExceptionResource.ArgumentOutOfRange_NeedNonNegNum:
				result = "Non-negative number required.";
				break;
			case ExceptionResource.Arg_ArrayPlusOffTooSmall:
				result = "Destination array is not long enough to copy all the items in the collection. Check array index and length.";
				break;
			case ExceptionResource.Arg_NonZeroLowerBound:
				result = "The lower bound of target array must be zero.";
				break;
			case ExceptionResource.Arg_RankMultiDimNotSupported:
				result = "Only single dimensional arrays are supported for the requested action.";
				break;
			case ExceptionResource.Arg_RegKeyDelHive:
				result = "Cannot delete a registry hive's subtree.";
				break;
			case ExceptionResource.Arg_RegKeyStrLenBug:
				result = "Registry key names should not be greater than 255 characters.";
				break;
			case ExceptionResource.Arg_RegSetStrArrNull:
				result = "RegistryKey.SetValue does not allow a String[] that contains a null String reference.";
				break;
			case ExceptionResource.Arg_RegSetMismatchedKind:
				result = "The type of the value object did not match the specified RegistryValueKind or the object could not be properly converted.";
				break;
			case ExceptionResource.Arg_RegSubKeyAbsent:
				result = "Cannot delete a subkey tree because the subkey does not exist.";
				break;
			case ExceptionResource.Arg_RegSubKeyValueAbsent:
				result = "No value exists with that name.";
				break;
			case ExceptionResource.Argument_AddingDuplicate:
				result = "An item with the same key has already been added.";
				break;
			case ExceptionResource.Serialization_InvalidOnDeser:
				result = "OnDeserialization method was called while the object was not being deserialized.";
				break;
			case ExceptionResource.Serialization_MissingKeys:
				result = "The Keys for this Hashtable are missing.";
				break;
			case ExceptionResource.Serialization_NullKey:
				result = "One of the serialized keys is null.";
				break;
			case ExceptionResource.Argument_InvalidArrayType:
				result = "Target array type is not compatible with the type of items in the collection.";
				break;
			case ExceptionResource.NotSupported_KeyCollectionSet:
				result = "Mutating a key collection derived from a dictionary is not allowed.";
				break;
			case ExceptionResource.NotSupported_ValueCollectionSet:
				result = "Mutating a value collection derived from a dictionary is not allowed.";
				break;
			case ExceptionResource.ArgumentOutOfRange_SmallCapacity:
				result = "capacity was less than the current size.";
				break;
			case ExceptionResource.ArgumentOutOfRange_Index:
				result = "Index was out of range. Must be non-negative and less than the size of the collection.";
				break;
			case ExceptionResource.Argument_InvalidOffLen:
				result = "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.";
				break;
			case ExceptionResource.Argument_ItemNotExist:
				result = "The specified item does not exist in this KeyedCollection.";
				break;
			case ExceptionResource.ArgumentOutOfRange_Count:
				result = "Count must be positive and count must refer to a location within the string/array/collection.";
				break;
			case ExceptionResource.ArgumentOutOfRange_InvalidThreshold:
				result = "The specified threshold for creating dictionary is out of range.";
				break;
			case ExceptionResource.ArgumentOutOfRange_ListInsert:
				result = "Index must be within the bounds of the List.";
				break;
			case ExceptionResource.NotSupported_ReadOnlyCollection:
				result = "Collection is read-only.";
				break;
			case ExceptionResource.InvalidOperation_CannotRemoveFromStackOrQueue:
				result = "Removal is an invalid operation for Stack or Queue.";
				break;
			case ExceptionResource.InvalidOperation_EmptyQueue:
				result = "Queue empty.";
				break;
			case ExceptionResource.InvalidOperation_EnumOpCantHappen:
				result = "Enumeration has either not started or has already finished.";
				break;
			case ExceptionResource.InvalidOperation_EnumFailedVersion:
				result = "Collection was modified; enumeration operation may not execute.";
				break;
			case ExceptionResource.InvalidOperation_EmptyStack:
				result = "Stack empty.";
				break;
			case ExceptionResource.ArgumentOutOfRange_BiggerThanCollection:
				result = "Larger than collection size.";
				break;
			case ExceptionResource.InvalidOperation_EnumNotStarted:
				result = "Enumeration has not started. Call MoveNext.";
				break;
			case ExceptionResource.InvalidOperation_EnumEnded:
				result = "Enumeration already finished.";
				break;
			case ExceptionResource.NotSupported_SortedListNestedWrite:
				result = "This operation is not supported on SortedList nested types because they require modifying the original SortedList.";
				break;
			case ExceptionResource.InvalidOperation_NoValue:
				result = "Nullable object must have a value.";
				break;
			case ExceptionResource.InvalidOperation_RegRemoveSubKey:
				result = "Registry key has subkeys and recursive removes are not supported by this method.";
				break;
			case ExceptionResource.Security_RegistryPermission:
				result = "Requested registry access is not allowed.";
				break;
			case ExceptionResource.UnauthorizedAccess_RegistryNoWrite:
				result = "Cannot write to the registry key.";
				break;
			case ExceptionResource.ObjectDisposed_RegKeyClosed:
				result = "Cannot access a closed registry key.";
				break;
			case ExceptionResource.NotSupported_InComparableType:
				result = "A type must implement IComparable<T> or IComparable to support comparison.";
				break;
			case ExceptionResource.Argument_InvalidRegistryOptionsCheck:
				result = "The specified RegistryOptions value is invalid.";
				break;
			case ExceptionResource.Argument_InvalidRegistryViewCheck:
				result = "The specified RegistryView value is invalid.";
				break;
			default:
				return string.Empty;
			}
			return result;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0004E49B File Offset: 0x0004C69B
		internal static void ThrowValueArgumentOutOfRange_NeedNonNegNumException()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
		}
	}
}

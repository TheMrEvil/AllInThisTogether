using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace System
{
	// Token: 0x02000150 RID: 336
	internal class LazyHelper
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x000328B5 File Offset: 0x00030AB5
		internal LazyState State
		{
			[CompilerGenerated]
			get
			{
				return this.<State>k__BackingField;
			}
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000328BD File Offset: 0x00030ABD
		internal LazyHelper(LazyState state)
		{
			this.State = state;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x000328CC File Offset: 0x00030ACC
		internal LazyHelper(LazyThreadSafetyMode mode, Exception exception)
		{
			switch (mode)
			{
			case LazyThreadSafetyMode.None:
				this.State = 2;
				break;
			case LazyThreadSafetyMode.PublicationOnly:
				this.State = 6;
				break;
			case LazyThreadSafetyMode.ExecutionAndPublication:
				this.State = 9;
				break;
			}
			this._exceptionDispatch = ExceptionDispatchInfo.Capture(exception);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00032919 File Offset: 0x00030B19
		internal void ThrowException()
		{
			this._exceptionDispatch.Throw();
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00032928 File Offset: 0x00030B28
		private LazyThreadSafetyMode GetMode()
		{
			switch (this.State)
			{
			case LazyState.NoneViaConstructor:
			case LazyState.NoneViaFactory:
			case LazyState.NoneException:
				return LazyThreadSafetyMode.None;
			case LazyState.PublicationOnlyViaConstructor:
			case LazyState.PublicationOnlyViaFactory:
			case LazyState.PublicationOnlyWait:
			case LazyState.PublicationOnlyException:
				return LazyThreadSafetyMode.PublicationOnly;
			case LazyState.ExecutionAndPublicationViaConstructor:
			case LazyState.ExecutionAndPublicationViaFactory:
			case LazyState.ExecutionAndPublicationException:
				return LazyThreadSafetyMode.ExecutionAndPublication;
			default:
				return LazyThreadSafetyMode.None;
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00032974 File Offset: 0x00030B74
		internal static LazyThreadSafetyMode? GetMode(LazyHelper state)
		{
			if (state == null)
			{
				return null;
			}
			return new LazyThreadSafetyMode?(state.GetMode());
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00032999 File Offset: 0x00030B99
		internal static bool GetIsValueFaulted(LazyHelper state)
		{
			return ((state != null) ? state._exceptionDispatch : null) != null;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000329AC File Offset: 0x00030BAC
		internal static LazyHelper Create(LazyThreadSafetyMode mode, bool useDefaultConstructor)
		{
			switch (mode)
			{
			case LazyThreadSafetyMode.None:
				if (!useDefaultConstructor)
				{
					return LazyHelper.NoneViaFactory;
				}
				return LazyHelper.NoneViaConstructor;
			case LazyThreadSafetyMode.PublicationOnly:
				if (!useDefaultConstructor)
				{
					return LazyHelper.PublicationOnlyViaFactory;
				}
				return LazyHelper.PublicationOnlyViaConstructor;
			case LazyThreadSafetyMode.ExecutionAndPublication:
				return new LazyHelper(useDefaultConstructor ? LazyState.ExecutionAndPublicationViaConstructor : LazyState.ExecutionAndPublicationViaFactory);
			default:
				throw new ArgumentOutOfRangeException("mode", "The mode argument specifies an invalid value.");
			}
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00032A08 File Offset: 0x00030C08
		internal static object CreateViaDefaultConstructor(Type type)
		{
			object result;
			try
			{
				result = Activator.CreateInstance(type);
			}
			catch (MissingMethodException)
			{
				throw new MissingMemberException("The lazily-initialized type does not have a public, parameterless constructor.");
			}
			return result;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00032A3C File Offset: 0x00030C3C
		internal static LazyThreadSafetyMode GetModeFromIsThreadSafe(bool isThreadSafe)
		{
			if (!isThreadSafe)
			{
				return LazyThreadSafetyMode.None;
			}
			return LazyThreadSafetyMode.ExecutionAndPublication;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00032A44 File Offset: 0x00030C44
		// Note: this type is marked as 'beforefieldinit'.
		static LazyHelper()
		{
		}

		// Token: 0x04001266 RID: 4710
		internal static readonly LazyHelper NoneViaConstructor = new LazyHelper(LazyState.NoneViaConstructor);

		// Token: 0x04001267 RID: 4711
		internal static readonly LazyHelper NoneViaFactory = new LazyHelper(LazyState.NoneViaFactory);

		// Token: 0x04001268 RID: 4712
		internal static readonly LazyHelper PublicationOnlyViaConstructor = new LazyHelper(LazyState.PublicationOnlyViaConstructor);

		// Token: 0x04001269 RID: 4713
		internal static readonly LazyHelper PublicationOnlyViaFactory = new LazyHelper(LazyState.PublicationOnlyViaFactory);

		// Token: 0x0400126A RID: 4714
		internal static readonly LazyHelper PublicationOnlyWaitForOtherThreadToPublish = new LazyHelper(LazyState.PublicationOnlyWait);

		// Token: 0x0400126B RID: 4715
		[CompilerGenerated]
		private readonly LazyState <State>k__BackingField;

		// Token: 0x0400126C RID: 4716
		private readonly ExceptionDispatchInfo _exceptionDispatch;
	}
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Provides helper methods for marshaling data between the .NET Framework and the Windows Runtime.</summary>
	// Token: 0x0200078C RID: 1932
	public static class WindowsRuntimeMarshal
	{
		/// <summary>Adds the specified event handler to a Windows Runtime event.</summary>
		/// <param name="addMethod">A delegate that represents the method that adds event handlers to the Windows Runtime event.</param>
		/// <param name="removeMethod">A delegate that represents the method that removes event handlers from the Windows Runtime event.</param>
		/// <param name="handler">A delegate the represents the event handler that is added.</param>
		/// <typeparam name="T">The type of the delegate that represents the event handler.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="addMethod" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="removeMethod" /> is <see langword="null" />.</exception>
		// Token: 0x0600449C RID: 17564 RVA: 0x000E3FA8 File Offset: 0x000E21A8
		[SecurityCritical]
		public static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
		{
			if (addMethod == null)
			{
				throw new ArgumentNullException("addMethod");
			}
			if (removeMethod == null)
			{
				throw new ArgumentNullException("removeMethod");
			}
			if (handler == null)
			{
				return;
			}
			object target = removeMethod.Target;
			if (target == null || Marshal.IsComObject(target))
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.AddEventHandler<T>(addMethod, removeMethod, handler);
				return;
			}
			WindowsRuntimeMarshal.ManagedEventRegistrationImpl.AddEventHandler<T>(addMethod, removeMethod, handler);
		}

		/// <summary>Removes the specified event handler from a Windows Runtime event.</summary>
		/// <param name="removeMethod">A delegate that represents the method that removes event handlers from the Windows Runtime event.</param>
		/// <param name="handler">The event handler that is removed.</param>
		/// <typeparam name="T">The type of the delegate that represents the event handler.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="removeMethod" /> is <see langword="null" />.</exception>
		// Token: 0x0600449D RID: 17565 RVA: 0x000E4000 File Offset: 0x000E2200
		[SecurityCritical]
		public static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
		{
			if (removeMethod == null)
			{
				throw new ArgumentNullException("removeMethod");
			}
			if (handler == null)
			{
				return;
			}
			object target = removeMethod.Target;
			if (target == null || Marshal.IsComObject(target))
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.RemoveEventHandler<T>(removeMethod, handler);
				return;
			}
			WindowsRuntimeMarshal.ManagedEventRegistrationImpl.RemoveEventHandler<T>(removeMethod, handler);
		}

		/// <summary>Removes all the event handlers that can be removed by using the specified method.</summary>
		/// <param name="removeMethod">A delegate that represents the method that removes event handlers from the Windows Runtime event.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="removeMethod" /> is <see langword="null" />.</exception>
		// Token: 0x0600449E RID: 17566 RVA: 0x000E4048 File Offset: 0x000E2248
		[SecurityCritical]
		public static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
		{
			if (removeMethod == null)
			{
				throw new ArgumentNullException("removeMethod");
			}
			object target = removeMethod.Target;
			if (target == null || Marshal.IsComObject(target))
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.RemoveAllEventHandlers(removeMethod);
				return;
			}
			WindowsRuntimeMarshal.ManagedEventRegistrationImpl.RemoveAllEventHandlers(removeMethod);
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x000E4084 File Offset: 0x000E2284
		internal static int GetRegistrationTokenCacheSize()
		{
			int num = 0;
			if (WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations != null)
			{
				ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> s_eventRegistrations = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations;
				lock (s_eventRegistrations)
				{
					num += WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.Keys.Count;
				}
			}
			if (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations != null)
			{
				Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> s_eventRegistrations2 = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations;
				lock (s_eventRegistrations2)
				{
					num += WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Count;
				}
			}
			return num;
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x000E4124 File Offset: 0x000E2324
		internal static void CallRemoveMethods(Action<EventRegistrationToken> removeMethod, List<EventRegistrationToken> tokensToRemove)
		{
			List<Exception> list = new List<Exception>();
			foreach (EventRegistrationToken obj in tokensToRemove)
			{
				try
				{
					removeMethod(obj);
				}
				catch (Exception item)
				{
					list.Add(item);
				}
			}
			if (list.Count > 0)
			{
				throw new AggregateException(list.ToArray());
			}
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x000E41A8 File Offset: 0x000E23A8
		[SecurityCritical]
		internal unsafe static string HStringToString(IntPtr hstring)
		{
			if (hstring == IntPtr.Zero)
			{
				return string.Empty;
			}
			uint num;
			return new string(UnsafeNativeMethods.WindowsGetStringRawBuffer(hstring, &num), 0, checked((int)num));
		}

		// Token: 0x060044A2 RID: 17570 RVA: 0x000E41DC File Offset: 0x000E23DC
		internal static Exception GetExceptionForHR(int hresult, Exception innerException, string messageResource)
		{
			Exception ex;
			if (innerException != null)
			{
				string text = innerException.Message;
				if (text == null && messageResource != null)
				{
					text = Environment.GetResourceString(messageResource);
				}
				ex = new Exception(text, innerException);
			}
			else
			{
				ex = new Exception((messageResource != null) ? Environment.GetResourceString(messageResource) : null);
			}
			ex.SetErrorCode(hresult);
			return ex;
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x000E4226 File Offset: 0x000E2426
		internal static Exception GetExceptionForHR(int hresult, Exception innerException)
		{
			return WindowsRuntimeMarshal.GetExceptionForHR(hresult, innerException, null);
		}

		// Token: 0x060044A4 RID: 17572 RVA: 0x000E4230 File Offset: 0x000E2430
		[SecurityCritical]
		private static bool RoOriginateLanguageException(int error, string message, IntPtr languageException)
		{
			if (WindowsRuntimeMarshal.s_haveBlueErrorApis)
			{
				try
				{
					return UnsafeNativeMethods.RoOriginateLanguageException(error, message, languageException);
				}
				catch (EntryPointNotFoundException)
				{
					WindowsRuntimeMarshal.s_haveBlueErrorApis = false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x000E426C File Offset: 0x000E246C
		[SecurityCritical]
		private static void RoReportUnhandledError(IRestrictedErrorInfo error)
		{
			if (WindowsRuntimeMarshal.s_haveBlueErrorApis)
			{
				try
				{
					UnsafeNativeMethods.RoReportUnhandledError(error);
				}
				catch (EntryPointNotFoundException)
				{
					WindowsRuntimeMarshal.s_haveBlueErrorApis = false;
				}
			}
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x000E42A4 File Offset: 0x000E24A4
		[FriendAccessAllowed]
		[SecuritySafeCritical]
		internal static bool ReportUnhandledError(Exception e)
		{
			if (!AppDomain.IsAppXModel())
			{
				return false;
			}
			if (!WindowsRuntimeMarshal.s_haveBlueErrorApis)
			{
				return false;
			}
			if (e != null)
			{
				IntPtr intPtr = IntPtr.Zero;
				IntPtr zero = IntPtr.Zero;
				try
				{
					intPtr = Marshal.GetIUnknownForObject(e);
					if (intPtr != IntPtr.Zero)
					{
						Marshal.QueryInterface(intPtr, ref WindowsRuntimeMarshal.s_iidIErrorInfo, out zero);
						if (zero != IntPtr.Zero && WindowsRuntimeMarshal.RoOriginateLanguageException(Marshal.GetHRForException_WinRT(e), e.Message, zero))
						{
							IRestrictedErrorInfo restrictedErrorInfo = UnsafeNativeMethods.GetRestrictedErrorInfo();
							if (restrictedErrorInfo != null)
							{
								WindowsRuntimeMarshal.RoReportUnhandledError(restrictedErrorInfo);
								return true;
							}
						}
					}
				}
				finally
				{
					if (zero != IntPtr.Zero)
					{
						Marshal.Release(zero);
					}
					if (intPtr != IntPtr.Zero)
					{
						Marshal.Release(intPtr);
					}
				}
				return false;
			}
			return false;
		}

		/// <summary>Returns an object that implements the activation factory interface for the specified Windows Runtime type.</summary>
		/// <param name="type">The Windows Runtime type to get the activation factory interface for.</param>
		/// <returns>An object that implements the activation factory interface.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not represent a Windows Runtime type (that is, belonging to the Windows Runtime itself or defined in a Windows Runtime component).  
		/// -or-  
		/// The object specified for <paramref name="type" /> was not provided by the common language runtime type system.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.TypeLoadException">The specified Windows Runtime class is not properly registered. For example, the .winmd file was located, but the Windows Runtime failed to locate the implementation.</exception>
		// Token: 0x060044A7 RID: 17575 RVA: 0x000E436C File Offset: 0x000E256C
		[SecurityCritical]
		public static IActivationFactory GetActivationFactory(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsWindowsRuntimeObject && type.IsImport)
			{
				return (IActivationFactory)Marshal.GetNativeActivationFactory(type);
			}
			throw new NotSupportedException();
		}

		/// <summary>Allocates a Windows RuntimeHSTRING and copies the specified managed string to it.</summary>
		/// <param name="s">The managed string to copy.</param>
		/// <returns>An unmanaged pointer to the new HSTRING, or <see cref="F:System.IntPtr.Zero" /> if <paramref name="s" /> is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The Windows Runtime is not supported on the current version of the operating system.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		// Token: 0x060044A8 RID: 17576 RVA: 0x000E43A4 File Offset: 0x000E25A4
		[SecurityCritical]
		public unsafe static IntPtr StringToHString(string s)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("Windows Runtime is not supported on this operating system."));
			}
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			IntPtr result;
			Marshal.ThrowExceptionForHR(UnsafeNativeMethods.WindowsCreateString(s, s.Length, &result), new IntPtr(-1));
			return result;
		}

		/// <summary>Returns a managed string that contains a copy of the specified Windows RuntimeHSTRING.</summary>
		/// <param name="ptr">An unmanaged pointer to the HSTRING to copy.</param>
		/// <returns>A managed string that contains a copy of the HSTRING if <paramref name="ptr" /> is not <see cref="F:System.IntPtr.Zero" />; otherwise, <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The Windows Runtime is not supported on the current version of the operating system.</exception>
		// Token: 0x060044A9 RID: 17577 RVA: 0x000E43F1 File Offset: 0x000E25F1
		[SecurityCritical]
		public static string PtrToStringHString(IntPtr ptr)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("Windows Runtime is not supported on this operating system."));
			}
			return WindowsRuntimeMarshal.HStringToString(ptr);
		}

		/// <summary>Frees the specified Windows RuntimeHSTRING.</summary>
		/// <param name="ptr">The address of the HSTRING to free.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The Windows Runtime is not supported on the current version of the operating system.</exception>
		// Token: 0x060044AA RID: 17578 RVA: 0x000E4410 File Offset: 0x000E2610
		[SecurityCritical]
		public static void FreeHString(IntPtr ptr)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("Windows Runtime is not supported on this operating system."));
			}
			if (ptr != IntPtr.Zero)
			{
				UnsafeNativeMethods.WindowsDeleteString(ptr);
			}
		}

		// Token: 0x060044AB RID: 17579 RVA: 0x000E4440 File Offset: 0x000E2640
		// Note: this type is marked as 'beforefieldinit'.
		static WindowsRuntimeMarshal()
		{
		}

		// Token: 0x04002C27 RID: 11303
		private static bool s_haveBlueErrorApis = true;

		// Token: 0x04002C28 RID: 11304
		private static Guid s_iidIErrorInfo = new Guid(485667104, 21629, 4123, 142, 101, 8, 0, 43, 43, 209, 25);

		// Token: 0x0200078D RID: 1933
		internal struct EventRegistrationTokenList
		{
			// Token: 0x060044AC RID: 17580 RVA: 0x000E4480 File Offset: 0x000E2680
			internal EventRegistrationTokenList(EventRegistrationToken token)
			{
				this.firstToken = token;
				this.restTokens = null;
			}

			// Token: 0x060044AD RID: 17581 RVA: 0x000E4490 File Offset: 0x000E2690
			internal EventRegistrationTokenList(WindowsRuntimeMarshal.EventRegistrationTokenList list)
			{
				this.firstToken = list.firstToken;
				this.restTokens = list.restTokens;
			}

			// Token: 0x060044AE RID: 17582 RVA: 0x000E44AC File Offset: 0x000E26AC
			public bool Push(EventRegistrationToken token)
			{
				bool result = false;
				if (this.restTokens == null)
				{
					this.restTokens = new List<EventRegistrationToken>();
					result = true;
				}
				this.restTokens.Add(token);
				return result;
			}

			// Token: 0x060044AF RID: 17583 RVA: 0x000E44E0 File Offset: 0x000E26E0
			public bool Pop(out EventRegistrationToken token)
			{
				if (this.restTokens == null || this.restTokens.Count == 0)
				{
					token = this.firstToken;
					return false;
				}
				int index = this.restTokens.Count - 1;
				token = this.restTokens[index];
				this.restTokens.RemoveAt(index);
				return true;
			}

			// Token: 0x060044B0 RID: 17584 RVA: 0x000E453D File Offset: 0x000E273D
			public void CopyTo(List<EventRegistrationToken> tokens)
			{
				tokens.Add(this.firstToken);
				if (this.restTokens != null)
				{
					tokens.AddRange(this.restTokens);
				}
			}

			// Token: 0x04002C29 RID: 11305
			private EventRegistrationToken firstToken;

			// Token: 0x04002C2A RID: 11306
			private List<EventRegistrationToken> restTokens;
		}

		// Token: 0x0200078E RID: 1934
		internal static class ManagedEventRegistrationImpl
		{
			// Token: 0x060044B1 RID: 17585 RVA: 0x000E4560 File Offset: 0x000E2760
			[SecurityCritical]
			internal static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
			{
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> eventRegistrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(removeMethod.Target, removeMethod);
				EventRegistrationToken token = addMethod(handler);
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> obj = eventRegistrationTokenTable;
				lock (obj)
				{
					WindowsRuntimeMarshal.EventRegistrationTokenList value;
					if (!eventRegistrationTokenTable.TryGetValue(handler, out value))
					{
						value = new WindowsRuntimeMarshal.EventRegistrationTokenList(token);
						eventRegistrationTokenTable[handler] = value;
					}
					else if (value.Push(token))
					{
						eventRegistrationTokenTable[handler] = value;
					}
				}
			}

			// Token: 0x060044B2 RID: 17586 RVA: 0x000E45EC File Offset: 0x000E27EC
			private static Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> GetEventRegistrationTokenTable(object instance, Action<EventRegistrationToken> removeMethod)
			{
				ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> obj = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations;
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> result;
				lock (obj)
				{
					Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>> dictionary = null;
					if (!WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.TryGetValue(instance, out dictionary))
					{
						dictionary = new Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>();
						WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.Add(instance, dictionary);
					}
					Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary2 = null;
					if (!dictionary.TryGetValue(removeMethod.Method, out dictionary2))
					{
						dictionary2 = new Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>();
						dictionary.Add(removeMethod.Method, dictionary2);
					}
					result = dictionary2;
				}
				return result;
			}

			// Token: 0x060044B3 RID: 17587 RVA: 0x000E4678 File Offset: 0x000E2878
			[SecurityCritical]
			internal static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
			{
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> eventRegistrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(removeMethod.Target, removeMethod);
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> obj = eventRegistrationTokenTable;
				EventRegistrationToken obj2;
				lock (obj)
				{
					WindowsRuntimeMarshal.EventRegistrationTokenList eventRegistrationTokenList;
					if (!eventRegistrationTokenTable.TryGetValue(handler, out eventRegistrationTokenList))
					{
						return;
					}
					if (!eventRegistrationTokenList.Pop(out obj2))
					{
						eventRegistrationTokenTable.Remove(handler);
					}
				}
				removeMethod(obj2);
			}

			// Token: 0x060044B4 RID: 17588 RVA: 0x000E46EC File Offset: 0x000E28EC
			[SecurityCritical]
			internal static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
			{
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> eventRegistrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(removeMethod.Target, removeMethod);
				List<EventRegistrationToken> list = new List<EventRegistrationToken>();
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> obj = eventRegistrationTokenTable;
				lock (obj)
				{
					foreach (WindowsRuntimeMarshal.EventRegistrationTokenList eventRegistrationTokenList in eventRegistrationTokenTable.Values)
					{
						eventRegistrationTokenList.CopyTo(list);
					}
					eventRegistrationTokenTable.Clear();
				}
				WindowsRuntimeMarshal.CallRemoveMethods(removeMethod, list);
			}

			// Token: 0x060044B5 RID: 17589 RVA: 0x000E4788 File Offset: 0x000E2988
			// Note: this type is marked as 'beforefieldinit'.
			static ManagedEventRegistrationImpl()
			{
			}

			// Token: 0x04002C2B RID: 11307
			internal static volatile ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> s_eventRegistrations = new ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>>();
		}

		// Token: 0x0200078F RID: 1935
		internal static class NativeOrStaticEventRegistrationImpl
		{
			// Token: 0x060044B6 RID: 17590 RVA: 0x000E4798 File Offset: 0x000E2998
			[SecuritySafeCritical]
			private static object GetInstanceKey(Action<EventRegistrationToken> removeMethod)
			{
				object target = removeMethod.Target;
				if (target == null)
				{
					return removeMethod.Method.DeclaringType;
				}
				return Marshal.GetRawIUnknownForComObjectNoAddRef(target);
			}

			// Token: 0x060044B7 RID: 17591 RVA: 0x000E47C8 File Offset: 0x000E29C8
			[SecurityCritical]
			internal static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
			{
				object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
				EventRegistrationToken eventRegistrationToken = addMethod(handler);
				bool flag = false;
				try
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
					try
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
						ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> orCreateEventRegistrationTokenTable = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetOrCreateEventRegistrationTokenTable(instanceKey, removeMethod, out tokenListCount);
						ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> obj = orCreateEventRegistrationTokenTable;
						lock (obj)
						{
							WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount eventRegistrationTokenListWithCount;
							if (orCreateEventRegistrationTokenTable.FindEquivalentKeyUnsafe(handler, out eventRegistrationTokenListWithCount) == null)
							{
								eventRegistrationTokenListWithCount = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount(tokenListCount, eventRegistrationToken);
								orCreateEventRegistrationTokenTable.Add(handler, eventRegistrationTokenListWithCount);
							}
							else
							{
								eventRegistrationTokenListWithCount.Push(eventRegistrationToken);
							}
							flag = true;
						}
					}
					finally
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
					}
				}
				catch (Exception)
				{
					if (!flag)
					{
						removeMethod(eventRegistrationToken);
					}
					throw;
				}
			}

			// Token: 0x060044B8 RID: 17592 RVA: 0x000E4898 File Offset: 0x000E2A98
			private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetEventRegistrationTokenTableNoCreate(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount)
			{
				return WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableInternal(instance, removeMethod, out tokenListCount, false);
			}

			// Token: 0x060044B9 RID: 17593 RVA: 0x000E48A3 File Offset: 0x000E2AA3
			private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetOrCreateEventRegistrationTokenTable(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount)
			{
				return WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableInternal(instance, removeMethod, out tokenListCount, true);
			}

			// Token: 0x060044BA RID: 17594 RVA: 0x000E48B0 File Offset: 0x000E2AB0
			private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetEventRegistrationTokenTableInternal(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount, bool createIfNotFound)
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key;
				key.target = instance;
				key.method = removeMethod.Method;
				Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> obj = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations;
				ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> registrationTable;
				lock (obj)
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry eventCacheEntry;
					if (!WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.TryGetValue(key, out eventCacheEntry))
					{
						if (!createIfNotFound)
						{
							tokenListCount = null;
							return null;
						}
						eventCacheEntry = default(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry);
						eventCacheEntry.registrationTable = new ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount>();
						eventCacheEntry.tokenListCount = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount(key);
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Add(key, eventCacheEntry);
					}
					tokenListCount = eventCacheEntry.tokenListCount;
					registrationTable = eventCacheEntry.registrationTable;
				}
				return registrationTable;
			}

			// Token: 0x060044BB RID: 17595 RVA: 0x000E4960 File Offset: 0x000E2B60
			[SecurityCritical]
			internal static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
			{
				object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
				EventRegistrationToken obj2;
				try
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> eventRegistrationTokenTableNoCreate = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableNoCreate(instanceKey, removeMethod, out tokenListCount);
					if (eventRegistrationTokenTableNoCreate == null)
					{
						return;
					}
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> obj = eventRegistrationTokenTableNoCreate;
					lock (obj)
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount eventRegistrationTokenListWithCount;
						object key = eventRegistrationTokenTableNoCreate.FindEquivalentKeyUnsafe(handler, out eventRegistrationTokenListWithCount);
						if (eventRegistrationTokenListWithCount == null)
						{
							return;
						}
						if (!eventRegistrationTokenListWithCount.Pop(out obj2))
						{
							eventRegistrationTokenTableNoCreate.Remove(key);
						}
					}
				}
				finally
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
				}
				removeMethod(obj2);
			}

			// Token: 0x060044BC RID: 17596 RVA: 0x000E4A08 File Offset: 0x000E2C08
			[SecurityCritical]
			internal static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
			{
				object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
				List<EventRegistrationToken> list = new List<EventRegistrationToken>();
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
				try
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> eventRegistrationTokenTableNoCreate = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableNoCreate(instanceKey, removeMethod, out tokenListCount);
					if (eventRegistrationTokenTableNoCreate == null)
					{
						return;
					}
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> obj = eventRegistrationTokenTableNoCreate;
					lock (obj)
					{
						foreach (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount eventRegistrationTokenListWithCount in eventRegistrationTokenTableNoCreate.Values)
						{
							eventRegistrationTokenListWithCount.CopyTo(list);
						}
						eventRegistrationTokenTableNoCreate.Clear();
					}
				}
				finally
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
				}
				WindowsRuntimeMarshal.CallRemoveMethods(removeMethod, list);
			}

			// Token: 0x060044BD RID: 17597 RVA: 0x000E4AD0 File Offset: 0x000E2CD0
			// Note: this type is marked as 'beforefieldinit'.
			static NativeOrStaticEventRegistrationImpl()
			{
			}

			// Token: 0x04002C2C RID: 11308
			internal static volatile Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> s_eventRegistrations = new Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry>(new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKeyEqualityComparer());

			// Token: 0x04002C2D RID: 11309
			private static volatile WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.MyReaderWriterLock s_eventCacheRWLock = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.MyReaderWriterLock();

			// Token: 0x02000790 RID: 1936
			internal struct EventCacheKey
			{
				// Token: 0x060044BE RID: 17598 RVA: 0x000E4AF0 File Offset: 0x000E2CF0
				public override string ToString()
				{
					string[] array = new string[5];
					array[0] = "(";
					int num = 1;
					object obj = this.target;
					array[num] = ((obj != null) ? obj.ToString() : null);
					array[2] = ", ";
					int num2 = 3;
					MethodInfo methodInfo = this.method;
					array[num2] = ((methodInfo != null) ? methodInfo.ToString() : null);
					array[4] = ")";
					return string.Concat(array);
				}

				// Token: 0x04002C2E RID: 11310
				internal object target;

				// Token: 0x04002C2F RID: 11311
				internal MethodInfo method;
			}

			// Token: 0x02000791 RID: 1937
			internal class EventCacheKeyEqualityComparer : IEqualityComparer<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey>
			{
				// Token: 0x060044BF RID: 17599 RVA: 0x000E4B4A File Offset: 0x000E2D4A
				public bool Equals(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey lhs, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey rhs)
				{
					return object.Equals(lhs.target, rhs.target) && object.Equals(lhs.method, rhs.method);
				}

				// Token: 0x060044C0 RID: 17600 RVA: 0x000E4B72 File Offset: 0x000E2D72
				public int GetHashCode(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key)
				{
					return key.target.GetHashCode() ^ key.method.GetHashCode();
				}

				// Token: 0x060044C1 RID: 17601 RVA: 0x0000259F File Offset: 0x0000079F
				public EventCacheKeyEqualityComparer()
				{
				}
			}

			// Token: 0x02000792 RID: 1938
			internal class EventRegistrationTokenListWithCount
			{
				// Token: 0x060044C2 RID: 17602 RVA: 0x000E4B8B File Offset: 0x000E2D8B
				internal EventRegistrationTokenListWithCount(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount, EventRegistrationToken token)
				{
					this._tokenListCount = tokenListCount;
					this._tokenListCount.Inc();
					this._tokenList = new WindowsRuntimeMarshal.EventRegistrationTokenList(token);
				}

				// Token: 0x060044C3 RID: 17603 RVA: 0x000E4BB4 File Offset: 0x000E2DB4
				~EventRegistrationTokenListWithCount()
				{
					this._tokenListCount.Dec();
				}

				// Token: 0x060044C4 RID: 17604 RVA: 0x000E4BE8 File Offset: 0x000E2DE8
				public void Push(EventRegistrationToken token)
				{
					this._tokenList.Push(token);
				}

				// Token: 0x060044C5 RID: 17605 RVA: 0x000E4BF7 File Offset: 0x000E2DF7
				public bool Pop(out EventRegistrationToken token)
				{
					return this._tokenList.Pop(out token);
				}

				// Token: 0x060044C6 RID: 17606 RVA: 0x000E4C05 File Offset: 0x000E2E05
				public void CopyTo(List<EventRegistrationToken> tokens)
				{
					this._tokenList.CopyTo(tokens);
				}

				// Token: 0x04002C30 RID: 11312
				private WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount _tokenListCount;

				// Token: 0x04002C31 RID: 11313
				private WindowsRuntimeMarshal.EventRegistrationTokenList _tokenList;
			}

			// Token: 0x02000793 RID: 1939
			internal class TokenListCount
			{
				// Token: 0x060044C7 RID: 17607 RVA: 0x000E4C13 File Offset: 0x000E2E13
				internal TokenListCount(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key)
				{
					this._key = key;
				}

				// Token: 0x17000AA7 RID: 2727
				// (get) Token: 0x060044C8 RID: 17608 RVA: 0x000E4C22 File Offset: 0x000E2E22
				internal WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey Key
				{
					get
					{
						return this._key;
					}
				}

				// Token: 0x060044C9 RID: 17609 RVA: 0x000E4C2A File Offset: 0x000E2E2A
				internal void Inc()
				{
					Interlocked.Increment(ref this._count);
				}

				// Token: 0x060044CA RID: 17610 RVA: 0x000E4C38 File Offset: 0x000E2E38
				internal void Dec()
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireWriterLock(-1);
					try
					{
						if (Interlocked.Decrement(ref this._count) == 0)
						{
							this.CleanupCache();
						}
					}
					finally
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseWriterLock();
					}
				}

				// Token: 0x060044CB RID: 17611 RVA: 0x000E4C84 File Offset: 0x000E2E84
				private void CleanupCache()
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Remove(this._key);
				}

				// Token: 0x04002C32 RID: 11314
				private int _count;

				// Token: 0x04002C33 RID: 11315
				private WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey _key;
			}

			// Token: 0x02000794 RID: 1940
			internal struct EventCacheEntry
			{
				// Token: 0x04002C34 RID: 11316
				internal ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> registrationTable;

				// Token: 0x04002C35 RID: 11317
				internal WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
			}

			// Token: 0x02000795 RID: 1941
			internal class ReaderWriterLockTimedOutException : ApplicationException
			{
				// Token: 0x060044CC RID: 17612 RVA: 0x000E4C99 File Offset: 0x000E2E99
				public ReaderWriterLockTimedOutException()
				{
				}
			}

			// Token: 0x02000796 RID: 1942
			internal class MyReaderWriterLock
			{
				// Token: 0x060044CD RID: 17613 RVA: 0x0000259F File Offset: 0x0000079F
				internal MyReaderWriterLock()
				{
				}

				// Token: 0x060044CE RID: 17614 RVA: 0x000E4CA4 File Offset: 0x000E2EA4
				internal void AcquireReaderLock(int millisecondsTimeout)
				{
					this.EnterMyLock();
					while (this.owners < 0 || this.numWriteWaiters != 0U)
					{
						if (this.readEvent == null)
						{
							this.LazyCreateEvent(ref this.readEvent, false);
						}
						else
						{
							this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, millisecondsTimeout);
						}
					}
					this.owners++;
					this.ExitMyLock();
				}

				// Token: 0x060044CF RID: 17615 RVA: 0x000E4D0C File Offset: 0x000E2F0C
				internal void AcquireWriterLock(int millisecondsTimeout)
				{
					this.EnterMyLock();
					while (this.owners != 0)
					{
						if (this.writeEvent == null)
						{
							this.LazyCreateEvent(ref this.writeEvent, true);
						}
						else
						{
							this.WaitOnEvent(this.writeEvent, ref this.numWriteWaiters, millisecondsTimeout);
						}
					}
					this.owners = -1;
					this.ExitMyLock();
				}

				// Token: 0x060044D0 RID: 17616 RVA: 0x000E4D62 File Offset: 0x000E2F62
				internal void ReleaseReaderLock()
				{
					this.EnterMyLock();
					this.owners--;
					this.ExitAndWakeUpAppropriateWaiters();
				}

				// Token: 0x060044D1 RID: 17617 RVA: 0x000E4D7E File Offset: 0x000E2F7E
				internal void ReleaseWriterLock()
				{
					this.EnterMyLock();
					this.owners++;
					this.ExitAndWakeUpAppropriateWaiters();
				}

				// Token: 0x060044D2 RID: 17618 RVA: 0x000E4D9C File Offset: 0x000E2F9C
				private void LazyCreateEvent(ref EventWaitHandle waitEvent, bool makeAutoResetEvent)
				{
					this.ExitMyLock();
					EventWaitHandle eventWaitHandle;
					if (makeAutoResetEvent)
					{
						eventWaitHandle = new AutoResetEvent(false);
					}
					else
					{
						eventWaitHandle = new ManualResetEvent(false);
					}
					this.EnterMyLock();
					if (waitEvent == null)
					{
						waitEvent = eventWaitHandle;
					}
				}

				// Token: 0x060044D3 RID: 17619 RVA: 0x000E4DD0 File Offset: 0x000E2FD0
				private void WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, int millisecondsTimeout)
				{
					waitEvent.Reset();
					numWaiters += 1U;
					bool flag = false;
					this.ExitMyLock();
					try
					{
						if (!waitEvent.WaitOne(millisecondsTimeout, false))
						{
							throw new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.ReaderWriterLockTimedOutException();
						}
						flag = true;
					}
					finally
					{
						this.EnterMyLock();
						numWaiters -= 1U;
						if (!flag)
						{
							this.ExitMyLock();
						}
					}
				}

				// Token: 0x060044D4 RID: 17620 RVA: 0x000E4E2C File Offset: 0x000E302C
				private void ExitAndWakeUpAppropriateWaiters()
				{
					if (this.owners == 0 && this.numWriteWaiters > 0U)
					{
						this.ExitMyLock();
						this.writeEvent.Set();
						return;
					}
					if (this.owners >= 0 && this.numReadWaiters != 0U)
					{
						this.ExitMyLock();
						this.readEvent.Set();
						return;
					}
					this.ExitMyLock();
				}

				// Token: 0x060044D5 RID: 17621 RVA: 0x000E4E87 File Offset: 0x000E3087
				private void EnterMyLock()
				{
					if (Interlocked.CompareExchange(ref this.myLock, 1, 0) != 0)
					{
						this.EnterMyLockSpin();
					}
				}

				// Token: 0x060044D6 RID: 17622 RVA: 0x000E4EA0 File Offset: 0x000E30A0
				private void EnterMyLockSpin()
				{
					int num = 0;
					for (;;)
					{
						if (num < 3 && Environment.ProcessorCount > 1)
						{
							Thread.SpinWait(20);
						}
						else
						{
							Thread.Sleep(0);
						}
						if (Interlocked.CompareExchange(ref this.myLock, 1, 0) == 0)
						{
							break;
						}
						num++;
					}
				}

				// Token: 0x060044D7 RID: 17623 RVA: 0x000E4EDF File Offset: 0x000E30DF
				private void ExitMyLock()
				{
					this.myLock = 0;
				}

				// Token: 0x04002C36 RID: 11318
				private int myLock;

				// Token: 0x04002C37 RID: 11319
				private int owners;

				// Token: 0x04002C38 RID: 11320
				private uint numWriteWaiters;

				// Token: 0x04002C39 RID: 11321
				private uint numReadWaiters;

				// Token: 0x04002C3A RID: 11322
				private EventWaitHandle writeEvent;

				// Token: 0x04002C3B RID: 11323
				private EventWaitHandle readEvent;
			}
		}
	}
}

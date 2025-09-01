using System;
using System.Collections.Generic;

namespace System
{
	/// <summary>Provides members for setting and retrieving data about an application's context.</summary>
	// Token: 0x020001E0 RID: 480
	public static class AppContext
	{
		/// <summary>Gets the pathname of the base directory that the assembly resolver uses to probe for assemblies.</summary>
		/// <returns>the pathname of the base directory that the assembly resolver uses to probe for assemblies.</returns>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x00051781 File Offset: 0x0004F981
		public static string BaseDirectory
		{
			get
			{
				return ((string)AppDomain.CurrentDomain.GetData("APP_CONTEXT_BASE_DIRECTORY")) ?? AppDomain.CurrentDomain.BaseDirectory;
			}
		}

		/// <summary>Gets the name of the framework version targeted by the current application.</summary>
		/// <returns>The name of the framework version targeted by the current application.</returns>
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x000517A5 File Offset: 0x0004F9A5
		public static string TargetFrameworkName
		{
			get
			{
				return AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName;
			}
		}

		/// <summary>Returns the value of the named data element assigned to the current application domain.</summary>
		/// <param name="name">The name of the data element.</param>
		/// <returns>The value of <paramref name="name" />, if <paramref name="name" /> identifies a named value; otherwise, <see langword="null" />.</returns>
		// Token: 0x060014AD RID: 5293 RVA: 0x000517B6 File Offset: 0x0004F9B6
		public static object GetData(string name)
		{
			return AppDomain.CurrentDomain.GetData(name);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x000517C4 File Offset: 0x0004F9C4
		private static void InitializeDefaultSwitchValues()
		{
			Dictionary<string, AppContext.SwitchValueState> obj = AppContext.s_switchMap;
			lock (obj)
			{
				if (!AppContext.s_defaultsInitialized)
				{
					AppContextDefaultValues.PopulateDefaultValues();
					AppContext.s_defaultsInitialized = true;
				}
			}
		}

		/// <summary>Tries to get the value of a switch.</summary>
		/// <param name="switchName">The name of the switch.</param>
		/// <param name="isEnabled">When this method returns, contains the value of <paramref name="switchName" /> if <paramref name="switchName" /> was found, or <see langword="false" /> if <paramref name="switchName" /> was not found. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="switchName" /> was set and the <paramref name="isEnabled" /> argument contains the value of the switch; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="switchName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="switchName" /> is <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x060014AF RID: 5295 RVA: 0x00051814 File Offset: 0x0004FA14
		public static bool TryGetSwitch(string switchName, out bool isEnabled)
		{
			if (switchName == null)
			{
				throw new ArgumentNullException("switchName");
			}
			if (switchName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Empty name is not legal."), "switchName");
			}
			if (!AppContext.s_defaultsInitialized)
			{
				AppContext.InitializeDefaultSwitchValues();
			}
			isEnabled = false;
			Dictionary<string, AppContext.SwitchValueState> obj = AppContext.s_switchMap;
			lock (obj)
			{
				AppContext.SwitchValueState switchValueState;
				if (AppContext.s_switchMap.TryGetValue(switchName, out switchValueState))
				{
					if (switchValueState == AppContext.SwitchValueState.UnknownValue)
					{
						isEnabled = false;
						return false;
					}
					isEnabled = ((switchValueState & AppContext.SwitchValueState.HasTrueValue) == AppContext.SwitchValueState.HasTrueValue);
					if ((switchValueState & AppContext.SwitchValueState.HasLookedForOverride) == AppContext.SwitchValueState.HasLookedForOverride)
					{
						return true;
					}
					bool flag2;
					if (AppContextDefaultValues.TryGetSwitchOverride(switchName, out flag2))
					{
						isEnabled = flag2;
					}
					AppContext.s_switchMap[switchName] = ((isEnabled ? AppContext.SwitchValueState.HasTrueValue : AppContext.SwitchValueState.HasFalseValue) | AppContext.SwitchValueState.HasLookedForOverride);
					return true;
				}
				else
				{
					bool flag3;
					if (AppContextDefaultValues.TryGetSwitchOverride(switchName, out flag3))
					{
						isEnabled = flag3;
						AppContext.s_switchMap[switchName] = ((isEnabled ? AppContext.SwitchValueState.HasTrueValue : AppContext.SwitchValueState.HasFalseValue) | AppContext.SwitchValueState.HasLookedForOverride);
						return true;
					}
					AppContext.s_switchMap[switchName] = AppContext.SwitchValueState.UnknownValue;
				}
			}
			return false;
		}

		/// <summary>Sets the value of a switch.</summary>
		/// <param name="switchName">The name of the switch.</param>
		/// <param name="isEnabled">The value of the switch.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="switchName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="switchName" /> is <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x060014B0 RID: 5296 RVA: 0x00051918 File Offset: 0x0004FB18
		public static void SetSwitch(string switchName, bool isEnabled)
		{
			if (switchName == null)
			{
				throw new ArgumentNullException("switchName");
			}
			if (switchName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Empty name is not legal."), "switchName");
			}
			if (!AppContext.s_defaultsInitialized)
			{
				AppContext.InitializeDefaultSwitchValues();
			}
			AppContext.SwitchValueState value = (isEnabled ? AppContext.SwitchValueState.HasTrueValue : AppContext.SwitchValueState.HasFalseValue) | AppContext.SwitchValueState.HasLookedForOverride;
			Dictionary<string, AppContext.SwitchValueState> obj = AppContext.s_switchMap;
			lock (obj)
			{
				AppContext.s_switchMap[switchName] = value;
			}
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x000519A0 File Offset: 0x0004FBA0
		internal static void DefineSwitchDefault(string switchName, bool isEnabled)
		{
			AppContext.s_switchMap[switchName] = (isEnabled ? AppContext.SwitchValueState.HasTrueValue : AppContext.SwitchValueState.HasFalseValue);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x000519B4 File Offset: 0x0004FBB4
		internal static void DefineSwitchOverride(string switchName, bool isEnabled)
		{
			AppContext.s_switchMap[switchName] = ((isEnabled ? AppContext.SwitchValueState.HasTrueValue : AppContext.SwitchValueState.HasFalseValue) | AppContext.SwitchValueState.HasLookedForOverride);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x000519CA File Offset: 0x0004FBCA
		// Note: this type is marked as 'beforefieldinit'.
		static AppContext()
		{
		}

		// Token: 0x04001470 RID: 5232
		private static readonly Dictionary<string, AppContext.SwitchValueState> s_switchMap = new Dictionary<string, AppContext.SwitchValueState>();

		// Token: 0x04001471 RID: 5233
		private static volatile bool s_defaultsInitialized = false;

		// Token: 0x020001E1 RID: 481
		[Flags]
		private enum SwitchValueState
		{
			// Token: 0x04001473 RID: 5235
			HasFalseValue = 1,
			// Token: 0x04001474 RID: 5236
			HasTrueValue = 2,
			// Token: 0x04001475 RID: 5237
			HasLookedForOverride = 4,
			// Token: 0x04001476 RID: 5238
			UnknownValue = 8
		}
	}
}

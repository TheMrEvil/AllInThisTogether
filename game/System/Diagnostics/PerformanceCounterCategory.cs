using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Represents a performance object, which defines a category of performance counters.</summary>
	// Token: 0x02000271 RID: 625
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public sealed class PerformanceCounterCategory
	{
		// Token: 0x060013E6 RID: 5094
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool CategoryDelete_icall(char* name, int name_length);

		// Token: 0x060013E7 RID: 5095 RVA: 0x00052790 File Offset: 0x00050990
		private unsafe static bool CategoryDelete(string name)
		{
			char* ptr = name;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return PerformanceCounterCategory.CategoryDelete_icall(ptr, (name != null) ? name.Length : 0);
		}

		// Token: 0x060013E8 RID: 5096
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern string CategoryHelp_icall(char* category, int category_length);

		// Token: 0x060013E9 RID: 5097 RVA: 0x000527C0 File Offset: 0x000509C0
		private unsafe static string CategoryHelpInternal(string category)
		{
			char* ptr = category;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return PerformanceCounterCategory.CategoryHelp_icall(ptr, (category != null) ? category.Length : 0);
		}

		// Token: 0x060013EA RID: 5098
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool CounterCategoryExists_icall(char* counter, int counter_length, char* category, int category_length);

		// Token: 0x060013EB RID: 5099 RVA: 0x000527F0 File Offset: 0x000509F0
		private unsafe static bool CounterCategoryExists(string counter, string category)
		{
			char* ptr = counter;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = category;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return PerformanceCounterCategory.CounterCategoryExists_icall(ptr, (counter != null) ? counter.Length : 0, ptr2, (category != null) ? category.Length : 0);
		}

		// Token: 0x060013EC RID: 5100
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool Create_icall(char* categoryName, int categoryName_length, char* categoryHelp, int categoryHelp_length, PerformanceCounterCategoryType categoryType, CounterCreationData[] items);

		// Token: 0x060013ED RID: 5101 RVA: 0x0005283C File Offset: 0x00050A3C
		private unsafe static bool Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, CounterCreationData[] items)
		{
			char* ptr = categoryName;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = categoryHelp;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return PerformanceCounterCategory.Create_icall(ptr, (categoryName != null) ? categoryName.Length : 0, ptr2, (categoryHelp != null) ? categoryHelp.Length : 0, categoryType, items);
		}

		// Token: 0x060013EE RID: 5102
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool InstanceExistsInternal_icall(char* instance, int instance_length, char* category, int category_length);

		// Token: 0x060013EF RID: 5103 RVA: 0x0005288C File Offset: 0x00050A8C
		private unsafe static bool InstanceExistsInternal(string instance, string category)
		{
			char* ptr = instance;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = category;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return PerformanceCounterCategory.InstanceExistsInternal_icall(ptr, (instance != null) ? instance.Length : 0, ptr2, (category != null) ? category.Length : 0);
		}

		// Token: 0x060013F0 RID: 5104
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetCategoryNames();

		// Token: 0x060013F1 RID: 5105
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern string[] GetCounterNames_icall(char* category, int category_length);

		// Token: 0x060013F2 RID: 5106 RVA: 0x000528D8 File Offset: 0x00050AD8
		private unsafe static string[] GetCounterNames(string category)
		{
			char* ptr = category;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return PerformanceCounterCategory.GetCounterNames_icall(ptr, (category != null) ? category.Length : 0);
		}

		// Token: 0x060013F3 RID: 5107
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern string[] GetInstanceNames_icall(char* category, int category_length);

		// Token: 0x060013F4 RID: 5108 RVA: 0x00052908 File Offset: 0x00050B08
		private unsafe static string[] GetInstanceNames(string category)
		{
			char* ptr = category;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return PerformanceCounterCategory.GetInstanceNames_icall(ptr, (category != null) ? category.Length : 0);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00052937 File Offset: 0x00050B37
		private static void CheckCategory(string categoryName)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if (categoryName == "")
			{
				throw new ArgumentException("categoryName");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> class, leaves the <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property empty, and sets the <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> property to the local computer.</summary>
		// Token: 0x060013F6 RID: 5110 RVA: 0x0005295F File Offset: 0x00050B5F
		public PerformanceCounterCategory() : this("", ".")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> class, sets the <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property to the specified value, and sets the <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> property to the local computer.</summary>
		/// <param name="categoryName">The name of the performance counter category, or performance object, with which to associate this <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> is <see langword="null" />.</exception>
		// Token: 0x060013F7 RID: 5111 RVA: 0x00052971 File Offset: 0x00050B71
		public PerformanceCounterCategory(string categoryName) : this(categoryName, ".")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> class and sets the <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> and <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> properties to the specified values.</summary>
		/// <param name="categoryName">The name of the performance counter category, or performance object, with which to associate this <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> instance.</param>
		/// <param name="machineName">The computer on which the performance counter category and its associated counters exist.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> is an empty string ("").  
		///  -or-  
		///  The <paramref name="machineName" /> syntax is invalid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> is <see langword="null" />.</exception>
		// Token: 0x060013F8 RID: 5112 RVA: 0x0005297F File Offset: 0x00050B7F
		public PerformanceCounterCategory(string categoryName, string machineName)
		{
			PerformanceCounterCategory.CheckCategory(categoryName);
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			this.categoryName = categoryName;
			this.machineName = machineName;
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0005248C File Offset: 0x0005068C
		private static bool IsValidMachine(string machine)
		{
			return machine == ".";
		}

		/// <summary>Gets the category's help text.</summary>
		/// <returns>A description of the performance object that this category measures.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property is <see langword="null" />. The category name must be set before getting the category help.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x000529B0 File Offset: 0x00050BB0
		public string CategoryHelp
		{
			get
			{
				string text = null;
				if (PerformanceCounterCategory.IsValidMachine(this.machineName))
				{
					text = PerformanceCounterCategory.CategoryHelpInternal(this.categoryName);
				}
				if (text != null)
				{
					return text;
				}
				throw new InvalidOperationException();
			}
		}

		/// <summary>Gets or sets the name of the performance object that defines this category.</summary>
		/// <returns>The name of the performance counter category, or performance object, with which to associate this <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> instance.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> is <see langword="null" />.</exception>
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x000529E2 File Offset: 0x00050BE2
		// (set) Token: 0x060013FC RID: 5116 RVA: 0x000529EA File Offset: 0x00050BEA
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == "")
				{
					throw new ArgumentException("value");
				}
				this.categoryName = value;
			}
		}

		/// <summary>Gets or sets the name of the computer on which this category exists.</summary>
		/// <returns>The name of the computer on which the performance counter category and its associated counters exist.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> syntax is invalid.</exception>
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x00052A19 File Offset: 0x00050C19
		// (set) Token: 0x060013FE RID: 5118 RVA: 0x00052A21 File Offset: 0x00050C21
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == "")
				{
					throw new ArgumentException("value");
				}
				this.machineName = value;
			}
		}

		/// <summary>Gets the performance counter category type.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.PerformanceCounterCategoryType" /> values.</returns>
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00052A50 File Offset: 0x00050C50
		public PerformanceCounterCategoryType CategoryType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Determines whether the specified counter is registered to this category, which is indicated by the <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> and <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> properties.</summary>
		/// <param name="counterName">The name of the performance counter to look for.</param>
		/// <returns>
		///   <see langword="true" /> if the counter is registered to the category that is specified by the <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> and <see cref="P:System.Diagnostics.PerformanceCounterCategory.MachineName" /> properties; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property has not been set.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06001400 RID: 5120 RVA: 0x00052A58 File Offset: 0x00050C58
		public bool CounterExists(string counterName)
		{
			return PerformanceCounterCategory.CounterExists(counterName, this.categoryName, this.machineName);
		}

		/// <summary>Determines whether the specified counter is registered to the specified category on the local computer.</summary>
		/// <param name="counterName">The name of the performance counter to look for.</param>
		/// <param name="categoryName">The name of the performance counter category, or performance object, with which the specified performance counter is associated.</param>
		/// <returns>
		///   <see langword="true" />, if the counter is registered to the specified category on the local computer; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> is an empty string ("").</exception>
		/// <exception cref="T:System.InvalidOperationException">The category name does not exist.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06001401 RID: 5121 RVA: 0x00052A6C File Offset: 0x00050C6C
		public static bool CounterExists(string counterName, string categoryName)
		{
			return PerformanceCounterCategory.CounterExists(counterName, categoryName, ".");
		}

		/// <summary>Determines whether the specified counter is registered to the specified category on a remote computer.</summary>
		/// <param name="counterName">The name of the performance counter to look for.</param>
		/// <param name="categoryName">The name of the performance counter category, or performance object, with which the specified performance counter is associated.</param>
		/// <param name="machineName">The name of the computer on which the performance counter category and its associated counters exist.</param>
		/// <returns>
		///   <see langword="true" />, if the counter is registered to the specified category on the specified computer; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> is an empty string ("").  
		///  -or-  
		///  The <paramref name="machineName" /> is invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The category name does not exist.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06001402 RID: 5122 RVA: 0x00052A7A File Offset: 0x00050C7A
		public static bool CounterExists(string counterName, string categoryName, string machineName)
		{
			if (counterName == null)
			{
				throw new ArgumentNullException("counterName");
			}
			PerformanceCounterCategory.CheckCategory(categoryName);
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			return PerformanceCounterCategory.IsValidMachine(machineName) && PerformanceCounterCategory.CounterCategoryExists(counterName, categoryName);
		}

		/// <summary>Registers the custom performance counter category containing the specified counters on the local computer.</summary>
		/// <param name="categoryName">The name of the custom performance counter category to create and register with the system.</param>
		/// <param name="categoryHelp">A description of the custom category.</param>
		/// <param name="counterData">A <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> that specifies the counters to create as part of the new category.</param>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> that is associated with the new custom category, or performance object.</returns>
		/// <exception cref="T:System.ArgumentException">A counter name that is specified within the <paramref name="counterData" /> collection is <see langword="null" /> or an empty string ("").  
		///  -or-  
		///  A counter that is specified within the <paramref name="counterData" /> collection already exists.  
		///  -or-  
		///  The <paramref name="counterName" /> parameter has invalid syntax. It might contain backslash characters ("\") or have length greater than 80 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The category already exists on the local computer.  
		///  -or-  
		///  The layout of the <paramref name="counterData" /> collection is incorrect for base counters. A counter of type <see langword="AverageCount64" />, <see langword="AverageTimer32" />, <see langword="CounterMultiTimer" />, <see langword="CounterMultiTimerInverse" />, <see langword="CounterMultiTimer100Ns" />, <see langword="CounterMultiTimer100NsInverse" />, <see langword="RawFraction" />, <see langword="SampleFraction" /> or <see langword="SampleCounter" /> has to be immediately followed by one of the base counter types (<see langword="AverageBase" />, <see langword="MultiBase" />, <see langword="RawBase" />, or <see langword="SampleBase" />).</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06001403 RID: 5123 RVA: 0x00052AAF File Offset: 0x00050CAF
		[Obsolete("Use another overload that uses PerformanceCounterCategoryType instead")]
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, CounterCreationDataCollection counterData)
		{
			return PerformanceCounterCategory.Create(categoryName, categoryHelp, PerformanceCounterCategoryType.Unknown, counterData);
		}

		/// <summary>Registers a custom performance counter category containing a single counter of type <see langword="NumberOfItems32" /> on the local computer.</summary>
		/// <param name="categoryName">The name of the custom performance counter category to create and register with the system.</param>
		/// <param name="categoryHelp">A description of the custom category.</param>
		/// <param name="counterName">The name of a new counter, of type <see langword="NumberOfItems32" />, to create as part of the new category.</param>
		/// <param name="counterHelp">A description of the counter that is associated with the new custom category.</param>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> that is associated with the new system category, or performance object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="counterName" /> is <see langword="null" /> or is an empty string ("").  
		/// -or-  
		/// The counter that is specified by <paramref name="counterName" /> already exists.  
		/// -or-  
		/// <paramref name="counterName" /> has invalid syntax. It might contain backslash characters ("\") or have length greater than 80 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The category already exists on the local computer.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="counterHelp" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06001404 RID: 5124 RVA: 0x00052ABA File Offset: 0x00050CBA
		[Obsolete("Use another overload that uses PerformanceCounterCategoryType instead")]
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, string counterName, string counterHelp)
		{
			return PerformanceCounterCategory.Create(categoryName, categoryHelp, PerformanceCounterCategoryType.Unknown, counterName, counterHelp);
		}

		/// <summary>Registers the custom performance counter category containing the specified counters on the local computer.</summary>
		/// <param name="categoryName">The name of the custom performance counter category to create and register with the system.</param>
		/// <param name="categoryHelp">A description of the custom category.</param>
		/// <param name="categoryType">One of the <see cref="T:System.Diagnostics.PerformanceCounterCategoryType" /> values.</param>
		/// <param name="counterData">A <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> that specifies the counters to create as part of the new category.</param>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> that is associated with the new custom category, or performance object.</returns>
		/// <exception cref="T:System.ArgumentException">A counter name that is specified within the <paramref name="counterData" /> collection is <see langword="null" /> or an empty string ("").  
		///  -or-  
		///  A counter that is specified within the <paramref name="counterData" /> collection already exists.  
		///  -or-  
		///  <paramref name="counterName" /> has invalid syntax. It might contain backslash characters ("\") or have length greater than 80 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="counterData" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="categoryType" /> value is outside of the range of the following values: <see langword="MultiInstance" />, <see langword="SingleInstance" />, or <see langword="Unknown" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The category already exists on the local computer.  
		///  -or-  
		///  The layout of the <paramref name="counterData" /> collection is incorrect for base counters. A counter of type <see langword="AverageCount64" />, <see langword="AverageTimer32" />, <see langword="CounterMultiTimer" />, <see langword="CounterMultiTimerInverse" />, <see langword="CounterMultiTimer100Ns" />, <see langword="CounterMultiTimer100NsInverse" />, <see langword="RawFraction" />, <see langword="SampleFraction" />, or <see langword="SampleCounter" /> must be immediately followed by one of the base counter types (<see langword="AverageBase" />, <see langword="MultiBase" />, <see langword="RawBase" />, or <see langword="SampleBase" />).</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06001405 RID: 5125 RVA: 0x00052AC8 File Offset: 0x00050CC8
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, CounterCreationDataCollection counterData)
		{
			PerformanceCounterCategory.CheckCategory(categoryName);
			if (counterData == null)
			{
				throw new ArgumentNullException("counterData");
			}
			if (counterData.Count == 0)
			{
				throw new ArgumentException("counterData");
			}
			CounterCreationData[] array = new CounterCreationData[counterData.Count];
			counterData.CopyTo(array, 0);
			if (!PerformanceCounterCategory.Create(categoryName, categoryHelp, categoryType, array))
			{
				throw new InvalidOperationException();
			}
			return new PerformanceCounterCategory(categoryName, categoryHelp);
		}

		/// <summary>Registers the custom performance counter category containing a single counter of type <see cref="F:System.Diagnostics.PerformanceCounterType.NumberOfItems32" /> on the local computer.</summary>
		/// <param name="categoryName">The name of the custom performance counter category to create and register with the system.</param>
		/// <param name="categoryHelp">A description of the custom category.</param>
		/// <param name="categoryType">One of the <see cref="T:System.Diagnostics.PerformanceCounterCategoryType" /> values specifying whether the category is <see cref="F:System.Diagnostics.PerformanceCounterCategoryType.MultiInstance" />, <see cref="F:System.Diagnostics.PerformanceCounterCategoryType.SingleInstance" />, or <see cref="F:System.Diagnostics.PerformanceCounterCategoryType.Unknown" />.</param>
		/// <param name="counterName">The name of a new counter to create as part of the new category.</param>
		/// <param name="counterHelp">A description of the counter that is associated with the new custom category.</param>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> that is associated with the new system category, or performance object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="counterName" /> is <see langword="null" /> or is an empty string ("").  
		/// -or-  
		/// The counter that is specified by <paramref name="counterName" /> already exists.  
		/// -or-  
		/// <paramref name="counterName" /> has invalid syntax. It might contain backslash characters ("\") or have length greater than 80 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The category already exists on the local computer.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="counterHelp" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06001406 RID: 5126 RVA: 0x00052B28 File Offset: 0x00050D28
		public static PerformanceCounterCategory Create(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, string counterName, string counterHelp)
		{
			PerformanceCounterCategory.CheckCategory(categoryName);
			if (!PerformanceCounterCategory.Create(categoryName, categoryHelp, categoryType, new CounterCreationData[]
			{
				new CounterCreationData(counterName, counterHelp, PerformanceCounterType.NumberOfItems32)
			}))
			{
				throw new InvalidOperationException();
			}
			return new PerformanceCounterCategory(categoryName, categoryHelp);
		}

		/// <summary>Removes the category and its associated counters from the local computer.</summary>
		/// <param name="categoryName">The name of the custom performance counter category to delete.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> parameter has invalid syntax. It might contain backslash characters ("\") or have length greater than 80 characters.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The category cannot be deleted because it is not a custom category.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06001407 RID: 5127 RVA: 0x00052B6A File Offset: 0x00050D6A
		public static void Delete(string categoryName)
		{
			PerformanceCounterCategory.CheckCategory(categoryName);
			if (!PerformanceCounterCategory.CategoryDelete(categoryName))
			{
				throw new InvalidOperationException();
			}
		}

		/// <summary>Determines whether the category is registered on the local computer.</summary>
		/// <param name="categoryName">The name of the performance counter category to look for.</param>
		/// <returns>
		///   <see langword="true" /> if the category is registered; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> parameter is an empty string ("").</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06001408 RID: 5128 RVA: 0x00052B80 File Offset: 0x00050D80
		public static bool Exists(string categoryName)
		{
			return PerformanceCounterCategory.Exists(categoryName, ".");
		}

		/// <summary>Determines whether the category is registered on the specified computer.</summary>
		/// <param name="categoryName">The name of the performance counter category to look for.</param>
		/// <param name="machineName">The name of the computer to examine for the category.</param>
		/// <returns>
		///   <see langword="true" /> if the category is registered; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="categoryName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> parameter is an empty string ("").  
		///  -or-  
		///  The <paramref name="machineName" /> parameter is invalid.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.IO.IOException">The network path cannot be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.  
		///  -or-  
		///  Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06001409 RID: 5129 RVA: 0x00052B8D File Offset: 0x00050D8D
		public static bool Exists(string categoryName, string machineName)
		{
			PerformanceCounterCategory.CheckCategory(categoryName);
			return PerformanceCounterCategory.IsValidMachine(machineName) && PerformanceCounterCategory.CounterCategoryExists(null, categoryName);
		}

		/// <summary>Retrieves a list of the performance counter categories that are registered on the local computer.</summary>
		/// <returns>An array of <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> objects indicating the categories that are registered on the local computer.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x0600140A RID: 5130 RVA: 0x00052BA6 File Offset: 0x00050DA6
		public static PerformanceCounterCategory[] GetCategories()
		{
			return PerformanceCounterCategory.GetCategories(".");
		}

		/// <summary>Retrieves a list of the performance counter categories that are registered on the specified computer.</summary>
		/// <param name="machineName">The computer to look on.</param>
		/// <returns>An array of <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> objects indicating the categories that are registered on the specified computer.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is invalid.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x0600140B RID: 5131 RVA: 0x00052BB4 File Offset: 0x00050DB4
		public static PerformanceCounterCategory[] GetCategories(string machineName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if (!PerformanceCounterCategory.IsValidMachine(machineName))
			{
				return Array.Empty<PerformanceCounterCategory>();
			}
			string[] categoryNames = PerformanceCounterCategory.GetCategoryNames();
			PerformanceCounterCategory[] array = new PerformanceCounterCategory[categoryNames.Length];
			for (int i = 0; i < categoryNames.Length; i++)
			{
				array[i] = new PerformanceCounterCategory(categoryNames[i], machineName);
			}
			return array;
		}

		/// <summary>Retrieves a list of the counters in a performance counter category that contains exactly one instance.</summary>
		/// <returns>An array of <see cref="T:System.Diagnostics.PerformanceCounter" /> objects indicating the counters that are associated with this single-instance performance counter category.</returns>
		/// <exception cref="T:System.ArgumentException">The category is not a single instance.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The category does not have an associated instance.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x0600140C RID: 5132 RVA: 0x00052C07 File Offset: 0x00050E07
		public PerformanceCounter[] GetCounters()
		{
			return this.GetCounters("");
		}

		/// <summary>Retrieves a list of the counters in a performance counter category that contains one or more instances.</summary>
		/// <param name="instanceName">The performance object instance for which to retrieve the list of associated counters.</param>
		/// <returns>An array of <see cref="T:System.Diagnostics.PerformanceCounter" /> objects indicating the counters that are associated with the specified object instance of this performance counter category.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property for this <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> instance has not been set.  
		///  -or-  
		///  The category does not contain the instance that is specified by the <paramref name="instanceName" /> parameter.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x0600140D RID: 5133 RVA: 0x00052C14 File Offset: 0x00050E14
		public PerformanceCounter[] GetCounters(string instanceName)
		{
			if (!PerformanceCounterCategory.IsValidMachine(this.machineName))
			{
				return Array.Empty<PerformanceCounter>();
			}
			string[] counterNames = PerformanceCounterCategory.GetCounterNames(this.categoryName);
			PerformanceCounter[] array = new PerformanceCounter[counterNames.Length];
			for (int i = 0; i < counterNames.Length; i++)
			{
				array[i] = new PerformanceCounter(this.categoryName, counterNames[i], instanceName, this.machineName);
			}
			return array;
		}

		/// <summary>Retrieves the list of performance object instances that are associated with this category.</summary>
		/// <returns>An array of strings representing the performance object instance names that are associated with this category or, if the category contains only one performance object instance, a single-entry array that contains an empty string ("").</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property is <see langword="null" />. The property might not have been set.  
		///  -or-  
		///  The category does not have an associated instance.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x0600140E RID: 5134 RVA: 0x00052C70 File Offset: 0x00050E70
		public string[] GetInstanceNames()
		{
			if (!PerformanceCounterCategory.IsValidMachine(this.machineName))
			{
				return Array.Empty<string>();
			}
			return PerformanceCounterCategory.GetInstanceNames(this.categoryName);
		}

		/// <summary>Determines whether the specified performance object instance exists in the category that is identified by this <see cref="T:System.Diagnostics.PerformanceCounterCategory" /> object's <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property.</summary>
		/// <param name="instanceName">The performance object instance in this performance counter category to search for.</param>
		/// <returns>
		///   <see langword="true" /> if the category contains the specified performance object instance; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property is <see langword="null" />. The property might not have been set.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x0600140F RID: 5135 RVA: 0x00052C90 File Offset: 0x00050E90
		public bool InstanceExists(string instanceName)
		{
			return PerformanceCounterCategory.InstanceExists(instanceName, this.categoryName, this.machineName);
		}

		/// <summary>Determines whether a specified category on the local computer contains the specified performance object instance.</summary>
		/// <param name="instanceName">The performance object instance to search for.</param>
		/// <param name="categoryName">The performance counter category to search.</param>
		/// <returns>
		///   <see langword="true" /> if the category contains the specified performance object instance; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="categoryName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> parameter is an empty string ("").</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06001410 RID: 5136 RVA: 0x00052CA4 File Offset: 0x00050EA4
		public static bool InstanceExists(string instanceName, string categoryName)
		{
			return PerformanceCounterCategory.InstanceExists(instanceName, categoryName, ".");
		}

		/// <summary>Determines whether a specified category on a specified computer contains the specified performance object instance.</summary>
		/// <param name="instanceName">The performance object instance to search for.</param>
		/// <param name="categoryName">The performance counter category to search.</param>
		/// <param name="machineName">The name of the computer on which to look for the category instance pair.</param>
		/// <returns>
		///   <see langword="true" /> if the category contains the specified performance object instance; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="categoryName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="categoryName" /> parameter is an empty string ("").  
		///  -or-  
		///  The <paramref name="machineName" /> parameter is invalid.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06001411 RID: 5137 RVA: 0x00052CB2 File Offset: 0x00050EB2
		public static bool InstanceExists(string instanceName, string categoryName, string machineName)
		{
			if (instanceName == null)
			{
				throw new ArgumentNullException("instanceName");
			}
			PerformanceCounterCategory.CheckCategory(categoryName);
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			return PerformanceCounterCategory.InstanceExistsInternal(instanceName, categoryName);
		}

		/// <summary>Reads all the counter and performance object instance data that is associated with this performance counter category.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.InstanceDataCollectionCollection" /> that contains the counter and performance object instance data for the category.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.PerformanceCounterCategory.CategoryName" /> property is <see langword="null" />. The property might not have been set.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A call to an underlying system API failed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06001412 RID: 5138 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public InstanceDataCollectionCollection ReadCategory()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000B19 RID: 2841
		private string categoryName;

		// Token: 0x04000B1A RID: 2842
		private string machineName;

		// Token: 0x04000B1B RID: 2843
		private PerformanceCounterCategoryType type = PerformanceCounterCategoryType.Unknown;
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides the ability to create events for event tracing for Windows (ETW).</summary>
	// Token: 0x020009F7 RID: 2551
	public class EventSource : IDisposable
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class.</summary>
		// Token: 0x06005AD2 RID: 23250 RVA: 0x0013452D File Offset: 0x0013272D
		protected EventSource()
		{
			this.Name = base.GetType().Name;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class and specifies whether to throw an exception when an error occurs in the underlying Windows code.</summary>
		/// <param name="throwOnEventWriteErrors">
		///   <see langword="true" /> to throw an exception when an error occurs in the underlying Windows code; otherwise, <see langword="false" />.</param>
		// Token: 0x06005AD3 RID: 23251 RVA: 0x0007B584 File Offset: 0x00079784
		protected EventSource(bool throwOnEventWriteErrors) : this()
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class with the specified configuration settings.</summary>
		/// <param name="settings">A bitwise combination of the enumeration values that specify the configuration settings to apply to the event source.</param>
		// Token: 0x06005AD4 RID: 23252 RVA: 0x00134546 File Offset: 0x00132746
		protected EventSource(EventSourceSettings settings) : this()
		{
			this.Settings = settings;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> to be used with non-contract events that contains the specified settings and traits.</summary>
		/// <param name="settings">A bitwise combination of the enumeration values that specify the configuration settings to apply to the event source.</param>
		/// <param name="traits">The key-value pairs that specify traits for the event source.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="traits" /> is not specified in key-value pairs.</exception>
		// Token: 0x06005AD5 RID: 23253 RVA: 0x00134555 File Offset: 0x00132755
		protected EventSource(EventSourceSettings settings, params string[] traits) : this(settings)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class with the specified name.</summary>
		/// <param name="eventSourceName">The name to apply to the event source. Must not be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eventSourceName" /> is <see langword="null" />.</exception>
		// Token: 0x06005AD6 RID: 23254 RVA: 0x0013455E File Offset: 0x0013275E
		public EventSource(string eventSourceName)
		{
			this.Name = eventSourceName;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class with the specified name and settings.</summary>
		/// <param name="eventSourceName">The name to apply to the event source. Must not be <see langword="null" />.</param>
		/// <param name="config">A bitwise combination of the enumeration values that specify the configuration settings to apply to the event source.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eventSourceName" /> is <see langword="null" />.</exception>
		// Token: 0x06005AD7 RID: 23255 RVA: 0x0013456D File Offset: 0x0013276D
		public EventSource(string eventSourceName, EventSourceSettings config) : this(eventSourceName)
		{
			this.Settings = config;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class with the specified configuration settings.</summary>
		/// <param name="eventSourceName">The name to apply to the event source. Must not be <see langword="null" />.</param>
		/// <param name="config">A bitwise combination of the enumeration values that specify the configuration settings to apply to the event source.</param>
		/// <param name="traits">The key-value pairs that specify traits for the event source.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eventSourceName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="traits" /> is not specified in key-value pairs.</exception>
		// Token: 0x06005AD8 RID: 23256 RVA: 0x0013457D File Offset: 0x0013277D
		public EventSource(string eventSourceName, EventSourceSettings config, params string[] traits) : this(eventSourceName, config)
		{
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x00134587 File Offset: 0x00132787
		internal EventSource(Guid eventSourceGuid, string eventSourceName) : this(eventSourceName)
		{
		}

		/// <summary>Allows the <see cref="T:System.Diagnostics.Tracing.EventSource" /> object to attempt to free resources and perform other cleanup operations before the  object is reclaimed by garbage collection.</summary>
		// Token: 0x06005ADA RID: 23258 RVA: 0x00134590 File Offset: 0x00132790
		~EventSource()
		{
			this.Dispose(false);
		}

		/// <summary>Gets any exception that was thrown during the construction of the event source.</summary>
		/// <returns>The exception that was thrown during the construction of the event source, or <see langword="null" /> if no exception was thrown.</returns>
		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06005ADB RID: 23259 RVA: 0x0000AF5E File Offset: 0x0000915E
		public Exception ConstructionException
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the activity ID of the current thread.</summary>
		/// <returns>The activity ID of the current thread.</returns>
		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06005ADC RID: 23260 RVA: 0x001345C0 File Offset: 0x001327C0
		public static Guid CurrentThreadActivityId
		{
			get
			{
				return Guid.Empty;
			}
		}

		/// <summary>The unique identifier for the event source.</summary>
		/// <returns>A unique identifier for the event source.</returns>
		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06005ADD RID: 23261 RVA: 0x001345C0 File Offset: 0x001327C0
		public Guid Guid
		{
			get
			{
				return Guid.Empty;
			}
		}

		/// <summary>The friendly name of the class that is derived from the event source.</summary>
		/// <returns>The friendly name of the derived class.  The default is the simple name of the class.</returns>
		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06005ADE RID: 23262 RVA: 0x001345C7 File Offset: 0x001327C7
		// (set) Token: 0x06005ADF RID: 23263 RVA: 0x001345CF File Offset: 0x001327CF
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		/// <summary>Gets the settings applied to this event source.</summary>
		/// <returns>The settings applied to this event source.</returns>
		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06005AE0 RID: 23264 RVA: 0x001345D8 File Offset: 0x001327D8
		// (set) Token: 0x06005AE1 RID: 23265 RVA: 0x001345E0 File Offset: 0x001327E0
		public EventSourceSettings Settings
		{
			[CompilerGenerated]
			get
			{
				return this.<Settings>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Settings>k__BackingField = value;
			}
		}

		/// <summary>Determines whether the current event source is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the current event source is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005AE2 RID: 23266 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsEnabled()
		{
			return false;
		}

		/// <summary>Determines whether the current event source that has the specified level and keyword is enabled.</summary>
		/// <param name="level">The level of the event source.</param>
		/// <param name="keywords">The keyword of the event source.</param>
		/// <returns>
		///   <see langword="true" /> if the event source is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005AE3 RID: 23267 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsEnabled(EventLevel level, EventKeywords keywords)
		{
			return false;
		}

		/// <summary>Determines whether the current event source is enabled for events with the specified level, keywords and channel.</summary>
		/// <param name="level">The event level to check. An event source will be considered enabled when its level is greater than or equal to <paramref name="level" />.</param>
		/// <param name="keywords">The event keywords to check.</param>
		/// <param name="channel">The event channel to check.</param>
		/// <returns>
		///   <see langword="true" /> if the event source is enabled for the specified event level, keywords and channel; otherwise, <see langword="false" />.  
		/// The result of this method is only an approximation of whether a particular event is active.  Use it to avoid expensive computation for logging when logging is disabled. Event sources may have additional filtering that determines their activity.</returns>
		// Token: 0x06005AE4 RID: 23268 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsEnabled(EventLevel level, EventKeywords keywords, EventChannel channel)
		{
			return false;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class.</summary>
		// Token: 0x06005AE5 RID: 23269 RVA: 0x001345E9 File Offset: 0x001327E9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets the trait value associated with the specified key.</summary>
		/// <param name="key">The key of the trait to get.</param>
		/// <returns>The trait value associated with the specified key. If the key is not found, returns <see langword="null" />.</returns>
		// Token: 0x06005AE6 RID: 23270 RVA: 0x0000AF5E File Offset: 0x0000915E
		public string GetTrait(string key)
		{
			return null;
		}

		/// <summary>Writes an event without fields, but with the specified name and default options.</summary>
		/// <param name="eventName">The name of the event to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eventName" /> is <see langword="null" />.</exception>
		// Token: 0x06005AE7 RID: 23271 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Write(string eventName)
		{
		}

		/// <summary>Writes an event without fields, but with the specified name and options.</summary>
		/// <param name="eventName">The name of the event to write.</param>
		/// <param name="options">The options such as level, keywords and operation code for the event.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eventName" /> is <see langword="null" />.</exception>
		// Token: 0x06005AE8 RID: 23272 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Write(string eventName, EventSourceOptions options)
		{
		}

		/// <summary>Writes an event with the specified name and data.</summary>
		/// <param name="eventName">The name of the event.</param>
		/// <param name="data">The event data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" /> attribute.</param>
		/// <typeparam name="T">The type that defines the event and its associated data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" /> attribute.</typeparam>
		// Token: 0x06005AE9 RID: 23273 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Write<T>(string eventName, T data)
		{
		}

		/// <summary>Writes an event with the specified name, event data and options.</summary>
		/// <param name="eventName">The name of the event.</param>
		/// <param name="options">The event options.</param>
		/// <param name="data">The event data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" /> attribute.</param>
		/// <typeparam name="T">The type that defines the event and its associated data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" /> attribute.</typeparam>
		// Token: 0x06005AEA RID: 23274 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Write<T>(string eventName, EventSourceOptions options, T data)
		{
		}

		/// <summary>Writes an event with the specified name, options and event data.</summary>
		/// <param name="eventName">The name of the event.</param>
		/// <param name="options">The event options.</param>
		/// <param name="data">The event data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" /> attribute.</param>
		/// <typeparam name="T">The type that defines the event and its associated data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" /> attribute.</typeparam>
		// Token: 0x06005AEB RID: 23275 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[CLSCompliant(false)]
		public void Write<T>(string eventName, ref EventSourceOptions options, ref T data)
		{
		}

		/// <summary>Writes an event with the specified name, options, related activity and event data.</summary>
		/// <param name="eventName">The name of the event.</param>
		/// <param name="options">The event options.</param>
		/// <param name="activityId">The ID of the activity associated with the event.</param>
		/// <param name="relatedActivityId">The ID of an associated activity, or <see cref="F:System.Guid.Empty" /> if there is no associated activity.</param>
		/// <param name="data">The event data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" /> attribute.</param>
		/// <typeparam name="T">The type that defines the event and its associated data. This type must be an anonymous type or marked with the <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" /> attribute.</typeparam>
		// Token: 0x06005AEC RID: 23276 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Write<T>(string eventName, ref EventSourceOptions options, ref Guid activityId, ref Guid relatedActivityId, ref T data)
		{
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06005AED RID: 23277 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Called when the current event source is updated by the controller.</summary>
		/// <param name="command">The arguments for the event.</param>
		// Token: 0x06005AEE RID: 23278 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected virtual void OnEventCommand(EventCommandEventArgs command)
		{
		}

		// Token: 0x06005AEF RID: 23279 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal void ReportOutOfBandMessage(string msg, bool flush)
		{
		}

		/// <summary>Writes an event by using the provided event identifier.</summary>
		/// <param name="eventId">The event identifier. This value should be between 0 and 65535.</param>
		// Token: 0x06005AF0 RID: 23280 RVA: 0x001345F8 File Offset: 0x001327F8
		protected void WriteEvent(int eventId)
		{
			this.WriteEvent(eventId, new object[0]);
		}

		/// <summary>Writes an event by using the provided event identifier and byte array argument.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A byte array argument.</param>
		// Token: 0x06005AF1 RID: 23281 RVA: 0x00134607 File Offset: 0x00132807
		protected void WriteEvent(int eventId, byte[] arg1)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1
			});
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer argument.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">An integer argument.</param>
		// Token: 0x06005AF2 RID: 23282 RVA: 0x0013461A File Offset: 0x0013281A
		protected void WriteEvent(int eventId, int arg1)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1
			});
		}

		/// <summary>Writes an event by using the provided event identifier and string argument.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A string argument.</param>
		// Token: 0x06005AF3 RID: 23283 RVA: 0x00134607 File Offset: 0x00132807
		protected void WriteEvent(int eventId, string arg1)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1
			});
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">An integer argument.</param>
		/// <param name="arg2">An integer argument.</param>
		// Token: 0x06005AF4 RID: 23284 RVA: 0x00134632 File Offset: 0x00132832
		protected void WriteEvent(int eventId, int arg1, int arg2)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1,
				arg2
			});
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">An integer argument.</param>
		/// <param name="arg2">An integer argument.</param>
		/// <param name="arg3">An integer argument.</param>
		// Token: 0x06005AF5 RID: 23285 RVA: 0x00134653 File Offset: 0x00132853
		protected void WriteEvent(int eventId, int arg1, int arg2, int arg3)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1,
				arg2,
				arg3
			});
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer and string arguments.</summary>
		/// <param name="eventId">The event identifier. This value should be between 0 and 65535.</param>
		/// <param name="arg1">A 32-bit integer argument.</param>
		/// <param name="arg2">A string argument.</param>
		// Token: 0x06005AF6 RID: 23286 RVA: 0x0013467E File Offset: 0x0013287E
		protected void WriteEvent(int eventId, int arg1, string arg2)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1,
				arg2
			});
		}

		/// <summary>Writes an event by using the provided event identifier and 64-bit integer argument.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A 64 bit integer argument.</param>
		// Token: 0x06005AF7 RID: 23287 RVA: 0x0013469A File Offset: 0x0013289A
		protected void WriteEvent(int eventId, long arg1)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1
			});
		}

		/// <summary>Writes the event data using the specified indentifier and 64-bit integer and byte array arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A 64-bit integer argument.</param>
		/// <param name="arg2">A byte array argument.</param>
		// Token: 0x06005AF8 RID: 23288 RVA: 0x001346B2 File Offset: 0x001328B2
		protected void WriteEvent(int eventId, long arg1, byte[] arg2)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1,
				arg2
			});
		}

		/// <summary>Writes an event by using the provided event identifier and 64-bit arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A 64 bit integer argument.</param>
		/// <param name="arg2">A 64 bit integer argument.</param>
		// Token: 0x06005AF9 RID: 23289 RVA: 0x001346CE File Offset: 0x001328CE
		protected void WriteEvent(int eventId, long arg1, long arg2)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1,
				arg2
			});
		}

		/// <summary>Writes an event by using the provided event identifier and 64-bit arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A 64 bit integer argument.</param>
		/// <param name="arg2">A 64 bit integer argument.</param>
		/// <param name="arg3">A 64 bit integer argument.</param>
		// Token: 0x06005AFA RID: 23290 RVA: 0x001346EF File Offset: 0x001328EF
		protected void WriteEvent(int eventId, long arg1, long arg2, long arg3)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1,
				arg2,
				arg3
			});
		}

		/// <summary>Writes an event by using the provided event identifier and 64-bit integer, and string arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A 64-bit integer argument.</param>
		/// <param name="arg2">A string argument.</param>
		// Token: 0x06005AFB RID: 23291 RVA: 0x001346B2 File Offset: 0x001328B2
		protected void WriteEvent(int eventId, long arg1, string arg2)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1,
				arg2
			});
		}

		/// <summary>Writes an event by using the provided event identifier and array of arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="args">An array of objects.</param>
		// Token: 0x06005AFC RID: 23292 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected void WriteEvent(int eventId, params object[] args)
		{
		}

		/// <summary>Writes an event by using the provided event identifier and arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A 32 bit integer argument.</param>
		// Token: 0x06005AFD RID: 23293 RVA: 0x0013471A File Offset: 0x0013291A
		protected void WriteEvent(int eventId, string arg1, int arg2)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1,
				arg2
			});
		}

		/// <summary>Writes an event by using the provided event identifier and arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A 32 bit integer argument.</param>
		/// <param name="arg3">A 32 bit integer argument.</param>
		// Token: 0x06005AFE RID: 23294 RVA: 0x00134736 File Offset: 0x00132936
		protected void WriteEvent(int eventId, string arg1, int arg2, int arg3)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1,
				arg2,
				arg3
			});
		}

		/// <summary>Writes an event by using the provided event identifier and arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A 64 bit integer argument.</param>
		// Token: 0x06005AFF RID: 23295 RVA: 0x0013475C File Offset: 0x0013295C
		protected void WriteEvent(int eventId, string arg1, long arg2)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1,
				arg2
			});
		}

		/// <summary>Writes an event by using the provided event identifier and string arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A string argument.</param>
		// Token: 0x06005B00 RID: 23296 RVA: 0x00134778 File Offset: 0x00132978
		protected void WriteEvent(int eventId, string arg1, string arg2)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1,
				arg2
			});
		}

		/// <summary>Writes an event by using the provided event identifier and string arguments.</summary>
		/// <param name="eventId">The event identifier.  This value should be between 0 and 65535.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A string argument.</param>
		/// <param name="arg3">A string argument.</param>
		// Token: 0x06005B01 RID: 23297 RVA: 0x0013478F File Offset: 0x0013298F
		protected void WriteEvent(int eventId, string arg1, string arg2, string arg3)
		{
			this.WriteEvent(eventId, new object[]
			{
				arg1,
				arg2,
				arg3
			});
		}

		/// <summary>Creates a new <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overload by using the provided event identifier and event data.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="eventDataCount">The number of event data items.</param>
		/// <param name="data">The structure that contains the event data.</param>
		// Token: 0x06005B02 RID: 23298 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[CLSCompliant(false)]
		protected unsafe void WriteEventCore(int eventId, int eventDataCount, EventSource.EventData* data)
		{
		}

		/// <summary>Writes an event that indicates that the current activity is related to another activity.</summary>
		/// <param name="eventId">An identifier that uniquely identifies this event within the <see cref="T:System.Diagnostics.Tracing.EventSource" />.</param>
		/// <param name="relatedActivityId">The related activity identifier.</param>
		/// <param name="args">An array of objects that contain data about the event.</param>
		// Token: 0x06005B03 RID: 23299 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected void WriteEventWithRelatedActivityId(int eventId, Guid relatedActivityId, params object[] args)
		{
		}

		/// <summary>Writes an event that indicates that the current activity is related to another activity.</summary>
		/// <param name="eventId">An identifier that uniquely identifies this event within the <see cref="T:System.Diagnostics.Tracing.EventSource" />.</param>
		/// <param name="relatedActivityId">A pointer to the GUID of the related activity ID.</param>
		/// <param name="eventDataCount">The number of items in the <paramref name="data" /> field.</param>
		/// <param name="data">A pointer to the first item in the event data field.</param>
		// Token: 0x06005B04 RID: 23300 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[CLSCompliant(false)]
		protected unsafe void WriteEventWithRelatedActivityIdCore(int eventId, Guid* relatedActivityId, int eventDataCount, EventSource.EventData* data)
		{
		}

		/// <summary>Occurs when a command comes from an event listener.</summary>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06005B05 RID: 23301 RVA: 0x000479F8 File Offset: 0x00045BF8
		// (remove) Token: 0x06005B06 RID: 23302 RVA: 0x000479F8 File Offset: 0x00045BF8
		public event EventHandler<EventCommandEventArgs> EventCommandExecuted
		{
			add
			{
				throw new NotImplementedException();
			}
			remove
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns a string of the XML manifest that is associated with the current event source.</summary>
		/// <param name="eventSourceType">The type of the event source.</param>
		/// <param name="assemblyPathToIncludeInManifest">The path to the assembly file (.dll) to include in the provider element of the manifest.</param>
		/// <returns>The XML data string.</returns>
		// Token: 0x06005B07 RID: 23303 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a string of the XML manifest that is associated with the current event source.</summary>
		/// <param name="eventSourceType">The type of the event source.</param>
		/// <param name="assemblyPathToIncludeInManifest">The path to the assembly file (.dll) file to include in the provider element of the manifest.</param>
		/// <param name="flags">A bitwise combination of the enumeration values that specify how the manifest is generated.</param>
		/// <returns>The XML data string or <see langword="null" />.</returns>
		// Token: 0x06005B08 RID: 23304 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest, EventManifestOptions flags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the unique identifier for this implementation of the event source.</summary>
		/// <param name="eventSourceType">The type of the event source.</param>
		/// <returns>A unique identifier for this event source type.</returns>
		// Token: 0x06005B09 RID: 23305 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static Guid GetGuid(Type eventSourceType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the friendly name of the event source.</summary>
		/// <param name="eventSourceType">The type of the event source.</param>
		/// <returns>The friendly name of the event source. The default is the simple name of the class.</returns>
		// Token: 0x06005B0A RID: 23306 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static string GetName(Type eventSourceType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a snapshot of all the event sources for the application domain.</summary>
		/// <returns>An enumeration of all the event sources in the application domain.</returns>
		// Token: 0x06005B0B RID: 23307 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static IEnumerable<EventSource> GetSources()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends a command to a specified event source.</summary>
		/// <param name="eventSource">The event source to send the command to.</param>
		/// <param name="command">The event command to send.</param>
		/// <param name="commandArguments">The arguments for the event command.</param>
		// Token: 0x06005B0C RID: 23308 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static void SendCommand(EventSource eventSource, EventCommand command, IDictionary<string, string> commandArguments)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the activity ID on the current thread.</summary>
		/// <param name="activityId">The current thread's new activity ID, or <see cref="F:System.Guid.Empty" /> to indicate that work on the current thread is not associated with any activity.</param>
		// Token: 0x06005B0D RID: 23309 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static void SetCurrentThreadActivityId(Guid activityId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the activity ID on the current thread, and returns the previous activity ID.</summary>
		/// <param name="activityId">The current thread's new activity ID, or <see cref="F:System.Guid.Empty" /> to indicate that work on the current thread is not associated with any activity.</param>
		/// <param name="oldActivityThatWillContinue">When this method returns, contains the previous activity ID on the current thread.</param>
		// Token: 0x06005B0E RID: 23310 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static void SetCurrentThreadActivityId(Guid activityId, out Guid oldActivityThatWillContinue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400382F RID: 14383
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04003830 RID: 14384
		[CompilerGenerated]
		private EventSourceSettings <Settings>k__BackingField;

		/// <summary>Provides the event data for creating fast <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overloads by using the <see cref="M:System.Diagnostics.Tracing.EventSource.WriteEventCore(System.Int32,System.Int32,System.Diagnostics.Tracing.EventSource.EventData*)" /> method.</summary>
		// Token: 0x020009F8 RID: 2552
		protected internal struct EventData
		{
			/// <summary>Gets or sets the pointer to the data for the new <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overload.</summary>
			/// <returns>The pointer to the data.</returns>
			// Token: 0x17000F93 RID: 3987
			// (get) Token: 0x06005B0F RID: 23311 RVA: 0x001347AB File Offset: 0x001329AB
			// (set) Token: 0x06005B10 RID: 23312 RVA: 0x001347B3 File Offset: 0x001329B3
			public IntPtr DataPointer
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<DataPointer>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<DataPointer>k__BackingField = value;
				}
			}

			/// <summary>Gets or sets the number of payload items in the new <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overload.</summary>
			/// <returns>The number of payload items in the new overload.</returns>
			// Token: 0x17000F94 RID: 3988
			// (get) Token: 0x06005B11 RID: 23313 RVA: 0x001347BC File Offset: 0x001329BC
			// (set) Token: 0x06005B12 RID: 23314 RVA: 0x001347C4 File Offset: 0x001329C4
			public int Size
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<Size>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Size>k__BackingField = value;
				}
			}

			// Token: 0x17000F95 RID: 3989
			// (get) Token: 0x06005B13 RID: 23315 RVA: 0x001347CD File Offset: 0x001329CD
			// (set) Token: 0x06005B14 RID: 23316 RVA: 0x001347D5 File Offset: 0x001329D5
			internal int Reserved
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<Reserved>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Reserved>k__BackingField = value;
				}
			}

			// Token: 0x04003831 RID: 14385
			[CompilerGenerated]
			private IntPtr <DataPointer>k__BackingField;

			// Token: 0x04003832 RID: 14386
			[CompilerGenerated]
			private int <Size>k__BackingField;

			// Token: 0x04003833 RID: 14387
			[CompilerGenerated]
			private int <Reserved>k__BackingField;
		}
	}
}

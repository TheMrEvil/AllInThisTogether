using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security;
using System.ServiceModel.Internals;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000043 RID: 67
	internal sealed class EtwDiagnosticTrace : DiagnosticTraceBase
	{
		// Token: 0x06000269 RID: 617 RVA: 0x0000A330 File Offset: 0x00008530
		[SecurityCritical]
		static EtwDiagnosticTrace()
		{
			if (!PartialTrustHelpers.HasEtwPermissions())
			{
				EtwDiagnosticTrace.defaultEtwProviderId = Guid.Empty;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A3AC File Offset: 0x000085AC
		[SecurityCritical]
		public EtwDiagnosticTrace(string traceSourceName, Guid etwProviderId) : base(traceSourceName)
		{
			try
			{
				this.TraceSourceName = traceSourceName;
				base.EventSourceName = this.TraceSourceName + " " + "4.0.0.0";
				this.CreateTraceSource();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				new EventLogger(base.EventSourceName, null).LogEvent(TraceEventType.Error, 4, 3221291108U, false, new string[]
				{
					ex.ToString()
				});
			}
			try
			{
				this.CreateEtwProvider(etwProviderId);
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				this.etwProvider = null;
				new EventLogger(base.EventSourceName, null).LogEvent(TraceEventType.Error, 4, 3221291108U, false, new string[]
				{
					ex2.ToString()
				});
			}
			if (base.TracingEnabled || this.EtwTracingEnabled)
			{
				base.AddDomainEventHandlersForCleanup();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000A498 File Offset: 0x00008698
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000A49F File Offset: 0x0000869F
		public static Guid DefaultEtwProviderId
		{
			[SecuritySafeCritical]
			get
			{
				return EtwDiagnosticTrace.defaultEtwProviderId;
			}
			[SecurityCritical]
			set
			{
				EtwDiagnosticTrace.defaultEtwProviderId = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000A4A7 File Offset: 0x000086A7
		public EtwProvider EtwProvider
		{
			[SecurityCritical]
			get
			{
				return this.etwProvider;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000A4AF File Offset: 0x000086AF
		public bool IsEtwProviderEnabled
		{
			[SecuritySafeCritical]
			get
			{
				return this.EtwTracingEnabled && this.etwProvider.IsEnabled();
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000A4C6 File Offset: 0x000086C6
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000A4D3 File Offset: 0x000086D3
		public Action RefreshState
		{
			[SecuritySafeCritical]
			get
			{
				return this.EtwProvider.ControllerCallBack;
			}
			[SecuritySafeCritical]
			set
			{
				this.EtwProvider.ControllerCallBack = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000A4E1 File Offset: 0x000086E1
		public bool IsEnd2EndActivityTracingEnabled
		{
			[SecuritySafeCritical]
			get
			{
				return this.IsEtwProviderEnabled && this.EtwProvider.IsEnd2EndActivityTracingEnabled;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000A4F8 File Offset: 0x000086F8
		private bool EtwTracingEnabled
		{
			[SecuritySafeCritical]
			get
			{
				return this.etwProvider != null;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A503 File Offset: 0x00008703
		[SecuritySafeCritical]
		public void SetEnd2EndActivityTracingEnabled(bool isEnd2EndTracingEnabled)
		{
			this.EtwProvider.SetEnd2EndActivityTracingEnabled(isEnd2EndTracingEnabled);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A511 File Offset: 0x00008711
		public void SetAnnotation(Func<string> annotation)
		{
			EtwDiagnosticTrace.traceAnnotation = annotation;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A519 File Offset: 0x00008719
		public override bool ShouldTrace(TraceEventLevel level)
		{
			return base.ShouldTrace(level) || this.ShouldTraceToEtw(level);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A52D File Offset: 0x0000872D
		[SecuritySafeCritical]
		public bool ShouldTraceToEtw(TraceEventLevel level)
		{
			return this.EtwProvider != null && this.EtwProvider.IsEnabled((byte)level, 0L);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A548 File Offset: 0x00008748
		[SecuritySafeCritical]
		public void Event(int eventId, TraceEventLevel traceEventLevel, TraceChannel channel, string description)
		{
			if (base.TracingEnabled)
			{
				EventDescriptor eventDescriptor = EtwDiagnosticTrace.GetEventDescriptor(eventId, channel, traceEventLevel);
				this.Event(ref eventDescriptor, description);
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A570 File Offset: 0x00008770
		[SecurityCritical]
		public void Event(ref EventDescriptor eventDescriptor, string description)
		{
			if (base.TracingEnabled)
			{
				TracePayload serializedPayload = this.GetSerializedPayload(null, null, null);
				this.WriteTraceSource(ref eventDescriptor, description, serializedPayload);
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A598 File Offset: 0x00008798
		public void SetAndTraceTransfer(Guid newId, bool emitTransfer)
		{
			if (emitTransfer)
			{
				this.TraceTransfer(newId);
			}
			DiagnosticTraceBase.ActivityId = newId;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A5AC File Offset: 0x000087AC
		[SecuritySafeCritical]
		public void TraceTransfer(Guid newId)
		{
			Guid activityId = DiagnosticTraceBase.ActivityId;
			if (newId != activityId)
			{
				try
				{
					bool haveListeners = base.HaveListeners;
					if (this.IsEtwEventEnabled(ref EtwDiagnosticTrace.transferEventDescriptor, false))
					{
						this.etwProvider.WriteTransferEvent(ref EtwDiagnosticTrace.transferEventDescriptor, new EventTraceActivity(activityId, false), newId, (EtwDiagnosticTrace.traceAnnotation == null) ? string.Empty : EtwDiagnosticTrace.traceAnnotation(), DiagnosticTraceBase.AppDomainFriendlyName);
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					base.LogTraceFailure(null, exception);
				}
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A63C File Offset: 0x0000883C
		[SecurityCritical]
		public void WriteTraceSource(ref EventDescriptor eventDescriptor, string description, TracePayload payload)
		{
			if (base.TracingEnabled)
			{
				XPathNavigator xpathNavigator = null;
				try
				{
					string msdnTraceCode;
					int num;
					EtwDiagnosticTrace.GenerateLegacyTraceCode(ref eventDescriptor, out msdnTraceCode, out num);
					string xml = EtwDiagnosticTrace.BuildTrace(ref eventDescriptor, description, payload, msdnTraceCode);
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(xml);
					xpathNavigator = xmlDocument.CreateNavigator();
					if (base.CalledShutdown)
					{
						base.TraceSource.Flush();
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					base.LogTraceFailure((xpathNavigator == null) ? string.Empty : xpathNavigator.ToString(), exception);
				}
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A6C8 File Offset: 0x000088C8
		[SecurityCritical]
		private static string BuildTrace(ref EventDescriptor eventDescriptor, string description, TracePayload payload, string msdnTraceCode)
		{
			StringBuilder stringBuilder = EtwDiagnosticTrace.StringBuilderPool.Take();
			string result;
			try
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.CurrentCulture))
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
					{
						xmlTextWriter.WriteStartElement("TraceRecord");
						xmlTextWriter.WriteAttributeString("xmlns", "http://schemas.microsoft.com/2004/10/E2ETraceEvent/TraceRecord");
						xmlTextWriter.WriteAttributeString("Severity", TraceLevelHelper.LookupSeverity((TraceEventLevel)eventDescriptor.Level, (TraceEventOpcode)eventDescriptor.Opcode));
						xmlTextWriter.WriteAttributeString("Channel", EtwDiagnosticTrace.LookupChannel((TraceChannel)eventDescriptor.Channel));
						xmlTextWriter.WriteElementString("TraceIdentifier", msdnTraceCode);
						xmlTextWriter.WriteElementString("Description", description);
						xmlTextWriter.WriteElementString("AppDomain", payload.AppDomainFriendlyName);
						if (!string.IsNullOrEmpty(payload.EventSource))
						{
							xmlTextWriter.WriteElementString("Source", payload.EventSource);
						}
						if (!string.IsNullOrEmpty(payload.ExtendedData))
						{
							xmlTextWriter.WriteRaw(payload.ExtendedData);
						}
						if (!string.IsNullOrEmpty(payload.SerializedException))
						{
							xmlTextWriter.WriteRaw(payload.SerializedException);
						}
						xmlTextWriter.WriteEndElement();
						xmlTextWriter.Flush();
						stringWriter.Flush();
						result = stringBuilder.ToString();
					}
				}
			}
			finally
			{
				EtwDiagnosticTrace.StringBuilderPool.Return(stringBuilder);
			}
			return result;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A840 File Offset: 0x00008A40
		[SecurityCritical]
		private static void GenerateLegacyTraceCode(ref EventDescriptor eventDescriptor, out string msdnTraceCode, out int legacyEventId)
		{
			switch (eventDescriptor.EventId)
			{
			case 57393:
				msdnTraceCode = EtwDiagnosticTrace.GenerateMsdnTraceCode("System.ServiceModel.Diagnostics", "AppDomainUnload");
				legacyEventId = 131073;
				return;
			case 57394:
			case 57404:
			case 57405:
			case 57406:
				msdnTraceCode = EtwDiagnosticTrace.GenerateMsdnTraceCode("System.ServiceModel.Diagnostics", "TraceHandledException");
				legacyEventId = 131076;
				return;
			case 57396:
			case 57407:
				msdnTraceCode = EtwDiagnosticTrace.GenerateMsdnTraceCode("System.ServiceModel.Diagnostics", "ThrowingException");
				legacyEventId = 131075;
				return;
			case 57397:
				msdnTraceCode = EtwDiagnosticTrace.GenerateMsdnTraceCode("System.ServiceModel.Diagnostics", "UnhandledException");
				legacyEventId = 131077;
				return;
			}
			msdnTraceCode = eventDescriptor.EventId.ToString(CultureInfo.InvariantCulture);
			legacyEventId = eventDescriptor.EventId;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A91F File Offset: 0x00008B1F
		private static string GenerateMsdnTraceCode(string traceSource, string traceCodeString)
		{
			return string.Format(CultureInfo.InvariantCulture, "http://msdn.microsoft.com/{0}/library/{1}.{2}.aspx", CultureInfo.CurrentCulture.Name, traceSource, traceCodeString);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A93C File Offset: 0x00008B3C
		private static string LookupChannel(TraceChannel traceChannel)
		{
			string result;
			if (traceChannel != TraceChannel.Application)
			{
				switch (traceChannel)
				{
				case TraceChannel.Admin:
					result = "Admin";
					break;
				case TraceChannel.Operational:
					result = "Operational";
					break;
				case TraceChannel.Analytic:
					result = "Analytic";
					break;
				case TraceChannel.Debug:
					result = "Debug";
					break;
				case TraceChannel.Perf:
					result = "Perf";
					break;
				default:
					result = traceChannel.ToString();
					break;
				}
			}
			else
			{
				result = "Application";
			}
			return result;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A9AC File Offset: 0x00008BAC
		public TracePayload GetSerializedPayload(object source, TraceRecord traceRecord, Exception exception)
		{
			return this.GetSerializedPayload(source, traceRecord, exception, false);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A9B8 File Offset: 0x00008BB8
		public TracePayload GetSerializedPayload(object source, TraceRecord traceRecord, Exception exception, bool getServiceReference)
		{
			string eventSource = null;
			string extendedData = null;
			string serializedException = null;
			if (source != null)
			{
				eventSource = DiagnosticTraceBase.CreateSourceString(source);
			}
			if (traceRecord != null)
			{
				StringBuilder stringBuilder = EtwDiagnosticTrace.StringBuilderPool.Take();
				try
				{
					using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.CurrentCulture))
					{
						using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
						{
							xmlTextWriter.WriteStartElement("ExtendedData");
							traceRecord.WriteTo(xmlTextWriter);
							xmlTextWriter.WriteEndElement();
							xmlTextWriter.Flush();
							stringWriter.Flush();
							extendedData = stringBuilder.ToString();
						}
					}
				}
				finally
				{
					EtwDiagnosticTrace.StringBuilderPool.Return(stringBuilder);
				}
			}
			if (exception != null)
			{
				serializedException = EtwDiagnosticTrace.ExceptionToTraceString(exception, 28672);
			}
			if (getServiceReference && EtwDiagnosticTrace.traceAnnotation != null)
			{
				return new TracePayload(serializedException, eventSource, DiagnosticTraceBase.AppDomainFriendlyName, extendedData, EtwDiagnosticTrace.traceAnnotation());
			}
			return new TracePayload(serializedException, eventSource, DiagnosticTraceBase.AppDomainFriendlyName, extendedData, string.Empty);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000AAB4 File Offset: 0x00008CB4
		[SecuritySafeCritical]
		public bool IsEtwEventEnabled(ref EventDescriptor eventDescriptor)
		{
			return this.IsEtwEventEnabled(ref eventDescriptor, true);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000AABE File Offset: 0x00008CBE
		[SecuritySafeCritical]
		public bool IsEtwEventEnabled(ref EventDescriptor eventDescriptor, bool fullCheck)
		{
			if (fullCheck)
			{
				return this.EtwTracingEnabled && this.etwProvider.IsEventEnabled(ref eventDescriptor);
			}
			return this.EtwTracingEnabled && this.etwProvider.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000AAFB File Offset: 0x00008CFB
		[SecuritySafeCritical]
		private void CreateTraceSource()
		{
			if (!string.IsNullOrEmpty(this.TraceSourceName))
			{
				base.SetTraceSource(new DiagnosticTraceSource(this.TraceSourceName));
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000AB1C File Offset: 0x00008D1C
		[SecurityCritical]
		private void CreateEtwProvider(Guid etwProviderId)
		{
			if (etwProviderId != Guid.Empty && EtwDiagnosticTrace.isVistaOrGreater)
			{
				this.etwProvider = (EtwProvider)EtwDiagnosticTrace.etwProviderCache[etwProviderId];
				if (this.etwProvider == null)
				{
					Hashtable obj = EtwDiagnosticTrace.etwProviderCache;
					lock (obj)
					{
						this.etwProvider = (EtwProvider)EtwDiagnosticTrace.etwProviderCache[etwProviderId];
						if (this.etwProvider == null)
						{
							this.etwProvider = new EtwProvider(etwProviderId);
							EtwDiagnosticTrace.etwProviderCache.Add(etwProviderId, this.etwProvider);
						}
					}
				}
				this.etwProviderId = etwProviderId;
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		[SecurityCritical]
		private static EventDescriptor GetEventDescriptor(int eventId, TraceChannel channel, TraceEventLevel traceEventLevel)
		{
			long num = 0L;
			if (channel == TraceChannel.Admin)
			{
				num |= long.MinValue;
			}
			else if (channel == TraceChannel.Operational)
			{
				num |= 4611686018427387904L;
			}
			else if (channel == TraceChannel.Analytic)
			{
				num |= 2305843009213693952L;
			}
			else if (channel == TraceChannel.Debug)
			{
				num |= 72057594037927936L;
			}
			else if (channel == TraceChannel.Perf)
			{
				num |= 576460752303423488L;
			}
			return new EventDescriptor(eventId, 0, (byte)channel, (byte)traceEventLevel, 0, 0, num);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000AC5B File Offset: 0x00008E5B
		protected override void OnShutdownTracing()
		{
			this.ShutdownTraceSource();
			this.ShutdownEtwProvider();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000AC6C File Offset: 0x00008E6C
		private void ShutdownTraceSource()
		{
			try
			{
				if (TraceCore.AppDomainUnloadIsEnabled(this))
				{
					TraceCore.AppDomainUnload(this, AppDomain.CurrentDomain.FriendlyName, DiagnosticTraceBase.ProcessName, DiagnosticTraceBase.ProcessId.ToString(CultureInfo.CurrentCulture));
				}
				base.TraceSource.Flush();
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				base.LogTraceFailure(null, exception);
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000ACDC File Offset: 0x00008EDC
		[SecuritySafeCritical]
		private void ShutdownEtwProvider()
		{
			try
			{
				if (this.etwProvider != null)
				{
					this.etwProvider.Dispose();
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				base.LogTraceFailure(null, exception);
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000AD24 File Offset: 0x00008F24
		public override bool IsEnabled()
		{
			return TraceCore.TraceCodeEventLogCriticalIsEnabled(this) || TraceCore.TraceCodeEventLogVerboseIsEnabled(this) || TraceCore.TraceCodeEventLogInfoIsEnabled(this) || TraceCore.TraceCodeEventLogWarningIsEnabled(this) || TraceCore.TraceCodeEventLogErrorIsEnabled(this);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000AD50 File Offset: 0x00008F50
		public override void TraceEventLogEvent(TraceEventType type, TraceRecord traceRecord)
		{
			switch (type)
			{
			case TraceEventType.Critical:
				if (TraceCore.TraceCodeEventLogCriticalIsEnabled(this))
				{
					TraceCore.TraceCodeEventLogCritical(this, traceRecord);
					return;
				}
				break;
			case TraceEventType.Error:
				if (TraceCore.TraceCodeEventLogErrorIsEnabled(this))
				{
					TraceCore.TraceCodeEventLogError(this, traceRecord);
				}
				break;
			case (TraceEventType)3:
				break;
			case TraceEventType.Warning:
				if (TraceCore.TraceCodeEventLogWarningIsEnabled(this))
				{
					TraceCore.TraceCodeEventLogWarning(this, traceRecord);
					return;
				}
				break;
			default:
				if (type != TraceEventType.Information)
				{
					if (type != TraceEventType.Verbose)
					{
						return;
					}
					if (TraceCore.TraceCodeEventLogVerboseIsEnabled(this))
					{
						TraceCore.TraceCodeEventLogVerbose(this, traceRecord);
						return;
					}
				}
				else if (TraceCore.TraceCodeEventLogInfoIsEnabled(this))
				{
					TraceCore.TraceCodeEventLogInfo(this, traceRecord);
					return;
				}
				break;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000ADCE File Offset: 0x00008FCE
		protected override void OnUnhandledException(Exception exception)
		{
			if (TraceCore.UnhandledExceptionIsEnabled(this))
			{
				TraceCore.UnhandledException(this, (exception != null) ? exception.ToString() : string.Empty, exception);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000ADF0 File Offset: 0x00008FF0
		internal static string ExceptionToTraceString(Exception exception, int maxTraceStringLength)
		{
			StringBuilder stringBuilder = EtwDiagnosticTrace.StringBuilderPool.Take();
			string result;
			try
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.CurrentCulture))
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
					{
						EtwDiagnosticTrace.WriteExceptionToTraceString(xmlTextWriter, exception, maxTraceStringLength, 64);
						xmlTextWriter.Flush();
						stringWriter.Flush();
						result = stringBuilder.ToString();
					}
				}
			}
			finally
			{
				EtwDiagnosticTrace.StringBuilderPool.Return(stringBuilder);
			}
			return result;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000AE7C File Offset: 0x0000907C
		private static void WriteExceptionToTraceString(XmlTextWriter xml, Exception exception, int remainingLength, int remainingAllowedRecursionDepth)
		{
			if (remainingAllowedRecursionDepth < 1)
			{
				return;
			}
			if (!EtwDiagnosticTrace.WriteStartElement(xml, "Exception", ref remainingLength))
			{
				return;
			}
			try
			{
				IList<Tuple<string, string>> list = new List<Tuple<string, string>>
				{
					new Tuple<string, string>("ExceptionType", DiagnosticTraceBase.XmlEncode(exception.GetType().AssemblyQualifiedName)),
					new Tuple<string, string>("Message", DiagnosticTraceBase.XmlEncode(exception.Message)),
					new Tuple<string, string>("StackTrace", DiagnosticTraceBase.XmlEncode(DiagnosticTraceBase.StackTraceString(exception))),
					new Tuple<string, string>("ExceptionString", DiagnosticTraceBase.XmlEncode(exception.ToString()))
				};
				Win32Exception ex = exception as Win32Exception;
				if (ex != null)
				{
					list.Add(new Tuple<string, string>("NativeErrorCode", ex.NativeErrorCode.ToString("X", CultureInfo.InvariantCulture)));
				}
				foreach (Tuple<string, string> tuple in list)
				{
					if (!EtwDiagnosticTrace.WriteXmlElementString(xml, tuple.Item1, tuple.Item2, ref remainingLength))
					{
						return;
					}
				}
				if (exception.Data != null && exception.Data.Count > 0)
				{
					string exceptionData = EtwDiagnosticTrace.GetExceptionData(exception);
					if (exceptionData.Length < remainingLength)
					{
						xml.WriteRaw(exceptionData);
						remainingLength -= exceptionData.Length;
					}
				}
				if (exception.InnerException != null)
				{
					string innerException = EtwDiagnosticTrace.GetInnerException(exception, remainingLength, remainingAllowedRecursionDepth - 1);
					if (!string.IsNullOrEmpty(innerException) && innerException.Length < remainingLength)
					{
						xml.WriteRaw(innerException);
					}
				}
			}
			finally
			{
				xml.WriteEndElement();
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000B034 File Offset: 0x00009234
		private static string GetInnerException(Exception exception, int remainingLength, int remainingAllowedRecursionDepth)
		{
			if (remainingAllowedRecursionDepth < 1)
			{
				return null;
			}
			StringBuilder stringBuilder = EtwDiagnosticTrace.StringBuilderPool.Take();
			string result;
			try
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.CurrentCulture))
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
					{
						if (!EtwDiagnosticTrace.WriteStartElement(xmlTextWriter, "InnerException", ref remainingLength))
						{
							result = null;
						}
						else
						{
							EtwDiagnosticTrace.WriteExceptionToTraceString(xmlTextWriter, exception.InnerException, remainingLength, remainingAllowedRecursionDepth);
							xmlTextWriter.WriteEndElement();
							xmlTextWriter.Flush();
							stringWriter.Flush();
							result = stringBuilder.ToString();
						}
					}
				}
			}
			finally
			{
				EtwDiagnosticTrace.StringBuilderPool.Return(stringBuilder);
			}
			return result;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000B0E0 File Offset: 0x000092E0
		private static string GetExceptionData(Exception exception)
		{
			StringBuilder stringBuilder = EtwDiagnosticTrace.StringBuilderPool.Take();
			string result;
			try
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.CurrentCulture))
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
					{
						xmlTextWriter.WriteStartElement("DataItems");
						foreach (object obj in exception.Data.Keys)
						{
							xmlTextWriter.WriteStartElement("Data");
							xmlTextWriter.WriteElementString("Key", DiagnosticTraceBase.XmlEncode(obj.ToString()));
							if (exception.Data[obj] == null)
							{
								xmlTextWriter.WriteElementString("Value", string.Empty);
							}
							else
							{
								xmlTextWriter.WriteElementString("Value", DiagnosticTraceBase.XmlEncode(exception.Data[obj].ToString()));
							}
							xmlTextWriter.WriteEndElement();
						}
						xmlTextWriter.WriteEndElement();
						xmlTextWriter.Flush();
						stringWriter.Flush();
						result = stringBuilder.ToString();
					}
				}
			}
			finally
			{
				EtwDiagnosticTrace.StringBuilderPool.Return(stringBuilder);
			}
			return result;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000B224 File Offset: 0x00009424
		private static bool WriteStartElement(XmlTextWriter xml, string localName, ref int remainingLength)
		{
			int num = localName.Length * 2 + 5;
			if (num <= remainingLength)
			{
				xml.WriteStartElement(localName);
				remainingLength -= num;
				return true;
			}
			return false;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000B254 File Offset: 0x00009454
		private static bool WriteXmlElementString(XmlTextWriter xml, string localName, string value, ref int remainingLength)
		{
			int num;
			if (string.IsNullOrEmpty(value) && !LocalAppContextSwitches.IncludeNullExceptionMessageInETWTrace)
			{
				num = localName.Length + 4;
			}
			else
			{
				num = localName.Length * 2 + 5 + value.Length;
			}
			if (num <= remainingLength)
			{
				xml.WriteElementString(localName, value);
				remainingLength -= num;
				return true;
			}
			return false;
		}

		// Token: 0x04000158 RID: 344
		private const int WindowsVistaMajorNumber = 6;

		// Token: 0x04000159 RID: 345
		private const string EventSourceVersion = "4.0.0.0";

		// Token: 0x0400015A RID: 346
		private const ushort TracingEventLogCategory = 4;

		// Token: 0x0400015B RID: 347
		private const int MaxExceptionStringLength = 28672;

		// Token: 0x0400015C RID: 348
		private const int MaxExceptionDepth = 64;

		// Token: 0x0400015D RID: 349
		private const string DiagnosticTraceSource = "System.ServiceModel.Diagnostics";

		// Token: 0x0400015E RID: 350
		private const int XmlBracketsLength = 5;

		// Token: 0x0400015F RID: 351
		private const int XmlBracketsLengthForNullValue = 4;

		// Token: 0x04000160 RID: 352
		public static readonly Guid ImmutableDefaultEtwProviderId = new Guid("{c651f5f6-1c0d-492e-8ae1-b4efd7c9d503}");

		// Token: 0x04000161 RID: 353
		[SecurityCritical]
		private static Guid defaultEtwProviderId = EtwDiagnosticTrace.ImmutableDefaultEtwProviderId;

		// Token: 0x04000162 RID: 354
		private static Hashtable etwProviderCache = new Hashtable();

		// Token: 0x04000163 RID: 355
		private static bool isVistaOrGreater = Environment.OSVersion.Version.Major >= 6;

		// Token: 0x04000164 RID: 356
		private static Func<string> traceAnnotation;

		// Token: 0x04000165 RID: 357
		[SecurityCritical]
		private EtwProvider etwProvider;

		// Token: 0x04000166 RID: 358
		private Guid etwProviderId;

		// Token: 0x04000167 RID: 359
		[SecurityCritical]
		private static EventDescriptor transferEventDescriptor = new EventDescriptor(499, 0, 18, 0, 0, 0, 2305843009215397989L);

		// Token: 0x0200008F RID: 143
		private static class TraceCodes
		{
			// Token: 0x040002E3 RID: 739
			public const string AppDomainUnload = "AppDomainUnload";

			// Token: 0x040002E4 RID: 740
			public const string TraceHandledException = "TraceHandledException";

			// Token: 0x040002E5 RID: 741
			public const string ThrowingException = "ThrowingException";

			// Token: 0x040002E6 RID: 742
			public const string UnhandledException = "UnhandledException";
		}

		// Token: 0x02000090 RID: 144
		private static class EventIdsWithMsdnTraceCode
		{
			// Token: 0x040002E7 RID: 743
			public const int AppDomainUnload = 57393;

			// Token: 0x040002E8 RID: 744
			public const int ThrowingExceptionWarning = 57396;

			// Token: 0x040002E9 RID: 745
			public const int ThrowingExceptionVerbose = 57407;

			// Token: 0x040002EA RID: 746
			public const int HandledExceptionInfo = 57394;

			// Token: 0x040002EB RID: 747
			public const int HandledExceptionWarning = 57404;

			// Token: 0x040002EC RID: 748
			public const int HandledExceptionError = 57405;

			// Token: 0x040002ED RID: 749
			public const int HandledExceptionVerbose = 57406;

			// Token: 0x040002EE RID: 750
			public const int UnhandledException = 57397;
		}

		// Token: 0x02000091 RID: 145
		private static class LegacyTraceEventIds
		{
			// Token: 0x040002EF RID: 751
			public const int Diagnostics = 131072;

			// Token: 0x040002F0 RID: 752
			public const int AppDomainUnload = 131073;

			// Token: 0x040002F1 RID: 753
			public const int EventLog = 131074;

			// Token: 0x040002F2 RID: 754
			public const int ThrowingException = 131075;

			// Token: 0x040002F3 RID: 755
			public const int TraceHandledException = 131076;

			// Token: 0x040002F4 RID: 756
			public const int UnhandledException = 131077;
		}

		// Token: 0x02000092 RID: 146
		private static class StringBuilderPool
		{
			// Token: 0x0600040F RID: 1039 RVA: 0x00012F74 File Offset: 0x00011174
			public static StringBuilder Take()
			{
				StringBuilder result = null;
				if (EtwDiagnosticTrace.StringBuilderPool.freeStringBuilders.TryDequeue(out result))
				{
					return result;
				}
				return new StringBuilder();
			}

			// Token: 0x06000410 RID: 1040 RVA: 0x00012F98 File Offset: 0x00011198
			public static void Return(StringBuilder sb)
			{
				if (EtwDiagnosticTrace.StringBuilderPool.freeStringBuilders.Count <= 64)
				{
					sb.Clear();
					EtwDiagnosticTrace.StringBuilderPool.freeStringBuilders.Enqueue(sb);
				}
			}

			// Token: 0x06000411 RID: 1041 RVA: 0x00012FBA File Offset: 0x000111BA
			// Note: this type is marked as 'beforefieldinit'.
			static StringBuilderPool()
			{
			}

			// Token: 0x040002F5 RID: 757
			private const int maxPooledStringBuilders = 64;

			// Token: 0x040002F6 RID: 758
			private static readonly ConcurrentQueue<StringBuilder> freeStringBuilders = new ConcurrentQueue<StringBuilder>();
		}
	}
}

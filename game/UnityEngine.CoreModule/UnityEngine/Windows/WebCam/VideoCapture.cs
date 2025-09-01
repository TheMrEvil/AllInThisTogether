using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x020002A7 RID: 679
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[NativeHeader("PlatformDependent/Win/Webcam/VideoCaptureBindings.h")]
	[StaticAccessor("VideoCaptureBindings", StaticAccessorType.DoubleColon)]
	[StructLayout(LayoutKind.Sequential)]
	public class VideoCapture : IDisposable
	{
		// Token: 0x06001CDB RID: 7387 RVA: 0x0002E464 File Offset: 0x0002C664
		private static VideoCapture.VideoCaptureResult MakeCaptureResult(VideoCapture.CaptureResultType resultType, long hResult)
		{
			return new VideoCapture.VideoCaptureResult
			{
				resultType = resultType,
				hResult = hResult
			};
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0002E490 File Offset: 0x0002C690
		private static VideoCapture.VideoCaptureResult MakeCaptureResult(long hResult)
		{
			VideoCapture.VideoCaptureResult result = default(VideoCapture.VideoCaptureResult);
			bool flag = hResult == VideoCapture.HR_SUCCESS;
			VideoCapture.CaptureResultType resultType;
			if (flag)
			{
				resultType = VideoCapture.CaptureResultType.Success;
			}
			else
			{
				resultType = VideoCapture.CaptureResultType.UnknownError;
			}
			result.resultType = resultType;
			result.hResult = hResult;
			return result;
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x0002E4D4 File Offset: 0x0002C6D4
		public static IEnumerable<Resolution> SupportedResolutions
		{
			get
			{
				bool flag = VideoCapture.s_SupportedResolutions == null;
				if (flag)
				{
					VideoCapture.s_SupportedResolutions = VideoCapture.GetSupportedResolutions_Internal();
				}
				return VideoCapture.s_SupportedResolutions;
			}
		}

		// Token: 0x06001CDE RID: 7390
		[NativeName("GetSupportedResolutions")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Resolution[] GetSupportedResolutions_Internal();

		// Token: 0x06001CDF RID: 7391 RVA: 0x0002E504 File Offset: 0x0002C704
		public static IEnumerable<float> GetSupportedFrameRatesForResolution(Resolution resolution)
		{
			return VideoCapture.GetSupportedFrameRatesForResolution_Internal(resolution.width, resolution.height);
		}

		// Token: 0x06001CE0 RID: 7392
		[NativeName("GetSupportedFrameRatesForResolution")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float[] GetSupportedFrameRatesForResolution_Internal(int resolutionWidth, int resolutionHeight);

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001CE1 RID: 7393
		public extern bool IsRecording { [NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")] [NativeMethod("VideoCaptureBindings::IsRecording", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0002E530 File Offset: 0x0002C730
		public static void CreateAsync(bool showHolograms, VideoCapture.OnVideoCaptureResourceCreatedCallback onCreatedCallback)
		{
			bool flag = onCreatedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCreatedCallback");
			}
			VideoCapture.Instantiate_Internal(showHolograms, onCreatedCallback);
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0002E55C File Offset: 0x0002C75C
		public static void CreateAsync(VideoCapture.OnVideoCaptureResourceCreatedCallback onCreatedCallback)
		{
			bool flag = onCreatedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCreatedCallback");
			}
			VideoCapture.Instantiate_Internal(false, onCreatedCallback);
		}

		// Token: 0x06001CE4 RID: 7396
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("Instantiate")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Instantiate_Internal(bool showHolograms, VideoCapture.OnVideoCaptureResourceCreatedCallback onCreatedCallback);

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0002E588 File Offset: 0x0002C788
		[RequiredByNativeCode]
		private static void InvokeOnCreatedVideoCaptureResourceDelegate(VideoCapture.OnVideoCaptureResourceCreatedCallback callback, IntPtr nativePtr)
		{
			bool flag = nativePtr == IntPtr.Zero;
			if (flag)
			{
				callback(null);
			}
			else
			{
				callback(new VideoCapture(nativePtr));
			}
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0002E5C0 File Offset: 0x0002C7C0
		private VideoCapture(IntPtr nativeCaptureObject)
		{
			this.m_NativePtr = nativeCaptureObject;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0002E5D4 File Offset: 0x0002C7D4
		public void StartVideoModeAsync(CameraParameters setupParams, VideoCapture.AudioState audioState, VideoCapture.OnVideoModeStartedCallback onVideoModeStartedCallback)
		{
			bool flag = onVideoModeStartedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onVideoModeStartedCallback");
			}
			bool flag2 = setupParams.cameraResolutionWidth == 0 || setupParams.cameraResolutionHeight == 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("setupParams", "The camera resolution must be set to a supported resolution.");
			}
			bool flag3 = setupParams.frameRate == 0f;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("setupParams", "The camera frame rate must be set to a supported recording frame rate.");
			}
			this.StartVideoMode_Internal(setupParams, audioState, onVideoModeStartedCallback);
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0002E64E File Offset: 0x0002C84E
		[NativeMethod("VideoCaptureBindings::StartVideoMode", HasExplicitThis = true)]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		private void StartVideoMode_Internal(CameraParameters cameraParameters, VideoCapture.AudioState audioState, VideoCapture.OnVideoModeStartedCallback onVideoModeStartedCallback)
		{
			this.StartVideoMode_Internal_Injected(ref cameraParameters, audioState, onVideoModeStartedCallback);
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0002E65A File Offset: 0x0002C85A
		[RequiredByNativeCode]
		private static void InvokeOnVideoModeStartedDelegate(VideoCapture.OnVideoModeStartedCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x06001CEA RID: 7402
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeMethod("VideoCaptureBindings::StopVideoMode", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopVideoModeAsync([NotNull("ArgumentNullException")] VideoCapture.OnVideoModeStoppedCallback onVideoModeStoppedCallback);

		// Token: 0x06001CEB RID: 7403 RVA: 0x0002E66A File Offset: 0x0002C86A
		[RequiredByNativeCode]
		private static void InvokeOnVideoModeStoppedDelegate(VideoCapture.OnVideoModeStoppedCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0002E67C File Offset: 0x0002C87C
		public void StartRecordingAsync(string filename, VideoCapture.OnStartedRecordingVideoCallback onStartedRecordingVideoCallback)
		{
			bool flag = onStartedRecordingVideoCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onStartedRecordingVideoCallback");
			}
			bool flag2 = string.IsNullOrEmpty(filename);
			if (flag2)
			{
				throw new ArgumentNullException("filename");
			}
			string directoryName = Path.GetDirectoryName(filename);
			bool flag3 = !string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName);
			if (flag3)
			{
				throw new ArgumentException("The specified directory does not exist.", "filename");
			}
			FileInfo fileInfo = new FileInfo(filename);
			bool flag4 = fileInfo.Exists && fileInfo.IsReadOnly;
			if (flag4)
			{
				throw new ArgumentException("Cannot write to the file because it is read-only.", "filename");
			}
			this.StartRecordingVideoToDisk_Internal(fileInfo.FullName, onStartedRecordingVideoCallback);
		}

		// Token: 0x06001CED RID: 7405
		[NativeMethod("VideoCaptureBindings::StartRecordingVideoToDisk", HasExplicitThis = true)]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StartRecordingVideoToDisk_Internal(string filename, VideoCapture.OnStartedRecordingVideoCallback onStartedRecordingVideoCallback);

		// Token: 0x06001CEE RID: 7406 RVA: 0x0002E723 File Offset: 0x0002C923
		[RequiredByNativeCode]
		private static void InvokeOnStartedRecordingVideoToDiskDelegate(VideoCapture.OnStartedRecordingVideoCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x06001CEF RID: 7407
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeMethod("VideoCaptureBindings::StopRecordingVideoToDisk", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopRecordingAsync([NotNull("ArgumentNullException")] VideoCapture.OnStoppedRecordingVideoCallback onStoppedRecordingVideoCallback);

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0002E733 File Offset: 0x0002C933
		[RequiredByNativeCode]
		private static void InvokeOnStoppedRecordingVideoToDiskDelegate(VideoCapture.OnStoppedRecordingVideoCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x06001CF1 RID: 7409
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeMethod("VideoCaptureBindings::GetUnsafePointerToVideoDeviceController", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetUnsafePointerToVideoDeviceController();

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0002E744 File Offset: 0x0002C944
		public void Dispose()
		{
			bool flag = this.m_NativePtr != IntPtr.Zero;
			if (flag)
			{
				this.Dispose_Internal();
				this.m_NativePtr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001CF3 RID: 7411
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeMethod("VideoCaptureBindings::Dispose", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Dispose_Internal();

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0002E784 File Offset: 0x0002C984
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_NativePtr != IntPtr.Zero;
				if (flag)
				{
					this.DisposeThreaded_Internal();
					this.m_NativePtr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06001CF5 RID: 7413
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[ThreadAndSerializationSafe]
		[NativeMethod("VideoCaptureBindings::DisposeThreaded", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DisposeThreaded_Internal();

		// Token: 0x06001CF6 RID: 7414
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StartVideoMode_Internal_Injected(ref CameraParameters cameraParameters, VideoCapture.AudioState audioState, VideoCapture.OnVideoModeStartedCallback onVideoModeStartedCallback);

		// Token: 0x0400096B RID: 2411
		internal IntPtr m_NativePtr;

		// Token: 0x0400096C RID: 2412
		private static Resolution[] s_SupportedResolutions;

		// Token: 0x0400096D RID: 2413
		private static readonly long HR_SUCCESS;

		// Token: 0x020002A8 RID: 680
		public enum CaptureResultType
		{
			// Token: 0x0400096F RID: 2415
			Success,
			// Token: 0x04000970 RID: 2416
			UnknownError
		}

		// Token: 0x020002A9 RID: 681
		public enum AudioState
		{
			// Token: 0x04000972 RID: 2418
			MicAudio,
			// Token: 0x04000973 RID: 2419
			ApplicationAudio,
			// Token: 0x04000974 RID: 2420
			ApplicationAndMicAudio,
			// Token: 0x04000975 RID: 2421
			None
		}

		// Token: 0x020002AA RID: 682
		public struct VideoCaptureResult
		{
			// Token: 0x170005B7 RID: 1463
			// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x0002E7D8 File Offset: 0x0002C9D8
			public bool success
			{
				get
				{
					return this.resultType == VideoCapture.CaptureResultType.Success;
				}
			}

			// Token: 0x04000976 RID: 2422
			public VideoCapture.CaptureResultType resultType;

			// Token: 0x04000977 RID: 2423
			public long hResult;
		}

		// Token: 0x020002AB RID: 683
		// (Invoke) Token: 0x06001CF9 RID: 7417
		public delegate void OnVideoCaptureResourceCreatedCallback(VideoCapture captureObject);

		// Token: 0x020002AC RID: 684
		// (Invoke) Token: 0x06001CFD RID: 7421
		public delegate void OnVideoModeStartedCallback(VideoCapture.VideoCaptureResult result);

		// Token: 0x020002AD RID: 685
		// (Invoke) Token: 0x06001D01 RID: 7425
		public delegate void OnVideoModeStoppedCallback(VideoCapture.VideoCaptureResult result);

		// Token: 0x020002AE RID: 686
		// (Invoke) Token: 0x06001D05 RID: 7429
		public delegate void OnStartedRecordingVideoCallback(VideoCapture.VideoCaptureResult result);

		// Token: 0x020002AF RID: 687
		// (Invoke) Token: 0x06001D09 RID: 7433
		public delegate void OnStoppedRecordingVideoCallback(VideoCapture.VideoCaptureResult result);
	}
}

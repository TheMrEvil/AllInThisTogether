using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	// Token: 0x0200000E RID: 14
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandler.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class DownloadHandler : IDisposable
	{
		// Token: 0x060000D7 RID: 215
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Release();

		// Token: 0x060000D8 RID: 216 RVA: 0x00004A92 File Offset: 0x00002C92
		[VisibleToOtherModules]
		internal DownloadHandler()
		{
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004A9C File Offset: 0x00002C9C
		~DownloadHandler()
		{
			this.Dispose();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004ACC File Offset: 0x00002CCC
		public virtual void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Release();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004B04 File Offset: 0x00002D04
		public bool isDone
		{
			get
			{
				return this.IsDone();
			}
		}

		// Token: 0x060000DC RID: 220
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsDone();

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004B1C File Offset: 0x00002D1C
		public string error
		{
			get
			{
				return this.GetErrorMsg();
			}
		}

		// Token: 0x060000DE RID: 222
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetErrorMsg();

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004B34 File Offset: 0x00002D34
		public NativeArray<byte>.ReadOnly nativeData
		{
			get
			{
				return this.GetNativeData().AsReadOnly();
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004B54 File Offset: 0x00002D54
		public byte[] data
		{
			get
			{
				return this.GetData();
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004B6C File Offset: 0x00002D6C
		public string text
		{
			get
			{
				return this.GetText();
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004B84 File Offset: 0x00002D84
		protected virtual NativeArray<byte> GetNativeData()
		{
			return default(NativeArray<byte>);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004BA0 File Offset: 0x00002DA0
		protected virtual byte[] GetData()
		{
			return DownloadHandler.InternalGetByteArray(this);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004BB8 File Offset: 0x00002DB8
		protected unsafe virtual string GetText()
		{
			NativeArray<byte> nativeData = this.GetNativeData();
			bool flag = nativeData.IsCreated && nativeData.Length > 0;
			string result;
			if (flag)
			{
				result = new string((sbyte*)nativeData.GetUnsafeReadOnlyPtr<byte>(), 0, nativeData.Length, this.GetTextEncoder());
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004C10 File Offset: 0x00002E10
		private Encoding GetTextEncoder()
		{
			string contentType = this.GetContentType();
			bool flag = !string.IsNullOrEmpty(contentType);
			if (flag)
			{
				int num = contentType.IndexOf("charset", StringComparison.OrdinalIgnoreCase);
				bool flag2 = num > -1;
				if (flag2)
				{
					int num2 = contentType.IndexOf('=', num);
					bool flag3 = num2 > -1;
					if (flag3)
					{
						string text = contentType.Substring(num2 + 1).Trim().Trim(new char[]
						{
							'\'',
							'"'
						}).Trim();
						int num3 = text.IndexOf(';');
						bool flag4 = num3 > -1;
						if (flag4)
						{
							text = text.Substring(0, num3);
						}
						try
						{
							return Encoding.GetEncoding(text);
						}
						catch (ArgumentException ex)
						{
							Debug.LogWarning(string.Format("Unsupported encoding '{0}': {1}", text, ex.Message));
						}
						catch (NotSupportedException ex2)
						{
							Debug.LogWarning(string.Format("Unsupported encoding '{0}': {1}", text, ex2.Message));
						}
					}
				}
			}
			return Encoding.UTF8;
		}

		// Token: 0x060000E6 RID: 230
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetContentType();

		// Token: 0x060000E7 RID: 231 RVA: 0x00004D2C File Offset: 0x00002F2C
		[RequiredByNativeCode]
		protected virtual bool ReceiveData(byte[] data, int dataLength)
		{
			return true;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004D3F File Offset: 0x00002F3F
		[RequiredByNativeCode]
		protected virtual void ReceiveContentLengthHeader(ulong contentLength)
		{
			this.ReceiveContentLength((int)contentLength);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004D4B File Offset: 0x00002F4B
		[Obsolete("Use ReceiveContentLengthHeader")]
		protected virtual void ReceiveContentLength(int contentLength)
		{
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004D4B File Offset: 0x00002F4B
		[RequiredByNativeCode]
		protected virtual void CompleteContent()
		{
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004D50 File Offset: 0x00002F50
		[RequiredByNativeCode]
		protected virtual float GetProgress()
		{
			return 0f;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004D68 File Offset: 0x00002F68
		protected static T GetCheckedDownloader<T>(UnityWebRequest www) where T : DownloadHandler
		{
			bool flag = www == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get content from a null UnityWebRequest object");
			}
			bool flag2 = !www.isDone;
			if (flag2)
			{
				throw new InvalidOperationException("Cannot get content from an unfinished UnityWebRequest object");
			}
			bool flag3 = www.result == UnityWebRequest.Result.ProtocolError;
			if (flag3)
			{
				throw new InvalidOperationException(www.error);
			}
			return (T)((object)www.downloadHandler);
		}

		// Token: 0x060000ED RID: 237
		[NativeThrows]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern byte* InternalGetByteArray(DownloadHandler dh, out int length);

		// Token: 0x060000EE RID: 238 RVA: 0x00004DCC File Offset: 0x00002FCC
		internal static byte[] InternalGetByteArray(DownloadHandler dh)
		{
			NativeArray<byte> nativeData = dh.GetNativeData();
			bool isCreated = nativeData.IsCreated;
			byte[] result;
			if (isCreated)
			{
				result = nativeData.ToArray();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004DFC File Offset: 0x00002FFC
		internal unsafe static NativeArray<byte> InternalGetNativeArray(DownloadHandler dh, ref NativeArray<byte> nativeArray)
		{
			int num;
			byte* bytes = DownloadHandler.InternalGetByteArray(dh, out num);
			bool isCreated = nativeArray.IsCreated;
			if (isCreated)
			{
				bool flag = nativeArray.Length == num;
				if (flag)
				{
					return nativeArray;
				}
				DownloadHandler.DisposeNativeArray(ref nativeArray);
			}
			DownloadHandler.CreateNativeArrayForNativeData(ref nativeArray, bytes, num);
			return nativeArray;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004E54 File Offset: 0x00003054
		internal static void DisposeNativeArray(ref NativeArray<byte> data)
		{
			bool flag = !data.IsCreated;
			if (!flag)
			{
				data = default(NativeArray<byte>);
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004E78 File Offset: 0x00003078
		internal unsafe static void CreateNativeArrayForNativeData(ref NativeArray<byte> data, byte* bytes, int length)
		{
			data = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)bytes, length, Allocator.Persistent);
		}

		// Token: 0x0400005A RID: 90
		[VisibleToOtherModules]
		[NonSerialized]
		internal IntPtr m_Ptr;
	}
}

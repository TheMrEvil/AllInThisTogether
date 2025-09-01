using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Analytics
{
	// Token: 0x0200000F RID: 15
	[NativeHeader("Modules/UnityAnalytics/Public/Events/UserCustomEvent.h")]
	[StructLayout(LayoutKind.Sequential)]
	internal class CustomEventData : IDisposable
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00002431 File Offset: 0x00000631
		private CustomEventData()
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003597 File Offset: 0x00001797
		public CustomEventData(string name)
		{
			this.m_Ptr = CustomEventData.Internal_Create(this, name);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000035B0 File Offset: 0x000017B0
		~CustomEventData()
		{
			this.Destroy();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000035E0 File Offset: 0x000017E0
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				CustomEventData.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000361B File Offset: 0x0000181B
		public void Dispose()
		{
			this.Destroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000CF RID: 207
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Internal_Create(CustomEventData ced, string name);

		// Token: 0x060000D0 RID: 208
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x060000D1 RID: 209
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddString(string key, string value);

		// Token: 0x060000D2 RID: 210
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddInt32(string key, int value);

		// Token: 0x060000D3 RID: 211
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddUInt32(string key, uint value);

		// Token: 0x060000D4 RID: 212
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddInt64(string key, long value);

		// Token: 0x060000D5 RID: 213
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddUInt64(string key, ulong value);

		// Token: 0x060000D6 RID: 214
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddBool(string key, bool value);

		// Token: 0x060000D7 RID: 215
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddDouble(string key, double value);

		// Token: 0x060000D8 RID: 216 RVA: 0x0000362C File Offset: 0x0000182C
		public bool AddDictionary(IDictionary<string, object> eventData)
		{
			foreach (KeyValuePair<string, object> keyValuePair in eventData)
			{
				string key = keyValuePair.Key;
				object value = keyValuePair.Value;
				bool flag = value == null;
				if (flag)
				{
					this.AddString(key, "null");
				}
				else
				{
					Type type = value.GetType();
					bool flag2 = type == typeof(string);
					if (flag2)
					{
						this.AddString(key, (string)value);
					}
					else
					{
						bool flag3 = type == typeof(char);
						if (flag3)
						{
							this.AddString(key, char.ToString((char)value));
						}
						else
						{
							bool flag4 = type == typeof(sbyte);
							if (flag4)
							{
								this.AddInt32(key, (int)((sbyte)value));
							}
							else
							{
								bool flag5 = type == typeof(byte);
								if (flag5)
								{
									this.AddInt32(key, (int)((byte)value));
								}
								else
								{
									bool flag6 = type == typeof(short);
									if (flag6)
									{
										this.AddInt32(key, (int)((short)value));
									}
									else
									{
										bool flag7 = type == typeof(ushort);
										if (flag7)
										{
											this.AddUInt32(key, (uint)((ushort)value));
										}
										else
										{
											bool flag8 = type == typeof(int);
											if (flag8)
											{
												this.AddInt32(key, (int)value);
											}
											else
											{
												bool flag9 = type == typeof(uint);
												if (flag9)
												{
													this.AddUInt32(keyValuePair.Key, (uint)value);
												}
												else
												{
													bool flag10 = type == typeof(long);
													if (flag10)
													{
														this.AddInt64(key, (long)value);
													}
													else
													{
														bool flag11 = type == typeof(ulong);
														if (flag11)
														{
															this.AddUInt64(key, (ulong)value);
														}
														else
														{
															bool flag12 = type == typeof(bool);
															if (flag12)
															{
																this.AddBool(key, (bool)value);
															}
															else
															{
																bool flag13 = type == typeof(float);
																if (flag13)
																{
																	this.AddDouble(key, (double)Convert.ToDecimal((float)value));
																}
																else
																{
																	bool flag14 = type == typeof(double);
																	if (flag14)
																	{
																		this.AddDouble(key, (double)value);
																	}
																	else
																	{
																		bool flag15 = type == typeof(decimal);
																		if (flag15)
																		{
																			this.AddDouble(key, (double)Convert.ToDecimal((decimal)value));
																		}
																		else
																		{
																			bool isValueType = type.IsValueType;
																			if (!isValueType)
																			{
																				throw new ArgumentException(string.Format("Invalid type: {0} passed", type));
																			}
																			this.AddString(key, value.ToString());
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0400002B RID: 43
		[NonSerialized]
		internal IntPtr m_Ptr;
	}
}

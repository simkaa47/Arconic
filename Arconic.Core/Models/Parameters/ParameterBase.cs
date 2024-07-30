﻿using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using S7.Net;
using S7.Net.Types;
using String = System.String;

namespace Arconic.Core.Models.Parameters;

public abstract partial class ParameterBase:ObservableValidator
{
    public string Description { get; set; } = String.Empty;
    [ObservableProperty]
    private bool _validationOk = false;
    [ObservableProperty]
    private bool _isWriting = false;
    [ObservableProperty]
    private bool _isReadOnly = false;
    public DataType MemoryType { get; init; }
    public int DbNum { get; init; }
    public int ByteNum { get; init; }
    public int BitNum { get; set; }
    public int Length { get; set; }
    
    public int SizeBytes => GetSize();

    private int GetSize()
    {
        if (this is Parameter<bool>) return 1;
        else if (this is Parameter<string>) return Length;
        else if (this is Parameter<short> || this is Parameter<ushort>) return 2;
        else if (this is Parameter<int> || this is Parameter<uint>) return 4;
        else if (this is Parameter<float>) return 4;
        else return 0;
    }

    public async Task WriteToPlcAsync(S7.Net.Plc plc, CancellationToken cancellationToken = default(CancellationToken))
    {
        if (this is Parameter<bool> parBool)
            await plc.WriteBitAsync(MemoryType, DbNum, ByteNum, BitNum, parBool.WriteValue, cancellationToken);
        else if (this is Parameter<string> parString)
        {
            if (String.IsNullOrEmpty(parString.WriteValue)) return;
            var bytes = new byte[Length];
            var valueStr = Encoding.GetEncoding(1251).GetBytes(parString.WriteValue);
            Array.Copy(valueStr, bytes, Math.Min(valueStr.Length, Length));
            await plc.WriteBytesAsync(MemoryType, DbNum, ByteNum, bytes, cancellationToken);
        }
        else if (this is Parameter<short> parShort)
        {
            var arr = S7.Net.Types.Int.ToByteArray(parShort.Value);
            await plc.WriteBytesAsync(MemoryType, DbNum, ByteNum, arr, cancellationToken);
        }
        else if (this is Parameter<ushort> parUShort)
        {
            var arr = S7.Net.Types.Word.ToByteArray(parUShort.Value);
            await plc.WriteBytesAsync(MemoryType, DbNum, ByteNum, arr, cancellationToken);
        }
        else if (this is Parameter<int> parInt)
        {
            var arr = S7.Net.Types.DInt.ToByteArray(parInt.Value);
            await plc.WriteBytesAsync(MemoryType, DbNum, ByteNum, arr, cancellationToken);
        }
        else if (this is Parameter<uint> parUInt)
        {
            var arr = S7.Net.Types.DWord.ToByteArray(parUInt.Value);
            await plc.WriteBytesAsync(MemoryType, DbNum, ByteNum, arr, cancellationToken);
        }
        else if (this is Parameter<float> parFloat)
        {
            var arr = S7.Net.Types.Real.ToByteArray(parFloat.Value);
            await plc.WriteBytesAsync(MemoryType, DbNum, ByteNum, arr, cancellationToken);
        }
    }

    public void GetValue(byte[] arr)
    {
        if (this is Parameter<bool> parBool)
            parBool.Value = arr[ByteNum].SelectBit(BitNum);
        else if (this is Parameter<string> parString)
        {
            var strArr = arr.Skip(ByteNum)
                .Take(Length)
                .TakeWhile(b => b != 0)
                .ToArray();

            parString.Value = Encoding.GetEncoding(1251).GetString(strArr);
        }
        else if (this is Parameter<short> parShort)
        {
            var shortArr = arr.Skip(ByteNum).Take(2).ToArray();
            parShort.Value = Int.FromByteArray(shortArr);
        }
        else if (this is Parameter<ushort> parUShort)
        {
            var ushortArr = arr.Skip(ByteNum).Take(2).ToArray();
            parUShort.Value = Word.FromByteArray(ushortArr);
        }
        else if (this is Parameter<int> parInt)
        {
            var intArr = arr.Skip(ByteNum).Take(4).ToArray();
            parInt.Value = DInt.FromByteArray(intArr);
        }
        else if (this is Parameter<uint> parUInt)
        {
            var uintArr = arr.Skip(ByteNum).Take(4).ToArray();
            parUInt.Value = DWord.FromByteArray(uintArr);
        }
        else if (this is Parameter<float> parFloat)
        {
            var floatArr = arr.Skip(ByteNum).Take(4).ToArray();
            parFloat.Value = Real.FromByteArray(floatArr);
        }
            
    }

    public static List<ParameterBase> Parameters { get; } = new List<ParameterBase>();
}
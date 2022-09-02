using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sandbox._2022._202208;

// ref変数についての造詣を深める。
// あと、いまどき（？）の、intをbyte配列（Span）として読む、ｱﾌﾞﾅｲコード
internal class Class1
{
    public Class1()
    {
        int int1 = 100;

        ref int int2 = ref int1;

        int2 = 200;
        Console.WriteLine(int1);



        //昔からできるやつ。構造体を定義しておくやつ。

        var s1 = new S1 { _i = 0x12345678 };
        Console.WriteLine(s1._x1);



        // refとかSpanとかを使う新しめのやり方。
        // 使い方を間違えるとかなり危険。

        int a = 12345;
        Console.WriteLine($"a = {a}");

        var bs = MemoryMarshal.CreateSpan(ref Unsafe.As<int, byte>(ref a), 4);

        foreach (var item in bs)
        {
            Console.WriteLine(item.ToString());
        }

        bs[3] = 1;

        Console.WriteLine($"a = {a}");



        // スタック上にintを4つ確保しておいて、byte配列的に操作することもできる。

        Span<int> ab = stackalloc int[4];
        var bs2 = MemoryMarshal.CreateSpan(ref Unsafe.As<int, byte>(ref ab[0]), 16);

        bs2[15] = 1;



        // 中身無しのくそでか構造体を宣言しておいて、byte配列的に操作することもできる。

        S2 s2;
        var bss2 = MemoryMarshal.CreateSpan(ref Unsafe.As<S2, byte>(ref s2), 100);

        ref var s2b0 = ref Unsafe.As<S2, byte>(ref s2);
        s2b0 = 1;

        ref var s2b1 = ref Unsafe.AddByteOffset(ref s2b0, 1);
        s2b1 = 2;



        // まさかこんなことが…？

        byte t1 = 0x10;
        byte t2 = 0x20;
        byte t3 = 0x30;
        byte t4 = 0x40;
        byte t5 = 0x50;
        byte t6 = 0x60;
        byte t7 = 0x70;
        byte t8 = 0x80;


        var ts = MemoryMarshal.CreateSpan(ref t5, 100);



        // bool型の正体とは

        bool bool1 = true;
        bool bool2 = false;

        ref var bool1byte = ref Unsafe.As<bool, byte>(ref bool1);
        ref var bool2byte = ref Unsafe.As<bool, byte>(ref bool2);

        bool2byte = 2;

        if (bool2) Console.WriteLine("true1");
        if (bool2) Console.WriteLine("true2");
        if (bool2 == bool1) Console.WriteLine("true3");

        switch (bool2)
        {
            case true: Console.WriteLine("true"); break;
            case false: Console.WriteLine("false"); break;
            default: Console.WriteLine("default"); break;
        }

        F(bool2 == true);




        //Unsafeクラスができる以前、こんなこともした。

        var x = 1;
        TypedReference r = __makeref(x);
        __refvalue(r, int) = 2;

        var t = __reftype(r);
    }

    private static void F(bool a)
    {
        ref var bool1byte = ref Unsafe.As<bool, byte>(ref a);
    }
}

// 昔からできるやつ
[StructLayout(LayoutKind.Explicit)]
internal struct S1
{
    [FieldOffset(0)]
    public byte _x1;

    [FieldOffset(1)]
    public byte _x2;

    [FieldOffset(2)]
    public byte _x3;

    [FieldOffset(3)]
    public byte _x4;

    [FieldOffset(0)]
    public uint _i;
}

// 中身は何もないけど、サイズは100ある構造体
// （通常は、型のサイズは、中身のフィールドから計算される。）
[StructLayout(LayoutKind.Explicit, Size = 100)]
internal struct S2
{ }

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Sandbox._2022._202205;

internal class Class3
{
    public Class3()
    {

        var set = new[] { 1, 2, 3 };
        foreach (var item in set)
        {

        }


        for (int i = 0; i < set.Length; i++)
        {
            var item = set[i];

        }


        var v = M1(set, 4);
        Console.WriteLine(v);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int M1(int[] a, int b)
    {
        ArgumentNullException.ThrowIfNull(a);
        return a.Length + b;
    }

    private void ThrowException(string message)
    {
        throw new ArgumentException(message);
    }
}


using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Xunit;

namespace Sandbox._2022._202205;

public class Class2
{
    public Class2()
    {
        var i = -1 >>> 2;
        var s = "hello"u8;
        ReadOnlySpan<byte> ros = "hello"u8;

        // 文字列の最適化がんばってる

        Regex.EnumerateMatches("test", $".");

        var input = "test";
        foreach (var match in Regex.EnumerateMatches(input, "a"))
        {
            var ros2 = input.AsSpan(match.Index, match.Length);
        }
    }

    [Fact]
    public void Check()
    {
        var subset = "hello".AsSpan()[2..4];
    }
}

static class Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<char> ToSpan(this ValueMatch m, string input)
    {
        return input.AsSpan(m.Index, m.Length);
    }
}

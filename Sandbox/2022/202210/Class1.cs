﻿using System.Text.RegularExpressions;

namespace Sandbox._2022._202210;

internal partial class Class1
{
    [GeneratedRegex(@"Bearer\s+(.+?)\s*$")]
    private static partial Regex BearerRegex();

    public Class1(string input)
    {
        var match = BearerRegex().Match(input);
    }
}

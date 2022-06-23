using System.Diagnostics;
using Xunit;

namespace Sandbox._2022._202206;

public class Class2
{
    [Fact]
    public void Test()
    {
        var r = Log(1) + Log(2) * Log(3);
    }

    private int Log(int v)
    {
        Debug.WriteLine(v);
        return v;
    }
}

using System;
using System.Reflection;

namespace Sandbox._2022._202207;

internal class Class2
{
    public Class2()
    {
        // アセンブリバージョンを 1.0.* みたいに設定しておく
        // 決定論的コンパイルをOFFにしておく

        var version = Assembly.GetEntryAssembly()!.GetName().Version!;
        var buildDateTime = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
    }
}

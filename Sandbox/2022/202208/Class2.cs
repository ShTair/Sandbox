using System;

namespace Sandbox._2022._202208;

// 結合性の話
// （数学的には）結合性がある→右結合性も左結合性もあること
// プログラミング的には、グループを作る方法かな…
// 同じ優先順位の演算子間での優先順位
// https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/#operator-precedence
internal class Class2
{
    public void M1()
    {
        var a1 = 1 + 2 + 3;

        var a2 = ((1 + 2) + 3) + 4;


        bool a3 = true, a4 = true, a5 = true;

        var a6 = a3 || (a4 && a5);


        int b1, b2, b3;

        b1 = (b2 = (b3 = 1));


        var c1 = M2()()();

        c1 = ((M2())())();


        var d1 = b1 < 10 ? "a" :
                 b1 < 20 ? "b" : "c";

        d1 = b1 < 10 ? "a" : b1 < 20 ? "b" : "c";
    }

    public Func<Func<int>> M2()
    {
        throw new NotImplementedException();
    }
}

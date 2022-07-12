//using System.Collections.Generic;

//namespace Sandbox._2022._202207;

//internal class Class1
//{
//    public Class1()
//    {
//        // (代入は）一見問題ないように思える
//        IDictionary<string, IEnumerable<string>> data = new Dictionary<string, string[]>();


//        // Dictionaryは値の書き換えが可能
//        // List<string> は IEnumerable<string> を満たすが、 string[] ではないから、
//        // もし上の代入が成功してたら死ぬ
//        data["list"] = new List<string>();


//        // つまり、この場合は、書き換えができないDictionaryなら、上の代入が成功しても良い
//        // （型を、出力にしか使わない…　共変性を持てる）
//        // ただ、書き換えができないDictionaryは存在しないので、できないけど。







//        // これは IEnumerable<string> に限った話ではなくて、もっと本質的な話。

//        IList<object> data2 = new List<string>();
//        data2.Add(3);


//        IReadOnlyList<object> data3 = new List<string>();
//        data3.Add(3);




//        // ジェネリックの中身の型が継承関係にあるとき、ジェネリック自身は継承関係とみなせるか？

//        IReadOnlyList<string> a1 = new List<string>();
//        IReadOnlyList<object> a2 = a1;




//        // 共変性とは？
//        // object <- string （string は object を継承している） のとき、
//        // I<object> <- I<string> （I<string> は I<object> を継承している）とみなせる性質のこと
//        // IReadOnlyList<object> <- IReadOnlyList<string> OK



//        // 逆に、書き換え（set）できるけど読みだせない場合には、反変性を持てる

//        // 反変性とは？
//        // object <- string のとき、
//        // I<string> <- I<object> （I<object> は I<string> を継承している）とみなせる性質のこと

//        // IComparer<string> <- IComparer<object> OK

//        IComparer<object> a = Comparer<object>.Default;
//        IComparer<string> b = a;


//        // 両方満たす場合、双変性という。そんな状況あるかな？C#では作れないと思う。
//        // 両方満たさない場合、非変性という。ほとんど非変性
//    }
//}

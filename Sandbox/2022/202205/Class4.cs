using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Sandbox._2022._202205;

internal class Class4
{
    public Class4()
    {
        // 基本的には、配列が一番速い。
        // 要素が増えたり減ったりしないなら、配列にしておけばいい。
        var array = new int[] { 1, 2, 3, 4 };
        array = Enumerable.Range(1, 100).ToArray();

        // コンパイル時にfor相当の処理に最適化されるはず…？
        Console.WriteLine("foreach array");
        foreach (var item in array)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("for array");
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine(array[i]);
        }


        // 要素が増えたり減ったりするなら、リスト。
        // 内部的には配列なので、配列と同じくらい速い…と思いきや、
        // 本当のただの配列は、ただの配列であることを使って最適化されるから配列の方が速い。
        var list = new List<int> { 1, 2, 3, 4 };
        list.Add(5);
        list = Enumerable.Range(1, 100).ToList();

        Console.WriteLine("foreach list");
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }


        // 複数要素入れたい場合はタプルが便利（もちろんタプルはリスト以外でも使える）
        var list2 = new List<(string, int)>();

        // クラスにしてもよい
        var list3 = new List<Record>();



        // キーで値を検索したいなら、Dictionaryを使う。
        // キーは、内部で索引が作られるので、キーの検索は速い。（配列とかリストをforeachで全検索するよりもね）
        var dictionary = new Dictionary<string, int>();
        dictionary["key1"] = 3;
        dictionary["key2"] = 5;

        if (dictionary.TryGetValue("key1", out var value1))
        {
            // dictionaryの中にkey1が入ってたら、ここが実行される。
            // value1に、key1の値が入ってる。
        }

        if (dictionary.ContainsKey("key2"))
        {
            // キーがあるかないかだけ調べるならこう。
        }

        // キーは重複できない。
        dictionary.Add("key1", 4); // エラー。key1はすでに入ってるから追加できない。
        dictionary.TryAdd("key1", 4); // エラーは起きない。追加が成功するとtrueが返る。今回は成功しないのでfalse。
        dictionary["key1"] = 4; // キーの値を上書きする。成功するし、key1の値は4になる。

        // 値にクラスを指定するのもいいね
        var dictionary2 = new Dictionary<string, Record>();




        // 値を保存する必要が無く、キーがあるか無いかを保存するだけでいいなら、HashSetが便利。
        // Dictionaryのキーしかないバージョン。キー（要素）の検索は速い。
        var hashSet = new HashSet<string>();
        hashSet.Add("item1");
        hashSet.Add("item2");

        // 重複は許されない
        hashSet.Add("item1"); // 追加できないとfalseが返る。（エラーは起きない）

        if (hashSet.Contains("item1"))
        {
            // 要素が入ってるかどうか
        }




        // Dictionaryのキーや、HashSetの要素に、複雑な型を指定しても動くか？
        var dictionary3 = new Dictionary<(string, string), int>(); // 大丈夫。タプルの同一性は、うまいことやってくれる。
        var dictionary4 = new Dictionary<string[], int>(); // ダメ！配列の同一性は、既定ではうまいことやってくれない。
        var hashSet2 = new HashSet<string[]>();
        var dictionary5 = new Dictionary<Record, int>(); // 自分で作ったクラスもダメ。

        // そういう時はIEqualityComparerを作って、何が同じなのかを指定する。
        dictionary4 = new Dictionary<string[], int>(StringArrayEqualityComparer.Default);
    }
}

internal class StringArrayEqualityComparer : IEqualityComparer<string[]>
{
    public static readonly StringArrayEqualityComparer Default = new();

    public bool Equals(string[]? x, string[]? y)
    {
        if (x is null || y is null) throw new NullReferenceException();
        return Enumerable.SequenceEqual(x, y);
    }

    public int GetHashCode([DisallowNull] string[] obj)
    {
        HashCode hash = default;
        for (int i = 0; i < obj.Length; i++) hash.Add(obj[i]);
        return hash.ToHashCode();
    }
}

internal class DateStringArrayEqualityComparer : IEqualityComparer<(DateTime Date, string[] Keys)>
{
    public static readonly DateStringArrayEqualityComparer Default = new();

    public bool Equals((DateTime Date, string[] Keys) x, (DateTime Date, string[] Keys) y)
    {
        if (x.Date != y.Date) return false;
        return Enumerable.SequenceEqual(x.Keys, y.Keys);
    }

    public int GetHashCode([DisallowNull] (DateTime Date, string[] Keys) obj)
    {
        return HashCode.Combine(obj.Date, StringArrayEqualityComparer.Default.GetHashCode(obj.Keys));
    }
}

internal class OrderEqualityComparer : IEqualityComparer<(DateTime Date, string[] Keys, decimal Quantity)>
{
    public static readonly OrderEqualityComparer Default = new();

    public bool Equals((DateTime Date, string[] Keys, decimal Quantity) x, (DateTime Date, string[] Keys, decimal Quantity) y)
    {
        // 追加する数が0じゃない場合、たとえすべて一致していても同一じゃない
        if (y.Quantity != 0) return false;

        // 追加する数が0だった場合、キーとか日付があってれば同一判定
        if (x.Date != y.Date) return false;
        return Enumerable.SequenceEqual(x.Keys, y.Keys);
    }

    public int GetHashCode([DisallowNull] (DateTime Date, string[] Keys, decimal Quantity) obj)
    {
        // 『キーと日付があってれば同一判定』をするために、キーと日付でHashCodeを作る
        return DateStringArrayEqualityComparer.Default.GetHashCode((obj.Date, obj.Keys));
    }
}

internal class Record
{
    public string Key { get; set; } = default!;

    public int Value { get; set; }
}

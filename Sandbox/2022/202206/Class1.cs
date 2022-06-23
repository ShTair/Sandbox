using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox._2022._202206;

internal class Class1
{
    public Class1()
    {
        Console.WriteLine(Console.ReadLine().AsSpan()[-3..-1].ToString());
    }
}

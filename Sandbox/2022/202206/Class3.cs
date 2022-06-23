#pragma warning disable CS8618

namespace Sandbox._2022._202206;

internal class Class3
{
    public Class3()
    {
        var line1 = new Line
        {
            A = new Point { X = 1, Y = 2 },
            B = new Point { X = 3, Y = 4 },
        };

        var line2 = new Line
        {
            A = new() { X = 1, Y = 2 },
            B = new() { X = 3, Y = 4 },
        };

        //var line3 = new Line
        //{
        //    A = new { X = 1, Y = 2 },
        //    B = new { X = 3, Y = 4 },
        //};

        var line4 = new Line
        {
            A = { X = 1, Y = 2 },
            B = { X = 3, Y = 4 },
        };
    }

    private class Line
    {
        public Point A { get; set; }

        public Point B { get; set; }
    }

    private class Point
    {
        public int X { get; set; }

        public int Y { get; set; }
    }
}

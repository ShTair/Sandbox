using System;

namespace Sandbox._2022._202205;

internal class Class1
{
    public Class1()
    {
        Recipe.Recipe.Define("test")
            .WithCategory("")
            .WithAction("", Guid.NewGuid())
                .DefineIncar()
                    .WithItem("Abcd", "B")
                    .WithItem("A", "B")
                    .AttachIncar()
                .AttachAction()
            .WithAction("", Guid.NewGuid())
                .DefineIncar()
                    .WithItem("A", "B")
                    .WithItem("A", "B")
                    .AttachIncar()
                .AttachAction()
            .WithAction("", Guid.NewGuid())
                .DefineIncar()
                    .WithItem("A", "B")
                    .WithItem("A", "B")
                    .AttachIncar()
                .AttachAction()
            .Create();
    }
}

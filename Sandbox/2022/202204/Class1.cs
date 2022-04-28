using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Sandbox._2022._202204;

public class Class1
{
    [Fact]
    public async Task RunAsync()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IChild, Child>();
        services.AddTransient<IParent, Parent>();


        await using var provider = services.BuildServiceProvider();

        var parent = provider.GetRequiredService<IParent>();


        await using (var scope = provider.CreateAsyncScope())
        {
            var parent2 = scope.ServiceProvider.GetRequiredService<IParent>();
            var parent3 = scope.ServiceProvider.GetRequiredService<IParent>();


            await using (var scope2 = scope.ServiceProvider.CreateAsyncScope())
            {
                // 入れ子できる
            }
        }
    }

    public interface IParent
    { }

    public class Parent : IParent, IDisposable
    {
        private readonly IChild _child;

        public Parent(IChild child)
        {
            _child = child;
        }

        public void Dispose() { }
    }

    public interface IChild
    { }

    public class Child : IChild, IAsyncDisposable
    {
        public async ValueTask DisposeAsync() { }
    }







    private class DummyChild : IChild { }

    [Fact]
    public void ParentTest()
    {
        var dummyChild = new DummyChild();
        var parent = new Parent(dummyChild);
    }
}

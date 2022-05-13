#pragma warning disable CS8618
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox._2022._202205.Recipe;

internal partial class Recipe
{
    public string Name { get; set; }

    public Guid? Category { get; set; }

    public IList<Action> Actions { get; set; }
}

internal class Action
{
    public string Name { get; set; }

    public Guid FacultyId { get; set; }

    public IList<IData> Datas { get; set; }
}

internal interface IData { }

internal class Data : IData
{
    public Guid Id { get; set; }
}

internal class Incar : IData
{
    public IList<IncarItem> Items { get; set; }
}

internal class IncarItem
{
    public string Name { get; set; }

    public string Value { get; set; }
}

internal partial class Recipe : IWithCategory, IWithAction, IWithCreate
{
    public static IWithCategory Define(string name)
    {
        return new Recipe { Name = name };
    }

    #region IWithCategory

    IWithAction IWithCategory.WithCategory(Guid id)
    {
        throw new NotImplementedException();
    }

    IWithAction IWithCategory.WithCategory(string name)
    {
        throw new NotImplementedException();
    }

    IWithAction IWithCategory.WithoutCategory()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region  IWithAction

    IWithData IWithAction.WithAction(string name, Guid facultyId)
    {
        return new RecipeAction { Recipe = this, Action = new Action() };
    }

    #endregion

    void IWithCreate.Create()
    {
        throw new NotImplementedException();
    }
}

internal class RecipeAction : IWithData
{
    public Recipe Recipe { get; set; }

    public Action Action { get; set; }

    IWithCreate IWithData.AttachAction()
    {
        if (Recipe.Actions is null) Recipe.Actions = new List<Action>();
        Recipe.Actions.Add(Action);
        return Recipe;
    }

    IWithIncarItem IWithData.DefineIncar()
    {
        throw new NotImplementedException();
    }

    IWithData IWithData.WithData(Guid id)
    {
        throw new NotImplementedException();
    }
}

#region インタフェース

internal interface IWithCategory
{
    public IWithAction WithCategory(Guid id);

    public IWithAction WithCategory(string name);

    public IWithAction WithoutCategory();
}

internal interface IWithAction
{
    public IWithData WithAction(string name, Guid facultyId);
}

internal interface IWithData
{
    public IWithData WithData(Guid id);

    public IWithIncarItem DefineIncar();

    public IWithCreate AttachAction();
}

internal interface IWithIncarItem
{
    public IWithNextIncarItem WithItem(string name, string value);
}

internal interface IWithNextIncarItem : IWithIncarItem
{
    public IWithData AttachIncar();
}

internal interface IWithCreate : IWithAction
{
    public void Create();
}

#endregion

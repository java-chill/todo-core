using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.Tests;

public class TodoListPersistenceTests
{
    private static string CreateTempFile() =>
        Path.Combine(Path.GetTempPath(), $"todo-{Guid.NewGuid():N}.json");

    [Fact]
    public void Save_ThenLoad_RestoresItems()
    {
        var path = CreateTempFile();
        try
        {
            var list = new TodoList();
            var first = list.Add(" Buy milk ");
            list.Add("Read book");
            first.MarkDone();

            list.Save(path);

            var loaded = TodoList.Load(path);

            Assert.Equal(2, loaded.Count);
            Assert.Equal("Buy milk", loaded.Items.First().Title);
            Assert.True(loaded.Items.First().IsDone);
            Assert.Equal(first.Id, loaded.Items.First().Id);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void Load_NonExistingFile_ReturnsEmptyList()
    {
        var path = Path.Combine(Path.GetTempPath(), $"todo-missing-{Guid.NewGuid():N}.json");
        var loaded = TodoList.Load(path);
        Assert.Equal(0, loaded.Count);
    }

    [Fact]
    public void Save_WritesJsonFile()
    {
        var path = CreateTempFile();
        try
        {
            var list = new TodoList();
            list.Add("task");
            list.Save(path);

            Assert.True(File.Exists(path));
            Assert.Contains("task", File.ReadAllText(path));
        }
        finally
        {
            File.Delete(path);
        }
    }
}

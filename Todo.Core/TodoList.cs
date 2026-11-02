using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Todo.Core;

public class TodoList
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
    };

    private readonly List<TodoItem> _items = new();

    public IReadOnlyList<TodoItem> Items => _items.AsReadOnly();

    public TodoItem Add(string title)
    {
        var item = new TodoItem(title);
        _items.Add(item);
        return item;
    }

    public bool Remove(Guid id) => _items.RemoveAll(i => i.Id == id) > 0;

    public IEnumerable<TodoItem> Find(string substring) =>
        _items.Where(i => i.Title.Contains(substring ?? string.Empty, StringComparison.OrdinalIgnoreCase));

    public int Count => _items.Count;

    public void Save(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Path required", nameof(path));
        }

        var json = JsonSerializer.Serialize(_items, JsonOptions);
        File.WriteAllText(path, json);
    }

    public static TodoList Load(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Path required", nameof(path));
        }

        var list = new TodoList();
        if (!File.Exists(path))
        {
            return list;
        }

        var json = File.ReadAllText(path);
        var items = JsonSerializer.Deserialize<List<TodoItem>>(json, JsonOptions);
        if (items is not null)
        {
            list._items.AddRange(items);
        }

        return list;
    }
}


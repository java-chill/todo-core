# Todo.Core — .NET-библиотека списка задач

**Практическое занятие 3. Проектирование и публикация .NET-библиотеки с CI/CD.**

**Студент:** Ковтуненко А. Д., группа ПР-23.103
**МДК:** 02.02 Инструментальные средства разработки программного обеспечения

## Структура решения

- `Todo.Core` — библиотека классов (net8.0), содержит `TodoItem` и `TodoList`
- `Todo.Core.Tests` — проект тестов xUnit

## Возможности библиотеки

- `Add(title)` — добавить задачу
- `Remove(id)` — удалить задачу по идентификатору
- `Find(substring)` — поиск задач по подстроке
- `MarkDone()` / `MarkUndone()` — отметка выполнения
- `Rename(newTitle)` — переименование
- `Save(path)` / `Load(path)` — сохранение и загрузка списка в JSON

## Сборка и тесты

```bash
dotnet build TodoSolution.sln -c Release
dotnet test TodoSolution.sln -c Release
dotnet pack Todo.Core/Todo.Core.csproj -c Release -o ./artifacts
```

## CI/CD

- `ci.yml` — при push и Pull Request в `main`: restore → build → test → pack → загрузка артефакта
- `publish-package.yml` — после успешного CI публикует `.nupkg` в GitHub Packages

## Ссылки

- Репозиторий: https://github.com/java-chill/todo-core
- Actions: https://github.com/java-chill/todo-core/actions
- Packages: https://github.com/java-chill/todo-core/packages

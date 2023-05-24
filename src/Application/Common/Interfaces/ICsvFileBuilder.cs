using LogOT.Application.TodoLists.Queries.ExportTodos;

namespace LogOT.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}

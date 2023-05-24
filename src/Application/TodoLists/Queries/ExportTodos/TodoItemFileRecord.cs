using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}

using System.Globalization;
using LogOT.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;

namespace LogOT.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).Convert(c => c.Value.Done ? "Yes" : "No");
    }
}

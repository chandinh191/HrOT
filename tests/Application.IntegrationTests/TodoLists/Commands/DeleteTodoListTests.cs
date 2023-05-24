using hrOT.Application.Common.Exceptions;
using hrOT.Application.TodoLists.Commands.CreateTodoList;
using hrOT.Application.TodoLists.Commands.DeleteTodoList;
using hrOT.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace hrOT.Application.IntegrationTests.TodoLists.Commands;

using static Testing;

public class DeleteTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteTodoListCommand(new Guid());
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await SendAsync(new DeleteTodoListCommand(listId));

        var list = await FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}

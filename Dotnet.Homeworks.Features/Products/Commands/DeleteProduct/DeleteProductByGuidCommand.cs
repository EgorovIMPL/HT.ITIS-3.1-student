using System.Windows.Input;
using ICommand = Dotnet.Homeworks.Infrastructure.Cqrs.Commands.ICommand;

namespace Dotnet.Homeworks.Features.Products.Commands.DeleteProduct;

public class DeleteProductByGuidCommand : ICommand
{
    public Guid Guid { get; init; }

    public DeleteProductByGuidCommand(Guid guid)
    {
        Guid = guid;
    }
}
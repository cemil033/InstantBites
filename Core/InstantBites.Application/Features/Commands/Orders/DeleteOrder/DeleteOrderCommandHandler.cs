using InstantBites.Application.Repositories.OrderRepository;
using InstantBites.Application.UnitOfWork;
using InstantBites.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.Orders.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommandRequest, DeleteOrderCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }        

        public async Task<DeleteOrderCommandResponse> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWork.R_Order.Get(request.Id, false);

                var result = await _unitOfWork.W_Order.RemoveAsync(request.Id);
                if (result)
                {
                    await _unitOfWork.W_Order.SaveChangesAsync();
                }
                return new() { Success = result };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}

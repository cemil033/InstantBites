using InstantBites.Application.Repositories.ClientOrdersRepository;
using InstantBites.Application.Repositories.ClientRepository;
using InstantBites.Application.Repositories.MealRepository;
using InstantBites.Application.Repositories.NotificationRepository;
using InstantBites.Application.Repositories.OrderCategoryRepository;
using InstantBites.Application.Repositories.OrderRepository;
using InstantBites.Application.Repositories.RestaurantRepository;
using InstantBites.Domain.Entites;
using InstantBites.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Infrastructure.Middleware
{
    public class RenewSubscription 
    {
        private readonly ILogger<RenewSubscription> _logger;
        private readonly IServiceProvider _serviceProvider;

        private readonly IClientOrdersReadRepository _clientOrdersReadRepository;
        private readonly IClientOrdersWriteRepository _clientOrdersWriteRepository;
        private readonly INotificationWriteRepository _notificationWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IClientReadRepository _clientReadRepository;
        private readonly IClientWriteRepository _clientWriteRepository;
        private readonly UserManager<Client> _userManager;
        private readonly RequestDelegate _next;
        private Timer _timer;

        public RenewSubscription(ILogger<RenewSubscription> logger, RequestDelegate next, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            var scope = _serviceProvider.CreateScope();
            _clientOrdersReadRepository = scope.ServiceProvider.GetRequiredService<IClientOrdersReadRepository>();
            _clientOrdersWriteRepository = scope.ServiceProvider.GetRequiredService<IClientOrdersWriteRepository>();
            _notificationWriteRepository = scope.ServiceProvider.GetRequiredService<INotificationWriteRepository>();
            _orderReadRepository = scope.ServiceProvider.GetRequiredService<IOrderReadRepository>();
            _clientReadRepository = scope.ServiceProvider.GetRequiredService<IClientReadRepository>();
            _clientWriteRepository = scope.ServiceProvider.GetRequiredService<IClientWriteRepository>();
            _userManager = scope.ServiceProvider.GetRequiredService<UserManager<Client>>();

            _next = next;
            _timer = new Timer(Do, null, TimeSpan.Zero, TimeSpan.FromHours(6));
        }



        public async Task InvokeAsync(HttpContext context)
        {
            try
            {                
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"{DateTime.Now} :: {e.Message}");
                
            }
        }
        private async void Do(object state)
        {
            await CheckSubs();
        }

        private async Task CheckSubs()
        {
            try
            {
                var ClOrder = _clientOrdersReadRepository.GetAll(false).ToList();
                if (ClOrder.Count() > 0)
                {
                    foreach (var c in ClOrder)
                    {
                        if ((DateTime.Now - c.LastSubscribedTime).TotalDays == 30)
                        {
                            var notf = new Notification()
                            {
                                ClientId = c.ClientId,
                                IsRead = false,
                                Id = Guid.NewGuid().ToString()
                            };

                            var user = await _clientReadRepository.Get(c.ClientId, false);
                            var order = await _orderReadRepository.Get(c.OrderId, false);
                            if (user.Balance > order.Price + c.Shipping)
                            {
                                user.Balance -= order.Price + c.Shipping;
                                c.LastSubscribedTime = DateTime.Now;
                                var res = await _clientWriteRepository.Update(user);
                                var res1 = await _clientOrdersWriteRepository.Update(c);
                                var res2 = await _clientOrdersWriteRepository.SaveChangesAsync();
                                notf.Message = $"You have Successfully updated  {order.Name} order subscription";
                            }
                            else
                            {
                                var res = await _clientOrdersWriteRepository.RemoveAsync(c.Id);
                                var res2 = await _clientOrdersWriteRepository.SaveChangesAsync();
                                notf.Message = $"You have Unsuccessfully updated  {order.Name} order subscription";
                            }
                            await _notificationWriteRepository.AddAsync(notf);
                            await _notificationWriteRepository.SaveChangesAsync();

                        }
                        else if ((DateTime.Now - c.LastSubscribedTime).TotalDays == 23)
                        {
                        var notf = new Notification()
                        {
                            ClientId = c.ClientId,
                            IsRead = false,
                            Id = Guid.NewGuid().ToString()
                        };
                            var user = await _clientReadRepository.Get(c.ClientId, false);
                            var order = await _orderReadRepository.Get(c.OrderId, false);
                            notf.Message = $"Your subscription will be update {order.Name} order 7 days later";
                            await _notificationWriteRepository.AddAsync(notf);
                            await _notificationWriteRepository.SaveChangesAsync();
                        }
                    }
                }
                Console.WriteLine("Renewsubs");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Renew Subscribe throw Exception::{ex.Message}");
                throw;
            }
            
            
        }
    }
}

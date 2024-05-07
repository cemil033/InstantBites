using InstantBites.Application.Repositories.ClientLocationRepository;
using InstantBites.Application.Repositories.ClientOrdersRepository;
using InstantBites.Application.Repositories.ClientRepository;
using InstantBites.Application.Repositories.RestaurantLocationRepository;
using InstantBites.Application.Repositories.MealCategoryRepository;
using InstantBites.Application.Repositories.MealRepository;
using InstantBites.Application.Repositories.NotificationRepository;
using InstantBites.Application.Repositories.OrderCategoryRepository;
using InstantBites.Application.Repositories.OrderMealsRepository;
using InstantBites.Application.Repositories.OrderRepository;
using InstantBites.Application.Repositories.RestaurantRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        
        public IClientOrdersReadRepository R_ClientOrders { get; set; }
        public IClientOrdersWriteRepository W_ClientOrders { get; set; }
        public IClientReadRepository R_Client{get;set;}
        public IClientWriteRepository W_Client{get;set;}
        public IClientLocationReadRepository R_ClientLocation{get;set;}
        public IClientLocationWriteRepository W_ClientLocation{get;set;}
        public IRestaurantLocationReadRepository R_RestaurantLocation { get; set; }
        public IRestaurantLocationWriteRepository W_RestaurantLocation { get; set; }
        public IMealCategoryReadRepository R_MealCategory{get;set;}
        public IMealCategoryWriteRepository W_MealCategory{get;set;}
        public IMealReadRepository R_Meal{get;set;}
        public IMealWriteRepository W_Meal{get;set;}
        public INotificationReadRepository R_Notification{get;set;}
        public INotificationWriteRepository W_Notification{get;set;}
        public IOrderCategoryReadRepository R_OrderCategory{get;set;}
        public IOrderCategoryWriteRepository W_OrderCategory{get;set;}
        public IOrderMealsReadRepository R_OrderMeals{get;set;}
        public IOrderMealsWriteRepository W_OrderMeals{get;set;}
        public IOrderReadRepository R_Order{get;set;}
        public IOrderWriteRepository W_Order{get;set;}
        public IRestaurantReadRepository R_Restaurant{get;set;}
        public IRestaurantWriteRepository W_Restaurant{get;set;}
    }
}

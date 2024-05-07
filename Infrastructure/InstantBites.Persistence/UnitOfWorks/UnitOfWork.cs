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
using InstantBites.Application.UnitOfWork;
using InstantBites.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {     
        
        public UnitOfWork( IClientOrdersReadRepository r_ClientOrders, IClientOrdersWriteRepository w_ClientOrders, IClientReadRepository r_Client, IClientWriteRepository w_Client,IRestaurantLocationReadRepository r_RestaurantLocation,IRestaurantLocationWriteRepository w_RestaurantLocation, IClientLocationReadRepository r_ClientLocation, IClientLocationWriteRepository w_ClientLocation, IMealCategoryReadRepository r_MealCategory, IMealCategoryWriteRepository w_MealCategory, IMealReadRepository r_Meal, IMealWriteRepository w_Meal, INotificationReadRepository r_Notification, INotificationWriteRepository w_Notification, IOrderCategoryReadRepository r_OrderCategory, IOrderCategoryWriteRepository w_OrderCategory, IOrderMealsReadRepository r_OrderMeals, IOrderMealsWriteRepository w_OrderMeals, IOrderReadRepository r_Order, IOrderWriteRepository w_Order, IRestaurantReadRepository r_Restaurant, IRestaurantWriteRepository w_Restaurant)
        {
            
            R_ClientOrders = r_ClientOrders;
            W_ClientOrders = w_ClientOrders;
            R_Client = r_Client;
            W_Client = w_Client;
            R_ClientLocation = r_ClientLocation;
            W_ClientLocation = w_ClientLocation;
            R_RestaurantLocation=r_RestaurantLocation;
            W_RestaurantLocation = w_RestaurantLocation;
            R_MealCategory = r_MealCategory;
            W_MealCategory = w_MealCategory;
            R_Meal = r_Meal;
            W_Meal = w_Meal;
            R_Notification = r_Notification;
            W_Notification = w_Notification;
            R_OrderCategory = r_OrderCategory;
            W_OrderCategory = w_OrderCategory;
            R_OrderMeals = r_OrderMeals;
            W_OrderMeals = w_OrderMeals;
            R_Order = r_Order;
            W_Order = w_Order;
            R_Restaurant = r_Restaurant;
            W_Restaurant = w_Restaurant;
        }

        public IClientOrdersReadRepository R_ClientOrders { get; set; }
        public IClientOrdersWriteRepository W_ClientOrders { get; set; }
        public IClientReadRepository R_Client{ get; set; }

        public IClientWriteRepository W_Client { get; set; }
        public IClientLocationReadRepository R_ClientLocation { get; set; }
        public IClientLocationWriteRepository W_ClientLocation{ get; set; }

        public IRestaurantLocationReadRepository R_RestaurantLocation { get; set; }
        public IRestaurantLocationWriteRepository W_RestaurantLocation { get; set; }

        public IMealCategoryReadRepository R_MealCategory{ get; set; }

        public IMealCategoryWriteRepository W_MealCategory{ get; set; }

        public IMealReadRepository R_Meal{ get; set; }

        public IMealWriteRepository W_Meal{ get; set; }

        public INotificationReadRepository R_Notification { get; set; }
        public INotificationWriteRepository W_Notification{ get; set; }

        public IOrderCategoryReadRepository R_OrderCategory{ get; set; }

        public IOrderCategoryWriteRepository W_OrderCategory{ get; set; }

        public IOrderMealsReadRepository R_OrderMeals{ get; set; }

        public IOrderMealsWriteRepository W_OrderMeals{ get; set; }

        public IOrderReadRepository R_Order{ get; set; }

        public IOrderWriteRepository W_Order{ get; set; }

        public IRestaurantReadRepository R_Restaurant{ get; set; }

        public IRestaurantWriteRepository W_Restaurant{ get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Fusillade;
using JetBrains.Annotations;
using ReactiveUI;
using Refit;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Managers;
using Restaurant.Abstractions.Repositories;
using Restaurant.Abstractions.Services;
using Restaurant.Abstractions.ViewModels;
using Restaurant.DataTransferObjects;
using Restaurant.Facades;
using Restaurant.Managers;
using Restaurant.Model;
using Restaurant.Models;
using Restaurant.Pages;
using Restaurant.Repositories;
using Restaurant.Services;
using Restaurant.ViewModels;

namespace Restaurant
{
    [UsedImplicitly]
    public class Bootstrapper
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<NavigationFacade>().As<INavigationFacade>();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<ThemeManager>().As<IThemeManager>().SingleInstance();

            builder.RegisterType<WelcomeStartPage>().As<IViewFor<WelcomeViewModel>>();
            builder.RegisterType<SignInPage>().As<IViewFor<SignInViewModel>>();
            builder.RegisterType<SignUpPage>().As<IViewFor<SignUpViewModel>>();
            builder.RegisterType<MainPage>().As<IViewFor<MainViewModel>>();
            builder.RegisterType<FoodsPage>().As<IViewFor<FoodsViewModel>>();


            builder.RegisterType<WelcomeViewModel>().As<IWelcomeViewModel>();
            builder.RegisterType<SignInViewModel>().As<ISignInViewModel>();
            builder.RegisterType<SignUpViewModel>().As<ISignUpViewModel>();


            //var client = new HttpClient(NetCache.UserInitiated)
            //{
            //    BaseAddress = new Uri(Helper.Address)
            //};

            //var api = RestService.For<IRestaurantApi>(client);

            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<FoodRepository>().As<IFoodRepository>();
            builder.RegisterType<RestaurantApiTest>().As<IRestaurantApi>();

            builder.RegisterType<AuthentiticationManager>().As<IAuthenticationManager>();
            builder.RegisterType<AutoMapperFacade>().As<IAutoMapperFacade>();
            builder.RegisterType<MainViewModel>().As<IMainViewModel>();

            return builder.Build();
        }
    }

    public class RestaurantApiTest : IRestaurantApi
    {
        public const string test_email = "test@test.ru";
        public const string test_password = "123";

        public Task<object> RegesterRaw(RegisterDto registerDto)
        {
            return Task.FromResult(new object());
        }

        public Task<AuthenticationResult> GetTokenRaw(LoginDto loginDto)
        {
            return Task.FromResult(new AuthenticationResult()
            {
                ok = true,
                access_token = Guid.NewGuid().ToString()
            });
        }

        public Task<object> GetValues(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserInfoDto> GetUserInfoRaw(string accessToken)
        {
            return Task.FromResult(new UserInfoDto()
            {
                Picture = "https://media.licdn.com/mpr/mpr/shrinknp_100_100/AAEAAQAAAAAAAAmKAAAAJDE4OGFkYzA4LWFkMTYtNDE5YS05NDZmLTBhZGNhMzc0Y2Q5Mg.jpg",
                Email = "jurabek.azizkhujaev@gmail.com",
                IsRegistered = true,
                Name = "Jurabek"
            });

        }

        public Task<IEnumerable<FoodDto>> GetFoods(string accessToken)
        {
            return Task.FromResult<IEnumerable<FoodDto>>(new List<FoodDto>
            {
                new FoodDto  {Picture =  "http://xcook.info/sites/default/files/styles/large/public/borshh-bez-mjasa.jpg?itok=g4K2JTOD", Name = "Borsh"},
                new FoodDto { Picture = "https://sxodim.com/uploads/almaty/2016/06/200715021547_mzvadi__shashlik-745x493.jpg", Name = "Gusht"},
                new FoodDto { Picture = "http://realkebab.ru/wp-content/uploads/2013/09/kebab1.png", Name = "Qima"},
                new FoodDto(),
                new FoodDto(),
                new FoodDto(),
                new FoodDto(),
                new FoodDto(),
                new FoodDto(),
                new FoodDto(),
                new FoodDto(),
                new FoodDto(),
            });
        }
    }
}
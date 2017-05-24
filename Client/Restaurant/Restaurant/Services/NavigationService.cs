﻿using System;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;
using ReactiveUI;
using Restaurant.Abstractions;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Services;

namespace Restaurant.Services
{
    [UsedImplicitly]
    public class NavigationService : INavigationService
    {
        private readonly IContainer _container;
        private readonly INavigationFacade _navigationFacade;


        public IViewFor CurrentPage { get; private set; }

        public NavigationService(INavigationFacade navigationFacade) : this(App.Container, navigationFacade)
        { }

        public NavigationService(IContainer container, INavigationFacade navigationFacade)
        {
            _container = container;
            _navigationFacade = navigationFacade;
        }

        public Task NavigateAsync(INavigatableViewModel viewModel)
        {
            CurrentPage = GetView(viewModel);
            return _navigationFacade.PushAsync(CurrentPage);
        }

        public Task NavigateAsync(Type viewModelType)
        {
            var vm = _container.Resolve(viewModelType);
            CurrentPage = GetView(vm as INavigatableViewModel);
            return _navigationFacade.PushAsync(CurrentPage);
        }

        public Task NavigateModalAsync(INavigatableViewModel viewModel)
        {
            CurrentPage = GetView(viewModel);
            return _navigationFacade.PushModalAsync(CurrentPage);
        }

        public Task NavigateModalAsync(Type viewModelType)
        {
            var vm = _container.Resolve(viewModelType);
            CurrentPage = GetView(vm as INavigatableViewModel);
            return _navigationFacade.PushModalAsync(CurrentPage);
        }

        IViewFor GetView(INavigatableViewModel vm)
        {
            var viewType = typeof(IViewFor<>).MakeGenericType(vm.GetType());
            var view = _container.Resolve(viewType);
            var ret = view as IViewFor;

            if (ret == null)
                throw new Exception($"Resolve service type '{viewType.FullName}' does not implement '{typeof(IViewFor).FullName}'.");

            ret.ViewModel = vm;
            return ret;
        }

    }
}
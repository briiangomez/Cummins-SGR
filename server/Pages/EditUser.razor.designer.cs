﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Sgi.Models.SgiCore;
using Microsoft.EntityFrameworkCore;

namespace Sgi.Pages
{
    public partial class EditUserComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SgiCoreService SgiCore { get; set; }

        [Parameter]
        public dynamic Id { get; set; }

        Sgi.Models.SgiCore.User _user;
        protected Sgi.Models.SgiCore.User user
        {
            get
            {
                return _user;
            }
            set
            {
                if (!object.Equals(_user, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "user", NewValue = value, OldValue = _user };
                    _user = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var sgiCoreGetUserByIdResult = await SgiCore.GetUserById(Id);
            user = sgiCoreGetUserByIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(Sgi.Models.SgiCore.User args)
        {
            try
            {
                var sgiCoreUpdateUserResult = await SgiCore.UpdateUser(Id, user);
                DialogService.Close(user);
            }
            catch (System.Exception sgiCoreUpdateUserException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update User");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
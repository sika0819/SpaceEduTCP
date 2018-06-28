using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerContext : MVCSContext
{
    public UIManagerContext() : base()
    {
    }
    public UIManagerContext(MonoBehaviour view) : base(view)
    {
    }

    public UIManagerContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {

    }
    protected override void addCoreComponents()
    {
        base.addCoreComponents();
        injectionBinder.Unbind<ICommandBinder>();
        injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
    }
    override public IContext Start()
    {
        base.Start();
        StartSignal startSignal = injectionBinder.GetInstance<StartSignal>();
        startSignal.Dispatch();
        return this;
    }
    protected override void mapBindings()
    {
        commandBinder.Bind<StartSignal>().To<StartCommand>().Once();
        commandBinder.Bind<CallLoginHttpSingal>().To<CallLoginHttpCommand>();
        commandBinder.Bind<CallGetHttpSingal>().To<CallGetHttpCommand>();
        commandBinder.Bind<ActivePanelSingal>().To<PanelActiveCommand>();
        commandBinder.Bind<LoginByIdentySingal>().To<LoginByIndentityCommand>();

        injectionBinder.Bind<ILoginModel>().To<LoginModel>().ToSingleton();
        injectionBinder.Bind<IHttpRequestService>().To<HttpRequestService>().ToSingleton();
        injectionBinder.Bind<HttpRequestSignal>().ToSingleton();

        mediationBinder.Bind<LoginPanelView>().To<LoginPanelViewMediator>();
        mediationBinder.Bind<MainView>().To<MainViewMediator>();
        mediationBinder.Bind<SettingPanelView>().To<SettingPanelViewMediator>();
        mediationBinder.Bind<ControlPanelView>().To<ControlPanelViewMediator>();
        mediationBinder.Bind<NavBarView>().To<NavBarViewMediator>();
        mediationBinder.Bind<SelectCharView>().To<SelectCharViewMediator>();

    }
}

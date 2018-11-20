using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanoramaViewer : MVCSContext
{
    public PanoramaViewer() : base()
    {
    }
    public PanoramaViewer(MonoBehaviour view) : base(view)
    {
    }

    public PanoramaViewer(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
        commandBinder.Bind<StartSignal>().To<InitCommand>().Once();
        commandBinder.Bind<DownloadCourseSignal>().To<DownloadCourseCommand>();
        commandBinder.Bind<CallHttpGetIconSignal>().To<CallGetIconCommand>();
        commandBinder.Bind<ReceiveIconSignal>().To<ReceiveTextureCommand>();

        injectionBinder.Bind<InitPanoramaSignal>().ToSingleton();
        injectionBinder.Bind<ILoginModel>().To<LoginModel>().ToSingleton();
        injectionBinder.Bind<ITopicList>().To<TopicList>().ToSingleton();
        injectionBinder.Bind<IHttpRequestService>().To<HttpRequestService>().ToSingleton();
        injectionBinder.Bind<HttpRequestSignal>().ToSingleton();
        injectionBinder.Bind<ITextureListModel>().To<TextureListModel>().ToSingleton();
        injectionBinder.Bind<GenerateCourseListSignal>().ToSingleton();
        injectionBinder.Bind<SetTopicListSignal>().ToSingleton();
        mediationBinder.Bind<UIMangerView>().To<UIMangerViewMediator>();
        mediationBinder.Bind<SkyBoxView>().To<SkyBoxViewMediator>();
        mediationBinder.Bind<SphereView>().To<SphereViewMediator>();
        mediationBinder.Bind<TopicScrollView>().To<TopicScrollViewMediator>();
        mediationBinder.Bind<ItemView>().To<ItemViewMediator>();
    }
}

/// If you're new to Strange, start with MyFirstProject.
/// If you're interested in how Signals work, return here once you understand the
/// rest of Strange. This example shows how Signals differ from the default
/// EventDispatcher.
/// All comments from MyFirstProjectContext have been removed and replaced by comments focusing
/// on the differences 

using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;

public class DndClientContext : MVCSContext
{

    public DndClientContext (MonoBehaviour view) : base(view)
    {
    }

    public DndClientContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }
    
    // Unbind the default EventCommandBinder and rebind the SignalCommandBinder
    protected override void addCoreComponents()
    {
        base.addCoreComponents();
        injectionBinder.Unbind<ICommandBinder>();
        injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
    }
    
    // Override Start so that we can fire the StartSignal 
    override public IContext Start()
    {
        base.Start();
        StartSignal startSignal= (StartSignal)injectionBinder.GetInstance<StartSignal>();
        startSignal.Dispatch();
        return this;
    }
    
    protected override void mapBindings()
    {
        // injectionBinder.Bind<IExampleModel>().To<ExampleModel>().ToSingleton();
        // injectionBinder.Bind<IExampleService>().To<ExampleService>().ToSingleton();
        

        mediationBinder.Bind<WebSocketView>().To<WebSocketMediator>();
        

        // commandBinder.Bind<CallWebServiceSignal>().To<CallWebServiceCommand>();
        
        //StartSignal is now fired
        commandBinder.Bind<StartSignal>().To<StartCommand>().Once();
        
        //In MyFirstProject, there's are SCORE_CHANGE and FULFILL_SERVICE_REQUEST Events.
        //Here we change that to a Signal. The Signal isn't bound to any Command,
        //so we map it as an injection so a Command can fire it, and a Mediator can receive it
        // injectionBinder.Bind<ScoreChangedSignal>().ToSingleton();
        // injectionBinder.Bind<FulfillWebServiceRequestSignal>().ToSingleton();
    }
}
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
using WebSocketSharp;

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
        mediationBinder.Bind<WebSocketView>().To<WebSocketMediator>();
        mediationBinder.Bind<MainMenuView>().To<MainMenuMediator>();
        mediationBinder.Bind<TileView>().To<TileMediator>();
        mediationBinder.Bind<GameOverlayView>().To<GameOverlayMediator>();
        
        //Bind a start signal that we kick things off with
        commandBinder.Bind<StartSignal>().To<StartCommand>().Once();

        //Bind commands for the WebSocketMediator to use
        commandBinder.Bind<AddPlayerSignal>().To<AddPlayerCommand>();
        commandBinder.Bind<CreateMapSignal>().To<CreateMapCommand>();
        commandBinder.Bind<RemovePlayerSignal>().To<RemovePlayerCommand>();
        commandBinder.Bind<UpdatePlayerSignal>().To<UpdatePlayerCommand>();

        commandBinder.Bind<ConnectSignal>().To<ConnectCommand>();

        //Bind our websocket so that the WebSocketView can receive messages, and other GameObjects can send messages
        WebSocketWrapper ws = new WebSocketWrapper();
        injectionBinder.Bind<WebSocketWrapper>().ToValue(ws);


    }
}


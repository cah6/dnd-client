Client side application for D&D application. 

This client side uses:

Unity 4.3
StrangeIOC for better Unity code organization
NGUI in lieu Unity's shitty built-in GUI
Websocket-sharp for Websocket communication with server (note: Unity blocks websockets on android/Iphone unless you're using Untiy Pro, so that doesn't work right now)
JsonFX for auto serializing/deserializing data

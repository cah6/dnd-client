using System;

//Interface for what our Unity client will use to talk to outside world.
public interface IServerConnectionService{

	void Connect(string url);

	void Send(string data);

	void SendAsync(string data, Action<bool> completed);

}
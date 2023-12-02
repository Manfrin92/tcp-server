using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace WebSocketSample.Controllers;

public class WebSocketController : ControllerBase
{
    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            var port = HttpContext.Connection.RemotePort;

            await Echo(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    private static async Task Echo(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];

        var receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None
        );


        while (!receiveResult.CloseStatus.HasValue)
        {
            Console.WriteLine("Received in the server: "+Encoding.UTF8.GetString(buffer));
            Console.WriteLine("SubProtocol: "+webSocket.SubProtocol);

            byte[] serverResponse = Encoding.UTF8.GetBytes("Olar from server");

            await webSocket.SendAsync(
                serverResponse,
                WebSocketMessageType.Text,
                true,
                CancellationToken.None
            );

            receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None
            );
        }

        await webSocket.CloseAsync(
            receiveResult.CloseStatus.Value,
            receiveResult.CloseStatusDescription,
            CancellationToken.None
        );
    }
}
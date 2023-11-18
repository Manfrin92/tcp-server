import net from "net";

const server = net.createServer((socket) => {
  console.log(
    `TCP handshake successful with ${socket.remoteAddress} on port ${socket.remotePort}`
  );

  socket.write("Olar, client!");

  socket.on("data", (data) => {
    console.log("Received data, ", data);
    console.log("to string, ", data.toString());
  });
});

server.listen(8800, "127.0.0.1");

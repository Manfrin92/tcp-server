const WebSocket = require("ws");

// Replace 'ws://example.com:8080/socket' with your WebSocket server URI
const websocketUri = "ws://localhost:5168/ws";

// Create a WebSocket instance
const socket = new WebSocket(websocketUri);

// Connection opened
socket.on("open", () => {
  console.log("WebSocket connection opened");

  // Send a message to the server
  socket.send("OLAR, WebSocket, I'm a client!");
});

// Listen for messages from the server
socket.on("message", (data) => {
  console.log("Received message:", data.toString("utf-8"));
});

// Connection closed
socket.on("close", (code, reason) => {
  console.log("WebSocket connection closed:", code, reason.toString("utf8"));
});

// Connection error
socket.on("error", (error) => {
  console.error("WebSocket error:", error);
});

import dgram from "dgram";

const socket = dgram.createSocket("udp4");

socket.bind(5550, "172.28.64.1");

socket.on("message", (msg, inf) => {
  console.log(`received ${msg} from ${inf.address}`);
});

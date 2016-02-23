console.log('Hello world');
var net = require('net');
var udp = require('dgram');
var udpServer = udp.createSocket('udp4', console.log('udp server created'));
var discoveryMsg = new Buffer([0xff, 0x01, 0x01, 0x02]);
var udpResponse = new Buffer([0xFF, 0x24, 0x1, 0x0D, 0x44, 0x0A, 0x21, 0xED, 0xB4, 0xD8, 0xB0, 0x4C, 0xE0, 0x7E, 0xA3, 0x1, 0x0, 0x1, 0x0, 0x55, 0x53, 0x52, 0x2D, 0x57, 0x53, 0x61, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x8C]);


udpServer.on('listening', function(){
    var address = udpServer.address();
    console.log('listening udp on '+address.address+':'+address.port);
});

udpServer.on('message', function (msg, rinfo){
    console.log('udpmsg:',msg);
    if (discoveryMsg.compare(msg) == 0) {
        udpServer.send(udpResponse, 0, udpResponse.length, rinfo.port, rinfo.address, function (err) {
            if (err != 0) {
                console.log(err);
            }
            
            
        });
    }
});

udpServer.on('error', function (err){
    console.log(err);
}
)
udpServer.bind(1901);

net.createServer()